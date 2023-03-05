using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Range(1,int.MaxValue)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "El precio debe se mayor a cero")]
        public int Precio { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        [Display(Name = "Fecha Creacion")]
        [DataType(DataType.Date)]
        public string FechaCreacion { get; set; }

        [Required(ErrorMessage = "La categoria es requerida")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [Display(Name = "Marca")]
        public int IdMarca{ get; set; }

        [ForeignKey("IdMarca")]
        public Marca Marca { get; set; }
    }
}
