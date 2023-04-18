using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; } = new Pizza { Foto = "https://picsum.photos/200/300" };
        public IEnumerable<Categoria> Categories { get; set; } = Enumerable.Empty<Categoria>();
        public IEnumerable<SelectListItem> Ingridients { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<string> SelectedIngridients { get; set; } = new();
    }
}
