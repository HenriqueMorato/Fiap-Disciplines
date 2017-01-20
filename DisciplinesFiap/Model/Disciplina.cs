using System;
namespace DisciplinesFiap
{
	public class Disciplina : BaseViewModel
	{
		private string id;
		//[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Descricao { get; set; }
		public string Conteudo { get; set; }
	}
}
