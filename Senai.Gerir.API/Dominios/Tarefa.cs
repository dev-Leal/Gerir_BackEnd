﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.Gerir.API.Dominios
{
    public partial class Tarefa
    {
        public Tarefa()
        {
            //Gera um novo Id sempre que é cadastrado 
            Id = Guid.NewGuid();
            //Para que o valor NULL colocado no banco seja por padrão 0 
            //quando não for cadastrado
            Status = false;
        }
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime DataEntrega { get; set; }
        public bool? Status { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
