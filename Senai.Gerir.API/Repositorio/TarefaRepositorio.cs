using Microsoft.EntityFrameworkCore;
using Senai.Gerir.API.Contextos;
using Senai.Gerir.API.Dominios;
using Senai.Gerir.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.API.Repositorio
{
    public class TarefaRepositorio : ITarefaRepository
    {
        //Declaro um objeto do tipo GerirContext que será
        //a representação do banco de dados

        /// <summary>
        /// _context é a representação do banco de dados 
        /// pelo objeto GerirContext
        /// </summary>
        private readonly GerirContext _context;

        public TarefaRepositorio()
        {

            //Um objeto é uma instância de um tipo de objeto;
            //Instancia = termo  que significa "criar", é uma ação!

            //Cria uma Instancia de GerirContext
            _context = new GerirContext();
        }


        public Tarefa AlteraStatus(Guid IdTarefa)
        {
            throw new NotImplementedException();
        }

        public Tarefa BuscarPorId(Guid IdTarefa)
        {
            try
            {
                var tarefa = _context.Tarefas.Find(IdTarefa);

                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public Tarefa Cadastrar(Tarefa tarefa)

        {
            try
            {
                //Adicionamos uma tarefa ao nosso DbSet Tarefas do contexto
                _context.Tarefas.Add(tarefa);

                //Para que as alterações sejam realmente salvas no contexto 
                //precisamos executar o SaveChanges

                // Salva as alterações do contexto               
                _context.SaveChanges();

                return tarefa;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); 
            }
           
            
        }

        public Tarefa Editar(Tarefa tarefa)
        {
            try
            {
                //Busca o tarefa no banco
                var tarefaexiste = BuscarPorId(tarefa.Id);

                //Verifica se o tarefa existe
                if (tarefaexiste == null)
                    throw new Exception("Essa tarefa não encontrada");


                //Verifica se o valor descrição é nulo ou vazio
                //Se  valor se encaixar nessa situação ele pula a alteração
                //e mantém a descrição antiga
                if (!string.IsNullOrEmpty(tarefa.Descricao))
                //Altera os valores de tarefas
                tarefaexiste.Descricao = tarefa.Descricao;

                if (!string.IsNullOrEmpty(tarefa.Categoria))
                tarefaexiste.Categoria = tarefa.Categoria;

                //Fazer o if/ null para tarefas
                tarefaexiste.DataEntrega = tarefa.DataEntrega;

                if (!string.IsNullOrEmpty(tarefa.Titulo))
                tarefaexiste.Titulo = tarefa.Titulo;

                _context.Tarefas.Update(tarefaexiste);
                _context.SaveChanges();

                return tarefaexiste;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); 
            }
        }

        public List<Tarefa> ListarTodos(Guid IdUsuario)
        {
            throw new NotImplementedException();
        }

        public void Remover(Guid Id)
        {
            try
            {
                //Busca o Id da tarefa e o armazena na variável tarefa
                var tarefa = BuscarPorId(Id);

                //Removemos uma tarefa no DbSet do contexto 
                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
