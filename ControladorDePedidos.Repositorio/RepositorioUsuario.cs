using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<Usuario>
    {
        public bool ValideAcesso(int codigo, string senha)
        {
            contexto = new Contexto();
            var usuario = contexto.Set<Usuario>().FirstOrDefault(x => x.Codigo == codigo && x.Senha == senha);

            if(usuario != null)
            {
                return true;
            }
            return false;
        }
    }
}
