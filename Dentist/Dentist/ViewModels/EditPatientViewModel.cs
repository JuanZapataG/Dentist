namespace Dentist.ViewModels
{
    using Models;
    using Services;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Dentist.Helpers;
    using System;
    using System.Linq;
    using Plugin.Media;

    public class EditPatientViewModel : BaseViewModel
    {
    

        #region Attributes
        private Patient patient;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;


        #endregion

        #region Properties

        public Patient Patient
        {
            get { return this.patient; }
            set { this.SetValue(ref this.patient, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        public TimeSpan PatientSinceH { get; set; }
        



        #endregion

        #region Constructors
        public EditPatientViewModel(Patient Patient)
        {
            this.patient = Patient;
            this.ImageSource = Patient.ImageFullPath;
            this.PatientSinceH = patient.PatientSince.TimeOfDay;
            this.apiService = new ApiService();
            this.IsEnabled = true;
            
        }
        #endregion

        #region Methods
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.patient.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirtsNameError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.patient.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.patient.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.patient.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.patient.TreatmentDescription))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.TreatmentDescriptionError,
                    Languages.Accept);
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;

            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.Patient.ImageArray = imageArray;
            }

            
                this.Patient.PatientSince = this.Patient.PatientSince.AddHours(Convert.ToDouble(PatientSinceH.Hours));
                this.Patient.PatientSince = this.Patient.PatientSince.AddMinutes(Convert.ToDouble(PatientSinceH.Minutes));

            
           

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var patientsController = Application.Current.Resources["UrlPatientsController"].ToString();
            var response = await this.apiService.Put(url, prefix, patientsController, this.Patient, this.Patient.PatientId);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var newPatient = (Patient)response.Result;
            var patientViewModel = PatientsViewModel.GetInstastance();
            var oldPatient = patientViewModel.MyPatients.Where(p => p.PatientId == this.Patient.PatientId).FirstOrDefault();
            if (oldPatient != null)
            {
                patientViewModel.MyPatients.Remove(oldPatient);
            }

            patientViewModel.MyPatients.Add(newPatient);
            patientViewModel.RefreshList();




            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();


        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();
            var source = await Application.Current.MainPage.DisplayActionSheet(
            Languages.ImageSource,
            Languages.Cancel,
            null,
            Languages.FromGallery,
            Languages.NewPicture);
            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }
            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }
            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        private async void Delete()
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
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var patientsController = Application.Current.Resources["UrlPatientsController"].ToString();
            var response = await this.apiService.Delete(url, prefix, patientsController, this.Patient.PatientId);
            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            var patientsViewModel = PatientsViewModel.GetInstastance();
            var deletePatient = patientsViewModel.MyPatients.Where(p => p.PatientId == this.Patient.PatientId).FirstOrDefault();
            if (deletePatient != null)
            {
                patientsViewModel.MyPatients.Remove(deletePatient);
            }
            patientsViewModel.RefreshList();
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopToRootAsync();


        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        
        #endregion



    }
}