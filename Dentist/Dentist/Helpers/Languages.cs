namespace Dentist.Helpers
{
    using Xamarin.Forms;
        using Resources;
    using Interfaces.XFML.Interfaces;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }
        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string Patients
        {
            get { return Resource.Patients; }
        }

        public static string AddPatients
        {
            get { return Resource.AddPatients; }
        }
        public static string FirstName
        {
            get { return Resource.FirstName; }
        }
        public static string DescriptionFirstName
        {
            get { return Resource.DescriptionFirstName; }
        }
        public static string LastName
        {
            get { return Resource.LastName; }
        }
        public static string DescriptionLastName
        {
            get { return Resource.Address; }
        }
        public static string DescriptionAddress
        {
            get { return Resource.DescriptionAddress; }
        }
        public static string Phone
        {
            get { return Resource.Phone; }
        }
        public static string DescriptionPhone
        {
            get { return Resource.DescriptionPhone; }
        }
        public static string PatientSince
        {
            get { return Resource.PatientSince; }
        }
        public static string DescriptionPatientSince
        {
            get { return Resource.DescriptionPatientSince; }
        }
        public static string TreatmentDescription
        {
            get { return Resource.TreatmentDescription; }
        }
        public static string DescriptionTreatmentDescription
        {
            get { return Resource.DescriptionTreatmentDescription; }
        }

        public static string ImagePath
        {
            get { return Resource.ImagePath; }
        }
        public static string HasAllergies
        {
            get { return Resource.HasAllergies; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string FirtsNameError
        {
            get { return Resource.FirtsNameError; }
        }
        public static string LastNameError
        {
            get { return Resource.LastNameError; }
        }
        public static string AddressError
        {
            get { return Resource.AddressError; }
        }
        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }
        public static string TreatmentDescriptionError
        {
            get { return Resource.TreatmentDescriptionError; }
        }
        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }
        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }
        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }
        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string Edit
        {
            get { return Resource.Edit; }
        }
        public static string Delete
        {
            get { return Resource.Delete; }
        }
        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }
        public static string No
        {
            get { return Resource.No; }
        }
        public static string Confirm
        {
            get { return Resource.Confirm; }
        }
        public static string EditPatient
        {
            get { return Resource.EditPatient; }
        }
        public static string Search
        {
            get { return Resource.Search; }
        }
        public static string ChangesSince
        {
            get { return Resource.ChangesSince; }
        }

    }
    

}

