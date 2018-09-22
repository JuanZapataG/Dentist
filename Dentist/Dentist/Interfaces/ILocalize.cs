namespace Dentist.Interfaces
{
    namespace XFML.Interfaces
    {
        using System.Globalization;

        public interface ILocalize
        {
            CultureInfo GetCurrentCultureInfo();

            void SetLocale(CultureInfo ci);
        }
    }

}
