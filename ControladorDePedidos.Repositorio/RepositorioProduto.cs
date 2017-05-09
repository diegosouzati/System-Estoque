using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioProduto : RepositorioGenerico<Produto>
    {

        public override void Adicionar(Produto produto) //Método Adicionar Produto no Banco de Dados
        {
            var marcaOriginal = contexto.Set<Marca>().Find(produto.Marca.Codigo);
            produto.Marca = marcaOriginal;

            var fornecedorOriginal = contexto.Set<Fornecedor>().Find(produto.Fornecedor.Codigo);
            produto.Fornecedor = fornecedorOriginal;

            contexto.Set<Produto>().Add(produto);
            contexto.SaveChanges();
        }

        public override void Atualizar(Produto produto) // Método atualizar
        {
            var produtoOriginal = contexto.Set<Produto>().Find(produto.Codigo);
            var marcaOriginal = contexto.Set<Marca>().Find(produto.Marca.Codigo);
            var fornecedorOriginal = contexto.Set<Fornecedor>().Find(produto.Fornecedor.Codigo);

            contexto.Entry(produtoOriginal).CurrentValues.SetValues(produto);

            produtoOriginal.Marca = marcaOriginal;
            produtoOriginal.Fornecedor = fornecedorOriginal;

            contexto.SaveChanges();
        }


        public List<Produto> Buscar(string TermoDaBusca) //Método buscar na Lista
        {
            contexto = new Contexto();
            var lista = contexto.Set<Produto>().Where(x => x.Nome.Contains(TermoDaBusca)).ToList();
            return lista;

        }

        public Produto Consultar(int codigo) //Método consultar produto na Lista
        {
            contexto = new Contexto();
           return contexto.Set<Produto>().FirstOrDefault(x => x.Codigo == codigo);   
        }

        public  List<Produto> ObtenhaRecomendacao() // Obtem produtos que estejam com estoque baixo
        {
            contexto = new Contexto();
            var lista = contexto.Set<Produto>().Where(x => x.QuantidadeEmEstoque < x.QuantidadeDesejavelEmEstoque).ToList();
            return lista;
        }
    }
}
