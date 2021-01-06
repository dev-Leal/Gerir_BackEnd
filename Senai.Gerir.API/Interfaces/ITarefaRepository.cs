using Senai.Gerir.API.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.API.Interfaces
{
    interface ITarefaRepository
    {
        Tarefa Cadastrar(Tarefa tarefa);
        List<Tarefa> ListarTodos(Guid IdUsuario);
        Tarefa AlteraStatus(Guid IdTarefa);
        void Remover(Guid Id);
        Tarefa Editar(Tarefa tarefa);
        Tarefa BuscarPorId(Guid IdTarefa);
     }
}
