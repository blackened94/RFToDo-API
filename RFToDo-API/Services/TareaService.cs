using Microsoft.EntityFrameworkCore;
using RFToDo_API.Data;
using RFToDo_API.DTO;
using RFToDo_API.Models;
using System.Threading;

namespace RFToDo_API.Services
{
    public class TareaService(RFToDoDbContext context) : ITareaService
    {
        private readonly RFToDoDbContext _context = context;

        public async Task<List<Tarea>> ObtenerTareasPorMeta(int id)
        {
            return await _context.Tarea.Where(t => t.IdMeta == id).ToListAsync();
        }

        public async Task<bool> GuardarTarea(GuardarTareaDTO tareaDTO)
        {
            var existeTarea = await _context.Tarea.FirstOrDefaultAsync(m => m.IdMeta == tareaDTO.IdMeta && (m.Nombre ?? "").Equals(tareaDTO.Nombre));

            if (existeTarea != null)
                return false;

            Tarea nuevaTarea = new()
            {
                Nombre = tareaDTO.Nombre,
                FechaCreacion = DateTime.Now,
                Estado = 1,
                IdMeta = tareaDTO.IdMeta
            };

            await _context.Tarea.AddAsync(nuevaTarea ?? new());
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditarTarea(EditarTareaDTO tareaDTO, int id)
        {
            var existeTareaPorNombre = await _context.Tarea.FirstOrDefaultAsync(m => m.IdMeta == tareaDTO.IdMeta && (m.Nombre ?? "").Equals(tareaDTO.Nombre));

            if (existeTareaPorNombre != null)
                return false;

            var tareaEncontrada = await _context.Tarea.FindAsync(id);

            if (tareaEncontrada == null)
                return false;

            tareaEncontrada.Nombre = tareaDTO.Nombre;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTareas(List<Tarea> tareas)
        {
            _context.Tarea.RemoveRange(tareas ?? []);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompletarTareas(List<Tarea> tareas)
        {
            var ids = tareas.Select(t => t.Id).ToList();
            
            var tareasEncontradas = await _context.Tarea.Where(e => ids.Contains(e.Id)).ToListAsync();

            foreach (var tarea in tareasEncontradas)
            {
                tarea.Estado = 0;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private static double CalcularPorcentajeCompletadas(int total, List<Tarea> tareas)
        {
            if (tareas == null || tareas.Count == 0)
                return 0;

            int totalTareas = total;
            int tareasCompletadas = tareas.Count(t => t.Estado == 0);

            return (double)tareasCompletadas / totalTareas * 100;
        }

        public async Task<bool> ActualizarTotalesMeta(int idMeta)
        {
            var totalTareas = _context.Tarea.Where(e => e.IdMeta == idMeta).Count();
            var tareasCerradas = await _context.Tarea.Where(e => e.Estado == 0 && e.IdMeta == idMeta).ToListAsync();

            var meta = await _context.Meta.FindAsync(idMeta);

            if (meta != null)
            {
                meta.TotalTareas = totalTareas;
                meta.Porcentaje = Convert.ToDecimal(CalcularPorcentajeCompletadas(totalTareas, tareasCerradas));
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
