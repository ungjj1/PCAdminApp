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

            TxtHead.Text = isNew ? "Добавление конфигурации" : "Редактирование конфигурации";

            LoadConfigList();

            if (isNew)
            {
                BtnRemoveConfig.Visibility = Visibility.Collapsed;
            }

            if (!isNew && currentConfig != null)
            {
                try
                {
                    TBNameConfig.Text = currentConfig.ProfileName;
                    TBCpuName.Text = currentConfig.CPU;
                    TBGpuName.Text = currentConfig.GPU;
                    TBRamName.Text = currentConfig.RAM;
                    TBMotherBoardName.Text = currentConfig.Motherboard;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                           "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.Closed += (s, o) => isEditWindowOpen = false;
        }

        private void LoadConfigList()
        {
            var configList = App.db.HardwareProfile.ToList();
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(TBNameConfig.Text))
            {
                MessageBox.Show("Заполните название конфигурации", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBNameConfig.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBCpuName.Text))
            {
                MessageBox.Show("Заполните название процессора", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBCpuName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBGpuName.Text))
            {
                MessageBox.Show("Заполните название видеокарты", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBGpuName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBRamName.Text))
            {
                MessageBox.Show("Заполните название оперативной памяти", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBRamName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBMotherBoardName.Text))
            {
                MessageBox.Show("Заполните название материнской карты", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBMotherBoardName.Focus();
                return false;
            }

            return true;
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
        private void BtnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())

                return;

            currentConfig.ProfileName = TBNameConfig.Text.Trim();
            currentConfig.CPU = TBCpuName.Text.Trim();
            currentConfig.GPU = TBGpuName.Text.Trim();
            currentConfig.RAM = TBRamName.Text.Trim();
            currentConfig.Motherboard = TBMotherBoardName.Text.Trim();

            if (isNew)
            {
                App.db.Entry(currentConfig).State = System.Data.Entity.EntityState.Modified;
            }

            App.db.SaveChanges();

            MessageBox.Show("Данные успешно сохранены!", "Успех",
                          MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            this.Close();
        }

        private void BtnRemoveConfig_Click(object sender, RoutedEventArgs e)
        {
            if (currentConfig != null)
            {
                // Подтверждение удаления
                MessageBoxResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить конфигурацию \"{currentConfig.ProfileName}\"?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Проверяем, используется ли эта конфигурация на каких-либо компьютерах
                        bool isUsed = App.db.Computer.Any(c => c.HardwareProfileId == currentConfig.Id);

                        if (isUsed)
                        {
                            MessageBox.Show(
                                "Невозможно удалить конфигурацию, так как она используется на одном или нескольких компьютерах.",
                                "Ошибка удаления",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }

                        // Присоединяем объект к контексту, если он отсоединен
                        if (App.db.Entry(currentConfig).State == System.Data.Entity.EntityState.Detached)
                        {
                            App.db.HardwareProfile.Attach(currentConfig);
                        }

                        // Удаляем конфигурацию
                        App.db.HardwareProfile.Remove(currentConfig);
                        App.db.SaveChanges();

                        MessageBox.Show(
                            $"Конфигурация \"{currentConfig.ProfileName}\" успешно удалена.",
                            "Удаление выполнено",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        LoadConfigList();

                        currentConfig = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Ошибка при удалении конфигурации: {ex.Message}",
                            "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Не выбрана конфигурация для удаления.",
                    "Внимание",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
