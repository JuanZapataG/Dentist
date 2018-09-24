namespace Dentist.ViewModels
{
    using Models;
    using Services;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;


    public class EditPatientViewMode : BaseViewModel
    {
        #region Methods

        #endregion

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
            get { return this.Patient; }
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
        #endregion

        #region Constructors
        public EditPatientViewMode(Patient patient)
        {
            this.patient = patient;
            this.ImageSource = patient.ImageFullPath;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }
        #endregion

        #region Commands

        #endregion



    }
}
