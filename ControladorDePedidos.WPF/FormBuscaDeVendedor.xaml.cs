using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{
    public partial class FormBuscaDeVendedor : Window
    {
        RepositorioVendedor repositorioVendedor;
        public Vendedor VendedorSelecionado { get; set; }

        public FormBuscaDeVendedor()
        {
            InitializeComponent();
            repositorioVendedor = new RepositorioVendedor();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)// inicializa a lista com os dados do banco
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados() // carrega os elementos do banco na tela
        {
            lstBuscaDeVendedor.DataContext = repositorioVendedor.Liste();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if(lstBuscaDeVendedor.SelectedItem == null)
            {
                MessageBox.Show("Selecione um vendedor");
                return;
            }           
            VendedorSelecionado = (Vendedor)lstBuscaDeVendedor.SelectedItem;
            this.Close();
        }

        private void txtTermoDaBusca_KeyUp(object sender, KeyEventArgs e) // código para definição de busca por tecla pressionada
        {
            var listaDeVendedor = repositorioVendedor.Buscar(txtTermoDaBusca.Text);
            lstBuscaDeVendedor.DataContext = listaDeVendedor;
        }
    }
}
