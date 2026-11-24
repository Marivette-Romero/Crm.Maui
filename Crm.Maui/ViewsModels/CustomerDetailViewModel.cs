using CommunityToolkit.Mvvm.ComponentModel;
using Crm.Maui.Models;

//Actualizacion 24/11/2026 ---- Editar un contacto ya existente 
namespace Crm.Maui.ViewsModels
{
    public partial class CustomerDetailViewModel:ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Customer _customer;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Item",out object? currentItem))
            {
                Customer=(Customer)currentItem;
            }
            query.Clear();
        }
    }
}
