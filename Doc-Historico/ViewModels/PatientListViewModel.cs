using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;

namespace Doc_Historico.ViewModels
{
	public class PatientListViewModel:BaseViewModel
	{
		private IPatient _patient;
		private ObservableCollection<Patient> _patientList;
		public ObservableCollection<Patient> PatientList
		{
			get { return _patientList; }
			set { SetProperty(ref _patientList, value); }
		} 

		public PatientListViewModel(IPatient patient)
		{
			_patient = patient;
            Task.Run(GetPatientsExecute);
			RefreshCommand = new Command(async() => await GetPatientsExecute());
            DeletePatient = new Command<Patient>(async (patient) => await DeletePatientExecute(patient));
		}

        private async Task DeletePatientExecute(Patient patient)
        {
			await _patient.DeletePatient(patient.id);
			PatientList.Remove(patient);
			RefreshCommand.Execute(null);
        }

        private async Task GetPatientsExecute()
        {
			IsBusy = true;
			PatientList = new ObservableCollection<Patient>();
			PatientList = new ObservableCollection<Patient>(await _patient.GetPatientList());
			IsBusy = false;
        }

		public ICommand DeletePatient { private set; get; }
        public ICommand RefreshCommand { private set; get; }
        public ICommand GetPatients { private set; get; }
		
	}
}

