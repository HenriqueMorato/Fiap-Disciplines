using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarModuloView : ContentPage
	{
		public EditarModuloView(Modulo modulo)
		{
			if (modulo == null)
				throw new ArgumentNullException(nameof(modulo));

			InitializeComponent();

			BindingContext = new Modulo
			{
				Id = modulo.Id,
				Descricao = modulo.Descricao,
				Ordem = modulo.Ordem,
				Carga = modulo.Carga,
				Disciplina = modulo.Disciplina
			};
		}
	}
}
