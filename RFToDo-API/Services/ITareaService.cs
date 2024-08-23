using RFToDo_API.DTO;
using RFToDo_API.Models;

namespace RFToDo_API.Services
{
    public interface ITareaService
    {
        Task<List<Tarea>> ObtenerTareasPorMeta(int id);
        Task<bool> GuardarTarea(GuardarTareaDTO tareaDTO);
        Task<bool> EditarTarea(EditarTareaDTO tareaDTO, int id);
        Task<bool> EliminarTareas(List<Tarea> tareas);
        Task<bool> CompletarTareas(List<Tarea> tareas);
        Task<bool> ActualizarTotalesMeta(int idMeta);
    }
}
