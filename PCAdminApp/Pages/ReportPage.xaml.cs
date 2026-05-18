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
        public ReportPage(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadData();
        }

        private void LoadData()
        {
            allClients = App.db.Client.ToList();

            ClientsDG.ItemsSource = allClients;
        }

        private void BtnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDG.Items.Count == 0)
    {
        MessageBox.Show("Нет данных для экспорта");
        return;
    }
            Excel.Application ex = new Excel.Application();
            ex.Visible = true;
            Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet worksheet1 = (Worksheet)workbook.Sheets[1];

            for (int i = 0; i < ClientsDG.Columns.Count; i++)
            {
                Range myRange = (Range)worksheet1.Cells[1, i + 1];
                worksheet1.Cells[1, i + 1].Font.Bold = true;
                worksheet1.Columns[i + 1].ColumnWidth = 15;
                myRange.Value2 = ClientsDG.Columns[i].Header.ToString(); ;
            }

            for (int j = 0; j < ClientsDG.Items.Count; j++)  
            {
                for (int i = 0; i < ClientsDG.Columns.Count; i++)
                {
                    TextBlock b = ClientsDG.Columns[i].GetCellContent(ClientsDG.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)worksheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
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
