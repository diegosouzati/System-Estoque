using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{

    public partial class FormBuscaDeProduto : Window
    {
        RepositorioProduto repositorioProduto;

        public Produto ProdutoSelecionado { get; set; }
        public int Quantidade { get; set; }

        public FormBuscaDeProduto()
        {
            InitializeComponent();
            repositorioProduto = new RepositorioProduto();
        }

        private void CarregueElementosDoBancoDeDados() // carrega os elementos do banco na tela
        {
            lstBuscaDeProduto.DataContext = repositorioProduto.Liste();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if(lstBuscaDeProduto.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            
            if(txtQuantidade.Text == "")
            {
                MessageBox.Show("Informe uma quantidade");
                return;
            }

            int quantidade;

            if(int.TryParse(txtQuantidade.Text, out quantidade))
            {
                Quantidade = quantidade;
            }
            else
            {
                MessageBox.Show("Informe um valor númerico no campo quantidade");
            }
            ProdutoSelecionado = (Produto)lstBuscaDeProduto.SelectedItem;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)// inicializa a lista com os dados do banco
        {
            CarregueElementosDoBancoDeDados();
        }

        private void txtTermoDaBusca_KeyUp(object sender, KeyEventArgs e) // código para definição de busca por tecla pressionada
        {
            var listaDeProdutos = repositorioProduto.Buscar(txtTermoDaBusca.Text);
            lstBuscaDeProduto.DataContext = listaDeProdutos;
        }
    }
}
