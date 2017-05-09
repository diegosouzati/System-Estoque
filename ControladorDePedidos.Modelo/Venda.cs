using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControladorDePedidos.Modelo
{
    public class Venda : ClasseBase
    {
        public virtual Cliente Cliente { get; set; }

        public virtual Vendedor Vendedor { get; set; }

        public DateTime DataDeCadastro { get; set; }

        public DateTime DataDeEfetivacao { get; set; }

        public virtual List<ItemDaVenda> ItemDaVenda { get; set; }

        public eStatusDaVenda StatusDaVenda { get; set; }

        [NotMapped]
        public int QuantidadeDeProdutos
        {
            get
            {
                var quantidade = 0;
                foreach (var item in ItemDaVenda)
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
                foreach (var item in ItemDaVenda)
                {
                    valor = valor + item.ValorTotal;
                }
                return valor;
            }
        }
    }
}
