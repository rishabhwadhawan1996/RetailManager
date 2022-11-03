using System.ComponentModel;

using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel:Screen
    {
        private BindingList<string> products;

        public BindingList<string> Products
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

        private string itemQuantity;

        public string ItemQuantity
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
