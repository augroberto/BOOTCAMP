using System;
using System.Collections.Generic;

namespace gamecatalog.Models
{
	public class Game
	{
		public int id { get; set; }
		public string nome { get; set; }
		public string descricao { get; set; }
		public DateTime datacompra { get; set; }
		public bool finalizado { get; set; }
		public DateTime datafinalizacao { get; set; }
		public string thumb { get; set; }
		public string URLthumb { get; set; }
		public string console { get; set; }
        public int consoleid { get; set; }
    }
}

