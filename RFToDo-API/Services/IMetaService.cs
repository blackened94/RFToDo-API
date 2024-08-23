using RFToDo_API.DTO;
using RFToDo_API.Models;

namespace RFToDo_API.Services
{
    public interface IMetaService
    {
        Task<List<Meta>> ObtenerMetas();
        Task<bool> GuardarMeta(GuardarMetaDTO metaDto);
        Task<bool> EditarMeta(EditarMetaDTO metaDTO, int id);
        Task<bool> EliminarMeta(int id);
    }
}
