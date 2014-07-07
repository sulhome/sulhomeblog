using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServiceContractVersions
{
    [ServiceContract]
    public interface IVersionService
    {
        [OperationContract]
        string GetMessage();
    }
}
