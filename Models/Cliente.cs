namespace Facturacion24.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NombreORazon { get; set; } = string.Empty;
        public string RNCOCedula { get; set; } = string.Empty;
        public string CuentaContable { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
