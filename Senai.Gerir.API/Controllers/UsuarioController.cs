using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gerir.API.Dominios;
using Senai.Gerir.API.Interfaces;
using Senai.Gerir.API.Repositorio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//Recursos para depurar código C#
//F5-
//F10-
//F11-

//Ponto de interrupção
//indica quando o Visual Studio deve suspender 
//o código em execução para que você possa examinar os valores das variáveis 
// >>>Defina um ponto de interrupção clicando na margem esquerda<<<

namespace Senai.Gerir.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepositorio();

        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(usuario);

                return Ok(usuario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Quando trabalhamos com dois HttpsPost por exemplo, 
        //precisamos dar rotas diferetes para cada um deles
        //fazemos isso colocando ("login") 
        //informando que quando esse Post for executado
        //o caminho será o login, Logar
        [HttpPost("login")]
        public IActionResult Logar(Usuario usuario)
        {
            try
            {
                //Verifica se o usuário existe 
                var usuarioexiste = _usuarioRepository.
                                    Logar(usuario.Email, usuario.Senha);

                //Caso não exista retona NotFound
                if (usuarioexiste == null)
                    return NotFound();

                //Caso exista gera o token do usuário
                var token = GerarJsonWebToken(usuarioexiste);

                //retorna sucesso com o Token do Usuario
                return Ok(token);
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult MeusDados()
        {
            try
            {
                //Pega as informações referente ao usuário na claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti 
                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        
                var usuario = _usuarioRepository.BuscarPorId(new Guid(usuarioid.Value));


                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Editar(Usuario usuario)
        {
            try
            {

                //Pega as informações referente ao usuário na claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti 
                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                //Atribuo o valor do usuarioid ao id do usuario recebido
                usuario.Id = new Guid(usuarioid.Value);

                //Envia para o metodo editar os daods do usuário recebido
                _usuarioRepository.Editar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Remover()
        {
            try
            {

                //Pega as informações referente ao usuário na claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti 
                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
                _usuarioRepository.Remover(new Guid(usuarioid.Value));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GerarJsonWebToken(Usuario usuario)
        {
            //Chave de segurança
            var chaveSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GerirChaveSeguranca"));
            //Define as credenciais 
            var credenciais = new SigningCredentials(chaveSeguranca, SecurityAlgorithms.HmacSha256);

            var data = new[]
            {
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                "gerir.com.br",
                "gerir.com.br",
                data,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

       

    }
}
