using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BOVELO_PlanningList
{
    public partial class Détail_et_modification : Form
    {

        //Nos get/setter pour nos modif
        public string idBike { get { return textBox4.Text; } set { textBox4.Text = value; } } //j'ai mis en readonly, on modifie pas l'id d'une commande
        public string Monteur { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Horaire { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string Bike { get { return textBox3.Text; } set { textBox3.Text = value; } }


        public Détail_et_modification()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;  //Pour la modification du forme enfant soit pris en compte dans la forme mère
        }


        

        //je fais une connexion bdd degeux car on rajouterai peut etre des mots de passe,...

        MySqlConnection cn;

        private void button2_Click(object sender, EventArgs e)
        {
            cn = new MySqlConnection("SERVER=193.191.240.67;user=nick;database=DataBase;port=63307;password=1234");
            if (cn.State == ConnectionState.Closed) { cn.Open(); }

            listView5.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT Type FROM Bike WHERE idBike =@idBike", cn);
            cmd.Parameters.AddWithValue("@idBike", idBike);

            using (MySqlDataReader Lire = cmd.ExecuteReader()) //j'affiche sur ma listview5
            {
                while (Lire.Read())
                {
                    //string ID = Lire["ID"].ToString()
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Piece (NumberGuidon, NumberChassis,NumberRoue,Location) VALUES @NumberGuidon,@NumberChassis,@NumberRoue, @Location", cn);

                    switch (Bike.ToLower())
                    {
                        case "city":
                          
                            Part VeloCity = new Part(1, 1, 2);

                            cmd2.Parameters.AddWithValue("@NumberGuidon", VeloCity.getNombre().Item1);
                            cmd2.Parameters.AddWithValue("@NumberGuidon", VeloCity.getNombre().Item2);
                            cmd2.Parameters.AddWithValue("@NumberGuidon", VeloCity.getNombre().Item3);
                            cmd2.Parameters.AddWithValue("@Location", VeloCity.getLocation());
                            cmd2.ExecuteNonQuery();

                            listView5.Items.Add(new ListViewItem(new[] { String.Format("Il faut {0} guidons, {1} chassis, {2} roue", VeloCity.getNombre().Item1, VeloCity.getNombre().Item2, VeloCity.getNombre().Item3) }));
                            break;

                        case "adventure":
                            listView5.Items.Add(new ListViewItem(new[] { "work in process" }));
                            break;

                        case "explorer":
                            listView5.Items.Add(new ListViewItem(new[] { "work in process" }));
                            break;

                        default:
                            MessageBox.Show("hhuhuhuh");
                            break;

                    }
                }
            }
        }





        private void locationToolStripMenuItem_Click(object sender, EventArgs e)  //Affichage de toute les parties du velo correspondont a ID qu'on a selectionné ,,, peut etre changer vers IDBike 
        {

            //    if (listView5.SelectedItems.Count > 0) //pas selectioner le vide
            //        //pour afficher la localité des pieces selectioner
            //    {
            //        ListViewItem element = listView5.SelectedItems[0];

            //        MySqlCommand cmd2 = new MySqlCommand("SELECT Location FROM Parts WHERE idBike=@idBike", cn); //à modif pour une recherche plus précise, même chose qu'avant quoi
            //        cmd.Parameters.AddWithValue("@idBike", idBike);

            //        using (MySqlDataReader Lire2 = cmd2.ExecuteReader())
            //        {
            //            while (Lire.Read())
            //            {

            //                string Location = Lire["Location"].ToString();

            //                textBox5.Text = Location; //j'affiche la location de ma piece selectioner ds le textbox

            //            }

            //        }  
            //    }
            //}
        }
    }
}
