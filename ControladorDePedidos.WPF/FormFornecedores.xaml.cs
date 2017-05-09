using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormFornecedores : Window
    {
        RepositorioFornecedor repositorioFornecedor;

        public FormFornecedores()
        {
            repositorioFornecedor = new RepositorioFornecedor();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // traz o formulário carregado com os dados na lista
        {
            CarregueElementosDoBancoDeDados();
        }
       
        private void CarregueElementosDoBancoDeDados() // carrega a lista de dados do banco
        {
            lstFornecedores.DataContext = repositorioFornecedor.Liste();
        }
      
        private void btnNovo_Click(object sender, RoutedEventArgs e) // Cria um novo cadastro no banco de dados
        {
            this.Hide();
            FormCadastroDeFornecedores cadFornecedores = new FormCadastroDeFornecedores();
            cadFornecedores.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e) //  Edita um cadastro no banco de dados
        {
            if (lstFornecedores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
            }
            else
            {
                var fornecedor = (Fornecedor)lstFornecedores.SelectedItem;

                this.Hide();
                FormCadastroDeFornecedores cadFornecedores = new FormCadastroDeFornecedores(fornecedor);
                cadFornecedores.ShowDialog();

                CarregueElementosDoBancoDeDados();
            }
            MessageBox.Show("Cadastro atualizado com sucesso");
        }
       
        private void btnAtualizar_Click(object sender, RoutedEventArgs e)// Atualiza a lista apresentada na tela, de acordo com os dados do banco de dados
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso.");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)// Exclui um dado do banco
        {
            if (lstFornecedores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um fornecedor");
            }
            else
            {
                if (MessageBox.Show("Você deseja remover esta fornecedor?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var fornecedor = (Fornecedor)lstFornecedores.SelectedItem;
                    repositorioFornecedor.Excluir(fornecedor);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Fornecedor removido com sucesso");
                    }
                }
                else
                {
                }
            }   
        }
    }
}
