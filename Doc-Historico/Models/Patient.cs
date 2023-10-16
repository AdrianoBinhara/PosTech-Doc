using System;
namespace Doc_Historico.Models
{
	public class Patient
	{
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime dataNascimento { get; set; }
        public string email { get; set; }
        public string responsavel { get; set; }
        public List<Historico> historico { get; set; }
    }
}

