using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        private readonly PizzeriaContext _context;
        public PizzaController(PizzeriaContext context)
        {
            _context = context;
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }


        [HttpGet]
        public IActionResult GetPizzas([FromQuery] string? nome)
        {
            var pizze = _context.Pizzas!
                //.Include(p => p.Categoria)
                //.Include(p => p.Ingridients)
                .Where(p => nome == null || 
                p.Nome.ToLower()
                .Contains(nome.ToLower()))
                .ToList();

            return Ok(pizze);
        }

        [HttpGet("{id}")]
        public IActionResult GetPizza([FromQuery] long? id)
        {
            var pizza = _context.Pizzas!.FirstOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult CreatePizza(Pizza pizza)
        {
            _context.Pizzas!.Add(pizza);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPizza), pizza);
        }

        [HttpPut("{id}")]
        public IActionResult PutPizza(long id, [FromBody] Pizza pizza)
        {
            var pizzaToUpdate = _context.Pizzas!.Include(p => p.Ingridients).FirstOrDefault(p => p.Id == id);

            if (pizzaToUpdate is null)
            {
                return NotFound();
            }

            pizzaToUpdate.Nome = pizza.Nome;
            pizzaToUpdate.Descrizione = pizza.Descrizione;
            pizzaToUpdate.Prezzo = pizza.Prezzo;
            pizzaToUpdate.Foto = pizza.Foto;
            pizzaToUpdate.CategoriaId = pizza.CategoriaId;
            
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePizza(long id)
        {
            var pizzaToDelete = _context.Pizzas!.FirstOrDefault(p => p.Id == id);
            if (pizzaToDelete is null)
            {
                return NotFound();
            }
            _context.Pizzas!.Remove(pizzaToDelete);
            _context.SaveChanges();

            return Ok();
        }

    }
}
