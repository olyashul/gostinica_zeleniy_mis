using System.ComponentModel.DataAnnotations.Schema;
using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations;
using Humanizer.Localisation;
using static Azure.Core.HttpHeader;

namespace ptoba_svoego_vhoda_reg_2.Models
{
    public class Bron
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Дата заезда обязательна")]
        public DateTime Data_zaezd { get; set; }

        [Required(ErrorMessage = "Дата выезда обязательна")]
        public DateTime Data_viezd { get; set; }

        [Required(ErrorMessage = "Стоимость обязательна")]
        public double Stoimost { get; set; }

        [Required(ErrorMessage = "Пользователь обязателен")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; } // User остается, но nullable

        [Required(ErrorMessage = "Номер обязателен")]
        public int NomerId { get; set; }
        [ForeignKey("NomerId")]
        public Nomer? Nomer { get; set; } // Nomer остается, но nullable
    }
}

