﻿using System.IO;
using System.IO.IsolatedStorage;
using MonoSAMFramework.Portable.DeviceBridge;

namespace RetroReactor.OpenGL.Impl
{
	class WindowsFileHelper : FileHelper
	{
		public override void WriteData(string fileid, string data)
		{
			var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

			var fs = store.CreateFile(fileid);
			using (StreamWriter sw = new StreamWriter(fs))
			{
				sw.Write(data);
			}
		}

		public override string ReadDataOrNull(string fileid)
		{
			var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

			if (store.FileExists(fileid))
			{
				var fs = store.OpenFile(fileid, FileMode.Open);
				using (StreamReader sr = new StreamReader(fs))
				{
					return sr.ReadToEnd();
				}
			}

			return null;
		}
	}
}
