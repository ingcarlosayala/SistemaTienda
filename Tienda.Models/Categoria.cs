using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la marca es requerido")]
        [Display(Name = "Marca")]
        public string Nombre { get; set; }
    }
}
