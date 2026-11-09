using Crm.Maui.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Crm.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Estas lineas son importantes para la creación de las base de datos
            SQLitePCL.Batteries_V2.Init();
            using var context = new CrmDbContext();
            context.Database.EnsureCreated();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}