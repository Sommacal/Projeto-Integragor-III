using System;
using System.Collections.Generic;
using System.Text;

namespace Zeit
{
    public class Entrada
    {
        public int id { get; set; }
        public int quantidade { get; set; }
        public int id_produto { get; set; }
        public DateTime data { get; set; }
        public TimeSpan horario {get; set;}        
        public string cpf_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string nome_produto { get; set; }
    }
}
