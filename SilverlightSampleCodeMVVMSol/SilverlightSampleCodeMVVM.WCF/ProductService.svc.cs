using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SilverlightSampleCodeMVVM.WCF
{    
    public class ProductService : IProductService
    {
        public bool UpdateProduct(int productId)
        {
            return true;
        }

        public List<Product> GetProducts()
        {
            var productList = new List<Product>();

            for (int i = 1; i < 21; i++)
            {
                productList.Add(new Product { Id = i, Name = "Product " + i, Description = "This is Product " + i, Price = i * 1.1 });

            }

            return productList;
        }
    }
}
