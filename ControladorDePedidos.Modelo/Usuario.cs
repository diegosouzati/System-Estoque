using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Modelo
{
    public class Usuario : ClasseBase
    {
        public string Nome { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public bool Administrador { get; set; }

        public bool Clientes { get; set; }

        public bool Fornecedores { get; set; }        

        public bool Financeiro { get; set; }

        public bool Vendas { get; set; }

        public bool Vendedores { get; set; }

        public bool Produtos { get; set; }

        public bool Marcas { get; set; }

        public bool Compras { get; set; }  
        
        public bool Relatorios { get; set; }

        public bool Suporte { get; set; }
    }
}
