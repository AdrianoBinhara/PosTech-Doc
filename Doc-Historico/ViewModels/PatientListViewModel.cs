using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;
using Doc_Historico.Views;

namespace Doc_Historico.ViewModels
{
    public class PatientListViewModel : BaseViewModel
    {
        private IPatient _patient;
        private ObservableCollection<Patient> _allPatients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _patientList;
        public ObservableCollection<Patient> PatientList
        {
            get { return _patientList; }
            set { SetProperty(ref _patientList, value); }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value))
                    FilterPatients();
            }
        }
        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                if(SetProperty(ref _selectedPatient, value)&& value != null)
                {
                    NavigateToPatientDetail(value);
                    SelectedPatient = null;
                }
            }
        }

        public PatientListViewModel(INavigationService navigationService,IPatient patient):base (navigationService)
        {
            _patient = patient;
            _navigationService = navigationService;
            Task.Run(GetPatientsExecute);
            RefreshCommand = new Command(async () => await GetPatientsExecute());
            DeletePatient = new Command<Patient>(async (patient) => await DeletePatientExecute(patient));
        }
     
        private async void NavigateToPatientDetail(Patient patient)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Patient", patient }
            };
            await _navigationService.NavigateToAsync(nameof(PatientDetailPage), parameters);
        }

        public void FilterPatients()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                PatientList = new ObservableCollection<Patient>(_allPatients);
            }
            else
            {
                var filteredList = _allPatients.Where(p =>
                p.nome.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.responsavel.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

                PatientList = new ObservableCollection<Patient>(filteredList);
            }
        }

        private async Task DeletePatientExecute(Patient patient)
        {
            await _patient.DeletePatient(patient.id);
            RefreshCommand.Execute(null);
        }

        private async Task GetPatientsExecute()
        {
            IsBusy = true;
            _allPatients = new ObservableCollection<Patient>(await _patient.GetPatientList());
            PatientList = new ObservableCollection<Patient>(_allPatients);
            IsBusy = false;
        }

        public ICommand DeletePatient { private set; get; }
        public ICommand RefreshCommand { private set; get; }
        public ICommand GetPatients { private set; get; }

    }
}

