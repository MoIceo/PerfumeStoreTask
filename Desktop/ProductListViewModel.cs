using PerfumeLibrary.Models;
using PerfumeLibrary.Services;
using StoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> FilteredProducts { get; set; }
        public ObservableCollection<string> Manufacturers { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); _ = ApplyFiltersAsync(); }
        }

        private string _selectedManufacturer;
        public string SelectedManufacturer
        {
            get => _selectedManufacturer;
            set { _selectedManufacturer = value; OnPropertyChanged(nameof(SelectedManufacturer)); _ = ApplyFiltersAsync(); }
        }

        private string _minPrice;
        public string MinPrice
        {
            get => _minPrice;
            set { _minPrice = value; OnPropertyChanged(nameof(MinPrice)); _ = ApplyFiltersAsync(); }
        }

        private string _maxPrice;
        public string MaxPrice
        {
            get => _maxPrice;
            set { _maxPrice = value; OnPropertyChanged(nameof(MaxPrice)); _ = ApplyFiltersAsync(); }
        }

        public ICommand SortByPriceAscendingCommand { get; }
        public ICommand SortByPriceDescendingCommand { get; }
        public ICommand ResetFiltersCommand { get; }
        public ICommand OrderCommand { get; }

        public string ItemCountSummary => $"{FilteredProducts.Count} из {Products.Count}";

        public ProductListViewModel(ProductService productService)
        {
            _productService = productService;

            Products = new ObservableCollection<Product>();
            FilteredProducts = new ObservableCollection<Product>();
            Manufacturers = new ObservableCollection<string> { "Все производители" };

            SortByPriceAscendingCommand = new AsyncRelayCommand(async _ => await SortProductsAsync(true));
            SortByPriceDescendingCommand = new AsyncRelayCommand(async _ => await SortProductsAsync(false));
            ResetFiltersCommand = new RelayCommand(_ => ResetFilters());
            OrderCommand = new AsyncRelayCommand(async param => await PlaceOrderAsync((Product)param));

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            Products = new ObservableCollection<Product>(products);
            FilteredProducts = new ObservableCollection<Product>(Products);
            Manufacturers.AddRange(Products.Select(p => p.Manufacturer).Distinct());
            OnPropertyChanged(nameof(ItemCountSummary));
        }

        private async Task ApplyFiltersAsync()
        {
            var filtered = Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            if (SelectedManufacturer != "Все производители")
                filtered = filtered.Where(p => p.Manufacturer == SelectedManufacturer);

            if (decimal.TryParse(MinPrice, out var min))
                filtered = filtered.Where(p => p.Price >= min);

            if (decimal.TryParse(MaxPrice, out var max))
                filtered = filtered.Where(p => p.Price <= max);

            FilteredProducts.Clear();
            foreach (var product in filtered)
                FilteredProducts.Add(product);

            OnPropertyChanged(nameof(ItemCountSummary));
        }

        private async Task SortProductsAsync(bool ascending)
        {
            var sorted = ascending
                ? FilteredProducts.OrderBy(p => p.Price).ToList()
                : FilteredProducts.OrderByDescending(p => p.Price).ToList();

            FilteredProducts.Clear();
            foreach (var product in sorted)
                FilteredProducts.Add(product);

            await Task.CompletedTask; // Добавлено для асинхронности команды
        }

        private void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedManufacturer = "Все производители";
            MinPrice = string.Empty;
            MaxPrice = string.Empty;
        }

        private async Task PlaceOrderAsync(Product product)
        {
            await _productService.OrderProductAsync(product);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Асинхронная команда
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public async void Execute(object parameter) => await _execute(parameter);

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
