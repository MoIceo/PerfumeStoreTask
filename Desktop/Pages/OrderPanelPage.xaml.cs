using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using StoreLibrary.Models;
using StoreLibrary.Services;

namespace Desktop.Pages
{
    public partial class OrderPanelPage : Page
    {
        private readonly OrderService _orderService = new();
        private readonly UserService _userService = new();
        private Order _currentOrder;

        public OrderPanelPage()
        {
            InitializeComponent();
        }

        private async void SearchOrder_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                try
                {
                    _currentOrder = await _orderService.GetOrderByIdAsync(orderId);
                    await DisplayOrderDetailsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Заказ не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Указан неверный номер заказа", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task DisplayOrderDetailsAsync()
        {
            if (_currentOrder != null)
            {
                OrderDateTextBlock.Text = _currentOrder.OrderDate.ToString("d");

                DeliveryDatePicker.SelectedDate = _currentOrder.OrderDeliveryDate;

                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == _currentOrder.OrderStatus);

                if (_currentOrder.UserId != null)
                {
                    var user = await _userService.GetUserByIdAsync(_currentOrder.UserId);
                    CustomerNameTextBlock.Text = user?.UserName ?? "N/A";
                }
                else
                {
                    CustomerNameTextBlock.Text = "N/A";
                }
            }
        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrder != null)
            {
                try
                {
                    _currentOrder.OrderDeliveryDate = DeliveryDatePicker.SelectedDate;
                    _currentOrder.OrderStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    await _orderService.UpdateOrderAsync(_currentOrder);
                    MessageBox.Show("Данные заказа обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка при сохранении", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не загружено ни одного заказа для сохранения изменений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
