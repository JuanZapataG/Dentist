namespace Pratice1_2018_II.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }


        [Display(Name = "Zoo Location")]
        public string ZooLocation { get; set; }

        [DataType(DataType.MultilineText)]
        public string Characteristics { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Is Way To Extintion?")]
        public bool IsWayToExtintion { get; set; }

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