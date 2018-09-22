namespace Dentist.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dentist.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Methods
        private async void GotoAddProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddPatientPage()); 
        }
        #endregion
        #region Attributes

        #endregion
        #region Properties
        public PatientsViewModel Patients { get; set; }
        #endregion
        #region Constructors
        public MainViewModel()
        {
            this.Patients = new PatientsViewModel();
        }
        #endregion
        #region Commands
        public ICommand AddPatientCommand {
            get
            {
                return new RelayCommand(GotoAddProduct);

            }
                }

       
        #endregion
    }
}
