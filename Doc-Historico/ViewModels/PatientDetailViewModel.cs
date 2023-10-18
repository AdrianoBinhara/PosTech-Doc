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
                _patient = value;
				OnPropertyChanged(nameof(Patient));
				OnPropertyChanged(nameof(Title));
			}
		}
        private ObservableCollection<Historico> _medicalHistory;
        public ObservableCollection<Historico> MedicalHistory
        {
            get => _medicalHistory;
            set
            {
                _medicalHistory = value;
                SetProperty(ref _medicalHistory, value);
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
           
        }


        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Patient))
            {
                InitializePatient();
            }
        }

        private async void InitializePatient()
        {
            if(Patient != null)
            {
                Patient.historico = await _medicalService.GetAllPatientMedicalHistory(Patient.id);
                Nome = Patient.nome;
                Email = Patient.email;
                Responsavel = Patient.responsavel;
                DataNascimento = Patient.dataNascimento;
                MedicalHistory = new ObservableCollection<Historico>(Patient.historico);
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
        }

        public ICommand ConfirmButton {get;private set;}
	}
}

