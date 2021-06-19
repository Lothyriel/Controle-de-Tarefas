﻿using Controle_de_Tarefas.Dominio;
using System.Collections.Generic;

namespace Controle_de_Tarefas.Controladores
{
    public class Controlador<T> where T : Entidade
    {
        private int id;
        private readonly List<T> registros = new List<T>();
        public List<T> Registros => registros;

        public void inserir(T registro)
        {
            registro.id = ++id;
            registros.Add(registro);
        }
        public virtual void editar(int id, T registro)
        {
            int indice = registros.IndexOf(getById(id));
            registros[indice] = registro;
        }
        public void excluir(int id)
        {
            registros.Remove(getById(id));
        }
        public T getById(int id)
        {
            return registros.Find(x => x.id == id);
        }
        public bool existsById(int id)
        {
            return getById(id) != null;
        }
    }
}