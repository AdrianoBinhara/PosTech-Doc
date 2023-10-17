using System;
using Doc_Historico.Interfaces;

namespace Doc_Historico.ViewModels
{
	public class PatientDetailViewModel : BaseViewModel
	{
        public PatientDetailViewModel(INavigationService navigationService):base(navigationService)
		{
			_navigationService = navigationService;
		}
	}
}

