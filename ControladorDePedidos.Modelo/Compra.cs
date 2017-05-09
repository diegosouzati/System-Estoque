using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Modelo
{
    public class Compra : ClasseBase
    {
        public DateTime DataDeCadastro { get; set; }

        public DateTime DataDeEfetivacao { get; set; }

        public DateTime DataDeRecebimento { get; set; }

        public virtual List<ItemDaCompra> ItemDaCompra { get; set; }

        public eStatusDaCompra Status { get; set; }

        [NotMapped]
        public int QuantidadeDeProdutos
        {
            get
            {
                var quantidade = 0;
                foreach (var item in ItemDaCompra)
                {
                    quantidade = quantidade + item.Quantidade;
                }
                return quantidade;
            }
        }

        [NotMapped]
        public decimal ValorTotal
        {
            get
            {
                decimal valor = 0;
                foreach (var item in ItemDaCompra)
                {
                    valor = valor + item.ValorTotal;
                }
                return valor;
            }
        }
    }
}
