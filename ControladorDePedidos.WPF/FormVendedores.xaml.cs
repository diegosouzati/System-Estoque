using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{

    public partial class FormVendedores : Window
    {
        RepositorioVendedor repositorioVendedor; // variavel global usando o repositorio de vendedor

        public FormVendedores()
        {
            repositorioVendedor = new RepositorioVendedor(); //inicializando o repositorio
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // carregando os arquivos do banco de dados
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados() // listando os arquivos do banco de dados
        {
            lstVendedores.DataContext = repositorioVendedor.Liste();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e) //botão novo cadastro
        {
            this.Hide();
            var janelaVendedor = new FormCadastroDeVendedores();
            janelaVendedor.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)// botao editar
        {
            if (lstVendedores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um vendedor");
                return;
            }
            var vendedor = (Vendedor)lstVendedores.SelectedItem;

            this.Hide();
            var janelaVendedor = new FormCadastroDeVendedores(vendedor);
            janelaVendedor.ShowDialog();

            MessageBox.Show("Vendedor atualizado com sucesso");
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e) // botao atualizar
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) // botao excluir
        {
            if (lstVendedores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um vendedor");
            }
            else
            {
                if (MessageBox.Show("Deseja remover um vendedor?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var vendedor = (Vendedor)lstVendedores.SelectedItem;
                    repositorioVendedor.Excluir(vendedor);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Vendedor removido com sucesso");
                    }
                }
                else
                {
                }
            }
        }

        private void txtTermoDaBusca_KeyDown(object sender, KeyEventArgs e)
        {
            var listarVendedor = repositorioVendedor.Buscar(txtTermoDaBusca.Text);
            lstVendedores.DataContext = listarVendedor;
        }
    }
}
