using ControladorDePedidos.Modelo;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class MainWindow : Window
    {
        public Usuario Usuario { get; set; }

        public MainWindow(Usuario Usuario)
        {
            InitializeComponent();
            this.Usuario = Usuario;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)//botão para sair do sistema
        {
            if (MessageBox.Show("Você deseja realmente fechar o sistema?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
            }
        }

        private void btnProdutos_Click(object sender, RoutedEventArgs e) //botão no menu para chamar o formulario de produtos
        {
            if (!Usuario.Administrador && !Usuario.Produtos)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }

            var JanelaProdutos = new FormProdutos();
            JanelaProdutos.Show();
        }

        private void btnMarcas_Click(object sender, RoutedEventArgs e) //botão no menu para chamar o formulario de marcas
        {
            if (!Usuario.Administrador && !Usuario.Produtos)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var JanelaMarcas = new FormMarcas();
            JanelaMarcas.Show();
        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e) //botão no menu para chamar o formulario de usuarios
        {
            if (!Usuario.Administrador)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var JanelaUsuarios = new FormUsuarios();
            JanelaUsuarios.Show();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Clientes)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelaClientes = new FormClientes();
            janelaClientes.Show();
        }

        private void btnCompras_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Compras)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelaCompra = new FormCompras();
            janelaCompra.Show();
        }

        private void btnVendas_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Vendas)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelaVendas = new FormVendas();
            janelaVendas.Show();
        }

        private void btnVendedores_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Vendedores)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelavendedores = new FormVendedores();
            janelavendedores.Show();
        }

        private void btnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Fornecedores)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelaFornecedores = new FormFornecedores();
            janelaFornecedores.Show();
        }

        private void btnSuporte_Click(object sender, RoutedEventArgs e)
        {
            if (!Usuario.Administrador && !Usuario.Suporte)
            {
                MessageBox.Show("Acesso Negado");
                return;
            }
            var janelaSuporte = new FormSuporte();
            janelaSuporte.Show();
        }
    }
}
