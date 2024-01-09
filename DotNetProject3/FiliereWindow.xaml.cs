using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace DotNetProject3
{
    /// <summary>
    /// Logique d'interaction pour FiliereWindow.xaml
    /// </summary>
    public partial class FiliereWindow : Window
    {
        public FiliereWindow()
        {
            InitializeComponent();
            this.Carousel.ItemsSource = GetFiliere();
            this.Carousel.SelectionChanged += Carousel_SelectionChanged;
        }
        private void Carousel_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Filiere selectedFiliere = Carousel.SelectedItem as Filiere;

            if (selectedFiliere != null)
            {
                IdFiliere.Text = selectedFiliere.Id_filiere;
                textFiliere.Text = selectedFiliere.Nom_filiere;
            }
        }
        // Suppose que GetFiliere() récupère les données de la table Filiere dans la base de données.
        public static List<Filiere> GetFiliere()
        {
            List<Filiere> filieres = new List<Filiere>();

            // Utilisez votre propre logique pour récupérer les données de la table Filiere
            // Exemple simplifié :
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            SqlCommand cmd = new SqlCommand("SELECT Id_filiere, Nom_filiere FROM Filiere", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Filiere filiere = new Filiere
                {
                    Id_filiere = reader["Id_filiere"].ToString(),
                    Nom_filiere = reader["Nom_filiere"].ToString()
                };
                filieres.Add(filiere);
            }

            reader.Close();
            con.Close();

            return filieres;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            SqlCommand cmd;
            SqlDataAdapter adapt;

            //ID variable used in Updating and Deleting Record  
            int Id_Filiere = 0;
            if (textFiliere.Text != "" || IdFiliere.Text != "")
            {
                cmd = new SqlCommand("INSERT INTO Filiere(Id_filiere, Nom_filiere) VALUES(@Id_filiere, @Nom_filiere)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id_filiere", IdFiliere.Text);
                cmd.Parameters.AddWithValue("@Nom_filiere", textFiliere.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");


                //ClearData();
                this.Carousel.ItemsSource = GetFiliere();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (Carousel.SelectedItem != null)
            {
                Filiere selectedFiliere = Carousel.SelectedItem as Filiere;

                if (selectedFiliere != null)
                {
                    string idToUpdate = IdFiliere.Text;
                    string newNomFiliere = textFiliere.Text;

                    if (!string.IsNullOrEmpty(idToUpdate) && !string.IsNullOrEmpty(newNomFiliere))
                    {
                        if (int.TryParse(idToUpdate, out int id))
                        {
                            SqlConnection con = new SqlConnection("Data Source=DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
                            SqlCommand cmd;

                            con.Open();
                            cmd = new SqlCommand("UPDATE Filiere SET Nom_filiere = @Nom_filiere WHERE Id_filiere = @Id", con);
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nom_filiere", newNomFiliere);

                            cmd.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("Record Updated Successfully");

                            // Rafraîchir la liste après la mise à jour
                            this.Carousel.ItemsSource = GetFiliere();

                            // Réinitialiser les champs du formulaire
                            IdFiliere.Text = "";
                            textFiliere.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Invalid ID for update");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please provide details for update");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to update");
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (Carousel.SelectedItem != null)
            {
                Filiere selectedFiliere = Carousel.SelectedItem as Filiere;

                if (selectedFiliere != null)
                {
                    int idToDelete;
                    if (int.TryParse(selectedFiliere.Id_filiere, out idToDelete))
                    {
                        SqlConnection con = new SqlConnection("Data Source=DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
                        SqlCommand cmd;

                        con.Open();
                        cmd = new SqlCommand("DELETE FROM Filiere WHERE Id_filiere = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", idToDelete);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Record Deleted Successfully");

                        // Rafraîchir la liste après suppression
                        this.Carousel.ItemsSource = GetFiliere();
                        // Réinitialiser les champs du formulaire
                        IdFiliere.Text = "";
                        textFiliere.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID for deletion");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete");
            }
        }

        private void static_Click(object sender, RoutedEventArgs e)
        {
            Chart c = new Chart();
            c.ShowDialog();
        }
        private void btn(object sender, RoutedEventArgs e)
        {
            StudentWindow c = new StudentWindow();
            c.ShowDialog();
        }
    }
    }

