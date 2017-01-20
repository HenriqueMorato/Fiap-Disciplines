using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace DisciplinesFiap
{
	public partial class CursoDetalheView : ContentPage
	{
		private CursoService _service = new CursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }

		private ObservableCollection<GroupedDisciplines> _disciplinas = new ObservableCollection<GroupedDisciplines>();

		void CreateGroup ()
		{
			var group = new GroupedDisciplines();
			foreach(Modulo m in _modulos)
			{
				group = new GroupedDisciplines() { Descricao = m.Descricao };
				foreach(Disciplina d in _modulos.SelectMany(x => x.Disciplinas))
				{
					group.Add(d);
				}
				_disciplinas.Add(group);
			}

		}

		public CursoDetalheView(int cursoId)
		{
			InitializeComponent();

			_modulos = new ObservableCollection<Modulo>(_service.GetCurso(cursoId).Modulos);
			CreateGroup();
			listView.ItemsSource = _disciplinas;
		}
	}
}
