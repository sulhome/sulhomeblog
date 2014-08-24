using MVCWithAutofac.Core.Interfaces;
using System.Web.Mvc;

namespace MVCWithAutofac.Web.BasePages
{
    public class CustomBasePage : WebViewPage
    {
        public ITeamRepository teamRepo { get; set; }
        public IRepository repo { get; set; }

        public override void Execute() { }
    }
}