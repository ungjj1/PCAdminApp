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
    /// Логика взаимодействия для AddEditConfigurationWindow.xaml
    /// </summary>
    public partial class AddEditConfigurationWindow : Window
    {
        private HardwareProfile currentConfig;
        private static bool isNew = false;

        public static bool isEditWindowOpen { get; private set; }
        public AddEditConfigurationWindow(HardwareProfile config)
        {
            InitializeComponent();
            this.currentConfig = config;
            isEditWindowOpen = true;


        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            TBCpuName.Clear();
            TBGpuName.Clear();
            TBMotherBoardName.Clear();
            TBNameConfig.Clear();
            TBRamName.Clear();
            this.Close();
        }

        private void BtnAddConfig_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveConfig_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
