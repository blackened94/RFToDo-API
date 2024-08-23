using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RFToDo_API.DTO;
using RFToDo_API.Models;
using RFToDo_API.Services;

namespace RFToDo_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MetasController(IMetaService metaService) : ControllerBase
    {
        private readonly IMetaService _metaService = metaService;

        [HttpGet]
        public async Task<IActionResult> ObtenerMetas()
        {
            var result = await _metaService.ObtenerMetas();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarMeta([FromBody] GuardarMetaDTO metaDto)
        {
            var result = await _metaService.GuardarMeta(metaDto);

            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarMeta([FromBody] EditarMetaDTO metaDto, int id)
        {
            var result = await _metaService.EditarMeta(metaDto, id);
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMeta(int id)
        {
            var result = await _metaService.EliminarMeta(id);
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
