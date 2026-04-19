using PCAdminApp.Data;
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
        private User currentUser;
        public ComputersPage(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadData();
            RefreshStatus();
        }
        
        private void RefreshStatus()
        {
            var itemsControl = FindName("ComputersItemsControl") as ItemsControl;
            if (itemsControl != null)
            {
                itemsControl.Items.Refresh();
            }
        }

        private void LoadData()
        {
            var computers = App.db.Computer.Include("Status").ToList();

            this.DataContext = new { ComputersList = computers };
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
        }

        private void RegistrationClient_Click(object sender, RoutedEventArgs e)
        {
            RegistrationClientWindow reg = new RegistrationClientWindow();
            reg.Show();
        }

        private void BtnTurnOffOn_Click(object sender, RoutedEventArgs e)
        {
            var computer = (sender as Button)?.DataContext as Computer;

            if (computer == null) return;

            if (computer.Status.Id == 4)
            {
                computer.Status = App.db.Status.Find(1);
            }
            else
            {
                computer.Status = App.db.Status.Find(4);
            }
            App.db.SaveChanges();
            RefreshStatus();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            var computer = (sender as Button)?.DataContext as Computer;

            if (computer == null) return;
            {
                if (computer.Status.Id != 2)
                {
                    CreateTicket ticket = new CreateTicket(computer);
                    ticket.Show();
                    computer.Status = App.db.Status.Find(2);

                }
                else
                {
                    computer.Status = App.db.Status.Find(1);
                }
            }
        }

        private void BtnBlockPC_Click(object sender, RoutedEventArgs e)
        {
            var computer = (sender as Button)?.DataContext as Computer;

            if (computer == null) return;

            if (computer.Status.Id == 3)
            {
                computer.Status = App.db.Status.Find(1);
            }
            else
            {
                MessageBox.Show("Компьютер уже заблокирован", "Ошибка блокировки", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            App.db.SaveChanges();
            RefreshStatus();
        }

        private void BtnUnblockPC_Click(object sender, RoutedEventArgs e)
        {
            var computer = (sender as Button)?.DataContext as Computer;

            if (computer == null) return;

            if (computer.Status.Id == 1)
            {
                computer.Status = App.db.Status.Find(3);
            }
            else
            {
                MessageBox.Show("Компьютер уже разблокирован", "Ошибка разблокировки", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            App.db.SaveChanges();
            RefreshStatus();
        }
    }
}
