using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crm.Maui.Models;
using Crm.Maui.Persistence;
using System.Collections.ObjectModel;

namespace Crm.Maui.Views
{
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Customer> _customers; //Variable de instancia configurada para que pueda reflejarse los cambios.
        
        [ObservableProperty]
        private bool _refreshing;

        [RelayCommand]
        private async Task LoadCustomersAsync()
        {
            await Task.Run( () => { using CrmDbContext dbContext = new CrmDbContext();
                Customers = new ObservableCollection<Customer>(dbContext.Customers);
            });
            Refreshing = false;

        }
        [RelayCommand]
        private void Showing()
        {
            Refreshing = true;  
        }
    }
}
