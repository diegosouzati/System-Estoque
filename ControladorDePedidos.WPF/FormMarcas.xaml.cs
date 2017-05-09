using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormMarcas : Window
    {
        RepositorioMarca repositorio;

        public FormMarcas()
        {
            repositorio = new RepositorioMarca();
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)//Metodo de load da lista de marcas
        {
            CarregueElementosDoBancoDeDados();
        }
       
        private void CarregueElementosDoBancoDeDados()//Metodo para carregar a lista do banco de dados
        {
            lstMarcas.DataContext = repositorio.Liste();
        }
        
        private void btnNovo_Click(object sender, RoutedEventArgs e)//Botão Novo
        {
            this.Hide();
            FormCadastroDeMarcas cadMarcas = new FormCadastroDeMarcas();
            cadMarcas.ShowDialog();
            CarregueElementosDoBancoDeDados();
        }
        
        private void btnEditar_Click(object sender, RoutedEventArgs e)//Botão Editar
        {
            if (lstMarcas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
            }
            else
            {
                var itemSelecionado = (Marca)lstMarcas.SelectedItem;

                this.Hide();
                FormCadastroDeMarcas cadMarcas = new FormCadastroDeMarcas(itemSelecionado);
                cadMarcas.ShowDialog();

                CarregueElementosDoBancoDeDados();
            }
            MessageBox.Show("Cadastro atualizado com sucesso");
        }
       
        private void btnAtualizar_Click(object sender, RoutedEventArgs e) //Método Atualizar
        {
            CarregueElementosDoBancoDeDados();
            MessageBox.Show("Lista atualizada com sucesso.");
        }
       
        private void btnExcluir_Click(object sender, RoutedEventArgs e)//Método Excluir
        {
            if (lstMarcas.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
            }
            else
            {
                if (MessageBox.Show("Você deseja remover esta marca?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var itemSelecionado = (Marca)lstMarcas.SelectedItem;
                    repositorio.Excluir(itemSelecionado);

                    try
                    {
                        CarregueElementosDoBancoDeDados();
                    }
                    catch (Exception ex)
                    {
                        CarregueElementosDoBancoDeDados();
                        MessageBox.Show("Marca removida com sucesso");
                    }
                }
                else
                {
                }
            }

        }
    }
}