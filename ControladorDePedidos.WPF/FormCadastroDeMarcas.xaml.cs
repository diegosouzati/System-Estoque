using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;

namespace ControladorDePedidos.WPF
{

    public partial class FormCadastroDeMarcas : Window
    {
        public int Codigo { get; set; }
        
        public FormCadastroDeMarcas()//Método Construtor ou inicializador da Classe 01
        {
            InitializeComponent();
        }

        public FormCadastroDeMarcas(Marca marca)//Método Construtor ou inicializador da Classe 02
        {
            InitializeComponent();

            Codigo = marca.Codigo;
            txtNome.Text = marca.Nome;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)  //Botão Salvar
        {
            var codigo = Codigo;
            var nome = txtNome.Text;    

            RepositorioMarca repositorio = new RepositorioMarca();
            if (codigo == 0)//Condição do Botão Salvar para Novo e Editar
            {
                var marca = new Marca// Novo Cadastro de Marca
                {
                    Nome = nome,
                };                
                repositorio.Adicionar(marca);// Salvando no Banco de Dados
                MessageBox.Show("Marca adicionada com sucesso!");

                var JanelaMarcas = new FormMarcas();
                JanelaMarcas.Show();
            }
            else
            {               
                var marca = new Marca//Editando Cadastro de Marca
                {
                    Codigo = codigo,
                    Nome = nome,   
                };    
                repositorio.Atualizar(marca);// Atualizando no Banco de Dados
                MessageBox.Show("Marca atualizada com sucesso!");

                var JanelaMarcas = new FormMarcas();
                JanelaMarcas.Show();
            } 
            this.Close();
        }
    }
}
