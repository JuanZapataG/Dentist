
namespace Dentist.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Dentist.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;
    using Views;

    public class PatientItemViewModel : Patient
    {
        #region Methods

        private async void DeletePatient()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);
            if (!answer)
            {
                return;

            }
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var patientsController = Application.Current.Resources["UrlPatientsController"].ToString();
            var response = await this.apiService.Delete(url, prefix, patientsController, this.PatientId);
            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            var patientsViewModel = PatientsViewModel.GetInstastance();
            var deletePatient = patientsViewModel.MyPatients.Where(p => p.PatientId == this.PatientId).FirstOrDefault();
            if (deletePatient != null)
            {
                patientsViewModel.MyPatients.Remove(deletePatient);
            }
            patientsViewModel.RefreshList();
        }
        private async void EditPatient()
        {
            MainViewModel.GetInstastance().EditPatient = new EditPatientViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditPatientPage());
        }
        #endregion
        #region Attributes
        private ApiService apiService;
        #endregion
        #region Constructors
        public PatientItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion
        #region Commands
        public ICommand DeletePatientCommand
        {
            get
            {
                return new RelayCommand(DeletePatient);
            }
        }
        public ICommand EditPatientCommand
        {
            get
            {
                return new RelayCommand(EditPatient);
            }
        }




        #endregion
    }
}
