namespace Dentist.ViewModels
{
    public class MainViewModel
    {
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

        #endregion
    }
}
