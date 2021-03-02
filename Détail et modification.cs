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
        public string ID { get { return textBox1.Text; } set { textBox4.Text = value; } } //j'ai mis en readonly, on modifie pas l'id d'une commande
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

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)  //Affichage de toute les parties du velo correspondont a ID qu'on a selectionné ,,, peut etre changer vers IDBike 
        {
            cn = new MySqlConnection("SERVER=193.191.240.67;user=nick;database=mydb;port=63307;password=1234");

            listView5.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Parts WHERE ID=@id", cn); //soucis differentes pieces avec differents nom, solution: chaque piece sa table 
            cmd.Parameters.AddWithValue("@id", ID);
        
            using (MySqlDataReader Lire = cmd.ExecuteReader()) //j'affiche sur ma listview5
            {
                while (Lire.Read())
                {
                    //string ID = Lire["ID"].ToString();
                    string Part = Lire["Part"].ToString();

                    listView5.Items.Add(new ListViewItem(new[] { Part }));

                }


                if (listView5.SelectedItems.Count > 0) //pas selectioner le vide
                    //pour afficher la localité des pieces selectioner
                {
                    ListViewItem element = listView5.SelectedItems[0];

                    MySqlCommand cmd2 = new MySqlCommand("SELECT Location FROM Parts WHERE ID=@id", cn); //à modif pour une recherche plus précise, même chose qu'avant quoi
                    cmd.Parameters.AddWithValue("@id", ID);

                    using (MySqlDataReader Lire2 = cmd2.ExecuteReader())
                    {
                        while (Lire.Read())
                        {

                            string Location = Lire["Location"].ToString();

                            textBox5.Text = Location; //j'affiche la location de ma piece selectioner ds le textbox

                        }

                    }  
                }
            }
        }
    }
}
