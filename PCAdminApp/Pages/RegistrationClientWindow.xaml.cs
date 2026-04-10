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
    /// Логика взаимодействия для RegistrationClientWindow.xaml
    /// </summary>
    public partial class RegistrationClientWindow : Window
    {
        public RegistrationClientWindow()
        {
            InitializeComponent();
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            string ClientFullName = TBClientFullName.Text;
            string ClientPhoneNumber = TBClinetPhone.Text;

            if (string.IsNullOrEmpty(ClientFullName))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка регистрации клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                TBClientFullName.Focus();
            }
            else if (string.IsNullOrEmpty(ClientPhoneNumber))
            {
                MessageBox.Show("Введите телефон клиента", "Ошибка регистрации клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                TBClinetPhone.Focus();
            }
            else
            {
                using (var db = App.db)
                {
                    var newClient = new Client
                    {
                        FullName = ClientFullName,
                        PhoneNumber = ClientPhoneNumber,
                        BalanceBonus = 0,
                        BalanceRUB = 0
                    };
                    db.Client.Add(newClient);
                    db.SaveChanges();
                }
            }
        }
    }
}
