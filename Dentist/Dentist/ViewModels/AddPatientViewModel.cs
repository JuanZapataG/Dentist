namespace Dentist.ViewModels
{
    using System.Windows.Input;
    using Helpers;
    using Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Dentist.Models;
    using System;
    using System.Linq;
    using Plugin.Media.Abstractions;
    using Plugin.Media;

    public class AddPatientViewModel : BaseViewModel
    {
        #region Methods
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirtsNameError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.LastNameError, 
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.TreatmentDescription))
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
            }
           
            var patient = new Patient
            {
                
                FirstName = this.FirstName,
                LastName = this.LastName,
                Address = this.Address,
                Phone = this.Phone,
                PatientSince = this.PatientSinceD,
                HasAllergies = this.HasAllergies,
                ImageArray = imageArray,


            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var patientsController = Application.Current.Resources["UrlPatientsController"].ToString();
            var response = await this.apiService.Post(url, prefix, patientsController, patient);
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
        #endregion

        #region Attributes
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
       

        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TreatmentDescription { get; set; }
        public bool HasAllergies { get; set; }
        public DateTime PatientSinceD { get; set; }
        public DateTime PatientSinceH { get; set; }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value);}
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }



        #endregion

        #region Constructors
        public AddPatientViewModel()
        {
            this.ImageSource = "noimage";
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.HasAllergies = false;
            PatientSinceD = DateTime.Today;
            PatientSinceH = DateTime.Today;
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

       

        #endregion
    }
}
