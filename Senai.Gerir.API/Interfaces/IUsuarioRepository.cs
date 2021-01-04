using Senai.Gerir.API.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.API.Interfaces
{
    interface IUsuarioRepository
    {
        Usuario Cadastrar(Usuario usuario);
        Usuario Logar(String email, String senha);
        Usuario Editar(Usuario usuario);
        void Remover(Guid Id);
        Usuario BuscarPorId(Guid Id);


    }
}
