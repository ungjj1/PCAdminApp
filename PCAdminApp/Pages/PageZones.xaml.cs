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
    /// Логика взаимодействия для PageZones.xaml
    /// </summary>
    public partial class PageZones : Page
    {
        private User currentuser;
        private List<Zone> zones;
        public PageZones(User user)
        {
            InitializeComponent();
            this.currentuser = user;
            LoadData();
        }

        private void LoadData()
        {
            zones = App.db.Zone.ToList();
            ZonesList.ItemsSource = zones.ToList();
        }

        private void BtnDeleteZone_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var zona = button?.DataContext as Zone;

            if (zona == null)
            {
                MessageBox.Show("Пожалуйста, выберите зону для удаления.",
                              "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                    $"Вы действительно хотите удалить зону?\n\n" +
                    $"Название: {zona.Name}\n" +
                    $"Цена за час: {zona.PricePerHour}\n" +
                    $"Это действие нельзя отменить!",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                App.db.Zone.Remove(zona);
                App.db.SaveChanges();

                LoadData();
            }
        }
    }
}
