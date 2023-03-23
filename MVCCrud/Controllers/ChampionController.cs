using Database.EF;
using Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCrud.Models;
using MVCCrud.Models.Champion;

namespace MVCCrud.Controllers
{
    public class ChampionController : Controller
    {
        private mojDbContext _context;
        public ChampionController(mojDbContext _db)
        {
            _context = _db;
        }
        public IActionResult Index(string option, string search)
        {
            List<ChampionIndexVM> model = _context.Champion.Select(x => new ChampionIndexVM
            {
                Champion_ID = x.Champion_ID,
                JelChest = x.JelChest,
                Name = x.Name,
                Role = x.Role.NameRole
            }).ToList();
            if (option == "Name")
            {
                return View(model.Where(x => search == null || x.Name.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Role")
            {
                return View(model.Where(x => search == null || x.Role.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Chest")
            {
                return View(model.Where(x => x.JelChest==true).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            ChampionDodajVM model = new ChampionDodajVM
            {
                Role = _context.Role.OrderBy(x => x.NameRole)
                    .Select(x => new SelectListItem
                    {
                        Value=x.Role_ID.ToString(),
                        Text=x.NameRole,
                    }).ToList()
            };
            return View(model);
        }
        public IActionResult Snimi(ChampionDodajVM vm)
        {
            Validiraj(vm);
            if(!ModelState.IsValid)
            {
                vm.Role = _context.Role.OrderBy(x => x.NameRole)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Role_ID.ToString(),
                        Text=x.NameRole,
                    }).ToList();
            }

            Champion champion;

            if(vm.Champion_ID==0)
            {
                champion = new Champion();
                _context.Add(champion); 
            }
            else
            {
                champion = _context.Champion.Find(vm.Champion_ID);
            }
            champion.Champion_ID= vm.Champion_ID;
            champion.Name= vm.Name;
            champion.JelChest = vm.JelChest;
            champion.Role_ID=vm.Role_ID;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void Validiraj(ChampionDodajVM vm)
        {
            if (vm.Role_ID == 0)
            {
                ModelState.AddModelError("Role", "Ne postoji dodan ni jedan role u bazi!");
            }
            foreach (var item in _context.Champion)
            {
                if (item.Name == vm.Name)
                {
                    ModelState.AddModelError("Name", "Ime champa vec postoji!");
                }
            }
            
        }
        public IActionResult Uredi(int id)
        {
            Champion champion=_context.Champion.Find(id);
            if(champion == null)
            {
                return RedirectToAction("Index");
            }
            ChampionDodajVM model = new ChampionDodajVM
            {
                Champion_ID = champion.Champion_ID,
                Name = champion.Name,
                JelChest = champion.JelChest,
                Role_ID = champion.Role_ID,
                Role = _context.Role.OrderBy(x => x.NameRole)
                    .Select(x => new SelectListItem
                        (
                            x.NameRole, x.Role_ID.ToString()
                        
                        )
                        )
                    .ToList()
            };
            return View("Dodaj", model);
        }
        public IActionResult Obrisi(int id)
        {
            var champion = _context.Champion.Find(id);

            _context.Remove(champion);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    
}
