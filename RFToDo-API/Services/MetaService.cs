using Microsoft.EntityFrameworkCore;
using RFToDo_API.Data;
using RFToDo_API.DTO;
using RFToDo_API.Models;

namespace RFToDo_API.Services
{
    public class MetaService(
        RFToDoDbContext context,
        ITareaService tareaService
        ) : IMetaService
    {
        private readonly RFToDoDbContext _context = context;
        private readonly ITareaService _tareaService = tareaService;

        public async Task<List<Meta>> ObtenerMetas()
        {
            return await _context.Meta.ToListAsync();
        }

        public async Task<bool> GuardarMeta(GuardarMetaDTO metaDTO)
        {
            var existeMeta = await _context.Meta.FirstOrDefaultAsync(m => (m.Nombre ?? "").Equals(metaDTO.Nombre));

            if (existeMeta != null)
                return false;

            Meta nuevaMeta = new() 
            { 
                Nombre = metaDTO.Nombre,
                FechaCreacion = metaDTO.FechaCreacion,
                TotalTareas = 0,
                Porcentaje = 0
            };

            await _context.Meta.AddAsync(nuevaMeta ?? new ());
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditarMeta(EditarMetaDTO metaDTO, int id)
        {
            var existeMetaPorNombre = await _context.Meta.FirstOrDefaultAsync(m => (m.Nombre ?? "").Equals(metaDTO.Nombre));

            if (existeMetaPorNombre != null)
                return false;

            var metaEncontrada = await _context.Meta.FindAsync(id);

            if (metaEncontrada == null)
                return false;

            metaEncontrada.Nombre = metaDTO.Nombre;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMeta(int id)
        {
            var tareasPorMeta = await _context.Tarea.Where(t => t.IdMeta == id).ToListAsync();
            var tareasEliminadas = await  _tareaService.EliminarTareas(tareasPorMeta);

            if (tareasEliminadas)
            {
                var metaEncontrada = await _context.Meta.FindAsync(id);

                if (metaEncontrada != null)
                {
                    _context.Meta.Remove(metaEncontrada);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
