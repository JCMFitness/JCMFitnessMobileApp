using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace JCMFitnessMobileApp.ViewModels
{
    class MainPageViewModel:INotifyPropertyChanged
    {
      

        string clientName = string.Empty;
        public string ClientName
        {
            get => clientName;
            set
            {
                if (clientName == value)
                    return;
                clientName = value;
                OnPropertyChanged(nameof(ClientName));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName => $"Name Entered: {ClientName}";
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string clientName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(clientName));
        }
    }
}
