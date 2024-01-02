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
            string connectionString = "Data Source=DESKTOP-2RA9EQ3\\SQLEXPRESS;Initial Catalog=Tp1ADO;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT F.Nom_filiere, COUNT(E.Cne) AS NombreEtudiants FROM Etudiant E INNER JOIN Filiere F ON E.Id_filiere = F.Id_filiere GROUP BY F.Nom_filiere";

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
    }

}