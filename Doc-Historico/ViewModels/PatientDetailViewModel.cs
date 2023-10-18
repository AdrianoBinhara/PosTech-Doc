using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;
using Font = Microsoft.Maui.Font;

namespace Doc_Historico.ViewModels
{
	[QueryProperty(nameof(Patient), "Patient")]
	public class PatientDetailViewModel : BaseViewModel
	{
		private IPatient _patientService;
        private IMedicalHistory _medicalService;
        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                if (_patient != value)
                {
                    _patient = value;
                    OnPropertyChanged(nameof(Patient));
                    InitializePatient();
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private ObservableCollection<Historico> _medicalHistory;
        public ObservableCollection<Historico> MedicalHistory
        {
            get => _medicalHistory;
            set
            {
                _medicalHistory = value;
                OnPropertyChanged(nameof(MedicalHistory));
            }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                SetProperty(ref _nome, value);
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
            }
        }
        private string _responsavel;
        public string Responsavel
        {
            get { return _responsavel; }
            set
            {
                SetProperty(ref _responsavel, value);
            }
        }
        private DateTime _dataNascimento;
        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set
            {
                SetProperty(ref _dataNascimento, value);
            }
        }


        public string Title => Patient != null ? "Editar" : "Adicionar";

		public PatientDetailViewModel(INavigationService navigationService, IPatient patientService, IMedicalHistory medicalService) : base(navigationService)
		{
			_navigationService = navigationService;
            _patientService = patientService;
            _medicalService = medicalService;
            ConfirmButton = new Command(() => ConfirmButtonExecute());
            DeleteHistory = new Command<Historico>(async (historico) => await DeleteHistoryExecute(historico));
            RefreshCommand = new Command(async () => await GetHistoricoExecute());
            AddHistoricoMedico = new Command(async () => await AddHistoricoMedicoExecute());
            MedicalHistory = new ObservableCollection<Historico>();

        }

        private async Task AddHistoricoMedicoExecute()
        {
            string type = await Application.Current.MainPage.DisplayActionSheet("Tipo","Cancelar",null, "Sintoma", "Diagnostico", "Tratamento");
            if (type == "Cancelar")
                return;
            string descrpt = await Application.Current.MainPage.DisplayPromptAsync("Descrição", "Mínimo 10 caract.");
            if (!IsValid(descrpt))
                return;

            await _medicalService.AddHistoricoPatient(Patient.id, new Historico()
            {
                tipo = type,
                texto = descrpt,
                data = DateTime.Now
            });
            await GetHistoricoExecute();
        }
        public bool IsValid(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return input.Trim().Length >= 10;
        }

        private async Task GetHistoricoExecute()
        {
            MedicalHistory = new ObservableCollection<Historico>(await _medicalService.GetAllPatientMedicalHistory(Patient.id));
            OnPropertyChanged(nameof(MedicalHistory));
        }

        private async Task DeleteHistoryExecute(Historico historico)
        {
            var result = await _medicalService.DeleteHistorico(historico.id, Patient.id);

            if (result)
                MedicalHistory.Remove(historico);
        }

        private async void InitializePatient()
        {
            if(Patient != null)
            {
                MedicalHistory = new ObservableCollection<Historico>(await _medicalService.GetAllPatientMedicalHistory(Patient.id));
                OnPropertyChanged(nameof(MedicalHistory));
                Patient.historico = MedicalHistory.ToList();
                Nome = Patient.nome;
                Email = Patient.email;
                Responsavel = Patient.responsavel;
                DataNascimento = Patient.dataNascimento;
                
            }
        }

        private async void ConfirmButtonExecute()
        {
			if(Patient != null)
			{
                Patient.nome = Nome;
                Patient.email = Email;
                Patient.responsavel = Responsavel;
                Patient.dataNascimento = DataNascimento;

                var editResult = await _patientService.ChangePatientInfo(Patient);
                if (editResult is Patient)
                {
                    await ShowSnackBar();
                    await _navigationService.PopAsync();
                }
                return;
            }

            var patient = new Patient()
            {
                nome = Nome,
                email = Email,
                dataNascimento = DataNascimento,
                responsavel = Responsavel,
            };
            var createResult = await _patientService.ChangePatientInfo(patient);
            if (createResult is Patient)
            {
                await ShowSnackBar();
                await _navigationService.PopAsync();
            }

        }

        private async Task ShowSnackBar()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#97A881"),
                TextColor = Colors.White,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(14),
                CharacterSpacing = 0.5
            };

            string text = "Atualizado com sucesso!";
            TimeSpan duration = TimeSpan.FromSeconds(3);

			var snackbar = Snackbar.Make(text, null, "Ok", duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }

        public void ResetProperties()
        {
            Nome = null;
            Email = null;
            Responsavel = null;
            DataNascimento = DateTime.MinValue; 
            Patient = null;
            MedicalHistory = null;
        }

        public ICommand AddHistoricoMedico { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand DeleteHistory { get; private set; }
        public ICommand ConfirmButton {get;private set;}
	}
}

