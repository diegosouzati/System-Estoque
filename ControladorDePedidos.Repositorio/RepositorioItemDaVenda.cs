using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioItemDaVenda : RepositorioGenerico<ItemDaVenda>
    { 
        public override void Adicionar(ItemDaVenda itemDaVenda)//Método Adicionar ItemDaVenda no Banco de Dados
        {
            var vendaOriginal = contexto.Set<Venda>().Find(itemDaVenda.Venda.Codigo);
            itemDaVenda.Venda = vendaOriginal;

            var produtoOriginal = contexto.Set<Produto>().Find(itemDaVenda.Produto.Codigo);
            itemDaVenda.Produto = produtoOriginal;

            contexto.Set<ItemDaVenda>().Add(itemDaVenda);
            contexto.SaveChanges();
        }  
        
        public List<ItemDaVenda> Liste(int CodigoDaVenda)
        {
            contexto = new Contexto();
            return contexto.Set<ItemDaVenda>().Where(x => x.Venda.Codigo == CodigoDaVenda).ToList(); 
        }
    }
}
