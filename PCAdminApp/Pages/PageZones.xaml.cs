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

        }
    }
}
