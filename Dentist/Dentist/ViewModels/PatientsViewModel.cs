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
    using System.Linq;
    using System;

    public class PatientsViewModel : BaseViewModel
    {
 
        #region Attributes
        private string filter;
        private ObservableCollection<PatientItemViewModel> patients;
        private bool isRefreshing;
        private bool refreshCommand;

        #endregion

        #region Properties
    
        public List<Patient> MyPatients { get; set; }
        public ObservableCollection<PatientItemViewModel> Patients {
            get { return this.patients; }
            set { this.SetValue(ref this.patients, value); }
        }
        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set {
                this.filter = value;
                this.RefreshList();
            }
        }

        #endregion

        #region Constructors
        public PatientsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadPatients();
        }

        #endregion

        #region Singleton
        private static PatientsViewModel instance;
        public static PatientsViewModel GetInstastance()
        {
            if (instance==null)
            {
                return new PatientsViewModel();
            }
            return instance;
        }


        #endregion

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


            this.MyPatients = (List<Patient>)response.Result;
            this.RefreshList();
            this.IsRefreshing = false;

        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListPatientItemViewModel = this.MyPatients.Select(p => new PatientItemViewModel
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Address = p.Address,
                    Phone = p.Phone,
                    PatientSince = p.PatientSince,
                    TreatmentDescription = p.TreatmentDescription,
                    ImagePath = p.ImagePath,
                    HasAllergies = p.HasAllergies,
                    ImageArray = p.ImageArray,

                });
                this.Patients = new ObservableCollection<PatientItemViewModel>(
                    myListPatientItemViewModel.OrderBy(p => p.FirstName));
            }
            else
            {
                var myListPatientItemViewModel = this.MyPatients.Select(p => new PatientItemViewModel
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Address = p.Address,
                    Phone = p.Phone,
                    PatientSince = p.PatientSince,
                    TreatmentDescription = p.TreatmentDescription,
                    ImagePath = p.ImagePath,
                    HasAllergies = p.HasAllergies,
                    ImageArray = p.ImageArray,

                }).Where(p => p.FirstName.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Patients = new ObservableCollection<PatientItemViewModel>(
                    myListPatientItemViewModel.OrderBy(p => p.FirstName));
            }

        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPatients);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }
        #endregion
    }
}
