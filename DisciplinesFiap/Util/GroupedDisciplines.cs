using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DisciplinesFiap
{
	public class GroupedDisciplines : ObservableCollection<Disciplina>
	{
		public string Descricao { get; set; }

		public static ObservableCollection<GroupedDisciplines> CriarGrupo (ObservableCollection<Modulo> modulo)
		{
			var grupo = new GroupedDisciplines();
			var colecaoGrupo = new ObservableCollection<GroupedDisciplines>();
			foreach (Modulo m in modulo)
			{
				grupo = new GroupedDisciplines() { Descricao = m.Descricao };
				foreach (Disciplina d in modulo.SelectMany(x => x.Disciplinas))
				{
					grupo.Add(d);
				}
				colecaoGrupo.Add(grupo);
			}

			return colecaoGrupo;
		}
	}
}
