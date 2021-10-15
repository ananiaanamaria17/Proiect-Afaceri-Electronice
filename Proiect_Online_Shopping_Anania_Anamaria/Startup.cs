using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proiect_Online_Shopping_Anania_Anamaria.Startup))]
namespace Proiect_Online_Shopping_Anania_Anamaria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
