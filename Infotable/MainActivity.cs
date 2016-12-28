using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Views;
using Android.Content;

namespace Infotable.Infotable
{
	[Activity(Label = "Infotable", MainLauncher = true, Icon = "@drawable/ic_today_white_24dp")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			//Отобразить макет "Main".
			SetContentView(Resource.Layout.Main);

			//Скопировать пустую БД на  устройство
			string blankdbName = "blank.sqlite";
			BinaryReader brBlank = new BinaryReader(Assets.Open(blankdbName));//Это неправильно. Надо перенести в DBhandler в качестве ссылки
			if (DBhandler.copyDB(brBlank, blankdbName) == true)
			{
				Toast.MakeText(this, "DB copied to device", ToastLength.Short).Show();
			}
			else
			{
				Toast.MakeText(this, "Failed to copy DB", ToastLength.Short).Show();
			}

			//notification builder (составитель уведомлений) //Это неправильно.
			Notification.Builder builder = new Notification.Builder(this);

			//Создание тулбара
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetActionBar(toolbar);
			ActionBar.Title = "Infotable";
			//Выбор из меню
			toolbar.MenuItemClick += (sender, e) =>
			{
				//Toast.MakeText(this, "Toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();//Отображение названия нажатого пункта для дебага
				switch (e.Item.TitleFormatted.ToString()) //Тут бы поменять на ID вместо Title
				{
					//Скопировать БД на устройсво
					case "DEBUG:Copy DB":
					{
							string dbName = "testtimetableworks.sqlite";
							BinaryReader br = new BinaryReader(Assets.Open(dbName));//Это неправильно. Надо перенести в DBhandler в качестве ссылки
							if (DBhandler.copyDB(br, dbName) == true)
							{
								Toast.MakeText(this, "DB copied to device", ToastLength.Short).Show();
							}
							else
							{
								Toast.MakeText(this, "Failed to copy DB", ToastLength.Short).Show();
							}
							break;
					}
					//Создать уведомление
					case "DEBUG:Push notification":
					{
							builder.SetContentTitle("Notification");
							builder.SetContentText(DBhandler.getNoniInfo("testtimetableworks.sqlite"));
							builder.SetSmallIcon(Resource.Drawable.ic_today_white_24dp);
							builder.SetVisibility(NotificationVisibility.Private);
							//builder.SetCategory(Notification.CategoryEvent);
							Notification notification = builder.Build();
							NotificationManager notificationManager =
									GetSystemService(Context.NotificationService) as NotificationManager;
							const int notificationId = 0;
							//notificationManager.Notify(notificationId, notification);
							notificationManager.Notify(notificationId, notification);
							Toast.MakeText(this, "Pushed a notification", ToastLength.Short).Show();
							break;
					}
					//Любой другой пункт
					default:
					{
							Toast.MakeText(this, "Not implemented yet", ToastLength.Short).Show();
							break;
					}
				}
			};
		}
		/// <summary>
		/// Действие при создании меню в тулбаре
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
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