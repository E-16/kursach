using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Views;

namespace Infotable.Infotable
{
	[Activity(Label = "Infotable", MainLauncher = true, Icon = "@drawable/ic_today_white_24dp")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//toolbar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetActionBar(toolbar);
			ActionBar.Title = "My Toolbar";

			//Выбор из меню
			toolbar.MenuItemClick += (sender, e) =>
			{
				//Toast.MakeText(this, "Toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
				switch ("" + e.Item.TitleFormatted)
				{
					//Скопировать БД на устройсво
					case "DEBUG:Copy DB":
					{
							string dbName = "testtimetableworks.sqlite";
							BinaryReader br = new BinaryReader(Assets.Open(dbName));
							if (copyDB.copy(br, dbName) == true)
							{
								Toast.MakeText(this, "DB copied to device", ToastLength.Short).Show();
							}
							else
							{
								Toast.MakeText(this, "Failed to copy DB", ToastLength.Short).Show();
							}
							break;
					}
					default:
					{
							Toast.MakeText(this, "Not implemented yet", ToastLength.Short).Show();
							break;
					}
				}
			};
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.top_menus, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//public override bool OnOptionsItemSelected(IMenuItem item)
		//{
		//	Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
		//		 ToastLength.Short).Show();
		//	return base.OnOptionsItemSelected(item);
		//}
	}
}