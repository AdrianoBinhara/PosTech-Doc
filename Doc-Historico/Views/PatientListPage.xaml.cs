using Doc_Historico.ViewModels;

namespace Doc_Historico.Views;

public partial class PatientListPage : ContentPage
{
	public PatientListPage(PatientListViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	
}
