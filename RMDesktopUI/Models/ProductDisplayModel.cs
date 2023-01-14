using System.ComponentModel;

namespace RMDesktopUI.Models
{
    public class ProductDisplayModel: INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal RetailPrice { get; set; }
        private int quantityInStock;
        
        /// <summary>
        /// 
        /// </summary>
        public int QuantityInStock 
        { 
            get
            {
                return quantityInStock;   
            } 
            set 
            {
                quantityInStock = value;
                CallPropertyChanged(nameof(QuantityInStock));
            }
        }

        public bool IsTaxable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}