using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SilverlightSampleCodeMVVM.WCF
{    
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        bool UpdateProduct(int productId);

        [OperationContract]
        List<Product> GetProducts();
    }   
}
