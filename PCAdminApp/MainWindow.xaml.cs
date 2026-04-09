using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PCAdminApp.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCAdminApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        public MainWindow(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            if(currentUser != null && currentUser.RoleId == 2)
            {
                BtnTicket.Visibility = Visibility.Collapsed;  
            }    
        }

        private void BtnComputers_Click(object sender, RoutedEventArgs e)
        {
            TxtPageTitle.Text = "Панель управления устройствами";
            MainFrame.Navigate(new Pages.ComputersPage());
        }

        private void BtnTicket_Click(object sender, RoutedEventArgs e)
        {
            TxtPageTitle.Text = "Заявки";
            //MainFrame.Navigate(new Pages.TicketPage());
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            TxtPageTitle.Text = "Склад";
            //MainFrame.Navigate(new Pages.IvnentoryPage());
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            TxtPageTitle.Text = "Отчет";
            //MainFrame.Navigate(new Pages.ReportPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
