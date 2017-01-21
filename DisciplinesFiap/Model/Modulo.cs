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

		private string _descricao;
		public string Descricao
		{
			get { return _descricao; }
			set { SetValue(ref _descricao, value); }
		}
		public string Carga { get; set; }
		private int _ordem;
		public int Ordem
		{
			get { return _ordem; }
			set { SetValue(ref _ordem, value); }
		}
		public List<Disciplina> Disciplina { get; set; }
	}
}
