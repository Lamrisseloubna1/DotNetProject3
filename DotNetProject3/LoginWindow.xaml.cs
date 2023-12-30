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
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string ConnectionString = "Data Source =DESKTOP-EREHUMV\\SQLEXPRESS; Initial Catalog =gestion_etudiant; Integrated Security = true";


        public LoginWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Email = textboxlogin.Text;
            string motDePasse = textboxpass.Text;

            if (Login(Email, motDePasse))
            {
                MessageBox.Show("Connexion réussie !");
                StudentWindow StudenWindow = new StudentWindow();
                this.Hide();
                StudenWindow.Show();
                this.Close();


            }
            else
            {
                MessageBox.Show("Email ou mot de passe incorrect.");
            }
        }
        private bool Login(string Email, string motDePasse)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM enseignant WHERE email = @Email AND password = @MotDePasse";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@MotDePasse", motDePasse);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
