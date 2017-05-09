using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static ControladorDePedidos.WPF.Utilitarios;

namespace ControladorDePedidos.WPF
{
    public partial class FormCompras : Window
    {
        RepositorioCompra repositorioCompra;
        public FormCompras()
        {
            repositorioCompra = new RepositorioCompra();
            InitializeComponent();
        }

        public FormCompras(Compra compra)
        {
            repositorioCompra = new RepositorioCompra();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados()
        {
            lstCompras.DataContext = repositorioCompra.Liste();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var janelaCompra = new FormCadastroDeCompras();
            janelaCompra.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            var compra = (Compra)lstCompras.SelectedItem;

            this.Hide();
            var janelaCompra = new FormCadastroDeCompras(compra);
            janelaCompra.ShowDialog();

            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lstCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja remover essa compra?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var compra = (Compra)lstCompras.SelectedItem;
                    repositorioCompra.Excluir(compra);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Compra removida com sucesso");
                    }     
                }
                else
                {
                }
            }
        }

        private void btnCompraEfetuada_Click(object sender, RoutedEventArgs e)
        {
            // 1. Listar itens da compra para enviar ao fornecedor.
            if (lstCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item");
                return;
            }
            var compra = (Compra)lstCompras.SelectedItem;

            if(compra.Status != eStatusDaCompra.NOVA)
            {
                MessageBox.Show("Compra já efetivada!");
                return;
            }

            if (compra.ItemDaCompra.Count == 0)
            {
                MessageBox.Show("Nenhum item a ser comprado para a seleção de compra");
                return;
            }
            var itemDaCompra = ObtenhaListaDeItemDaCompra(compra);

            var listaAgrupada = itemDaCompra.GroupBy(x => x.Produto.Fornecedor).ToList();
            foreach (var item in listaAgrupada)
            {
                var fornecedor = item.Key;
                var itens = item.ToList();
                string listaString = "";

                foreach(var itensDaCompra in itens)
                {
                    listaString += $"{itensDaCompra.Quantidade} - {itensDaCompra.Produto.Nome} - {itensDaCompra.Produto.Marca.Nome} <br>";

                }
                EnviarEmail(fornecedor.Email, "Solicitação de Compras", "Por favor, fazer a solicitação dos produtos listados para compra" + "<br>" + listaString); //2. E-mail ao fornecedor com a lista de compras.
            }

            //3. Atualizar o banco de dados informando que a compra foi realizada.          
            if(compra.Status == eStatusDaCompra.NOVA)
            {
                compra.Status = eStatusDaCompra.EFETIVADA;
                compra.DataDeEfetivacao = DateTime.Now;
                repositorioCompra.Atualizar(compra);
                CarregueElementosDoBancoDeDados();
                MessageBox.Show("Compra efetivada com sucesso");               
            }
            else
            {
                MessageBox.Show("Essa compra já foi efetivada");
                return;
            }
        }

        private static List<ItemDaCompra> ObtenhaListaDeItemDaCompra(Compra compra)
        {
            var repositorioItemDaCompra = new RepositorioItemDaCompra();
            var itemDaCompra = repositorioItemDaCompra.Liste(compra.Codigo);
            return itemDaCompra;
        }

        private void btnCompraRecebida_Click(object sender, RoutedEventArgs e)
        {
            if (lstCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item");
                return;
            }
            var compra = (Compra)lstCompras.SelectedItem;

            var itemDaCompra = ObtenhaListaDeItemDaCompra(compra);// Adicionar no estoque
            var repositorioProduto = new RepositorioProduto();

            foreach (var item in itemDaCompra)
            {
                var produtoDaCompra = item.Produto;
                var produtoDoBanco = repositorioProduto.Consultar(produtoDaCompra.Codigo);

                produtoDoBanco.QuantidadeEmEstoque += item.Quantidade;
                repositorioProduto.Atualizar(produtoDoBanco);
                CarregueElementosDoBancoDeDados();
            }
 
            if (compra.Status == eStatusDaCompra.EFETIVADA) // Atualizar o Banco de Dados
            {
                compra.Status = eStatusDaCompra.RECEBIDA;
                compra.DataDeRecebimento = DateTime.Now;
                repositorioCompra.Atualizar(compra);
                CarregueElementosDoBancoDeDados();
                MessageBox.Show("Compra recebida com sucesso");
                return;
            }
            else
            {
                MessageBox.Show("Essa compra já foi recebida");
                return;
            }
        }
    }
}
