namespace RFToDo_API.Models
{
    public class Meta
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TotalTareas { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
