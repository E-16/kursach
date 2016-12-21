using System.IO;

namespace Infotable.Infotable
{
	class copyDB// : Activity
	{
		/// <summary>
		/// Скопировать БД на карту памяти устройства
		/// </summary>
		/// <param name="br">Ассет БД</param>
		/// <param name="dbName">Имя БД</param>
		/// <returns></returns>
		public static bool copy(BinaryReader br, string dbName)
		{
			var dbPath = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.ToString()+"/Infotable/");
			if (!dbPath.Exists())
				dbPath.Mkdirs();
			//string dbName = "testtimetableworks.sqlite";
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
	}
}