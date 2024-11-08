using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Araba Markası")]
        public string? Make { get; set; }

        [Required]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Araba Modeli")]
        public string? Model { get; set; }

        [Required]
        [Display(Name = "Üretim Yılı")]
        public int Year { get; set; }

        [Required]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Renk")]
        public string? Color { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Günlük Fiyat")]
        public int DailyRate { get; set; }

        [Required]
        [Display(Name = "Mevcut")]
        public bool Available { get; set; }

        [Required]
        [Display(Name = "Fotoğraf")]
        public string? PhotoPath { get; set; }
        
        [NotMapped]
        public IFormFile? CarImage { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
