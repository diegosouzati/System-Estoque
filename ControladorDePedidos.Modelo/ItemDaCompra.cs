using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Modelo
{
    public class ItemDaCompra : ClasseBase
    {
        public virtual Produto Produto { get; set; } 

        public virtual Compra Compra { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        [NotMapped]
        public decimal ValorTotal
        {
            get
            {
                return Quantidade * Valor;
            }
        }

        public int QuantidadeFinalEmEstoque
        {
            get
            {
                return Produto.QuantidadeEmEstoque + Quantidade;
            }
        }
    }
}
