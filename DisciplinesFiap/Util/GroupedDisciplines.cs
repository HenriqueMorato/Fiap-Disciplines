using System;
using System.Collections.ObjectModel;

namespace DisciplinesFiap
{
	public class GroupedDisciplines : ObservableCollection<Disciplina>
	{
		public string Descricao { get; set; }
	}
}
