using ClientModule.Resources.Views;
using ClientModule.Resources.Interfaces;
namespace ClientModule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(OrderPage), typeof(IOrderPage));
        }
    }
}