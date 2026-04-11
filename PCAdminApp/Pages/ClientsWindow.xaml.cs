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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        private Client SelectedClient;
        public ClientsWindow()
        {
            InitializeComponent();
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            TBClientSearch.Clear();
            this.Close();
        }

        private void ListBoxResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxResults.SelectedItem is Client selected)
            {
                TBClientSearch.Text = $"{selected.FullName} - {selected.PhoneNumber}";
                PopupSearchResults.IsOpen = false;

                SelectedClient = selected;
            }
        }

        private void TBClientSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TBClientSearch.Text;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                PopupSearchResults.IsOpen = false;
                return;
            }

                var results = App.db.Client
                    .Where(c => c.FullName.Contains(searchText) ||
                                c.PhoneNumber.Contains(searchText))
                    .Take(10)
                    .ToList();

                ListBoxResults.ItemsSource = results;
                PopupSearchResults.IsOpen = results.Any();
            
        }

        private void BtnSelectClient_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null)
            {
                CurrentClientWindow currentClientWindow = new CurrentClientWindow(SelectedClient);
                currentClientWindow.Show();
                TBClientSearch.Clear();
                this.Close();
            }
        }
    }
}
