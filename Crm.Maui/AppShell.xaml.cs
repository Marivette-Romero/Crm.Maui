using Crm.Maui.Views;

namespace Crm.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Registramos las rutas que estan en la jerarquia visual sheell
            Routing.RegisterRoute(nameof (CustomerEditPage), typeof(CustomerEditPage));
        }
    }
}
