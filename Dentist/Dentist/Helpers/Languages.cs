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

    }
}

