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
using System.Windows.Shapes;

namespace PCAdminApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBalance.xaml
    /// </summary>
    public partial class AddBalance : Window
    {
        private Client currentClient;
        public AddBalance(Client client)
        {
            InitializeComponent();
            this.currentClient = client;
        }

        private void BtnBalanceAdd_Click(object sender, RoutedEventArgs e)
        {
            string addbalance = TBAddBalance.Text;
            int PlusBalance = int.Parse(addbalance);

            if (currentClient != null)
            {
                try
                {
                    
                        var clientToUpdate = App.db.Client.FirstOrDefault(c => c.Id == currentClient.Id);

                        if (clientToUpdate != null)
                        {
                            clientToUpdate.BalanceRUB += PlusBalance;
                            App.db.SaveChanges();
                            this.Close();
                        }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            TBAddBalance.Clear();
            this.Close();
        }
    }
}
