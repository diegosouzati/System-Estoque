using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioCliente : RepositorioGenerico<Cliente>
    {
        public List<Cliente> Buscar(string TermoDaBusca) //Método buscar na Lista
        {
            contexto = new Contexto();
            var lista = contexto.Set<Cliente>().Where(x => x.Nome.Contains(TermoDaBusca)).ToList();
            return lista;
        }
    }
}
