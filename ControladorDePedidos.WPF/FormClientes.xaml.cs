using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormClientes : Window
    {
        RepositorioCliente repositorioCliente; // variavel global usando o repositorio de cliente

        public FormClientes()
        {
            repositorioCliente = new RepositorioCliente(); //inicializando o repositorio
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // carregando os arquivos do banco de dados
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados() // listando os arquivos do banco de dados
        {
            lstClientes.DataContext = repositorioCliente.Liste();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e) //botão novo cadastro
        {
            this.Hide();
            var janelaCliente = new FormCadastroDeClientes();
            janelaCliente.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)// botao editar
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente");
                return;
            }
            var cliente = (Cliente)lstClientes.SelectedItem;

            this.Hide();
            var janelaCliente = new FormCadastroDeClientes(cliente);
            janelaCliente.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e) // botao atualizar
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) // botao excluir
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente");
            }
            else
            {
                if (MessageBox.Show("Deseja remover um cliente?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var cliente = (Cliente)lstClientes.SelectedItem;
                    repositorioCliente.Excluir(cliente);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Cliente removido com sucesso");
                    }
                }
                else
                {
                }
            }
        }

        private void btnPedidosPorCliente_Click(object sender, RoutedEventArgs e)
        {
            if(lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            else
            {
                var clienteSelecionado = (Cliente)lstClientes.SelectedItem;

                var formVendas = new FormVendas(clienteSelecionado);
                formVendas.ShowDialog();
            }
        }
    }
}
