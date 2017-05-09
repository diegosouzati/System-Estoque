using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{

    public partial class FormProdutos : Window
    {
        RepositorioProduto repositorioProduto; //inicializando uma variavel global

        public FormProdutos()
        {
            repositorioProduto = new RepositorioProduto();  //inicializando o repositorio de produtos
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) //abrindo a janela com os dados ja na lista
        {
            CarregueElementosDoBancoDeDados();   //Faz a chamada do método do carregamento da lista de produtos
        }

        private void btnMarcas_Click(object sender, RoutedEventArgs e) //botão cadastro de marcas dentro do formulario produto
        {
            this.Hide();
            FormMarcas frmMarca = new FormMarcas();
            frmMarca.ShowDialog();
        }

        private void CarregueElementosDoBancoDeDados() //método para carregar os arquivos do banco de dados na tela
        {
            lstProdutos.DataContext = repositorioProduto.Liste();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)  //botao de novo cadastro
        {
            this.Hide();
            FormCadastroDeProdutos frmProduto = new FormCadastroDeProdutos();
            frmProduto.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e) //botao editar cadastro
        {
            if (lstProdutos.SelectedItem == null)
            {
                MessageBox.Show("Selecione um produto");
                return;
            }
            var produto = (Produto)lstProdutos.SelectedItem;

            this.Hide();
            FormCadastroDeProdutos frmProduto = new FormCadastroDeProdutos(produto);
            frmProduto.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)  //botao atualizar lista de produtos
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)  //botao excluir produto da lista
        {
            if (lstProdutos.SelectedItem == null)
            {
                MessageBox.Show("Selecione um produto");
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja remover um produto?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var produto = (Produto)lstProdutos.SelectedItem;
                    repositorioProduto.Excluir(produto);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Produto removido com sucesso");
                    }
                }
                else
                {
                }
            }
        }
    }
}
