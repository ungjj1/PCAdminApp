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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = App.db.Role.ToList();
            RoleComboBox.DisplayMemberPath = "Name";
            RoleComboBox.SelectedValuePath = "Id";
            RoleComboBox.ItemsSource = roles;
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string fio = TBFullName.Text;
            string log = TBUserName.Text;
            string pas = TBPassword.Text;

            if (fio != null && log != null && pas != null && RoleComboBox.SelectedItem != null)
            {
                try
                {
                    User adduser = new User
                    {
                        FullName = fio,
                        Username = log,
                        Password = pas,
                        RoleId = (int)RoleComboBox.SelectedValue
                    };
                    App.db.User.Add(adduser);
                    App.db.SaveChanges();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCloseWin_Click(object sender, RoutedEventArgs e)
        {
            TBFullName.Clear();
            TBPassword.Clear();
            TBUserName.Clear();
            this.Close();
        }
    }
}
