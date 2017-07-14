using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SyndicateBank.Models;

[assembly: OwinStartupAttribute(typeof(SyndicateBank.Startup))]
namespace SyndicateBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();          
        }
        
    }
}
