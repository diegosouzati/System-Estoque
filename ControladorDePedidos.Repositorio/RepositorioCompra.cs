using ControladorDePedidos.Modelo;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioCompra : RepositorioGenerico<Compra>
    {
        public override void Excluir(Compra item)
        {
            try
            {
                contexto.Set<ItemDaCompra>().RemoveRange(item.ItemDaCompra); // remove os dados do banco relacionado ao item da compra

                var original = contexto.Set<Compra>().Find(item.Codigo); //Faz a busca no banco de Dados
                contexto.Set<Compra>().Remove(original); // Faz a exclusão no banco de dados.
                contexto.SaveChanges(); // Salva as alterações feitas no metodo.
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Não é possivel excluir esse elemento, pois contém dados associados a ele!");
            }
        }
    }
}
