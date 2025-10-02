using AutoMapper;
using Desafio.API.Models;
using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/divida")]
    public class DesafioController(IDesafioHandler desafioHandler, IMapper mapper) : ControllerBase
    {
        private readonly IDesafioHandler _desafioHandler = desafioHandler;

        private readonly IMapper _mapper = mapper;

        [HttpPost("cadastrar")]
        public async Task<IActionResult>  AdicionaDivida([FromBody] Divida divida)
        {
            await _desafioHandler.CadastraDividaHandler(_mapper.Map<DividaDto>(divida));

            return Ok(new[] { $"Divida cadastrada" });
        }

        [HttpGet("buscar-dividas")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _desafioHandler.BuscarDividaHandler();
            if (result.Count > 0)
                return Ok(result);
            else
                return BadRequest("Não foi encontrada nenhuma divida");
        }

        [HttpGet("buscar-dividas/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _desafioHandler.BuscarDividaPorIdHandler(id);
            if (result is not null)
                return Ok(result);
            else
                return BadRequest("Não foi encontrada nenhuma divida");
        }
    }
}