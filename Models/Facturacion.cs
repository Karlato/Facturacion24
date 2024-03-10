using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Facturacion24.Models
{
    public class Facturacion
    {
        public int Id { get; set; }

        [Display(Name = "Vendedor")]
        public int IdVendedor { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public DateTime Fecha { get; set; }
        public string Comentario { get; set; } = string.Empty;

        [Display(Name = "Artículo")]
        public int IdArticulo { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        [NotMapped]
        public List<SelectListItem> Vendedores { get; set; } = new List<SelectListItem>();

        [NotMapped]
        public List<SelectListItem> Clientes { get; set; } = new List<SelectListItem>();

        [NotMapped]
        public List<SelectListItem> Articulos { get; set; } = new List<SelectListItem>();

        public Facturacion()
        {
            Vendedores = new List<SelectListItem>();
            Clientes = new List<SelectListItem>();
            Articulos = new List<SelectListItem>();
        }
    }
}
