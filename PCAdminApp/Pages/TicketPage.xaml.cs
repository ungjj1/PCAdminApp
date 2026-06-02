using PCAdminApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        private User currentUser;
        private List<Ticket> allTickets;
        private static bool isEditWindowOpen = false;
        public TicketPage(User user)
        {
            InitializeComponent();
            this.currentUser = user;
         
            LoadData();
        }

        private void LoadData()
        {
            allTickets = App.db.Ticket.ToList();
            TicketList.ItemsSource = allTickets;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var ticket = border?.DataContext as Ticket;

            if (ticket != null)
            {
                OpenEditWindow(ticket);
            }
            else
            {
                MessageBox.Show("Данные не найдены", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenEditWindow(Ticket ticket)
        {
            if (isEditWindowOpen) return;

            isEditWindowOpen = true;

            var editWindow = new EditTicketWindow(ticket);
            editWindow.Owner = Window.GetWindow(this);

            var result = editWindow.ShowDialog();

            if (result == true) 
            {
                LoadData(); 
            }

            isEditWindowOpen = false;
        }
    }
}
