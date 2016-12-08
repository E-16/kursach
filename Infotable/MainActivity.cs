using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Views;

namespace Infotable
{
	[Activity(Label = "Infotable", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			//Скопировать БД на устройсво
			string dbName = "testtimetableworks.sqlite";
			BinaryReader br = new BinaryReader(Assets.Open(dbName));
			copyDB.copy(br, dbName);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//toolbar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetActionBar(toolbar);
			ActionBar.Title = "My Toolbar";
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.top_menus, menu);
			return base.OnCreateOptionsMenu(menu);
		}
	}
}