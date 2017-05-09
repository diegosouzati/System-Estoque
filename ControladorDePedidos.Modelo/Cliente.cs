using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Modelo
{
    public class Cliente : ClasseBase
    {
        public string Nome { get; set; }

        public string DataDeNascimento { get; set; }

        public string CPF { get; set; }

        public string  RG { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public List<Venda> Vendas { get; set; }
    }
}
