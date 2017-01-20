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

		public CursoDetalheView(int cursoId)
		{
			InitializeComponent();

			var cursos = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(cursos.Modulos);
			BindingContext = cursos;
			listView.ItemsSource = GroupedDisciplines.CriarGrupo(_modulos);
		}
	}
}
