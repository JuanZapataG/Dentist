namespace Dentist.Conmmon.Models
{
    public class Response
    {
        #region Properties
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        #endregion
    }
}
