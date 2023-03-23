using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entity
{
    public class Role
    {
        [Key]
        public int Role_ID { get; set; }
        public string NameRole { get; set; }
    }
}
