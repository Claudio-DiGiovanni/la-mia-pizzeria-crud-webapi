using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PizzaController : Controller
    {


        private readonly PizzeriaContext _context;
        public PizzaController(PizzeriaContext context)
        {
            _context = context;
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("Menu")]
        public IActionResult Menu()
        {
            var pizze = _context.Pizzas!.Include(p => p.Categoria).ToArray();
            return View(pizze);
        }

        [HttpGet("Contacts")]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Details(long id)
        {
            var pizza = _context.Pizzas!.Include(p => p.Categoria).Include(p => p.Ingridients).SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }

            return View(pizza);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var formModel = new PizzaFormModel
            {
                Categories = _context.Categories!.ToArray(),
                Ingridients = _context.Ingridients!.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList(),
            };
            return View(formModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.Categories = _context.Categories!.ToArray();
                formModel.Ingridients = _context.Ingridients.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
                return View(formModel);
            }

            formModel.Pizza.Ingridients = formModel.SelectedIngridients.Select(si => _context.Ingridients.FirstOrDefault(i => i.Id == Convert.ToInt64(si))).ToList();

            _context.Pizzas!.Add(formModel.Pizza);
            _context.SaveChanges();

            return RedirectToAction("Menu");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(long id)
        {
            var pizza = _context.Pizzas!.Include(p => p.Ingridients).FirstOrDefault(p => p.Id == id);
            if (pizza is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }
            var formModel = new PizzaFormModel
            {
                Pizza = pizza,
                Categories = _context.Categories!.ToArray(),
                Ingridients = _context.Ingridients!.ToArray().Select(i => new SelectListItem(i.Name, i.Id.ToString(), pizza.Ingridients!.Any(_i => _i.Id == i.Id))).ToArray()
            };

            formModel.SelectedIngridients = formModel.Ingridients.Where(i => i.Selected).Select(i => i.Value).ToList();
            return View(formModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(long id, PizzaFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.Categories = _context.Categories!.ToArray();
                formModel.Ingridients = _context.Ingridients.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
                return View(formModel);
            }

            var pizzaToUpdate = _context.Pizzas!.Include(p => p.Ingridients).FirstOrDefault(p => p.Id == id);

            if (pizzaToUpdate is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }

            pizzaToUpdate.Nome = formModel.Pizza.Nome;
            pizzaToUpdate.Descrizione = formModel.Pizza.Descrizione;
            pizzaToUpdate.Prezzo = formModel.Pizza.Prezzo;
            pizzaToUpdate.Foto = formModel.Pizza.Foto;
            pizzaToUpdate.CategoriaId = formModel.Pizza.CategoriaId;
            pizzaToUpdate.Categoria = formModel.Pizza.Categoria;
            pizzaToUpdate.Ingridients = formModel.SelectedIngridients.Select(si => _context.Ingridients.FirstOrDefault(i => i.Id == Convert.ToInt64(si))).ToList();

            //_context.Pizzas!.Update(pizza);
            _context.SaveChanges();

            return RedirectToAction("Menu");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            var pizzaToDelete = _context.Pizzas!.FirstOrDefault(p => p.Id == id);
            if (pizzaToDelete is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }
            _context.Pizzas!.Remove(pizzaToDelete);
            _context.SaveChanges();
            return RedirectToAction("Menu");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}