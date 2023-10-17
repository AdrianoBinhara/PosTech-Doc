using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Doc_Historico.Interfaces;

namespace Doc_Historico.ViewModels
{
	public class BaseViewModel:INotifyPropertyChanged
	{
        public INavigationService _navigationService { get;  set; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}

