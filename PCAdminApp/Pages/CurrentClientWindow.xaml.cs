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
    /// Логика взаимодействия для CurrentClientWindow.xaml
    /// </summary>
    public partial class CurrentClientWindow : Window
    {
        private Client currentClient;
        public CurrentClientWindow(Client SelectedClient)
        {
            InitializeComponent();
            this.currentClient = SelectedClient;
        }
    }
}
