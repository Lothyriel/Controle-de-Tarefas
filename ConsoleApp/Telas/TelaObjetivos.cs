﻿using e_Agenda.Controladores;
using e_Agenda.Dominio;
using System;

namespace e_Agenda.Telas
{
    public class TelaObjetivos : Tela<Objetivo>
    {
        private ControladorTarefas controladorT;
        private new ControladorTarefas.ControladorObjetivos controlador;
        private Tarefa tarefa;

        private TelaTarefas telaT;

        public TelaObjetivos(Tarefa tarefa) : base(new ControladorTarefas.ControladorObjetivos())
        {
            this.tarefa = tarefa;
            controlador = new ControladorTarefas.ControladorObjetivos();
            controladorT = new ControladorTarefas();
            telaT = new TelaTarefas();
            menu();
        }
        public override void menu()
        {
            String opcao = "";
            while (opcao != "S")
            {
                mostrarTarefa();
                Console.WriteLine("\nEscolha uma opção: \n");
                Console.WriteLine("Digite 1 para cadastrar novos objetivos para esta tarefa");
                Console.WriteLine("Digite 2 para marcar objetivos como concluídos");
                Console.WriteLine("Digite 3 para editar esta tarefa");
                Console.WriteLine("Digite 4 para excluir esta tarefa\n");
                Console.WriteLine("Digite S para Voltar\n");
                TipoMensagem.Requisicao.mostrarMensagem("Opção:");
                opcao = Console.ReadLine().ToUpperInvariant();
                switch (opcao)
                {
                    case "1": cadastrar(); break;
                    case "2": concluirObjetivo(); break;
                    case "3": editar(); break;
                    case "4": controladorT.excluir(tarefa.id); return;
                    case "S": break;
                    default: TipoMensagem.Erro.mostrarMensagem("\nSelecione uma opcão correta!"); break;
                }
            }
        }
        public override Objetivo registroValido()
        {
            String descricao;
            Console.Clear();
            while (true)
            {
                TipoMensagem.Requisicao.mostrarMensagem("Digite a descrição do objetivo\n");
                descricao = Console.ReadLine();
                if (descricao.Length > 0)
                    break;
                TipoMensagem.Erro.mostrarMensagem("\nDescrição não pode ser vazia");
            }
            return new Objetivo(descricao, tarefa.id);
        }
        protected override Objetivo cadastrar()
        {
            Objetivo objetivo = base.cadastrar();
            tarefa.addObjetivo(objetivo);
            controladorT.editar(tarefa.id, tarefa);

            return objetivo;
        }
        protected override void editar()
        {
            Tarefa nova = telaT.registroValido();
            tarefa.editar(nova);
            controladorT.editar(tarefa.id, nova);
        }
        private void mostrarTarefa()
        {
            Console.Clear();
            TipoMensagem.Item.mostrarMensagem(tarefa + "\n");
        }
        private void concluirObjetivo()
        {
            mostrarTarefa();
            string opcao = obterOpcao(controlador.objetivosIncompletos(tarefa.id));
            if (opcao == "S") return;

            int id = Convert.ToInt32(opcao);
            Objetivo objetivo = controlador.getById(id);

            objetivo.concluir();
            controlador.editar(id, objetivo);

            tarefa.atualizaConclusao();
            controladorT.editar(tarefa.id, tarefa);
        }
    }
}