using System;
using System.Collections.Generic;
using System.Text;

namespace Zeit
{
    public class Produto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; } 
        public DateTime? validade { get; set; }
        public int id_fornecedor { get; set; }
        public int id_departamento { get; set; }
    }
}
