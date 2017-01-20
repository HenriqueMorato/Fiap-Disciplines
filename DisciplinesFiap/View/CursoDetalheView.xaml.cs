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

		private ObservableCollection<GroupedDisciplines> _disciplinas { get; set; }

		void CreateGroup ()
		{
			_disciplinas = new ObservableCollection<GroupedDisciplines>();
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

			var cursos = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(cursos.Modulos);
			BindingContext = cursos;
			CreateGroup();
			listView.ItemsSource = _disciplinas;
		}
	}
}
