using System;
namespace Doc_Historico.Models
{
	public class Historico
	{
        public string id { get; set; }
        public DateTime data { get; set; }
        public string texto { get; set; }
        public string tipo { get; set; }
    }
}

