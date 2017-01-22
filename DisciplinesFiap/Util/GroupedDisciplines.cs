using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DisciplinesFiap
{
	public class GroupedDisciplines : ObservableCollection<Disciplina>
	{
		public int Id { get; set; }
		public string Descricao { get; set; }

		public static ObservableCollection<GroupedDisciplines> CriarGrupo (ObservableCollection<Modulo> modulo)
		{
			var grupo = new GroupedDisciplines();
			var colecaoGrupo = new ObservableCollection<GroupedDisciplines>();
			foreach (Modulo m in modulo)
			{
				grupo = new GroupedDisciplines() { Id = m.Id, Descricao = m.Descricao };
				foreach (Disciplina d in m.Disciplina)
				{
					grupo.Add(d);
				}
				colecaoGrupo.Add(grupo);
			}

			return colecaoGrupo;
		}
	}
}
