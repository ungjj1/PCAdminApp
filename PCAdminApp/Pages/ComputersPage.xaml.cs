using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCAdminApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ComputersPage.xaml
    /// </summary>
    public partial class ComputersPage : Page
    {
        public ComputersPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var computers = App.db.Computer.Include("Status").ToList();

            this.DataContext = new { ComputersList = computers };
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnChangeStatusPC_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
