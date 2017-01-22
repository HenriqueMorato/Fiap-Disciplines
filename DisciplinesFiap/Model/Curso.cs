using System;
using System.Collections.Generic;


namespace DisciplinesFiap
{
	public class Curso : BaseViewModel
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Local { get; set; }
		public string Inicio { get; set; }
		public string Duracao { get; set; }
		public string Dias { get; set; }
		public string Horario { get; set; }
		public string Investimento { get; set; }
		public List<Modulo> Modulo { get; set; }
	}
}
