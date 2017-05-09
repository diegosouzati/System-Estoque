using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;

namespace ControladorDePedidos.WPF
{

    public partial class FormCadastroDeFornecedores : Window
    {
        RepositorioFornecedor repositorioFornecedor;

        public FormCadastroDeFornecedores() //Método Construtor ou inicializador da Classe 01
        {
            repositorioFornecedor = new RepositorioFornecedor();
            InitializeComponent();
            this.DataContext = new Fornecedor();
        }

        public FormCadastroDeFornecedores(Fornecedor fornecedor)//Método Construtor ou inicializador da Classe 02
        {
            repositorioFornecedor = new RepositorioFornecedor();
            InitializeComponent();
            this.DataContext = fornecedor;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)//Botão Salvar
        {
            var fornecedor = (Fornecedor)this.DataContext;

            if (fornecedor.Codigo == 0)
            {
                repositorioFornecedor.Adicionar(fornecedor); // Adiciona um novo fornecedor no banco de dados
                MessageBox.Show("Fornecedor adicionada com sucesso!");

                var janelaFornecedores = new FormFornecedores();
                janelaFornecedores.Show();
            }
            else
            {
                repositorioFornecedor.Atualizar(fornecedor); // Atualiza um cadastro já existente no banco de dados
                MessageBox.Show("Fornecedor atualizada com sucesso!");

                var janelaFornecedores = new FormFornecedores();
                janelaFornecedores.Show();
            }
            this.Close(); //Fecha o formulário
        }
    }
}
