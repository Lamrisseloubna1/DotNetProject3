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

namespace DotNetProject3
{
    /// <summary>
    /// Logique d'interaction pour ModifStudentWindow.xaml
    /// </summary>
    public partial class ModifStudentWindow : Window
    {

        public ModifStudentWindow(string codeMassar, string firstName, string lastName, string email, string sexe, string adresse, string telephone, string niveau, DateTime naissance, string imagePath)
        {
            InitializeComponent();

            // Assurez-vous que vous avez des TextBox dans votre interface utilisateur (XAML) avec les noms appropriés.
            textBox1.Text = codeMassar;
            textBox2.Text = firstName;
            textBox3.Text = lastName;
            textBox4.Text = sexe;
            textBox5.Text = naissance.ToString();
            textBox6.Text = adresse;
            textBox7.Text = email;
            textBox8.Text = telephone;
            textBox9.Text = niveau;




        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Code-behind
        private void OnRadDataFormLoaded(object sender, RoutedEventArgs e)
        {
            var commitButton = FindButtonByName(RadDataForm1, "CommitButton");

            if (commitButton != null)
            {
                commitButton.Click += OnCommitButtonClick;
            }
        }

        private Button FindButtonByName(Visual visual, string buttonName)
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child is Button button && button.Name == buttonName)
                {
                    return button;
                }

                Button result = FindButtonByName(child, buttonName);
                if (result != null)
                    return result;
            }

            return null;
        }

        private void OnCommitButtonClick(object sender, RoutedEventArgs e)
        {
            // L'utilisateur a cliqué sur le bouton "OK" (Commit)

            // Placez ici le code pour gérer l'événement "OK"
            MessageBox.Show("hi");
            HandleOkButtonClick();
        }

        private void HandleOkButtonClick()
        {
            // Placez ici le code pour traiter le clic sur le bouton "OK"
            // par exemple, enregistrer les modifications dans la base de données.
            // ...
        }
        private void SauvegarderModifications()
        {
            string connectionString = "Data Source = DESKTOP-T3VF1P2\\SQLEXPRESS; Initial Catalog =Etudiant; Integrated Security = true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string updateEtudiantQuery = "UPDATE etudiant SET prenom = @Prenom, nom = @Nom, sexe= @Sexe, date_naiss=@Date_naiss, adresse=@Adresse, telephone=@Telephone, email = @Email WHERE cne = @CNE";

                using (SqlCommand command = new SqlCommand(updateEtudiantQuery, connection))
                {
                    // Remplacez les paramètres avec les valeurs actuelles de vos contrôles d'interface utilisateur
                    command.Parameters.AddWithValue("@Prenom", textBox3.Text);
                    command.Parameters.AddWithValue("@Nom", textBox2.Text);
                    command.Parameters.AddWithValue("@Email", textBox7.Text);
                    command.Parameters.AddWithValue("@CNE", textBox1.Text);
                    command.Parameters.AddWithValue("@Adresse", textBox6.Text);
                    command.Parameters.AddWithValue("@Date_naiss", textBox5.Text);
                    command.Parameters.AddWithValue("@Sexe", textBox4.Text);
                    command.Parameters.AddWithValue("@Telephone", textBox8.Text);
                    command.Parameters.AddWithValue("@Niveau", textBox9.Text);
                    command.ExecuteNonQuery();
                }

            }


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            StudentWindow StudentWindow = new StudentWindow();
            this.Hide();
            StudentWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SauvegarderModifications();

        }
    }
}
