﻿using BigCart.Models;
using BigCart.Services.Products;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private readonly IProductService _productService;
        private LoadStatus _loadStatus;

        public LoadStatus LoadStatus
        {
            get => _loadStatus;
            set => SetProperty(ref _loadStatus, value);
        }
        public Product[] Products { get; private set; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand AddToCartCommand { get; }

        public HomeViewModel(IProductService productService)
        {
            _productService = productService;
            ToggleFavoriteCommand = new Command<Product>(p => p.IsFavorite = !p.IsFavorite);
            AddToCartCommand = new Command<Product>(p =>
            {
                p.IsInCart = true;
                p.Quantity = 1;
            });
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            LoadStatus = LoadStatus.Loading;

            Products = await _productService.GetProductsAsync();
            for (int i = 0; i < Products.Length; i++)
                Products[i].Column = i % 2;

            OnPropertyChanged(nameof(Products));

            LoadStatus = LoadStatus.Success;
        }
    }
}