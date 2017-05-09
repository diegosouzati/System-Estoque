using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{

    public partial class FormBuscaDeCliente : Window
    {
        RepositorioCliente repositorioCliente;

        public Cliente ClienteSelecionado { get; set; }
        public int Quantidade { get; set; }

        public FormBuscaDeCliente()
        {
            InitializeComponent();
            repositorioCliente = new RepositorioCliente();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)// inicializa a lista com os dados do banco
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados() // carrega os elementos do banco na tela
        {
            lstBuscaDeCliente.DataContext = repositorioCliente.Liste();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if(lstBuscaDeCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            ClienteSelecionado = (Cliente)lstBuscaDeCliente.SelectedItem;
            this.Close();
        }

        private void txtTermoDaBusca_KeyUp(object sender, KeyEventArgs e) // código para definição de busca por tecla pressionada
        {
            var listaDeClientes = repositorioCliente.Buscar(txtTermoDaBusca.Text);
            lstBuscaDeCliente.DataContext = listaDeClientes;
        }
    }
}
