using StoreLibrary.Models;
using StoreLibrary.Services;
using System.Windows;
using System.Windows.Controls;

namespace Store.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private readonly UserService _userService;

        public AuthorizationPage()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ErrorMessageTextBlock.Text = "Пожалуйста, введите логин и пароль.";
                return;
            }

            User user = await _userService.AuthenticateUserAsync(login, password);

            if (user != null)
            {
                //  Сохраняем данные пользователя (в данном примере в свойство Application)
                Application.Current.Properties["CurrentUser"] = user;
                NavigationService?.Navigate(new ShopPage());
            }
            else
            {
                ErrorMessageTextBlock.Text = "Неверный логин или пароль.";
            }
        }
    }
}
