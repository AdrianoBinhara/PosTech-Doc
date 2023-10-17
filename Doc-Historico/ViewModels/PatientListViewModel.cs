using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;

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

        public PatientListViewModel(IPatient patient)
        {
            _patient = patient;
            Task.Run(GetPatientsExecute);
            RefreshCommand = new Command(async () => await GetPatientsExecute());
            DeletePatient = new Command<Patient>(async (patient) => await DeletePatientExecute(patient));
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

