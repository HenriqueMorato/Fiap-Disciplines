using System;
using System.Collections.Generic;

namespace DisciplinesFiap
{
	public class Modulo : BaseViewModel
	{
		private string id;
		//[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Descricao { get; set; }
		public string Carga { get; set; }
		public int Ordem { get; set; }
		public List<Disciplina> Disciplina { get; set; }
	}
}
