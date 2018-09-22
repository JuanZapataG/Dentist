namespace Pratice1_2018_II.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Census
    {
        [Key]
        public int CensusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public string Phone { get; set; }

        public int People { get; set; }

        public int Stratum { get; set; }

        [Display(Name = "Have Internet?")]
        public bool HaveInternet { get; set; }

        [Display(Name = "Have Aqueduct?")]
        public bool HaveAqueduct { get; set; }

        [Display(Name = "Have Electricity?")]
        public bool HaveElectricity { get; set; }
    
        [DataType(DataType.MultilineText)]
        public string Observations { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [NotMapped]
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