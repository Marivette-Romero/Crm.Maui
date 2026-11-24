using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crm.Maui.Models;
using Crm.Maui.Persistence;
using Crm.Maui.Views;
using System.Collections.ObjectModel;

namespace Crm.Maui.ViewsModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Customer> _customers; //Variable de instancia configurada para que pueda reflejarse los cambios.

        [ObservableProperty]
        private bool _refreshing;

        [RelayCommand]
        private async Task LoadCustomersAsync()
        {
            await Task.Run(() => {
                using CrmDbContext dbContext = new CrmDbContext();
                Customers = new ObservableCollection<Customer>(dbContext.Customers);
            });
            Refreshing = false;

        }
        [RelayCommand]
        private void Showing()
        {
            Refreshing = true;
        }

        [RelayCommand]
        private async Task ShowNewCustomerFormAsync()
        {
            await Shell.Current.GoToAsync(nameof(CustomerEditPage), parameters: new Dictionary<string, object>
            {
                {"ParameterRefreshAction",(Func<Customer,Task>) RefreshAddedAsync},
                {"Item", new Customer() }
            });
        }

        //este metodo actualiza el nuevo cliente agregandolo a la colección
        private Task RefreshAddedAsync(Customer addedCustomer)
        {
            Customers.Add(addedCustomer);
            return Task.CompletedTask;
        }
        
        //Actualizacion 24/11/26 Eliminar customer
        [RelayCommand]
        public void DeleteCustomer (Customer customer)
        {
            using var dbContext = new CrmDbContext();
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();

            Customers.Remove(customer);
        }

        [RelayCommand]
        public async Task ShowDetailFormsAsync(Customer customer)
        {
            await Shell.Current.GoToAsync(nameof(CustomerDetailPage), parameters: new Dictionary<string, object>
            {
                //{"ParentRefreshAction",(Func<Customer,Task>) RefreshEditedAddedAsync},
                {"Item", customer }
            });
        }
    }
}