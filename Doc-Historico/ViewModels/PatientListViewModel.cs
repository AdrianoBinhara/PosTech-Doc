using System;
using System.Windows.Input;
using Doc_Historico.Interfaces;

namespace Doc_Historico.ViewModels
{
	public class PatientListViewModel
	{
		private IPatient _patient;

		public PatientListViewModel(IPatient patient)
		{
			_patient = patient;
			GetPatients = new Command(execute: async () => { await GetPatientsExecute(); });
		}

        private async Task GetPatientsExecute()
        {
			var patients = await _patient.GetPatientList();
			
        }

        public ICommand GetPatients { private set; get; }
		
	}
}

