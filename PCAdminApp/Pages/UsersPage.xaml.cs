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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private User currentUser;
        private List<User> allusers;
        public UsersPage(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            UsersList.ItemsSource = App.db.User.ToList();
            LoadRoles();
            LoadUsers();
        }

        private void LoadUsers()
        {
            allusers = App.db.User.ToList();
            RefreshList();
        }
        private void LoadRoles()
        {
            SortComboBox.Items.Clear();

            SortComboBox.Items.Add("Все роли");

            var roles = App.db.Role.ToList();
            foreach (var role in roles)
            {
                SortComboBox.Items.Add(role.Name);
            }

            SortComboBox.SelectedIndex = 0;
        }
        private void RefreshList()
        {
            if (allusers == null)
            {
                return;
            }
            IEnumerable<User> filteredusers = allusers;

            string searchtxt = TBSearch.Text;

            if(searchtxt != null)
            {
                filteredusers = filteredusers.Where(u =>
                (u.Username != null && u.Username.ToLower().Contains(searchtxt)) ||
                (u.Password != null && u.Password.ToLower().Contains(searchtxt)) ||
                (u.FullName != null && u.FullName.ToLower().Contains(searchtxt))
                );
            }

            if (SortComboBox.SelectedIndex > 0 && SortComboBox.SelectedItem != null)
            {
                string selectedRole = SortComboBox.SelectedItem.ToString();

                filteredusers = filteredusers.Where(u =>
                    u.Role != null && u.Role.Name == selectedRole);
            }

            UsersList.ItemsSource = filteredusers.ToList();
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFireUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshList();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshList();
        }
    }
}
