using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{
     public partial class FormCadastroDeCompras : Window
    {
        RepositorioCompra repositorioCompra;
        RepositorioItemDaCompra repositorioItemDaCompra;
        RepositorioProduto repositorioProduto;

        public int Codigo { get; set; }
        public Compra Compra { get; set; }

        private void InicializeOperacoes()
        {
            repositorioCompra = new RepositorioCompra();
            repositorioItemDaCompra = new RepositorioItemDaCompra();
            repositorioProduto = new RepositorioProduto();
        }

        public FormCadastroDeCompras()
        {
            InitializeComponent();
            InicializeOperacoes();

            var compra = new Compra
            {
                DataDeCadastro = DateTime.Now,
                Status = eStatusDaCompra.NOVA
            };

            repositorioCompra.Adicionar(compra);
            Codigo = compra.Codigo;
            Compra = compra;
            lstCompras.DataContext = compra.ItemDaCompra;
        }

        public FormCadastroDeCompras(Compra compra)
        {
            InitializeComponent();
            InicializeOperacoes();

            lstCompras.DataContext = compra.ItemDaCompra;
            Codigo = compra.Codigo;
            Compra = compra;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (Compra.Status != eStatusDaCompra.NOVA)
            {
                MessageBox.Show("Não é possível solicitar produtos para uma compra já efetivada");
                return;
            }

            var formulario = new FormBuscaDeProduto();
            formulario.ShowDialog();

            if (formulario.ProdutoSelecionado != null)
            {
                var itemDaCompra = new ItemDaCompra
                {
                    Compra = new Compra { Codigo = this.Codigo },
                    Produto = formulario.ProdutoSelecionado,
                    Quantidade = formulario.Quantidade,
                    Valor = formulario.ProdutoSelecionado.ValorDeCompra
                };

                repositorioItemDaCompra.Adicionar(itemDaCompra);
                lstCompras.DataContext = repositorioItemDaCompra.Liste(Codigo);
            }
        }

        private void btnObterRecomendacao_Click(object sender, RoutedEventArgs e)
        {
            if(Compra.Status != eStatusDaCompra.NOVA)
            {
                MessageBox.Show("Não é possível solicitar produtos para uma compra já efetivada");
                return;
            }

            var listaEstoqueBaixo = repositorioProduto.ObtenhaRecomendacao();

            foreach (var produto in listaEstoqueBaixo)
            {
                var itemDaCompra = new ItemDaCompra
                {
                    Compra = new Compra { Codigo = this.Codigo },
                    Produto = produto,
                    Quantidade = produto.QuantidadeDesejavelEmEstoque - produto.QuantidadeEmEstoque,
                    Valor = produto.ValorDeCompra
                };
                repositorioItemDaCompra.Adicionar(itemDaCompra);
            }
            lstCompras.DataContext = repositorioItemDaCompra.Liste(Codigo);
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
                if (MessageBox.Show("Deseja excluir o produto?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var itemDaCompra = (ItemDaCompra)lstCompras.SelectedItem;
                    repositorioItemDaCompra.Excluir(itemDaCompra);

                    try
                    {
                        lstCompras.DataContext = repositorioItemDaCompra.Liste(Codigo);
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show("Produto excluído com sucesso");
                    lstCompras.DataContext = repositorioItemDaCompra.Liste(Codigo);
                    }
                    //MessageBox.Show("Produto excluído com sucesso");
                    //lstCompras.DataContext = repositorioItemDaCompra.Liste(Codigo);
                }
                else
                {
                }
            }
        }
    }
}
