using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class CursoDetalheView : ContentPage
	{
		private CursoService _service = new CursoService();
		private Curso _curso;

		public CursoDetalheView(int cursoId)
		{
			_curso = _service.GetCurso(cursoId);
			BindingContext = _curso.Modulos;

			
			InitializeComponent();

			//listView.ItemsSource = _curso.Modulos;
			//listViewDisciplinas.ItemsSource = _curso.Modulos;
		}
	}
}
