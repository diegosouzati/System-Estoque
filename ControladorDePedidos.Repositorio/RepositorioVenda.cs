using ControladorDePedidos.Modelo;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioVenda : RepositorioGenerico<Venda>
    {

      

        public override void Adicionar(Venda venda) //Método Adicionar Venda no Banco de Dados
        {
            contexto.Set<Venda>().Add(venda);

            if (venda.Cliente != null)
            {
                venda.Cliente = contexto.Set<Cliente>().Find(venda.Cliente.Codigo);
            }

            if (venda.Vendedor != null)
            {
                venda.Vendedor = contexto.Set<Vendedor>().Find(venda.Vendedor.Codigo);
            }

            contexto.SaveChanges();
        }

        public override void Atualizar(Venda venda) //método atualizar uma venda
        {
            var original = contexto.Set<Venda>().Find(venda.Codigo); //Faz a busca no Banco de Dados
            contexto.Entry(original).CurrentValues.SetValues(venda); // Atualiza os dados no banco, por intermedio do usuario.(Formulario)

            if (venda.Cliente != null)
            {
                original.Cliente = contexto.Set<Cliente>().Find(venda.Cliente.Codigo);
                contexto.Cliente.Attach(original.Cliente); //Attach, significa anexar
            }

            if (venda.Vendedor != null)
            {
                original.Vendedor = contexto.Set<Vendedor>().Find(venda.Vendedor.Codigo);
                contexto.Vendedor.Attach(original.Vendedor); //Attach, significa anexar
            }
            contexto.SaveChanges();// Salva as alterações feitas no metodo.
        }

        public override void Excluir(Venda item)
        {
            try
            {
                contexto.Set<ItemDaVenda>().RemoveRange(item.ItemDaVenda); // remove os dados do banco relacionado ao item da compra

                var original = contexto.Set<Venda>().Find(item.Codigo); //Faz a busca no banco de Dados
                contexto.Set<Venda>().Remove(original); // Faz a exclusão no banco de dados.
                contexto.SaveChanges(); // Salva as alterações feitas no metodo.
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Não é possivel excluir esse elemento, pois contém dados associados a ele!");
            }
        }

        public List<Venda> ListePorCliente(int codigoDoCliente)
        {
            contexto = new Contexto();
            return contexto.Set<Venda>().Where(x => x.Cliente.Codigo == codigoDoCliente).ToList();
        }
    }
}
