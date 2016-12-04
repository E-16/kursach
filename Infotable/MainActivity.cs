using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;

namespace Infotable
{
	[Activity(Label = "Infotable", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			string dbName = "testtimetableworks.sqlite";
			string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
			// Check if your DB has already been extracted.
			if (!File.Exists(dbPath))
			{
				using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, len);
						}
					}
				}
			}

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		}
	}
}