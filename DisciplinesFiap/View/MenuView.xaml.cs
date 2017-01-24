using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class MenuView : ContentPage
	{
		public ListView ListView { get { return lstMenu; } }
		public MenuView()
		{
			InitializeComponent();

			ObservableCollection<OpcoesMenu> menuItems = new ObservableCollection<OpcoesMenu>();
			menuItems.Add(new OpcoesMenu
			{
				Opcao = "Login",
				Icone = "Door_Opened.png",
				TargetType = typeof(LoginView)
			});
			menuItems.Add(new OpcoesMenu
			{
				Opcao = "Home",
				Icone = "Home.png",
				TargetType = typeof(HomeView)
			});
			menuItems.Add(new OpcoesMenu
			{
				Opcao = "Disciplinas",
				Icone = "Book.png",
				TargetType = typeof(CursosView)
			});

			lstMenu.ItemsSource = menuItems;
		}
	}
}
