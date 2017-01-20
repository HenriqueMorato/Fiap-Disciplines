using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarCursoView : ContentPage
	{
		public event EventHandler<Curso> CursoAdicionado;
		public event EventHandler<Curso> CursoEditado;

		public EditarCursoView(Curso curso)
		{
			if (curso == null)
				throw new ArgumentNullException(nameof(curso));

			InitializeComponent();

			BindingContext = new Curso
			{
				Id = curso.Id,
				Titulo = curso.Titulo,
				Local = curso.Local,
				Inicio = curso.Inicio,
				Duracao = curso.Duracao,
				Dias = curso.Dias,
				Horario = curso.Horario,
				Investimento = curso.Investimento
			};
		}

		void Save_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
