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
    /// Логика взаимодействия для PeripheriaPage.xaml
    /// </summary>
    public partial class PeripheriaPage : Page
    {
        private List<PeripheralProfile> allper;
        private User currentuser;
        public PeripheriaPage(User currentuser)
        {
            InitializeComponent();
            this.currentuser = currentuser;
            ListPheripheiral.ItemsSource = App.db.PeripheralProfile.ToList();
            LoadData();
            LoadSortOptions();
        }

        private void LoadSortOptions()
        {
            SortComboBox.Items.Clear();
            SortComboBox.Items.Add("Без фильтров");
            SortComboBox.Items.Add("По названию профиля (А-Я)");
            SortComboBox.Items.Add("По названию профилья (Я-А)");
            SortComboBox.SelectedIndex = 0;
        }

        private void LoadData()
        {
            allper = App.db.PeripheralProfile.ToList();
            RefreshData();
        }
        private void RefreshData()
        {
            if (allper == null)
            {
                return;
            }

            string search = TBSeacrh?.Text;
            IEnumerable<PeripheralProfile> filteredper = allper;

            if (search != null)
            {
                filteredper = filteredper.Where(p =>
                (p.ProfileName != null && p.ProfileName.ToLower().Contains(search)) ||
                (p.MouseModel != null && p.MouseModel.ToLower().Contains(search)) ||
                (p.KeyboardModel != null && p.KeyboardModel.ToLower().Contains(search)) ||
                (p.HeadsetModel != null && p.HeadsetModel.ToLower().Contains(search)) ||
                (p.MonitorModel != null && p.MonitorModel.ToLower().Contains(search))
                );
            }

            if (SortComboBox.SelectedItem != null && SortComboBox.SelectedItem.ToString() != "Без фильтров")
            {
                string selectedSort = SortComboBox.SelectedItem.ToString();

                switch (selectedSort)
                {
                    case "По названию профиля (А-Я)":
                        filteredper = filteredper.OrderBy(c => c.ProfileName);
                        break;

                    case "По названию профиля (Я-А)":
                        filteredper = filteredper.OrderByDescending(c => c.ProfileName);
                        break;
                }
            }
            ListPheripheiral.ItemsSource = filteredper.ToList();
        }

        private void TBSeacrh_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }
        private void BtnAddPeripProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditPeripheria_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}