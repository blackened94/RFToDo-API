using Microsoft.AspNetCore.Routing.Constraints;

namespace RFToDo_API.DTO
{
    public class GuardarTareaDTO
    {
        public string? Nombre { get; set; }
        public int IdMeta { get; set; }
    }
}
