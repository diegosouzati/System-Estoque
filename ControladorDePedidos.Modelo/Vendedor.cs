using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Modelo
{
    public class Vendedor : ClasseBase
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Endereco { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string CEP { get; set; }

        public string Estado { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public int Comissao { get; set; }

        public string Apelido { get; set; }

        public List<Venda> Vendas { get; set; }

       // public List<CtAPagar> APagar { get; set; }
    }
}
