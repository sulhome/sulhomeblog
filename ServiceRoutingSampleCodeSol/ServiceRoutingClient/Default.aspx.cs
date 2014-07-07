using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ServiceRoutingClient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var routerClient = new ServVersion1Ref.VersionServiceClient();
            using (OperationContextScope scope =
                    new OperationContextScope(routerClient.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(
             MessageHeader.CreateHeader(
             "VersionSelector",
             "http://service.versions.namespace/",
             "1"));
                Response.Write(routerClient.GetMessage());
            }
            routerClient.Close();
        }
    }
}