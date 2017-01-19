using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class LoginView : ContentPage
	{
		public LoginView()
		{
			InitializeComponent();
		}

		void OnLogin_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new CursosView());
		}
	}
}
