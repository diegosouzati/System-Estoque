using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeVendedores : Window
    {
        RepositorioVendedor repositorioVendedor;

        public FormCadastroDeVendedores()
        {
            repositorioVendedor = new RepositorioVendedor();
            InitializeComponent();
            this.DataContext = new Vendedor();
        }

        public FormCadastroDeVendedores(Vendedor vendedor)
        {
            repositorioVendedor = new RepositorioVendedor();
            InitializeComponent();
            this.DataContext = vendedor;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var vendedor = (Vendedor)this.DataContext;
            if (vendedor.Codigo == 0)
            {
                repositorioVendedor.Adicionar(vendedor);
                MessageBox.Show("Vendedor adicionado com sucesso");

                var janelavendedores = new FormVendedores();
                janelavendedores.Show();
            }
            else
            {
                repositorioVendedor.Atualizar(vendedor);

                var janelavendedores = new FormVendedores();
                janelavendedores.Show();
            }
            this.Close();
        }

        private void txtUF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var vendedor = (Vendedor)this.DataContext;
                if (vendedor.Codigo == 0)
                {
                    repositorioVendedor.Adicionar(vendedor);
                    MessageBox.Show("Vendedor adicionado com sucesso");
                }
                else
                {
                    repositorioVendedor.Atualizar(vendedor); 
                }
                this.Close();
            }
        }
    }
}
