using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DisciplinesFiap.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			//UINavigationBar.Appearance.TintColor = Color.Black.ToUIColor();
			//TODO tirar status bar ios e android
			//UIApplication.SharedApplication.StatusBarHidden = true;

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
