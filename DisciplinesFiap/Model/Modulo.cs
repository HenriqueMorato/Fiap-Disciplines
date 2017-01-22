using System;
using System.Collections.Generic;

namespace DisciplinesFiap
{
	public class Modulo : BaseViewModel
	{
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Carga { get; set; }
        public int Ordem { get; set; }
        public List<Disciplina> Disciplina { get; set; }
        public int Curso_Id { get; set; }
    }
}
