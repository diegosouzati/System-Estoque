using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioGenerico<T> where T : ClasseBase
    {
        protected Contexto contexto; //Variavel global para todos os métodos

        public RepositorioGenerico()
        {
            contexto = new Contexto(); //inicializa a instancia 
        }

        public virtual void Adicionar(T item) //Método Adicionar 
        {
            contexto = new Contexto();
            contexto.Set<T>().Add(item);
            contexto.SaveChanges();
        }

        public virtual void Atualizar(T item) //método atualizar 
        {
            contexto = new Contexto();
            var original = contexto.Set<T>().Find(item.Codigo); //Faz a busca no Banco de Dados
            contexto.Entry(original).CurrentValues.SetValues(item); // Atualiza os dados no banco, por intermedio do item.(Formulario)
            contexto.SaveChanges(); // Salva as alterações feitas no metodo.
        }

        public virtual List<T> Liste()  //Método mostrar na Lista
        {
            contexto = new Contexto();
            return contexto.Set<T>().ToList();  //contexto = new Contexto();
        }

        public virtual void Excluir(T item) //Método Excluir 
        {
            try
            {
                var original = contexto.Set<T>().Find(item.Codigo); //Faz a busca no banco de Dados
                contexto.Set<T>().Remove(original); // Faz a exclusão no banco de dados.
                contexto.SaveChanges(); // Salva as alterações feitas no metodo.
            }
            catch(DbUpdateException ex)
            {
                MessageBox.Show("Não é possivel excluir esse elemento, pois contém dados associados a ele!");
            }
        }
    }
}
