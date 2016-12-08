using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Views;
using Android.Content;

namespace Infotable
{
	[Activity(Label = "Infotable", MainLauncher = true, Icon = "@drawable/ic_today_white_24dp")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//notification builder
			Notification.Builder builder = new Notification.Builder(this)
							.SetContentTitle("Sample Notification")
							.SetContentText("Hello World! This is my first notification!")
							.SetSmallIcon(Resource.Drawable.ic_today_white_24dp);
			builder.SetVisibility(NotificationVisibility.Private);
			//builder.SetCategory(Notification.CategoryEvent);
			Notification notification = builder.Build();
			NotificationManager notificationManager =
					GetSystemService(Context.NotificationService) as NotificationManager;
			const int notificationId = 0;
			//notificationManager.Notify(notificationId, notification);

			//toolbar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetActionBar(toolbar);
			ActionBar.Title = "My Toolbar";
			//Выбор из меню
			toolbar.MenuItemClick += (sender, e) =>
			{
				//Toast.MakeText(this, "Toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
				switch (e.Item.TitleFormatted.ToString()) //Тут бы поменять на ID вместо Title
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
					case "DEBUG:Push notification":
					{
							notificationManager.Notify(notificationId, notification);
							Toast.MakeText(this, "Pushed a notification", ToastLength.Short).Show();
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