using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    public partial class StudentWindow : Window
    {
        string codeMassar;
        string firstName;
        string lastName;
        string email;
        string sexe;
        string adresse;
        string telephone;
        string niveau;
        DateTime naissance;
        string imagePath;
        public StudentWindow()
        {
            InitializeComponent();
            RemplirComboBoxFiliere();

        }
        private List<string> ChargerFiliereDeLaBaseDeDonnees()
        {
            List<string> filiereList = new List<string>();

            using (SqlConnection connection = new SqlConnection("Data Source = DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant; Integrated Security = true"))
            {
                connection.Open();

                string query = "SELECT Nom_filiere FROM Filiere";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filiereList.Add(reader["Nom_filiere"].ToString());
                        }
                    }
                }
            }

            return filiereList;
        }

        private void RemplirComboBoxFiliere()
        {
            List<string> filiereList = ChargerFiliereDeLaBaseDeDonnees();

            ComboBox1.Items.Clear();

            foreach (string nomFiliere in filiereList)
            {
                ComboBox1.Items.Add(nomFiliere);
            }
        }


        private void ChargerEtudiantsDeLaBaseDeDonnees()
        {
            if (ComboBox1.Items.Count > 0)
            {

                string filiere = ComboBox1.SelectedItem as string;

                using (SqlConnection connection = new SqlConnection("Data Source = DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant; Integrated Security = true"))
                {
                    connection.Open();

                    string query = $@"
                SELECT etudiant.cne AS CodeMassar,
                       etudiant.prenom AS FirstName,
                       etudiant.nom AS LastName,
                       etudiant.sexe AS Sexe,
                       etudiant.date_naiss AS Naissance,   
                       etudiant.adresse AS Adresse,
                       etudiant.telephone AS Telephone,
                       etudiant.email AS Email,
                       etudiant.imagePath AS ImagePath,
                       etudiant.niveau As Niveau
                       
                FROM etudiant
                JOIN Filiere ON etudiant.id_fil = Filiere.Id_filiere
                WHERE Filiere.Nom_filiere = '{filiere}'";



                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        DataTable etudiantsTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(etudiantsTable);
                        RadGridView1.ItemsSource = etudiantsTable.DefaultView;

                        etudiantsTable.Columns.Add("Photo", typeof(BitmapImage));

                        foreach (DataRow row in etudiantsTable.Rows)
                        {
                            string imagePath = row["ImagePath"].ToString();

                            row["Photo"] = new BitmapImage(new Uri($"C:/Users/Lenovo/source/repos/DotNetProject3/DotNetProject3/Images/{imagePath}"));


                        }
                        RadGridView1.ItemsSource = etudiantsTable.DefaultView;

                    }

                }
            }
            else
            {
                MessageBox.Show("La liste des filières est vide. Veuillez ajouter des filières avant de charger les étudiants.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void OnRadGridViewSelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (RadGridView1.SelectedItem != null)
            {
                // Obtenez la ligne sélectionnée
                DataRowView selectedRow = (DataRowView)RadGridView1.SelectedItem;

                // Récupérez les valeurs de chaque colonne
                codeMassar = selectedRow["CodeMassar"].ToString();
                firstName = selectedRow["FirstName"].ToString();
                lastName = selectedRow["LastName"].ToString();
                email = selectedRow["Email"].ToString();
                sexe = selectedRow["Sexe"].ToString();
                adresse = selectedRow["Adresse"].ToString();
                telephone = selectedRow["Telephone"].ToString();
                niveau = selectedRow["Niveau"].ToString();
                naissance = (DateTime)selectedRow["Naissance"];
                imagePath = selectedRow["ImagePath"].ToString();



            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ChargerEtudiantsDeLaBaseDeDonnees();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItems = RadGridView1.SelectedItems;

            ModifStudentWindow modifStudenWindow = new ModifStudentWindow(codeMassar, firstName, lastName, email, sexe, adresse, telephone, niveau, naissance, imagePath); ;
            this.Hide();
            modifStudenWindow.Show();
            this.Close();



        }

        private void Button1_Click_1(object sender, RoutedEventArgs e)
        {
            StudentWindow w = new StudentWindow();
            this.Hide();
            w.ShowDialog();
            this.Close();

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

            /*FiliereWindow filiere = new FiliereWindow();
                            this.Hide();
                            filiere.ShowDialog();
                            this.Close();*/

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {

            /*StatisticWindow w = new StatisticWindow();
                                    this.Hide();
                                    w.ShowDialog();
                                    this.Close();*/

        }
    }
}