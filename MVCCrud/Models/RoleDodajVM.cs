using System.ComponentModel.DataAnnotations;

namespace MVCCrud.Models
{
    public class RoleDodajVM
    {
        public int Role_ID { get; set; }
        [Required(ErrorMessage ="Unesite naziv role!")]
        public string NameRole { get; set; }
    }
}
