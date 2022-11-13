using System.ComponentModel;
using System.Threading.Tasks;

using Caliburn.Micro;

using RMDesktopUILibrary.API;
using RMDesktopUILibrary.Models;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel:Screen
    {

        public SalesViewModel(IProductEndpoint prodEndpoint)
        {
            productEndpoint = prodEndpoint;
            
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> products;

        public BindingList<ProductModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> cart;

        public BindingList<string> Cart
        {
            get { return cart; }
            set 
            { 
                cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int itemQuantity;
        private IProductEndpoint productEndpoint;

        public int ItemQuantity
        {
            get { return itemQuantity; }
            set
            {
                itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            get
            {
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                return output;
            }
        }
        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                return output;
            }
        }
        public void RemoveFromCart()
        {

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

    }
}
