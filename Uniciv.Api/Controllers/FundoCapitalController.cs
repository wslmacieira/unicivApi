using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Uniciv.Api.Models;
using Uniciv.Api.Repositories;

namespace Uniciv.Api.Controllers
{
    public class FundoCapitalController : Controller
    {
        private readonly IFundoCapitalRepository _repository;

        public FundoCapitalController(IFundoCapitalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("v1/fundoscapital")]
        public IActionResult ListarFundos()
        {
            return Ok(_repository.ListarFundos());
        }

        [HttpPost("v1/fundoscapital")]
        public IActionResult AdicionarFundo([FromBody] FundoCapital fundo)
        {
            _repository.Adicionar(fundo);
            return Created("", fundo);
        }

        [HttpPut("v1/fundoscapital/{id}")]
        public IActionResult AlterarFundo(Guid id, [FromBody] FundoCapital fundo)
        {
            var fundoAntigo = _repository.ObterPorId(id);

            if (fundoAntigo == null)
            {
                return NotFound();
            }
            fundoAntigo.Nome = fundo.Nome;
            fundoAntigo.ValorAtual = fundo.ValorAtual;
            fundoAntigo.ValorNecessario = fundo.ValorNecessario;
            fundoAntigo.DataResgate = fundo.DataResgate;
            _repository.Alterar(fundoAntigo);
            return Ok();
        }

        [HttpGet("v1/fundoscapital/{id}")]
        public IActionResult ObterFundo(Guid id)
        {
            var fundoAntigo = _repository.ObterPorId(id);

            if (fundoAntigo == null)
            {
                return NotFound();
            }
            return Ok(fundoAntigo);
        }

        [HttpDelete("v1/fundoscapital/{id}")]
        public IActionResult RemoverFundo(Guid id)
        {
            var fundo = _repository.ObterPorId(id);

            if (fundo == null)
            {
                return NotFound();
            }
            _repository.RemoverFundo(fundo);
            return Ok();
        }
    }
}
