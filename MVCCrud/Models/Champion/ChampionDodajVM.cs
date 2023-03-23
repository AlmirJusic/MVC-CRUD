using Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCrud.Models.Champion
{
    public class ChampionDodajVM
    {
        
        public int Champion_ID { get; set; }
        [Required(ErrorMessage ="Unesite naziv championa.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Odaberite chest championa.")]

        public bool JelChest { get; set; }

        [Required(ErrorMessage = "Odaberite rolu championa.")]
        public int Role_ID { get; set; }

        public List<SelectListItem> Role { get; set; }
    }
}
