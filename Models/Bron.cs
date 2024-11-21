using System.ComponentModel.DataAnnotations.Schema;
using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations;
using Humanizer.Localisation;
using static Azure.Core.HttpHeader;

namespace ptoba_svoego_vhoda_reg_2.Models
{
    public class Bron
    {
        [Required]
        public int Id { get; set; }
        public DateTime Data_zaezd { get; set; }
        public DateTime Data_viezd { get; set; }
        public double Stoimost { get; set; }
        
        public int? UserId { get; set; } //айди пользователя (внешний ключ)
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? NomerId { get; set; } //айди пользователя (внешний ключ)
        [ForeignKey("NomerId")]
        public Nomer? Nomer { get; set; }
    }
}
