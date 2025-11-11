using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Npgsql;
using DemoAlekhinn;
using System;

namespace DemoAlekhinn
{
    public partial class LoginWindow : Window
    {
        // Строка подключения к базе данных
        private const string ConnectionString = "Host=lorksipt.ru;Database=db526;Username=user526;Password=73518";

        public LoginWindow()
        {
            InitializeComponent();

            // Устанавливаем фокус на поле логина при загрузке
            LoginTextBox.AttachedToVisualTree += (s, e) => LoginTextBox.Focus();
        }

        private async void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text?.Trim() ?? "";
            string password = PasswordTextBox.Text?.Trim() ?? "";

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Логин и пароль обязательны для заполнения");
                return;
            }

            try
            {
                // Проверка учетных данных в базе данных
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var command = new NpgsqlCommand(
                        "SELECT Id, Login, Role FROM Users WHERE Login = @login AND Password = @password",
                        connection);

                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Успешная авторизация
                            int userId = reader.GetInt32(0);
                            string userLogin = reader.GetString(1);
                            string role = reader.GetString(2);

                            // Скрываем сообщение об ошибке
                            HideError();

                            // Показываем сообщение об успешной авторизации
                            await ShowSuccessMessage($"Вы успешно авторизовались!\nРоль: {role}");

                            // Открываем главное окно
                            var mainWindow = new MainWindow(userId, userLogin, role);
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            // Неверные учетные данные
                            ShowError("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка подключения к базе данных: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorBorder.IsVisible = true;

            // Подсвечиваем поля красной рамкой
            LoginTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            PasswordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void HideError()
        {
            ErrorBorder.IsVisible = false;

            // Возвращаем стандартную рамку
            LoginTextBox.ClearValue(BorderBrushProperty);
            PasswordTextBox.ClearValue(BorderBrushProperty);
        }

        private async System.Threading.Tasks.Task ShowSuccessMessage(string message)
        {
            var dialog = new Window
            {
                Title = "Успешная авторизация",
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = new StackPanel
                {
                    Spacing = 10,
                    Margin = new Thickness(20),
                    Children =
                    {
                        new TextBlock
                        {
                            Text = message,
                            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                        },
                        new Button
                        {
                            Content = "OK",
                            Width = 80,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                        }
                    }
                }
            };

            var closeButton = (Button)((StackPanel)dialog.Content).Children[1];
            closeButton.Click += (s, e) => dialog.Close();

            await dialog.ShowDialog(this);
        }
    }
}