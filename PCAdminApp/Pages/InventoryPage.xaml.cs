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
    /// Логика взаимодействия для InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        private User currentUser;
        private List<HardwareProfile> allInventory;
        public InventoryPage(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadData();
            RefreshInventoryList();
        }

        private void RefreshInventoryList()
        {
            try
            {
                if (allInventory == null) return;

                IEnumerable<HardwareProfile> filter = allInventory;

                string search = TBSearch.Text;

                if (!string.IsNullOrEmpty(search))
                {
                    filter = filter.Where(o =>
                    (o.ProfileName != null && o.ProfileName.ToLower().Contains(search)) ||
                    (o.CPU != null && o.CPU.ToLower().Contains(search)) ||
                    (o.GPU != null && o.GPU.ToLower().Contains(search)) ||
                    (o.RAM != null && o.RAM.ToLower().Contains(search)) ||
                    (o.Motherboard != null && o.Motherboard.ToLower().Contains(search))
                    );
                }

                InventoryList.ItemsSource = search.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка поиска", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void LoadData()
        {
            allInventory = App.db.HardwareProfile.ToList();
            InventoryList.ItemsSource = allInventory;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnAddConfiguration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
