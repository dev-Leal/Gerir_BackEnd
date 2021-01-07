using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gerir.API.Dominios;
using Senai.Gerir.API.Interfaces;
using Senai.Gerir.API.Repositorio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        //Criação do objeto que representa a interface
        private readonly ITarefaRepository _tarefaRepository;
        public TarefaController()
        {
            //Instanciamos o objeto, a classe
            //Não podemos instanciar a interface, mas por que?
            //
            _tarefaRepository = new TarefaRepositorio();
        }

        [Authorize]
        [HttpPost]

        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                //Pega o valor do usuário que está logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti);

                //Atribui o usuário a tarefa
                tarefa.UsuarioId = new System.Guid(usuarioid.Value);

                //Cadastra a tarefa
                _tarefaRepository.Cadastrar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        //api/tarefa/idTarefa
        [HttpGet("{id}")]

        public IActionResult BuscarTarefa(Guid id)
        {
            try
            {
                //Pega o valor do usuário que está logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti);

                //Busca uma tarefa pelo seu id
                var tarefa = _tarefaRepository.BuscarPorId(id);

                //Verifica  se a tarefa existe
                if (tarefa == null)
                    return NotFound();

                //Verifica se a tarefa é do usuário logado
                if (tarefa.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(Guid id, Tarefa tarefa)
        {
            try
            {
                //Pega o valor do usuário que esta logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );

                //Busca uma tarefa pelo seu Id
                var tarefaexiste = _tarefaRepository.BuscarPorId(id);

                //Verifica se a tarefa existe
                if (tarefaexiste == null)
                    return NotFound();

                //Verifica se a tarefa é do usuário logado
                if (tarefaexiste.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                //Atribui o valor do Id da tarefa ao id recebido como parametro na url
                tarefa.Id = id;
                _tarefaRepository.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                //Pega o valor do usuário que está logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti);

                var tarefas = _tarefaRepository.ListarTodos(
                    new System.Guid(usuarioid.Value));

                return Ok(tarefas);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        //api/tarefa/status/idTarefa
        [HttpPut("status/{id}")]
        public IActionResult AlterarStatus(Guid id)
        {
            try
            {
                //Pega o valor do usuário que esta logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );

                //Busca uma tarefa pelo seu Id
                var tarefa = _tarefaRepository.BuscarPorId(id);

                //Verifica se a tarefa existe
                if (tarefa == null)
                    return NotFound();

                //Verifica se a tarefa é do usuário logado
                if (tarefa.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                _tarefaRepository.AlterarStatus(id);

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
                //Pega o valor do usuário que esta logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );

                //Busca uma tarefa pelo seu Id
                var tarefaexiste = _tarefaRepository.BuscarPorId(id);

                //Verifica se a tarefa existe
                if (tarefaexiste == null)
                    return NotFound();

                //Verifica se a tarefa é do usuário logado
                if (tarefaexiste.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                _tarefaRepository.Remover(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
