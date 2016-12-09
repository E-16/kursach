using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace Infotable //Может быть, следует поменять на Infotable.Infotable?
{
	class copyDB : Activity
	{
		/// <summary>
		/// Скопировать БД на карту памяти устройства
		/// </summary>
		/// <param name="br">Ассет БД</param>
		/// <param name="dbName">Имя БД</param>
		/// <returns></returns>
		public static bool copy(BinaryReader br, string dbName)
		{
			//string dbName = "testtimetableworks.sqlite";
			string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
			// Проверить существование БД
			if (!File.Exists(dbPath))
			{
				using (br/*BinaryReader br = new BinaryReader(Assets.Open(dbName))*/)
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
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