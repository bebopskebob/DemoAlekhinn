using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace DemoAlekhinn
{
    public partial class MainWindow : Window
    {
        private int _userId;
        private string _userLogin;
        private string _userRole;

        public MainWindow(int userId, string userLogin, string userRole)
        {
            InitializeComponent();
            _userId = userId;
            _userLogin = userLogin;
            _userRole = userRole;

            InitializeWindow();
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            // Устанавливаем информацию о пользователе
            UserInfoTextBlock.Text = $"{_userLogin} ({_userRole})";

            // Показываем админскую секцию если пользователь админ
            if (_userRole == "Admin")
            {
                AdminSection.IsVisible = true;
            }
        }

        private void OnLogoutButtonClick(object sender, RoutedEventArgs e)
        {
            // Возвращаемся к окну авторизации
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void OnMaterialsButtonClick(object sender, RoutedEventArgs e)
        {
            WelcomeTextBlock.Text = "Управление материалами";
            ContentTextBlock.Text = "Здесь будет интерфейс для работы с материалами:\n- Просмотр списка материалов\n- Добавление новых материалов\n- Редактирование цен\n- Управление спецификациями";
        }

        private void OnCustomersButtonClick(object sender, RoutedEventArgs e)
        {
            WelcomeTextBlock.Text = "Управление заказчиками";
            ContentTextBlock.Text = "Здесь будет интерфейс для работы с заказчиками:\n- Просмотр списка заказчиков\n- Добавление новых заказчиков\n- Редактирование информации\n- Разделение на поставщиков и покупателей";
        }

        private void OnOrdersButtonClick(object sender, RoutedEventArgs e)
        {
            WelcomeTextBlock.Text = "Управление заказами";
            ContentTextBlock.Text = "Здесь будет интерфейс для работы с заказами:\n- Создание новых заказов\n- Просмотр истории заказов\n- Управление статусами заказов\n- Формирование документов";
        }

        private void OnProductionButtonClick(object sender, RoutedEventArgs e)
        {
            WelcomeTextBlock.Text = "Управление производством";
            ContentTextBlock.Text = "Здесь будет интерфейс для управления производством:\n- Учет производства\n- Планирование\n- Отслеживание выполнения\n- Калькуляция себестоимости";
        }
    }
}