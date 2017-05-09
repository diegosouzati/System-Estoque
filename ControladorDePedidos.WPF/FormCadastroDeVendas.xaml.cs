using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeVendas : Window
    {
        RepositorioVenda repositorioVenda;
        RepositorioItemDaVenda repositorioItemDaVenda;
        RepositorioProduto repositorioProduto;

        public int Codigo { get; set; }
        public Venda Venda { get; set; }

        private void InicializeOperacoes()
        {
            repositorioVenda = new RepositorioVenda();
            repositorioItemDaVenda = new RepositorioItemDaVenda();
            repositorioProduto = new RepositorioProduto();
        }

        public FormCadastroDeVendas()
        {
            InitializeComponent();
            InicializeOperacoes();

            var venda = new Venda
            {
                DataDeCadastro = DateTime.Now,
                StatusDaVenda = eStatusDaVenda.NOVA
            };

            repositorioVenda.Adicionar(venda);
            Codigo = venda.Codigo;
            Venda = venda;

            if (Venda.Cliente != null)
            {
                txtCliente.Text = Venda.Cliente.Nome;
            }

            if (Venda.Vendedor != null)
            {
                txtVendedor.Text = Venda.Vendedor.Nome;
            }
            lstVendas.DataContext = venda.ItemDaVenda;
        }

        public FormCadastroDeVendas(Venda venda)
        {
            InitializeComponent();
            InicializeOperacoes();

            lstVendas.DataContext = venda.ItemDaVenda;
            Codigo = venda.Codigo;
            Venda = venda;

            if (Venda.Cliente != null)
            {
                txtCliente.Text = Venda.Cliente.Nome;
            }

            if (Venda.Vendedor != null)
            {
                txtVendedor.Text = Venda.Vendedor.Nome;
            }
            lstVendas.DataContext = venda.ItemDaVenda;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (Venda.StatusDaVenda != eStatusDaVenda.NOVA)
            {
                MessageBox.Show("Não é possível solicitar produtos para uma compra já efetivada");
                return;
            }

            var formulario = new FormBuscaDeProduto();
            formulario.ShowDialog();
            if (formulario.ProdutoSelecionado != null)
            {
                var itemDaVenda = new ItemDaVenda
                {
                    Venda = new Venda { Codigo = this.Codigo },
                    Produto = formulario.ProdutoSelecionado,
                    Quantidade = formulario.Quantidade,
                    Valor = formulario.ProdutoSelecionado.ValorDeVenda
                };

                repositorioItemDaVenda.Adicionar(itemDaVenda);
                lstVendas.DataContext = repositorioItemDaVenda.Liste(Codigo);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (Venda.StatusDaVenda != eStatusDaVenda.NOVA)
            {
                MessageBox.Show("Essa venda já foi efetivada!");
                return;
            }

            if (lstVendas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja remover um item da lista?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var itemDaVenda = (ItemDaVenda)lstVendas.SelectedItem;
                    repositorioItemDaVenda.Excluir(itemDaVenda);

                    try
                    {
                        lstVendas.DataContext = repositorioItemDaVenda.Liste(Codigo);
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show("Item removido com sucesso");
                    lstVendas.DataContext = repositorioItemDaVenda.Liste(Codigo);
                    }
                    //MessageBox.Show("Item removido com sucesso");
                    //lstVendas.DataContext = repositorioItemDaVenda.Liste(Codigo);
                }
                else
                {
                }
            }
        }

        private void btnBuscaDeCliente_Click(object sender, RoutedEventArgs e)
        {
            if (Venda.StatusDaVenda != eStatusDaVenda.NOVA)
            {
                MessageBox.Show("Não é possível alterar e ou adicionar um cliente numa venda já efetivada");
                return;
            }

            var buscaDeCliente = new FormBuscaDeCliente();
            buscaDeCliente.ShowDialog();
            Venda.Cliente = buscaDeCliente.ClienteSelecionado;

            if (Venda.Cliente != null)
            {
                txtCliente.Text = Venda.Cliente.Nome;
            }

            repositorioVenda.Atualizar(Venda);
        }

        private void btnBuscaDeVendedor_Click(object sender, RoutedEventArgs e)
        {
            if (Venda.StatusDaVenda != eStatusDaVenda.NOVA)
            {
                MessageBox.Show("Não é possivel alterar e ou adicionar um vendedor numa venda já efetivada");
                return;
            }

            var buscarVendedor = new FormBuscaDeVendedor();
            buscarVendedor.ShowDialog();
            Venda.Vendedor = buscarVendedor.VendedorSelecionado;
            if (Venda.Vendedor != null)
            {
                txtVendedor.Text = Venda.Vendedor.Nome;
            }
            repositorioVenda.Atualizar(Venda);
        }
    }
}
