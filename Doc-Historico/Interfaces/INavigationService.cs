using System;
using Doc_Historico.ViewModels;

namespace Doc_Historico.Interfaces
{
	public interface INavigationService
	{
        Task InitializeAsync();
        Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);
        Task PopAsync();
    }
}

