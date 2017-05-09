using ControladorDePedidos.Modelo;
using ControladorDePedidos.Repositorio;
using System.Windows;

namespace ControladorDePedidos.WPF
{
    public partial class FormCadastroDeProdutos : Window
    {
        RepositorioMarca repositorioMarca;// instanciando variavel global do repositorio de marcas
        RepositorioFornecedor repositorioFornecedor; // instanciando variavel global do repositorio de fornecedor
        RepositorioProduto repositorioProduto;// instanciando variavel global do repositorio de produtos

        public FormCadastroDeProdutos()//instancia um novo produto e salva no banco de dados
        {
            repositorioMarca = new RepositorioMarca();
            repositorioFornecedor = new RepositorioFornecedor();
            repositorioProduto = new RepositorioProduto();

            InitializeComponent();
            this.DataContext = new Produto();
        }
       
        public FormCadastroDeProdutos(Produto produto)  //Metodo para edicao de um produto, vai instanciar o produto já existente no banco de dados
        {
            repositorioMarca = new RepositorioMarca();
            repositorioFornecedor = new RepositorioFornecedor();
            repositorioProduto = new RepositorioProduto();

            InitializeComponent();
            this.DataContext = produto;

            cmbMarcas.SelectedValue = produto.Marca.Codigo; //mostra a marca no combobox na hora da edicao
            cmbFornecedor.SelectedValue = produto.Fornecedor.Codigo; //mostra o fornecedor no combobox na hora da edicao
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)  //chama pra tela os dados do banco
        {
            var marcas = repositorioMarca.Liste();
            cmbMarcas.DataContext = marcas;

            var fornecedores = repositorioFornecedor.Liste();
            cmbFornecedor.DataContext = fornecedores;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var produto = (Produto)this.DataContext;
            if (cmbMarcas.SelectedItem == null)// condição de marca
            {               
                MessageBox.Show("Selecione uma marca");  //manda selecionar uma marca para o produto
            }
            else
            {               
                produto.Marca = (Marca)cmbMarcas.SelectedItem; //usa a marca selecionada para cadastrar o produto
            }

            if (cmbFornecedor.SelectedItem == null) // condição de fornecedor
            {
                MessageBox.Show("Selecione um fornecedor");  //manda selecionar um fornecedor para o produto
            }
            else
            {
                produto.Fornecedor = (Fornecedor)cmbFornecedor.SelectedItem; //usa o fornecedor selecionado para cadastrar o produto
            }

            if (produto.Codigo == 0)//condição adicionar e atualizar produto no banco de dados
            {                
                repositorioProduto.Adicionar(produto); //cria um novo produto no banco de dados
                MessageBox.Show("Produco cadastrado com sucesso");

                var JanelaProdutos = new FormProdutos();
                JanelaProdutos.Show();
            }
            else
            {                
                repositorioProduto.Atualizar(produto); //atualiza um produto já cadastrado
                MessageBox.Show("Produco atualizado com sucesso");

                var JanelaProdutos = new FormProdutos();
                JanelaProdutos.Show();
            }
            this.Close();
        }
    }
}
