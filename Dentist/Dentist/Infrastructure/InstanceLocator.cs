namespace Dentist.Infrastructure
{
    using ViewModels;

    class InstanceLocator
    {
        #region Attributes

        #endregion
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion
        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
        #region Commands

        #endregion
    }
}
