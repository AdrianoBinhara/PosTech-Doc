using Doc_Historico.ViewModels;

namespace Doc_Historico.Views;

public partial class PatientListPage : ContentPage
{
	public PatientListPage(PatientListViewModel viewModel)
	{
		BindingContext = viewModel;
        InitializeComponent();
    }
   
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as PatientListViewModel;
        if (viewModel != null)
            await viewModel.GetPatientsExecute();
    }


}
