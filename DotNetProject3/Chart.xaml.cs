using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Logique d'interaction pour Chart.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public Chart()
        {
            InitializeComponent();
            LoadChartData();
        }
        private void LoadChartData()
        {
            using (var context = new DataClasses1DataContext())
            {
                try
                {
                    // Récupération des données depuis la base de données
                    var data = (from etudiant in context.Etudiant
                                group etudiant by etudiant.Id_filiere into g
                                select new
                                {
                                    NomFiliere = g.Key,
                                    NombreEtudiants = g.Count()
                                }).ToList();

                    // Définition du contexte de données pour le graphique
                    DataContext = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                    // Gérer l'exception en fonction de la logique de votre application
                }
            }
        }
    }
}
