
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entity
{
    public class Champion
    {
        [Key]
        public int Champion_ID { get; set; }
        public string Name { get; set; }

        public bool JelChest { get; set; }

        [ForeignKey(nameof(Role))]
        public int Role_ID { get; set; }

        public Role Role { get; set; }
    }
}
