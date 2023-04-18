using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio")]
        [StringLength(100, ErrorMessage = "Il nome non può essere più lungo di 100 caratteri")]
        public string Nome { get; set; } = string.Empty;
        public IEnumerable<Pizza>? Pizzas { get; set; }
    }
}
