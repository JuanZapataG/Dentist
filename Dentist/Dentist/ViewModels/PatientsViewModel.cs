namespace Dentist.ViewModels
{
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using Dentist.Models;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Dentist.Helpers;

    public class PatientsViewModel : BaseViewModel
    {
        #region Methods
        private ApiService apiService;
        private async void LoadPatients()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var patientsController = Application.Current.Resources["UrlPatientsController"].ToString();
            var response = await this.apiService.Getlist<Patient>(url, prefix, patientsController);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            this.IsRefreshing = false;
            var list = (List<Patient>)response.Result;
            this.Patients = new ObservableCollection<Patient>(list);
            

        }
        #endregion
        #region Attributes
        private ObservableCollection<Patient> patients;
        private bool isRefreshing;
        private bool refreshCommand;
        
        #endregion
        #region Properties
        public ObservableCollection<Patient> Patients {
            get { return this.patients; }
            set { this.SetValue(ref this.patients, value); }
        }
        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPatients);
            }
        }



        #endregion
        #region Constructors
        public PatientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPatients();
        }
        
        #endregion
        #region Commands

        #endregion
    }
}
