using StoreLibrary.Models;
using StoreLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Store.ViewModels
{
    class ShopPageViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;

        private string _searchQuery;
        private string _selectedManufacturer;
        private string _minPrice;
        private string _maxPrice;
        private string _selectedSortOption;

        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Product> FilteredProducts { get; private set; }
        public ObservableCollection<string> Manufacturers { get; private set; }
        public ObservableCollection<string> SortOptions { get; private set; }

        public string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value; OnPropertyChanged(); FilterProducts(); }
        }

        public string SelectedManufacturer
        {
            get => _selectedManufacturer;
            set { _selectedManufacturer = value; OnPropertyChanged(); FilterProducts(); }
        }

        public string MinPrice
        {
            get => _minPrice;
            set { _minPrice = value; OnPropertyChanged(); FilterProducts(); }
        }

        public string MaxPrice
        {
            get => _maxPrice;
            set { _maxPrice = value; OnPropertyChanged(); FilterProducts(); }
        }

        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set { _selectedSortOption = value; OnPropertyChanged(); FilterProducts(); }
        }

        public string DisplayedProductCount => $"{FilteredProducts.Count} из {Products.Count}";

        public ShopPageViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            FilteredProducts = new ObservableCollection<Product>();
            Manufacturers = new ObservableCollection<string> { "Все производители" };
            SortOptions = new ObservableCollection<string> { "Цена по возрастанию", "Цена по убыванию" };
            SelectedManufacturer = "Все производители";

            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                    if (!Manufacturers.Contains(product.ProductManufacturer))
                        Manufacturers.Add(product.ProductManufacturer);
                }
                FilterProducts();
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке данных.");
            }
        }

        private void FilterProducts()
        {
            var filtered = Products.AsEnumerable();

            if (!string.IsNullOrEmpty(SearchQuery))
                filtered = filtered.Where(p => p.ProductName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(SelectedManufacturer) && SelectedManufacturer != "Все производители")
                filtered = filtered.Where(p => p.ProductManufacturer == SelectedManufacturer);

            if (decimal.TryParse(MinPrice, out var minPrice))
                filtered = filtered.Where(p => p.ProductCost >= minPrice);

            if (decimal.TryParse(MaxPrice, out var maxPrice))
                filtered = filtered.Where(p => p.ProductCost <= maxPrice);

            if (SelectedSortOption == "Цена по возрастанию")
                filtered = filtered.OrderBy(p => p.ProductCost);
            else if (SelectedSortOption == "Цена по убыванию")
                filtered = filtered.OrderByDescending(p => p.ProductCost);

            FilteredProducts.Clear();
            foreach (var product in filtered)
                FilteredProducts.Add(product);

            OnPropertyChanged(nameof(DisplayedProductCount));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
