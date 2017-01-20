using System;
using System.Collections.Generic;


namespace DisciplinesFiap
{
	public class Curso : BaseViewModel
	{
		private string id;
		//[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		private string _titulo;
		public string Titulo 
		{ 
			get { return _titulo; }
			set 
			{ 
				SetValue(ref _titulo, value); 
			}
		}
		public string Local { get; set; }
		public string Inicio { get; set; }
		public string Duracao { get; set; }
		public string Dias { get; set; }
		public string Horario { get; set; }
		public string Investimento { get; set; }
		public List<Modulo> Modulos { get; set; }
	}
}
