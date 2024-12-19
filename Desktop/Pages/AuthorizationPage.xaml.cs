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
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                ErrorMessageTextBlock.Text = "Введите логин и пароль.";
                return;
            }

            User user = await _userService.AuthenticateUserAsync(LoginTextBox.Text, PasswordBox.Password);

            if (user != null)
            {
                Application.Current.Properties["CurrentUser"] = user;
                NavigationService?.Navigate(new ShopPage());
            }
            else
            {
                ErrorMessageTextBlock.Text = "Неверный логин или пароль.";
            }
        }

        private void ContinueAsGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CurrentUser"] = null;
            NavigationService?.Navigate(new ShopPage());
        }
    }
}
