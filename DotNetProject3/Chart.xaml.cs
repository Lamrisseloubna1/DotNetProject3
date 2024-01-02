using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DotNetProject3
{
    public partial class Chart : Window
    {
        public ObservableCollection<ChartDataModel> ChartData { get; } = new ObservableCollection<ChartDataModel>();

        public Chart()
        {
            InitializeComponent();
            DataContext = this;
            FillChart();
        }

        private void FillChart()
        {
            string connectionString = "Data Source=DESKTOP-EREHUMV\\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT F.Nom_filiere, COUNT(E.Cne) AS NombreEtudiants FROM etudiant E INNER JOIN Filiere F ON E.Id_fil = F.Id_filiere GROUP BY F.Nom_filiere";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        ChartData.Add(new ChartDataModel
                        {
                            Nom_filiere = row["Nom_filiere"].ToString(),
                            NombreEtudiants = Convert.ToInt32(row["NombreEtudiants"])
                        });
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FiliereWindow w= new FiliereWindow();
            this.Close();
            w.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StudentWindow w = new StudentWindow();
            this.Close();
            w.Show();
        }
    }

}