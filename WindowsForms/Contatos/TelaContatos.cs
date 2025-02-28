﻿using e_Agenda.Controladores;
using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WindowsForms.Contatos
{
    public partial class TelaContatos : Form
    {
        ControladorContatos controlador;
        public TelaContatos()
        {
            InitializeComponent();
            controlador = new ControladorContatos();
            changeButtons(false);
            carregarTabelas();
        }
        public DataTable carregarTabela(List<Contato> fonte)
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add("ID");
            tabela.Columns.Add("Nome");
            tabela.Columns.Add("Email");
            tabela.Columns.Add("Telefone");
            tabela.Columns.Add("Empresa");
            tabela.Columns.Add("Cargo");

            List<Contato> contatos = fonte;

            foreach (var contato in contatos)
            {
                var linha = tabela.NewRow();
                linha["ID"] = contato.id;
                linha["Nome"] = contato.nome;
                linha["Email"] = contato.email;
                linha["Telefone"] = contato.telefone;
                linha["Empresa"] = contato.empresa;
                linha["Cargo"] = contato.cargo;

                tabela.Rows.Add(linha);
            }
            return tabela;
        }
        private void bt_cadastro_Click(object sender, EventArgs e)
        {
            Hide();
            using (CadastrarContatos tela = new CadastrarContatos(controlador)) { tela.ShowDialog(); }
            carregarTabelas();
            Show();
        }
        private void bt_editar_Click(object sender, EventArgs e)
        {
            Hide();
            Contato contato = controlador.getById(getIdSelecionadoTabela());
            using (CadastrarContatos tela = new CadastrarContatos(controlador, contato)) { tela.ShowDialog(); }
            carregarTabelas();
            Show();
        }
        private void bt_excluir_Click(object sender, EventArgs e)
        {
            controlador.excluir(getIdSelecionadoTabela());
            carregarTabelas();
            changeButtons(false);
        }
        private int getIdSelecionadoTabela()
        {
            return Convert.ToInt32(dg_visualizar.CurrentRow.Cells[0].Value);
        }
        private void dg_visualizar_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            changeButtons(true);
        }
        private void changeButtons(bool estado)
        {
            bt_excluir.Enabled = estado;
            bt_editar.Enabled = estado;
        }
        private void carregarTabelas()
        {
            dg_visualizar.DataSource = carregarTabela(controlador.Registros);
        }
    }
}
