using MaterialDesignColors.Recommended;
using PCAdminApp.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Логика взаимодействия для CreateTicket.xaml
    /// </summary>
    public partial class CreateTicket : Window
    {
        private Computer currentComputer;
        public CreateTicket(Computer computer)
        {
            InitializeComponent();
            this.currentComputer = computer;
            LoadData();
        }

        private void LoadData()
        {
            if (currentComputer != null)
            {
                TBPCName.Text = currentComputer.PCName;
            }
            PickerDate.SelectedDate = DateTime.Today;
        }

        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.SelectedDate = DateTime.Today;
            }
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValidateInput()
        {
            if (string.IsNullOrEmpty(TBTicketDescripton.Text))
            {
                MessageBox.Show("Заполните описание неисправности", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBTicketDescripton.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(PickerDate.Text))
            {
                MessageBox.Show("Выберите дату неисправности", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                PickerDate.Focus();
                return false;
            }

            return true;
        }
        
        private void BtnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            string pcName = TBPCName.Text;
            string desc = TBTicketDescripton.Text;
            string date = PickerDate.Text;

            if (!IsValidateInput())
            {
                return;
            }

            Ticket NewTicket = new Ticket
            {
                ComputerId = currentComputer.Id,
                Description = desc,
                CreatedAt = PickerDate.SelectedDate ?? DateTime.Today,
            };

            App.db.Ticket.Add(NewTicket);
            App.db.SaveChanges();

            MessageBox.Show("Заявка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
