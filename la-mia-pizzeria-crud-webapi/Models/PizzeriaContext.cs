using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza>? Pizzas { get; set; }        
        public DbSet<Categoria>? Categories { get; set; }
        public DbSet<Ingrediente>? Ingridients { get; set; }

        public PizzeriaContext(DbContextOptions<PizzeriaContext> options)
         : base(options)
        {
        }

        public void Seed()
        {
                var margherita = new Pizza("Margherita", "Mozzarella e pomodoro", "/img/margherita.jfif", Convert.ToDecimal(5.00));
                var diavola = new Pizza("Diavola", "Mozzarella, pomodoro e salame piccante", "/img/diavola.jfif", Convert.ToDecimal(6.00));
                var quattroStagioni = new Pizza("Quattro Stagioni", "Mozzarella e pomodoro e un altro po' di roba", "/img/quattro_stagioni.jfif", Convert.ToDecimal(7.50));
                var salsicciosa = new Pizza("Salsicciosa", "Mozzarella, patate e salsiccia", "/img/salsicciosa.jfif", Convert.ToDecimal(8.00));
                var pizze = new List<Pizza>() { margherita, diavola, quattroStagioni, salsicciosa };
            if(!Pizzas.Any())
            {
                Pizzas.AddRange(pizze);
            }

            if(!Categories.Any())
            {
                var categorie = new Categoria[]
                {
                    new()
                    {
                        Nome = "Pizza bianca",
                        Pizzas = pizze
                    },
                     new()
                    {
                        Nome = "Pizza rossa"
                    },
                      new()
                    {
                        Nome = "Pizza vegetariana"
                    },
                       new()
                    {
                        Nome = "Pizza halal"
                    },
                };
                Categories.AddRange(categorie);
            }
            if(!Ingridients.Any())
            {
                var ingredienti = new Ingrediente[]
                {
                    new()
                    {
                        Name = "Pomodoro",
                        Pizzas = pizze
                    },
                    new()
                    {
                        Name = "Mozzarella"
                    },
                    new()
                    {
                        Name = "Basilico"
                    },
                    new()
                    {
                        Name = "Salsiccia"
                    },
                };
            Ingridients.AddRange(ingredienti);
                
            }
            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("Admin"),
                    new("User")
                };

                Roles.AddRange(seed);
            }

            if (Users.Any(u => u.Email == "admin@dev.com" || u.Email == "user@dev.com")
            && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");
                var user = Users.First(u => u.Email == "user@dev.com");

                var adminRole = Roles.First(r => r.Name == "Admin");
                var userRole = Roles.First(r => r.Name == "User");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    },
                    new()
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id
                    }
                };

                UserRoles.AddRange(seed);
            }
            SaveChanges();
        }
    }
}
