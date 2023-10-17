using Doc_Historico.ViewModels;

namespace Doc_Historico.Views;

public partial class PatientDetailPage : ContentPage
{
	public PatientDetailPage(PatientDetailViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is PatientDetailViewModel viewModel)
        {
            viewModel.ResetProperties();
        }
    }

}
