using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001522 RID: 5410
	internal static class PacksManager
	{
		// Token: 0x06009D8B RID: 40331 RVA: 0x00199FB4 File Offset: 0x001981B4
		internal static void Initialize()
		{
			if (Directory.Exists("Data/pack"))
			{
				foreach (FileInfo file in new DirectoryInfo("Data/pack").GetFiles())
				{
					if (file.Name.ToLower().EndsWith(".db"))
					{
						PacksManager.LoadDataBase("Data/pack/" + file.Name);
					}
				}
				PacksManager.InitializeSec();
			}
		}

		// Token: 0x06009D8C RID: 40332 RVA: 0x0019A020 File Offset: 0x00198220
		internal static void LoadDataBase(string filePath)
		{
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + filePath))
			{
				connection.Open();
				using (IDbCommand command = new SqliteCommand("SELECT pack.* FROM pack;", connection))
				{
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							try
							{
								Card c = CardsManager.GetCardRaw((int)reader.GetInt64(0));
								if (c != null)
								{
									string @string = reader.GetString(1);
									c.packFullName = reader.GetString(2);
									string[] mats = @string.Split('-', StringSplitOptions.None);
									if (mats.Length > 1)
									{
										c.packShortName = mats[0];
									}
									else
									{
										c.packShortName = ((c.packFullName.Length > 10) ? (c.packFullName.Substring(0, 10) + "...") : c.packFullName);
									}
									c.reality = reader.GetString(3);
									string string2 = reader.GetString(4);
									mats = string2.Split('/', StringSplitOptions.None);
									if (mats.Length == 3)
									{
										c.month = int.Parse(mats[0]);
										c.day = int.Parse(mats[1]);
										c.year = int.Parse(mats[2]);
									}
									mats = string2.Split('-', StringSplitOptions.None);
									if (mats.Length == 3)
									{
										c.year = int.Parse(mats[0]);
										c.month = int.Parse(mats[1]);
										c.day = int.Parse(mats[2]);
									}
									c.packFullName = string.Concat(new string[]
									{
										c.year.ToString(),
										"-",
										c.month.ToString("D2"),
										"-",
										c.day.ToString("D2"),
										" ",
										c.packFullName
									});
									if (!PacksManager.pacDic.ContainsKey(c.packFullName))
									{
										PacksManager.pacDic.Add(c.packFullName, c.packShortName);
										PacksManager.PackName p = new PacksManager.PackName();
										p.day = c.day;
										p.year = c.year;
										p.month = c.month;
										p.fullName = c.packFullName;
										p.shortName = c.packShortName;
										PacksManager.packs.Add(p);
									}
								}
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x06009D8D RID: 40333 RVA: 0x0019A2E0 File Offset: 0x001984E0
		internal static void InitializeSec()
		{
			PacksManager.packs.Sort(delegate(PacksManager.PackName left, PacksManager.PackName right)
			{
				if (left.year > right.year)
				{
					return -1;
				}
				if (left.year < right.year)
				{
					return 1;
				}
				if (left.month > right.month)
				{
					return -1;
				}
				if (left.month < right.month)
				{
					return 1;
				}
				if (left.day > right.day)
				{
					return -1;
				}
				int day = left.day;
				int day2 = right.day;
				return 1;
			});
		}

		// Token: 0x0400DB79 RID: 56185
		public static List<PacksManager.PackName> packs = new List<PacksManager.PackName>();

		// Token: 0x0400DB7A RID: 56186
		private static readonly Dictionary<string, string> pacDic = new Dictionary<string, string>();

		// Token: 0x0400DB7B RID: 56187
		private const string PATH = "Data/pack";

		// Token: 0x02001523 RID: 5411
		public class PackName
		{
			// Token: 0x0400DB7C RID: 56188
			public string fullName;

			// Token: 0x0400DB7D RID: 56189
			public string shortName;

			// Token: 0x0400DB7E RID: 56190
			public int year;

			// Token: 0x0400DB7F RID: 56191
			public int month;

			// Token: 0x0400DB80 RID: 56192
			public int day;
		}
	}
}
