using Doc_Historico.Interfaces;
using Doc_Historico.Views;

namespace Doc_Historico;

public partial class AppShell : Shell
{
    private INavigationService _navigationService;
	public AppShell(INavigationService navigationService)
	{
		InitializeComponent();
        _navigationService = navigationService;

        Routing.RegisterRoute("PatientDetailPage", typeof(PatientDetailPage));
    }
    protected override async void OnParentSet()
    {
        base.OnParentSet();
        if (Parent is not null)
        {
            await _navigationService.InitializeAsync();
        }
    }
}
