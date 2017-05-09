using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormVendas : Window
    {
        RepositorioVenda repositorioVenda;

        public Cliente Cliente { get; set; }

        public FormVendas()
        {
            repositorioVenda = new RepositorioVenda();
            InitializeComponent();
        }

        public FormVendas(Cliente cliente)
        {
            InitializeComponent();

            this.Cliente = cliente;
            this.Title = "Vendas para o cliente " + cliente.Nome; // Muda o titulo da tela para o nome do cliente selecionado.

            repositorioVenda = new RepositorioVenda();
            
        }

        public FormVendas(Venda venda)
        {
            repositorioVenda = new RepositorioVenda();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados()
        {
            if (Cliente == null)
            {
                lstVendas.DataContext = repositorioVenda.Liste();
            }
            else
            {
                lstVendas.DataContext = repositorioVenda.ListePorCliente(Cliente.Codigo);
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var janelaVenda = new FormCadastroDeVendas();
            janelaVenda.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            var venda = (Venda)lstVendas.SelectedItem;

            this.Hide();
            var janelaVenda = new FormCadastroDeVendas(venda);
            janelaVenda.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja remover uma venda?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var venda = (Venda)lstVendas.SelectedItem;
                    repositorioVenda.Excluir(venda);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Venda removida com sucesso");
                    }
                }
                else
                {
                }
            }
        }

        private void btnVendaEfetuada_Click(object sender, RoutedEventArgs e)
        {

            // 1. Listar itens da venda para enviar ao fornecedor.
            if (lstVendas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item");
                return;
            }
            var venda = (Venda)lstVendas.SelectedItem;

            if (venda.StatusDaVenda != eStatusDaVenda.NOVA)
            {
                MessageBox.Show("Essa venda já foi efetivada");
                return;
            }

            if (venda.ItemDaVenda.Count == 0)
            {
                MessageBox.Show("Nenhum item a ser vendido nessa solicitação");
                return;
            }
            var itemDaVenda = ObtenhaListaDeItemDaVenda(venda);

            if (venda.StatusDaVenda == eStatusDaVenda.NOVA)//2. Atualizar o banco de dados informando que a venda foi realizada.
            {
                venda.StatusDaVenda = eStatusDaVenda.EFETIVADA;
                venda.DataDeEfetivacao = DateTime.Now;
                repositorioVenda.Atualizar(venda);
                CarregueElementosDoBancoDeDados();
                MessageBox.Show("Venda efetivada com sucesso");
            }
            else
            {
                MessageBox.Show("Essa venda já foi efetivada");
            }
        }

        private static List<ItemDaVenda> ObtenhaListaDeItemDaVenda(Venda venda)
        {
            var repositorioItemDaVenda = new RepositorioItemDaVenda();
            var itemDaVenda = repositorioItemDaVenda.Liste(venda.Codigo);
            return itemDaVenda;
        }
    }
}
