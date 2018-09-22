namespace Pratice1_2018_II.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Baby
    {
        [Key]
        public int BabyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Baby Name")]
        public string BabyName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Display(Name = "Born In")]
        public DateTime BornIn { get; set; }

        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observations { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Was Cesarean?")]
        public bool WasCesarean { get; set; }

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