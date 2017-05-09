using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loginUsuario = new RepositorioUsuario();
            var listaUsuario = loginUsuario.Liste();
            if (listaUsuario.Count == 0)
            {
                var usuario = new Usuario
                {
                    Administrador = true
                };
                this.Hide();
                var formPrincipal = new MainWindow(usuario);
                formPrincipal.ShowDialog();
                this.Close();
            }
            cmbUsuario.DataContext = listaUsuario;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsuario.SelectedItem == null)
            {
                MessageBox.Show("Selecione um Usuário!");
                return;
            }

            var senha = txtPassword.Password;
            var usuario = (Usuario)cmbUsuario.SelectedItem;

            var usuarioLogado = new RepositorioUsuario();

            if (usuarioLogado.ValideAcesso(usuario.Codigo, senha))
            {
                var listaUsuario = (List<Usuario>)cmbUsuario.DataContext;
                var quantidade = listaUsuario.Where(x => x.Administrador).Count();
                if (quantidade == 0)
                {
                    MessageBox.Show("Não existe administrador cadastrado no sistema, logo o usuário logado terá acesso de administrador");
                    usuario.Administrador = true;
                }
                this.Hide();
                var formPrincipal = new MainWindow(usuario);
                formPrincipal.ShowDialog();
                this.Close();

        }
            else
            {
                MessageBox.Show("Os dados informados estão incorretos");
            }
}

private void btnSair_Click(object sender, RoutedEventArgs e)
{
    this.Close();
}
    }
}
