using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Caliburn.Micro;

using RMDesktopUI.Models;

using RMDesktopUILibrary.API;
using RMDesktopUILibrary.Helpers;
using RMDesktopUILibrary.Models;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {

        public SalesViewModel(IProductEndpoint prodEndpoint,
            IConfigHelper configurationHelper, IMapper mapperInstance)
        {
            productEndpoint = prodEndpoint;
            configHelper = configurationHelper;
            mapper = mapperInstance;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await productEndpoint.GetAll();
            var products = mapper.Map<List<ProductDisplayModel>>(productList);
            Products = new BindingList<ProductDisplayModel>(products);
        }

        private BindingList<ProductDisplayModel> products;

        public BindingList<ProductDisplayModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<CartItemDisplayModel> cart = new BindingList<CartItemDisplayModel>();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return cart; }
            set
            {
                cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int itemQuantity = 1;
        private IProductEndpoint productEndpoint;
        private IConfigHelper configHelper;
        private IMapper mapper;

        public int ItemQuantity
        {
            get { return itemQuantity; }
            set
            {
                itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {
                decimal subTotal = CalculateSubTotal();
                return subTotal.ToString("C");
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subTotal;
        }

        public string Total
        {
            get
            {
                decimal total = CalculateTax() + CalculateSubTotal();
                return total.ToString("C");
            }
        }

        public string Tax
        {
            get
            {
                decimal taxAmount = CalculateTax();
                return taxAmount.ToString("C");
            }
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = configHelper.GetTaxRate()/100;

            foreach (var item in Cart)
            {

                if (item.Product.IsTaxable)
                {
                    taxAmount += item.Product.RetailPrice * item.QuantityInCart * taxRate;
                }
            }

            return taxAmount;
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }
        public void AddToCart()
        {
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(x => x.Product == selectedProduct);
            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
            }
            else
            {
                CartItemDisplayModel item = new CartItemDisplayModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                if (SelectedCartItem!=null)
                {
                    output = true;
                }
                return output;
            }
        }
        public void RemoveFromCart()
        {
            SelectedCartItem.Product.QuantityInStock += 1;
            if (SelectedCartItem.QuantityInCart > 1)
            {
                SelectedCartItem.QuantityInCart -= 1;
            }
            else
            {
                Cart.Remove(SelectedCartItem);
            }
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                return output;
            }
        }
        public void CheckOut()
        {

        }

        private ProductDisplayModel selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private CartItemDisplayModel selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return selectedCartItem; }
            set
            {
                selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

    }
}
