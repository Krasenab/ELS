using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsModels.SQL
{
    public class Category
    {
        public Category()
        {
            this.Equipments = new List<Equipment>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Equipment> Equipments { get; set; }

    }
}
