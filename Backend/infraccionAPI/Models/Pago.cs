namespace infraccionAPI.Models
{
    public class Pago
    {
        public int PagoID { get; set; }
        public int InfraccionID { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }

        public Infraccion Infraccion { get; set; }
    }
}
