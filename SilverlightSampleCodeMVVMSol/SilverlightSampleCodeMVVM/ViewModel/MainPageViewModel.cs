using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using SilverlightSampleCodeMVVM.ProductServiceRef;

namespace SilverlightSampleCodeMVVM.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private Product _currentProduct;
        private ObservableCollection<Product> _productList;
        private string _message;
        private bool _isLoading;

        public MainPageViewModel()
        {
            if (!IsDesignTime)
            {
                IsLoading = true;
                GetProducts();
                UpdateProductCommand = new RelayCommand(UpdateProduct);  
            }
        }      

        #region Commands

        public RelayCommand UpdateProductCommand
        {
            get;
            private set;
        }    

        #endregion

        #region Properties

        public ObservableCollection<Product> ProductList
        {
            get
            {
                return _productList;
            }

            set
            {
                if (_productList != value)
                {
                    _productList = value;
                    RaisePropertyChanged("ProductList");
                }
            }
        }

        public Product CurrentProduct
        {
            get
            {
                return _currentProduct;
            }

            set
            {
                if (_currentProduct != value)
                {
                    _currentProduct = value;
                    RaisePropertyChanged("CurrentProduct");                    
                    UpdateProductCommand.IsEnabled = true;
                    Message = string.Empty;
                }
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    RaisePropertyChanged("IsLoading");                                        
                }
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (_message != value)
                {
                    _message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        #endregion

        private void GetProducts()
        {
            var serviceClient = new ProductServiceClient();
            serviceClient.GetProductsCompleted += (s, e) => { ProductList = e.Result.GetProductsResult; IsLoading = false;};
            serviceClient.GetProductsAsync(new GetProductsRequest());            
        }    

        private void UpdateProduct()
        {
            IsLoading = true;
            var serviceClient = new ProductServiceClient();
            serviceClient.UpdateProductCompleted += (s, e) => { Message = (e.Result.UpdateProductResult) ? "Product successfully updated" : "Unable to update product"; IsLoading = false; };
            serviceClient.UpdateProductAsync(new UpdateProductRequest { productId = CurrentProduct.Id });
        }
    }
}