using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;
using System.Windows.Input;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeClientes : Window
    {
        RepositorioCliente repositorioCliente;

        public FormCadastroDeClientes()
        {
            repositorioCliente = new RepositorioCliente();
            InitializeComponent();
            this.DataContext = new Cliente();
        }

        public FormCadastroDeClientes(Cliente cliente)
        {
            repositorioCliente = new RepositorioCliente();
            InitializeComponent();
            this.DataContext = cliente;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (Cliente)this.DataContext;

            if (cliente.Codigo == 0)
            {
                repositorioCliente.Adicionar(cliente);
                MessageBox.Show("Cliente adicionado com sucesso");

                var janelaClientes = new FormClientes();
                janelaClientes.Show();
            }
            else
            {

                repositorioCliente.Atualizar(cliente);
                MessageBox.Show("Cliente Atualizado com Sucesso");

                var janelaClientes = new FormClientes();
                janelaClientes.Show();
            }
            this.Close();
        }

        private void txtUF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var cliente = (Cliente)this.DataContext;

                if (cliente.Codigo == 0)
                {
                    repositorioCliente.Adicionar(cliente);
                    MessageBox.Show("Cliente adicionado com sucesso");
                }
                else
                {
                    repositorioCliente.Atualizar(cliente);
                }
                this.Close();
            }
        }
    }
}
