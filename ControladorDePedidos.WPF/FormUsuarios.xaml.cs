using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{

    public partial class FormUsuarios : Window
    {
        RepositorioUsuario repositorioUsuario;

        public FormUsuarios()
        {
            repositorioUsuario = new RepositorioUsuario();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados()
        {
            lstUsuarios.DataContext = repositorioUsuario.Liste();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var cadastroUsuarios = new FormCadastroDeUsuarios();
            cadastroUsuarios.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Selecione um usuário");
                return;
            }
            var usuario = (Usuario)lstUsuarios.SelectedItem;

            this.Hide();
            var cadastroUsuarios = new FormCadastroDeUsuarios(usuario);
            cadastroUsuarios.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("lista atualizada com sucesso");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Selecione um usuário");
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja remover um usuário?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var usuario = (Usuario)lstUsuarios.SelectedItem;
                    repositorioUsuario.Excluir(usuario);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Usuário removido com sucesso");
                    }                   
                }
                else
                {
                }
            }
        }
    }
}
