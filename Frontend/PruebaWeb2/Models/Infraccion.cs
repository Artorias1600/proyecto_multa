namespace PruebaWeb2.Models
{
    public class Infraccion
    {
        public int InfraccionID { get; set; }
        public int VehiculoID { get; set; }
        public int ConductorID { get; set; }
        public int SancionID { get; set; }
        public int AgenteID { get; set; }
        public DateTime Fecha { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Conductor Conductor { get; set; }
        public Sancion Sancion { get; set; }
        public AgenteTransito AgenteTransito { get; set; }
    }
}
