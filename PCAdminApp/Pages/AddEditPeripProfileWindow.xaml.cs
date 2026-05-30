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
    /// Логика взаимодействия для AddEditPeripProfileWindow.xaml
    /// </summary>
    public partial class AddEditPeripProfileWindow : Window
    {
        private PeripheralProfile currentProfile;
        private static bool isNew = false;

        public static bool isEditWindowOpen { get; private set; }
        public AddEditPeripProfileWindow(PeripheralProfile profile)
        {
            InitializeComponent();
            this.currentProfile = profile;
            isEditWindowOpen = true;
            
            TxtHead.Text = isNew ? "Добавление профиля" : "Редактирование профиля";

            LoadProfileList();

            if (isNew)
            {
                BtnRemoveConfig.Visibility = Visibility.Collapsed;
            }

            if (!isNew && currentProfile != null)
            {
                try
                {
                    TBNamePerProfile.Text = currentProfile.ProfileName;
                    TBMouseName.Text = currentProfile.MouseModel;
                    TBKeyboardName.Text = currentProfile.KeyboardModel;
                    TBHeadsetName.Text = currentProfile.HeadsetModel;
                    TBMonitorName.Text = currentProfile.MonitorModel;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                          "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.Closed += (s, o) => isEditWindowOpen = false;
        }

        private void LoadProfileList()
        {
            var profileList = App.db.PeripheralProfile.ToList();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(TBNamePerProfile.Text))
            {
                MessageBox.Show("Заполните название профиля", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBNamePerProfile.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBMouseName.Text))
            {
                MessageBox.Show("Заполните модель мышки", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBMouseName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBKeyboardName.Text))
            {
                MessageBox.Show("Заполните модель клавиатуры", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBKeyboardName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBHeadsetName.Text))
            {
                MessageBox.Show("Заполните модель наушников", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBHeadsetName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TBMonitorName.Text))
            {
                MessageBox.Show("Заполните модель монитора", "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                TBMonitorName.Focus();
                return false;
            }

            return true;
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            TBNamePerProfile.Clear();
            TBMouseName.Clear();
            TBMonitorName.Clear();
            TBKeyboardName.Clear();
            TBHeadsetName.Clear();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BtnRemoveConfig_Click(object sender, RoutedEventArgs e)
        {
            if (currentProfile != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить конфигурацию \"{currentProfile.ProfileName}\"?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Проверяем, используется ли эта конфигурация на каких-либо компьютерах
                        bool isUsed = App.db.Computer.Any(c => c.PeripheralProfileId == currentProfile.Id);

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
                        if (App.db.Entry(currentProfile).State == System.Data.Entity.EntityState.Detached)
                        {
                            App.db.PeripheralProfile.Attach(currentProfile);
                        }

                        // Удаляем конфигурацию
                        App.db.PeripheralProfile.Remove(currentProfile);
                        App.db.SaveChanges();

                        MessageBox.Show(
                            $"Конфигурация \"{currentProfile.ProfileName}\" успешно удалена.",
                            "Удаление выполнено",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        LoadProfileList();

                        currentProfile = null;
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
private void BtnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
                return;

            currentProfile.ProfileName = TBNamePerProfile.Text.Trim();
            currentProfile.MouseModel = TBMouseName.Text.Trim();
            currentProfile.KeyboardModel = TBKeyboardName.Text.Trim();
            currentProfile.HeadsetModel = TBHeadsetName.Text.Trim();
            currentProfile.MonitorModel = TBMonitorName.Text.Trim();

            if (isNew)
            {
                App.db.Entry(currentProfile).State = System.Data.Entity.EntityState.Modified;
            }

            App.db.SaveChanges();

            MessageBox.Show("Данные успешно сохранены!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            this.Close();
        }
    }
}
        

    

