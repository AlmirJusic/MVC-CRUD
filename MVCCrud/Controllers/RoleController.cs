using Database.EF;
using Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCCrud.Models;

namespace MVCCrud.Controllers
{
    public class RoleController : Controller
    {
        private mojDbContext _context;
        public RoleController(mojDbContext _db)
        {
            _context = _db;
        }
        // GET: RoleController
        public IActionResult Index(string option,string search)
        {
            List<RoleIndexVM> model = _context.Role.Select(x => new RoleIndexVM
            {
                Role_ID = x.Role_ID,
                NameRole = x.NameRole,
            }).ToList();

            if(option=="Naziv")
            {
                return View(model.
                    Where(x=>search==null || x.NameRole.ToLower().Contains(search.ToLower())).ToList());
            }
            else if(option=="Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            RoleDodajVM model=new RoleDodajVM();
            return View(model);
        }
        public IActionResult Snimi(RoleDodajVM vm)
        {
            Validiraj(vm);
            if(!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }

            Role role;
            if(vm.Role_ID==0)
            {
                role = new Role();
                _context.Add(role);
            }
            else
            {
                role = _context.Role.Find(vm.Role_ID);
            }

            role.Role_ID=vm.Role_ID;
            role.NameRole=vm.NameRole;  

            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        private void Validiraj(RoleDodajVM vm)
        {
            foreach (var item in _context.Role)
            {
                if(item.NameRole==vm.NameRole)
                {
                    ModelState.AddModelError("NameRole", "Rola vec postoji!");
                }
            }
        }
        public IActionResult Uredi(int id)
        {
            Role role = _context.Role.Find(id);

            if(role==null)
            {
                return RedirectToAction(nameof(Index));
            }

            RoleDodajVM model=new RoleDodajVM
            {
                Role_ID = role.Role_ID,
                NameRole=role.NameRole,
            };

            return View("Dodaj",model);
        }
        public IActionResult Obrisi(int id)
        {
            var role = _context.Role.Find(id);

            _context.Remove(role);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
    }
}
