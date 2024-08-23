using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RFToDo_API.DTO;
using RFToDo_API.Models;
using RFToDo_API.Services;

namespace RFToDo_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TareasController(ITareaService tareaService) : ControllerBase
    {
        private readonly ITareaService _tareaService = tareaService;

        [HttpGet("{id}")]
        public async Task<List<Tarea>> ObtenerTareasPorMeta(int id)
        {
            var result = await _tareaService.ObtenerTareasPorMeta(id);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTarea([FromBody] GuardarTareaDTO tareaDto)
        {
            var result = await _tareaService.GuardarTarea(tareaDto);
            if (result) 
            {
                await _tareaService.ActualizarTotalesMeta(tareaDto.IdMeta);
                return Ok(result);
            }
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTarea([FromBody] EditarTareaDTO tareaDto, int id)
        {
            var result = await _tareaService.EditarTarea(tareaDto, id);
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTareas([FromBody] List<Tarea> tareas)
        {
            var idMeta = tareas.Select(t => t.IdMeta).FirstOrDefault();
            var result = await _tareaService.EliminarTareas(tareas);
            if (result) 
            {
                await _tareaService.ActualizarTotalesMeta(idMeta);
                return Ok(result);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CompletarTareas([FromBody] List<Tarea> tareas)
        {
            var idMeta = tareas.Select(t => t.IdMeta).FirstOrDefault();
            var result = await _tareaService.CompletarTareas(tareas);
            if (result) {
                await _tareaService.ActualizarTotalesMeta(idMeta);
                return Ok(result);
            }
            else
                return BadRequest();
        }
    }
}
