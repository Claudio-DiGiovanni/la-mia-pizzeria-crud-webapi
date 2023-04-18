namespace la_mia_pizzeria_static.Models
{
    public class Ingrediente
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Pizza>? Pizzas { get; set; }
    }
}
