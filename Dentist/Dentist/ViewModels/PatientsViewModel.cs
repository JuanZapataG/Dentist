namespace Dentist.ViewModels
{

    using Pratice1_2018_II.Domain.Models;
    using System.Collections.ObjectModel;
    using Services;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;

    public class PatientsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private async void LoadPatients()
        {
            var response = await this.apiService.Getlist<Patient>("https://pratice1-2018-iiapi.azurewebsites.net", "/api", "/Patients");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var list = (List<Patient>)response.Result;
            this.Patients = new ObservableCollection<Patient>(list);
        }

        #region Attributes

        #endregion
        #region Properties
        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients {
            get { return this.patients; }
            set { this.SetValue(ref this.patients, value); }
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
