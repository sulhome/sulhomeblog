using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ServiceContractVersions;

namespace ServiceVersion1
{
    public class ServVersion1 : IVersionService
    {      
        public string GetMessage()
        {
            return "Your response from version 1";
        }
    }
}
