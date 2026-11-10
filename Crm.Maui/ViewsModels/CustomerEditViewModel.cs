using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crm.Maui.Models;
using Crm.Maui.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Maui.ViewsModels
{
    public partial class CustomerEditViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Customer _cust;

        protected Func<Customer,Task> ParentRefreshAction {  get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                if (query.TryGetValue("Item", out var CurrentCustomer))
                {
                    Cust = (Customer)CurrentCustomer;
                }
                if (query.TryGetValue("ParameterRefreshAction",out object? parentRefreshAction))
                {
                    ParentRefreshAction = (Func<Customer, Task>)parentRefreshAction;
                }
                query.Clear();
            }
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            //Guardar en la bd a nuevo cliente
            using CrmDbContext dbContext= new CrmDbContext();
            await dbContext.Customers.AddAsync(Cust);
            await dbContext.SaveChangesAsync();

            /*Agregamos el nuevo cliente a la colección Customers de la pantalla padre invocando 
             * a ParentRefreshAction*/
            await ParentRefreshAction(Cust);

            //regresar a la pantalla padre
            await Shell.Current.GoToAsync("..");
        }
    }
}
