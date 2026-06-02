using PCAdminApp.Data;
using System;
using System.Linq;
using System.Windows;
using System.Data.Entity;

namespace PCAdminApp.Pages
{
    public partial class EditTicketWindow : Window
    {
        private Ticket currentTicket;
        private static bool isEditWindowOpen { get; set; }

        public EditTicketWindow(Ticket ticket)
        {
            InitializeComponent();
            isEditWindowOpen = true;

            currentTicket = App.db.Ticket
                .Include(t => t.Computer)
                .FirstOrDefault(t => t.Id == ticket.Id);

            if (currentTicket == null)
            {
                MessageBox.Show("Тикет не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
                this.Close();
                return;
            }

            try
            {
                if (currentTicket.Computer != null)
                {
                    TBComputerName.Text = currentTicket.Computer.PCName;
                }
                TBDescription.Text = currentTicket.Description;
                DGCreated.SelectedDate = currentTicket.CreatedAt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                      "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Closed += (s, o) => isEditWindowOpen = false;
        }

        private void BtnTicketDone_Click(object sender, RoutedEventArgs e)
        {
            if (!TBDescription.Text.Trim().Equals("ВЫПОЛНЕН", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Введите в поле описания тикета \"ВЫПОЛНЕН\"",
                    "Подтверждение выполнения", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                if (currentTicket.Computer == null)
                {
                    MessageBox.Show("Связанный компьютер не найден", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentTicket.Computer.StatusId = 1;

                currentTicket.Computer.LastMaintenanceDate = DateTime.Now;

                App.db.SaveChanges();

                App.db.Ticket.Remove(currentTicket);

                App.db.SaveChanges();

                MessageBox.Show("Тикет успешно выполнен!\n" +
                    "Статус компьютера изменен на 'Работает'\n" +
                    $"Дата обслуживания: {DateTime.Now:dd.MM.yyyy}",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении тикета: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void BtnSaveTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentTicket.Computer != null)
                {
                    currentTicket.Computer.PCName = TBComputerName.Text.Trim();
                }
                currentTicket.Description = TBDescription.Text.Trim();

                if (DGCreated.SelectedDate.HasValue)
                {
                    currentTicket.CreatedAt = DGCreated.SelectedDate.Value;
                }

                App.db.SaveChanges();

                MessageBox.Show("Данные успешно сохранены!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}