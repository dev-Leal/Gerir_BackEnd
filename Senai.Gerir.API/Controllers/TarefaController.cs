using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gerir.API.Dominios;
using Senai.Gerir.API.Interfaces;
using Senai.Gerir.API.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        //Criação do objeto que representa a interface
        private readonly ITarefaRepository _tarefaReposiroty;
        public TarefaController()
        {
            //Instanciamos o objeto 
            _tarefaReposiroty = new TarefaRepositorio();
        }

        [HttpPost]

        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                _tarefaReposiroty.Cadastrar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("{id}")]

        public IActionResult BuscarTarefa(Guid id)
        {
            try
            {
                var tarefa = _tarefaReposiroty.BuscarPorId(id);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            try
            {
                _tarefaReposiroty.Remover(id);

                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
