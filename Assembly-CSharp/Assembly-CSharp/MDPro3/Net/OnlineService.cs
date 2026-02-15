using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MDPro3.Net
{
	// Token: 0x02001353 RID: 4947
	public static class OnlineService
	{
		// Token: 0x06008F76 RID: 36726 RVA: 0x0013643E File Offset: 0x0013463E
		public static void Initialize()
		{
			OnlineService.InitializeGenesysLflist();
			OnlineService.InitializeMyCardAppsAsync();
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x0013644C File Offset: 0x0013464C
		private static async UniTask InitializeGenesysLflist()
		{
			string eTag = await OnlineService.GetETagAsync("https://cdntx.moecube.com/ygopro-genesys/lflist.conf");
			if (!string.IsNullOrEmpty(eTag))
			{
				if (!string.Equals(eTag, Config.Get(OnlineService.GetLocalETagKey("https://cdntx.moecube.com/ygopro-genesys/lflist.conf"), "@ui"), StringComparison.Ordinal))
				{
					Program.Debug("Update Genesys Lflist.");
					await OnlineService.DownloadGenesysLflist(eTag);
				}
				else
				{
					Program.Debug("Genesys Lflist do not need update.");
				}
			}
			OnlineService.ParseGenesysLflist();
		}

		// Token: 0x06008F78 RID: 36728 RVA: 0x00136488 File Offset: 0x00134688
		private static bool GenesysRequiresDownload()
		{
			if (!File.Exists("Data/lflist_genesys.conf"))
			{
				return true;
			}
			DateTime lastWriteTime = File.GetLastWriteTimeUtc("Data/lflist_genesys.conf");
			DateTime now = DateTime.UtcNow;
			DateTime updateTime = new DateTime(now.Year, now.Month, now.Day, 20, 0, 0, DateTimeKind.Utc);
			return now > updateTime && lastWriteTime < updateTime;
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x001364E8 File Offset: 0x001346E8
		private static async UniTask DownloadGenesysLflist(string ETag)
		{
			using (UnityWebRequest request = UnityWebRequest.Get("https://cdntx.moecube.com/ygopro-genesys/lflist.conf"))
			{
				request.timeout = 15;
				await request.SendWebRequest();
				if (request.result == UnityWebRequest.Result.Success)
				{
					File.WriteAllText("Data/lflist_genesys.conf", request.downloadHandler.text);
					Config.Set(OnlineService.GetLocalETagKey("https://cdntx.moecube.com/ygopro-genesys/lflist.conf"), ETag);
					Config.Save();
				}
				else
				{
					MessageManager.Cast(InterString.Get("下载Genesys禁卡表失败。", 0));
				}
			}
		}

		// Token: 0x06008F7A RID: 36730 RVA: 0x0013652C File Offset: 0x0013472C
		private static void ParseGenesysLflist()
		{
			if (!File.Exists("Data/lflist_genesys.conf"))
			{
				return;
			}
			try
			{
				string[] array = File.ReadAllLines("Data/lflist_genesys.conf");
				string currentType = string.Empty;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string line = array2[i].Trim();
					if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
					{
						if (line.StartsWith("$"))
						{
							currentType = line;
						}
						else
						{
							string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							if (parts.Length == 2 && parts[1] == "0")
							{
								int code;
								if (int.TryParse(parts[0], out code))
								{
									OnlineService.genesysBannedCards.Add(code);
								}
							}
							else if (parts.Length >= 3)
							{
								int commentIndex = Array.FindIndex<string>(parts, (string p) => p.StartsWith("--"));
								int code2;
								if (((commentIndex > 0) ? commentIndex : parts.Length) >= 3 && int.TryParse(parts[0], out code2))
								{
									GenesysPoint gp = new GenesysPoint
									{
										code = code2,
										banType = currentType
									};
									int result;
									if (int.TryParse(parts[2], out result))
									{
										gp.point = result;
									}
									else
									{
										gp.point = 0;
									}
									OnlineService.genesysPoints.Add(gp);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Program.Debug(ex.Message);
			}
		}

		// Token: 0x06008F7B RID: 36731 RVA: 0x001366B4 File Offset: 0x001348B4
		public static int GetGenesysPoint(int code)
		{
			if (OnlineService.genesysBannedCards.Contains(code))
			{
				return -1;
			}
			foreach (GenesysPoint gp in OnlineService.genesysPoints)
			{
				if (gp.code == code)
				{
					return gp.point;
				}
			}
			return 0;
		}

		// Token: 0x06008F7C RID: 36732 RVA: 0x00136724 File Offset: 0x00134924
		public static string GetGenesysPointString(int code)
		{
			int gp = OnlineService.GetGenesysPoint(code);
			if (gp < 0)
			{
				return "X";
			}
			return gp.ToString();
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x0013674C File Offset: 0x0013494C
		public static Color GetGenesysPointColor(int gp)
		{
			if (gp < 0)
			{
				return Color.red;
			}
			if (gp == 0)
			{
				return Color.gray;
			}
			if (gp <= OnlineService.officialGenesysLimit / 10)
			{
				return Color.green;
			}
			if (gp <= OnlineService.officialGenesysLimit / 2)
			{
				return Color.yellow;
			}
			if (gp <= OnlineService.officialGenesysLimit)
			{
				return Color.magenta;
			}
			return Color.red;
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x001367A0 File Offset: 0x001349A0
		public static Color GetGenesysPointsColor(int gp)
		{
			if (gp <= OnlineService.officialGenesysLimit)
			{
				return Color.white;
			}
			return Color.red;
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x001367B8 File Offset: 0x001349B8
		private static async UniTask InitializeMyCardAppsAsync()
		{
			string eTag = await OnlineService.GetETagAsync("https://cdntx.moecube.com/apps.json");
			if (!string.IsNullOrEmpty(eTag))
			{
				if (!string.Equals(eTag, Config.Get(OnlineService.GetLocalETagKey("https://cdntx.moecube.com/apps.json"), "@ui"), StringComparison.Ordinal))
				{
					Program.Debug("Update MyCard Apps.");
					await OnlineService.DownloadMyCardApps(eTag);
				}
				else
				{
					Program.Debug("MyCard Apps do not need update.");
				}
			}
			OnlineService.ParseMyCardNews();
		}

		// Token: 0x06008F80 RID: 36736 RVA: 0x001367F4 File Offset: 0x001349F4
		private static async UniTask DownloadMyCardApps(string ETag)
		{
			using (UnityWebRequest request = UnityWebRequest.Get("https://cdntx.moecube.com/apps.json"))
			{
				request.timeout = 15;
				await request.SendWebRequest();
				if (request.result == UnityWebRequest.Result.Success)
				{
					File.WriteAllText("Data/mycard_apps.json", request.downloadHandler.text);
					Config.Set(OnlineService.GetLocalETagKey("https://cdntx.moecube.com/apps.json"), ETag);
					Config.Save();
				}
				else
				{
					Program.Debug("下载MyCard apps.json失败。");
				}
			}
		}

		// Token: 0x06008F81 RID: 36737 RVA: 0x00136838 File Offset: 0x00134A38
		private static void ParseMyCardNews()
		{
			if (!File.Exists("Data/mycard_apps.json"))
			{
				return;
			}
			foreach (MyCardApp app in JsonConvert.DeserializeObject<MyCardApp[]>(File.ReadAllText("Data/mycard_apps.json").Replace("\"news\":[]", "\"news\":{}")))
			{
				if (app.id == "ygopro")
				{
					OnlineService.myCardNews = app.news;
					return;
				}
			}
		}

		// Token: 0x06008F82 RID: 36738 RVA: 0x001368A1 File Offset: 0x00134AA1
		private static string GetLocalETagKey(string url)
		{
			return string.Format("ETag_{0}", url.GetHashCode());
		}

		// Token: 0x06008F83 RID: 36739 RVA: 0x001368B8 File Offset: 0x00134AB8
		public static async UniTask<string> GetETagAsync(string url)
		{
			string text;
			using (UnityWebRequest headRequest = UnityWebRequest.Head(url))
			{
				await headRequest.SendWebRequest();
				if (headRequest.result != UnityWebRequest.Result.Success)
				{
					Program.Debug("HEAD(" + url + ")请求失败：" + headRequest.error);
					text = null;
				}
				else
				{
					string responseHeader = headRequest.GetResponseHeader("ETag");
					string onlineETag = ((responseHeader != null) ? responseHeader.Trim() : null);
					if (string.IsNullOrEmpty(onlineETag))
					{
						Program.Debug("未找到ETag(" + url + ")，服务器可能未启用缓存");
						text = null;
					}
					else
					{
						text = onlineETag;
					}
				}
			}
			return text;
		}

		// Token: 0x0400CDC6 RID: 52678
		private const string URL_GENESYS_LFLIST = "https://cdntx.moecube.com/ygopro-genesys/lflist.conf";

		// Token: 0x0400CDC7 RID: 52679
		private const string PATH_GENESYS_LFLIST = "Data/lflist_genesys.conf";

		// Token: 0x0400CDC8 RID: 52680
		private static readonly List<int> genesysBannedCards = new List<int>();

		// Token: 0x0400CDC9 RID: 52681
		private static readonly List<GenesysPoint> genesysPoints = new List<GenesysPoint>();

		// Token: 0x0400CDCA RID: 52682
		private static int officialGenesysLimit = 100;

		// Token: 0x0400CDCB RID: 52683
		private const string URL_MYCARD_APPS = "https://cdntx.moecube.com/apps.json";

		// Token: 0x0400CDCC RID: 52684
		private const string PATH_MYCARD_APPS = "Data/mycard_apps.json";

		// Token: 0x0400CDCD RID: 52685
		public static MyCardNews myCardNews;
	}
}
