using Doc_Historico.Interfaces;
using Doc_Historico.Services;

namespace Doc_Historico;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell(new NavigationService());
    }
}
