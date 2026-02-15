using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using UnityEngine;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200151D RID: 5405
	public static class BanlistManager
	{
		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06009D2D RID: 40237 RVA: 0x0019586D File Offset: 0x00193A6D
		// (set) Token: 0x06009D2E RID: 40238 RVA: 0x00195874 File Offset: 0x00193A74
		public static List<Banlist> Banlists { get; private set; }

		// Token: 0x06009D2F RID: 40239 RVA: 0x0019587C File Offset: 0x00193A7C
		public static void Initialize()
		{
			BanlistManager.Banlists = new List<Banlist>();
			if (Config.GetBool("Expansions", true))
			{
				string confPath = "Expansions/lflist.conf";
				if (File.Exists(confPath))
				{
					StreamReader streamReader = new StreamReader(confPath);
					BanlistManager.InitializeFromReader(streamReader);
					streamReader.Close();
				}
				foreach (ZipFile zip in ZipHelper.zips)
				{
					if (!zip.Name.ToLower().EndsWith("script.zip"))
					{
						foreach (string file in zip.EntryFileNames)
						{
							if (file.ToLower().EndsWith("lflist.conf"))
							{
								ZipEntry e = zip[file];
								if (!Directory.Exists("TempFolder/"))
								{
									Directory.CreateDirectory("TempFolder/");
								}
								string text = Path.Combine(Path.GetFullPath("TempFolder/"), file);
								e.Extract(Path.GetFullPath("TempFolder/"), ExtractExistingFileAction.OverwriteSilently);
								StreamReader streamReader2 = new StreamReader(text);
								BanlistManager.InitializeFromReader(streamReader2);
								streamReader2.Close();
								File.Delete(text);
							}
						}
					}
				}
			}
			StreamReader streamReader3 = new StreamReader("Data/lflist.conf");
			BanlistManager.InitializeFromReader(streamReader3);
			streamReader3.Close();
			Banlist current = new Banlist();
			current.Name = BanlistManager.EmptyBanlistName;
			BanlistManager.Banlists.Add(current);
		}

		// Token: 0x06009D30 RID: 40240 RVA: 0x001959FC File Offset: 0x00193BFC
		public static void InitializeFromReader(StreamReader reader)
		{
			Banlist current = null;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				try
				{
					if (line != null)
					{
						if (!line.StartsWith("#"))
						{
							if (line.StartsWith("!"))
							{
								current = new Banlist();
								current.Name = line.Substring(1, line.Length - 1);
								BanlistManager.Banlists.Add(current);
							}
							else if (line.Contains(" "))
							{
								if (current != null)
								{
									string[] array = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
									int id = int.Parse(array[0]);
									int count = int.Parse(array[1]);
									current.Add(id, count);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Log(line);
					Debug.Log(ex);
				}
			}
		}

		// Token: 0x06009D31 RID: 40241 RVA: 0x00195AD0 File Offset: 0x00193CD0
		public static int GetIndex(uint hash)
		{
			for (int i = 0; i < BanlistManager.Banlists.Count; i++)
			{
				if (BanlistManager.Banlists[i].Hash == hash)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06009D32 RID: 40242 RVA: 0x00195B08 File Offset: 0x00193D08
		public static int GetIndexByName(string name)
		{
			for (int i = 0; i < BanlistManager.Banlists.Count; i++)
			{
				if (BanlistManager.Banlists[i].Name == name)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06009D33 RID: 40243 RVA: 0x00195B48 File Offset: 0x00193D48
		public static string GetName(uint hash)
		{
			for (int i = 0; i < BanlistManager.Banlists.Count; i++)
			{
				if (BanlistManager.Banlists[i].Hash == hash)
				{
					return BanlistManager.Banlists[i].Name;
				}
			}
			return InterString.Get("未知卡表", 0);
		}

		// Token: 0x06009D34 RID: 40244 RVA: 0x00195B9C File Offset: 0x00193D9C
		public static List<string> GetAllName()
		{
			List<string> returnValue = new List<string>();
			foreach (Banlist item in BanlistManager.Banlists)
			{
				returnValue.Add(item.Name);
			}
			return returnValue;
		}

		// Token: 0x06009D35 RID: 40245 RVA: 0x00195BFC File Offset: 0x00193DFC
		public static Banlist GetByName(string name)
		{
			Banlist returnValue = BanlistManager.Banlists[BanlistManager.Banlists.Count - 1];
			foreach (Banlist item in BanlistManager.Banlists)
			{
				if (item.Name == name)
				{
					returnValue = item;
				}
			}
			return returnValue;
		}

		// Token: 0x06009D36 RID: 40246 RVA: 0x00195C70 File Offset: 0x00193E70
		public static Banlist GetByHash(uint hash)
		{
			Banlist returnValue = BanlistManager.Banlists[BanlistManager.Banlists.Count - 1];
			foreach (Banlist item in BanlistManager.Banlists)
			{
				if (item.Hash == hash)
				{
					returnValue = item;
				}
			}
			return returnValue;
		}

		// Token: 0x0400DB42 RID: 56130
		public static string EmptyBanlistName = "N/A";
	}
}
