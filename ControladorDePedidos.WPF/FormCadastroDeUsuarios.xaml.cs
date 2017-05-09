using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeUsuarios : Window
    {
        RepositorioUsuario repositorioUsuario;

        public FormCadastroDeUsuarios() // metodo inicializador da classe
        {
            repositorioUsuario = new RepositorioUsuario();
            InitializeComponent();
            this.DataContext = new Usuario();
        }

        public FormCadastroDeUsuarios(Usuario usuario)
        {
            repositorioUsuario = new RepositorioUsuario();
            InitializeComponent();
            this.DataContext = usuario;
        }

        private void btnSalvar_Click_1(object sender, RoutedEventArgs e)
        {
            var usuario = (Usuario)this.DataContext;
            if (usuario.Codigo == 0)
            {
                if (string.IsNullOrEmpty(txtSenha.Password)) //se os campos das senhas estiverem vazios, faz essa chamada
                {
                    MessageBox.Show("As senhas devem ser informadas!");
                    return;
                }
            }

            if(txtSenha.Password != txtConfirmeSenha.Password)//se as senhas forem diferentes, faz essa chamada.
            {
                MessageBox.Show("Senhas informadas não conferem");
                return;
            }

            if (usuario.Codigo == 0 || !string.IsNullOrEmpty(txtSenha.Password))
            {
                usuario.Senha = txtSenha.Password;
            }

            usuario.Email = txtEmail.Text;
      //----------------------------------------------------------------------------------------

            if (usuario.Codigo == 0)
            {
                repositorioUsuario.Adicionar(usuario); //buscando o metodo adicionar no repositorio de usuario
                MessageBox.Show("Usuário cadastrado com sucesso");

                var JanelaUsuarios = new FormUsuarios();
                JanelaUsuarios.Show();
            }
            else
            {
                repositorioUsuario.Atualizar(usuario); //buscando o metodo atualizar no repositorio de usuario
                MessageBox.Show("Usuário atualizado com sucesso");

                var JanelaUsuarios = new FormUsuarios();
                JanelaUsuarios.Show();
            }
            this.Close(); // fechando o formulario(tela de cadastro)
        }       
    }
}
