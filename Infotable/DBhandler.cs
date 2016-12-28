using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Views;
using Android.Content;
using SQLite.Net;
using SQLite;
using SQLite.Net.Attributes;

namespace Infotable.Infotable
{
	class DBhandler// : Activity
	{
		/// <summary>
		/// Скопировать БД на карту памяти устройства
		/// </summary>
		/// <param name="br">Ассет БД</param>
		/// <param name="dbName">Имя БД</param>
		/// <returns></returns>
		public static bool copyDB(BinaryReader br, string dbName)
		{
			var dbPath = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.ToString()+"/Infotable/");
			if (!dbPath.Exists())
				dbPath.Mkdirs();
			//string dbName = "testtimetable2.sqlite";
			string dbFile = Path.Combine(dbPath.ToString(), dbName);
			// Проверить существование БД
			if (!File.Exists(dbFile))
			{
				using (br/*BinaryReader br = new BinaryReader(Assets.Open(dbName))*/)
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbFile, FileMode.Create)))
					{
						//Копирование БД через буфер
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, len);
						}
						//Успех
						return true;
					}
				}
			}
			//Неудача
			else
			{
				return false;
			}
		}
		//----------------------------

		[Table("testtimetable_lessons")]
		public class testtimetable_lessons
		{
			[PrimaryKey, AutoIncrement, Column("_id")]
			public int less_ID { get; set; }
			//[MaxLength(8)]
			//public string Symbol { get; set; }

			public int subj_ID { get; set; }

			public int plac_ID { get; set; }
		}

		public static string getNoniInfo(string dbName)
		{
			var dbPath = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Infotable/");
			if (!dbPath.Exists())
				return null;
			//string dbName = "testtimetable2.sqlite";
			string dbFile = Path.Combine(dbPath.ToString(), dbName);
			var db = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbFile);
			//db.CreateTable<testtimetable_lessons>();
			//var stock = db.Get<testtimetable_lessons>(1); // primary key id of 5
			//var stockList = db.Table<testtimetable_lessons>();
			var table = db.Table<testtimetable_lessons>();
			string str = "";
			foreach (var s in table)
			{
				str+=(s.subj_ID + " " + s.plac_ID + "|");
			}
			return str;
		}
	}
}