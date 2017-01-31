using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Threading;
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

            Title = curso.Titulo;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			var curso = BindingContext as Curso;

			if (string.IsNullOrWhiteSpace(curso.Titulo))
			{
				await DisplayAlert("Erro", "Por Favor, adicione o Título", "OK");
				return;
			}

            var cancelSrc = new CancellationTokenSource();
            var config = new ProgressDialogConfig()
                .SetTitle("Loading")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (UserDialogs.Instance.Progress(config))
            {

                if (curso.Id == 0)
                {
                    CursoAdicionado?.Invoke(this, curso);
                }
                else
                    CursoEditado?.Invoke(this, curso);

            }

			await Navigation.PopAsync();
		}
	}
}
