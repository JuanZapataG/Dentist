namespace Dentist.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dentist.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Attributes

        #endregion
        #region Properties
        public PatientsViewModel Patients { get; set; }
        public AddPatientViewModel AddPatients { get; set; }
        public EditPatientViewMode EditPatient { get; set; }
        #endregion
        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Patients = new PatientsViewModel();
        }
        #endregion
        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstastance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }


        #endregion
        #region Methods
        private async void GotoAddProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddPatientPage());
        }
        #endregion
        #region Commands
        public ICommand AddPatientCommand
        {
            get
            {
                this.AddPatients = new AddPatientViewModel();
                return new RelayCommand(GotoAddProduct);

            }
        }


        #endregion



    }
}
