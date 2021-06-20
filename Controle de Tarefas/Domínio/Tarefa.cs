﻿using Controle_de_Tarefas.Controladores;
using System;

namespace Controle_de_Tarefas.Dominio
{
    public class Tarefa : Entidade
    {
        public readonly ControladorObjetivos ctrlObjetivos = new ControladorObjetivos();
        public Tarefa(int porcentagem_conclusao, DateTime dt_criacao, DateTime dt_conclusao, String titulo, int prioridade)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.porcentagem_conclusao = porcentagem_conclusao;
            this.dt_criacao = dt_criacao;
            this.dt_conclusao = dt_conclusao;
        }
        public int porcentagem_conclusao { get; private set; }
        public DateTime dt_criacao { get; private set; }
        public DateTime dt_conclusao { get; private set; }
        public string titulo { get; set; }
        public int prioridade { get; set; }
        public void atualizaConclusao()
        {
            var objetivos = ctrlObjetivos.Registros;
            var concluidos = objetivos.FindAll(x => x.finalizado);
            porcentagem_conclusao = concluidos.Count / objetivos.Count * 100;
        }
        public void concluiTarefa()
        {
            atualizaConclusao();
            if (porcentagem_conclusao == 100)
                dt_conclusao = DateTime.Now;
        }
        public override String ToString()
        {
            return $"ID: {id} | Titulo: {titulo} | Prioridade: {prioridade} | Conclusão: {porcentagem_conclusao}% | Data Criação: {dt_criacao} " +
            $"{(dt_conclusao != new DateTime(1900, 1, 1) ? $" | Data Conclusão: {dt_conclusao}" : "")}\n{ctrlObjetivos}";
        }
    }
}
