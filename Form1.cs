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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection cn;
        bool Connecter = false;

        private void button1_Click(object sender, EventArgs e) // connexion bdd
        {
            if(button1.Text == "Se connecter")
            {


                cn = new MySqlConnection("SERVER=193.191.240.67;user=nick;database=DataBase;port=63307;password=1234");
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    button1.Text = "Se déconnecter";
                    Connecter = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
                
            }else //pour la deco
            {
                cn.Close();
                button1.Text = "Se connecter";
                Connecter = false;
            }
        }

        private void button2_Click(object sender, EventArgs e) //pour chercher nos lignes de commandes ds la bdd. Deux options s'offre a nous, où on change encore la bdd où alors je fais des SELECT de plusieurs tables. J'ai choissi l'option 1 mais on peut modifier par après.      
        {
            if (Connecter)
            {
                listView1.Items.Clear();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Bike ", cn);
                using(MySqlDataReader Lire = cmd.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string ID = Lire["idBike"].ToString();
                        //string Bike = Lire["Bike"].ToString();
                        //string Monteur = Lire["Monteur"].ToString();
                        string Type  = Lire["Type"].ToString();
                        string Color = Lire["Color"].ToString();
                        string Size  = Lire["Size"].ToString();


                        listView1.Items.Add(new ListViewItem(new[] { ID, Type, Color, Size }));

                    } 

                }

            }
            else { MessageBox.Show("Vous n'etes pas connecter"); }
        }

        private void jaiFinisMaTacheToolStripMenuItem_Click(object sender, EventArgs e) //j'ai finis ma tache donc je la supprime de la bdd
        {
            if (Connecter)
            {
                if(listView1.SelectedItems.Count > 0) //je peux pas selectioner du vide
                {
                    ListViewItem element = listView1.SelectedItems[0]; //mon premier element ici est l'id ou peux rajouter les autres par apres #flemme

                    string idBike = element.SubItems[0].Text;

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Bike WHERE idBike = @idBike", cn);
                    cmd.Parameters.AddWithValue("@idBike", idBike);
                    cmd.ExecuteNonQuery();

                    element.Remove();
                    MessageBox.Show("Supprimé");
                }
            }
        }

        private void myBananasAreRipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) //je peux pas selectioner du vide
            {
                ListViewItem element = listView1.SelectedItems[0]; //je rerentre mes données ds cette boucle, pour un code plus soignée on peut les sortir mais flemme
                string idBike = element.SubItems[0].Text;
                string Bike = element.SubItems[1].Text;
                string Monteur = element.SubItems[2].Text;
                string Horaire = element.SubItems[3].Text;

                using(Détail_et_modification m = new Détail_et_modification()) //On crée notre nouvelle instante modification et detail
                {
                    m.idBike = idBike;
                    m.Monteur = Monteur;
                    m.Horaire = Horaire;
                    m.Bike = Bike;

                    if(m.ShowDialog() == DialogResult.Yes) //l'utilisateur a bien cliqué sur modifier?
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE CommandLines SET Monteur=@monteur, Horaire=@horaire, Bike=@bike WHERE idBike=@idBike", cn);
                        cmd.Parameters.AddWithValue("@monteur", m.Monteur);
                        cmd.Parameters.AddWithValue("@horaire", m.Horaire);
                        cmd.Parameters.AddWithValue("@bike", m.Bike);
                        cmd.Parameters.AddWithValue("@idBike", idBike);  //comme readonly pas de m.ID car je ne fais que afficher
                        cmd.ExecuteNonQuery();


                        //je le met directement à jour sans le bouton "actualiser"
                        element.SubItems[1].Text = m.Bike;
                        element.SubItems[2].Text = m.Monteur;
                        element.SubItems[3].Text = m.Horaire;
                        MessageBox.Show("Modifier");
                    }
                }

            }
        }

        private void ajoutCommandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //a faire
        }
    }
}
