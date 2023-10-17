using System;
using Doc_Historico.Interfaces;
using Doc_Historico.ViewModels;
using Doc_Historico.Views;

namespace Doc_Historico.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBackAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync($"//{nameof(PatientListPage)}");
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters =null)
        {
            return
            routeParameters != null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);
        }

        public Task PopAsync()
        {

            Shell.Current
        }
    }

}

