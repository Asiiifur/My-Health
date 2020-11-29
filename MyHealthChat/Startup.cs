using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyHealthChat.Startup))]
namespace MyHealthChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
