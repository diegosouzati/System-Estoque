using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioVendedor : RepositorioGenerico<Vendedor>
    {       
        public  List<Vendedor> Buscar(string TermoDaBusca) //Método buscar na Lista
        {
            var lista = contexto.Set<Vendedor>().Where(x => x.Nome.Contains(TermoDaBusca)).ToList();
            return lista;
        }
    }
}
