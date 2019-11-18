using System;

namespace Zeit
{
    public class Retirada
    {    
            public int id { get; set; }
            public int quantidade { get; set; }
            public int id_produto { get; set; }
            public DateTime data { get; set; }
            public TimeSpan horario { get; set; }
            public string cpf_usuario { get; set; }
      
    }
}

