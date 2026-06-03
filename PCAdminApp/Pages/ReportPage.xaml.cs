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


using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace PCAdminApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Microsoft.Office.Interop.Excel.Page
    {
        private User currentUser;
        private List<Client> allClients;
        private List<User> allusers;
        private List<Ticket> alltickets;
        public ReportPage(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadData();
        }

        private void LoadData()
        {
            allClients = App.db.Client.ToList();
            allusers = App.db.User.ToList();
            alltickets = App.db.Ticket.ToList();

            ClientsDG.ItemsSource = allClients;
            UsersDG.ItemsSource = allusers;
            TicketsDG.ItemsSource = alltickets;
        }

        private void BtnClientReport_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDG.Items.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            // Получаем данные из источника (allClients)
            var clientsToExport = allClients ?? ClientsDG.ItemsSource as List<Client>;
            if (clientsToExport == null) return;

            Excel.Application ex = new Excel.Application();
            ex.Visible = true;
            Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet worksheet1 = (Worksheet)workbook.Sheets[1];

            // Заголовки
            var properties = typeof(Client).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet1.Cells[1, i + 1] = properties[i].Name;
                ((Range)worksheet1.Cells[1, i + 1]).Font.Bold = true;
                worksheet1.Columns[i + 1].ColumnWidth = 15;
            }

            // Данные
            for (int j = 0; j < clientsToExport.Count; j++)
            {
                var client = clientsToExport[j];
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(client)?.ToString() ?? "";
                    worksheet1.Cells[j + 2, i + 1] = value;
                }
            }
        }
        private void BtnUserReport_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDG.Items.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }
            var usersToExport = allusers ?? UsersDG.ItemsSource as List<User>;
            if (usersToExport == null) return;

            var exportData = usersToExport.Select(u => new
            {
                u.Id,
                u.Username,
                u.Password,
                u.FullName,
                RoleName = u.Role?.Name ?? "Не назначена"
            }).ToList();

            Excel.Application ex = new Excel.Application();
            ex.Visible = true;
            Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet worksheet1 = (Worksheet)workbook.Sheets[1];

            // Заголовки
            var properties = exportData.First().GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet1.Cells[1, i + 1] = properties[i].Name;
                ((Range)worksheet1.Cells[1, i + 1]).Font.Bold = true;
                worksheet1.Columns[i + 1].ColumnWidth = 15;
            }

            // Данные
            for (int j = 0; j < exportData.Count; j++)
            {
                var user = exportData[j];
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(user)?.ToString() ?? "";
                    worksheet1.Cells[j + 2, i + 1] = value;
                }
            }
        }
        private void BtnTicketReport_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDG.Items.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            // Получаем данные из источника (allClients)
            var ticketsToExport = alltickets ?? TicketsDG.ItemsSource as List<Ticket>;
            if (ticketsToExport == null) return;

            Excel.Application ex = new Excel.Application();
            ex.Visible = true;
            Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet worksheet1 = (Worksheet)workbook.Sheets[1];

            // Заголовки
            var properties = typeof(Ticket).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                worksheet1.Cells[1, i + 1] = properties[i].Name;
                ((Range)worksheet1.Cells[1, i + 1]).Font.Bold = true;
                worksheet1.Columns[i + 1].ColumnWidth = 15;
            }

            // Данные
            for (int j = 0; j < ticketsToExport.Count; j++)
            {
                var ticket= ticketsToExport[j];
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var value = properties[i].GetValue(ticket)?.ToString() ?? "";
                    worksheet1.Cells[j + 2, i + 1] = value;
                }
            }
        }




        public HeaderFooter LeftHeader => throw new NotImplementedException();

        public HeaderFooter CenterHeader => throw new NotImplementedException();

        public HeaderFooter RightHeader => throw new NotImplementedException();

        public HeaderFooter LeftFooter => throw new NotImplementedException();

        public HeaderFooter CenterFooter => throw new NotImplementedException();

        public HeaderFooter RightFooter => throw new NotImplementedException();

    }
}
