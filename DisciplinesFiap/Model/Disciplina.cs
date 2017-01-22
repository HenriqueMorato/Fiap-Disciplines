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

		private string _descricao;
		public string Descricao 
		{ 
			get { return _descricao; } 
			set { SetValue(ref _descricao, value); }
		}
		public string Conteudo { get; set; }

        public int Modulo_Id { get; set; }
    }
}
