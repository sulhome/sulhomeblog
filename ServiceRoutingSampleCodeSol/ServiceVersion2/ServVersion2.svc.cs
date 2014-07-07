using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ServiceContractVersions;

namespace ServiceVersion2
{
    public class ServVersion2 : IVersionService
    {
        public string GetMessage()
        {
            return string.Format("Your response from version 2");
        }
    }
}