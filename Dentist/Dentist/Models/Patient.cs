
namespace Dentist.Models
{
    using System;

    public class Patient
    {
        
        public int PatientId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
         
        public string Address { get; set; }
                
        public string Phone { get; set; }
        
        public DateTime PatientSince { get; set; }
        
        public string TreatmentDescription { get; set; }
        
        public string ImagePath { get; set; }

        public bool HasAllergies { get; set; }

              public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noimage";
                }

                return $"https://pratice1-2018-iiapi.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
        }
    }
}