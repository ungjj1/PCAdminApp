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
            LoadData();
            this.currentuser = currentuser;
        }

        private void LoadData()
        {
            ListPheripheiral.ItemsSource = App.db.PeripheralProfile.ToList();
        }

        private void BtnEditPeripheries_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
