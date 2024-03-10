namespace Facturacion24.Models
{
    public class AsientoContable
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdCliente { get; set; }
        public string Cuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public DateTime FechaAsiento { get; set; }
        public decimal MontoAsiento { get; set; }
        public string Estado { get; set; }
    }
}
