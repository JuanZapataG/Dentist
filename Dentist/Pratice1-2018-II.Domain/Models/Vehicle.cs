namespace Pratice1_2018_II.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Owner { get; set; }

        public int Model { get; set; }

        public int Mileage { get; set; }

        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Specifications { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Is Negotiable?")]
        public bool IsNegotiable { get; set; }

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