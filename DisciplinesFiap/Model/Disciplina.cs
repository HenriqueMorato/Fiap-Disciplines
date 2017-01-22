using System;
namespace DisciplinesFiap
{
	public class Disciplina : BaseViewModel
	{
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public int Modulo_Id { get; set; }
    }
}
