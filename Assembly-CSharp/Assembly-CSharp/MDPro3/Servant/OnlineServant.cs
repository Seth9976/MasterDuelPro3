using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012F4 RID: 4852
	public class OnlineServant : Servant
	{
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06008DDE RID: 36318 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06008DDF RID: 36319 RVA: 0x0000763C File Offset: 0x0000583C
		protected override bool ShowLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008DE0 RID: 36320 RVA: 0x0012E4BB File Offset: 0x0012C6BB
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
			this.TryTokenIn();
		}

		// Token: 0x06008DE1 RID: 36321 RVA: 0x0012E4D9 File Offset: 0x0012C6D9
		protected override void ApplyShowArrangement(int preDepth)
		{
			if (Program.exitOnReturn)
			{
				Program.GameQuit();
				return;
			}
			base.ApplyShowArrangement(preDepth);
			if (this.servantUI != null)
			{
				this.RefreshDeckSelector();
			}
			base.StartCoroutine(this.RefreshMyCardAssets());
		}

		// Token: 0x06008DE2 RID: 36322 RVA: 0x0012E510 File Offset: 0x0012C710
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			this.RefreshDeckSelector();
			MyCard.ConnectToAthleticWatchListWebSocket();
		}

		// Token: 0x06008DE3 RID: 36323 RVA: 0x0012E523 File Offset: 0x0012C723
		protected override void ApplyHideArrangement(int preDepth)
		{
			base.ApplyHideArrangement(preDepth);
			Config.Save();
		}

		// Token: 0x06008DE4 RID: 36324 RVA: 0x0012E531 File Offset: 0x0012C731
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.servantUI != null)
			{
				this.GetUI<OnlineServantUI>().SelectLastSelectable(this.lastSelectable);
			}
		}

		// Token: 0x06008DE5 RID: 36325 RVA: 0x0012E560 File Offset: 0x0012C760
		public override void PerFrameFunction()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
			{
				this.OnReturn();
			}
			if (UserInput.WasLeftShoulderPressed)
			{
				this.GetUI<OnlineServantUI>().PageLeft();
			}
			if (UserInput.WasRightShoulderPressed)
			{
				this.GetUI<OnlineServantUI>().PageRight();
			}
		}

		// Token: 0x06008DE6 RID: 36326 RVA: 0x0012E5B0 File Offset: 0x0012C7B0
		public void KF_OnlineGame(string name, string ip, string port, string password)
		{
			if (string.IsNullOrEmpty(name))
			{
				MessageManager.Cast(InterString.Get("用户名不能为空。", 0));
				return;
			}
			if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port))
			{
				MessageManager.Cast(InterString.Get("主机地址和端口不能为空。", 0));
				return;
			}
			RoomServant.FromSolo = false;
			RoomServant.FromLocalHost = false;
			RoomServant.FromHandTest = false;
			TcpHelper.LinkStart(ip, name, port, password, false, null);
		}

		// Token: 0x06008DE7 RID: 36327 RVA: 0x0012E618 File Offset: 0x0012C818
		public void CreateServer(List<string> serverArgs)
		{
			int port = 7911;
			while (!TcpHelper.IsPortAvailable(port))
			{
				port++;
				if (port == 65536)
				{
					port = 1;
				}
			}
			string text = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", new object[]
			{
				port.ToString(),
				BanlistManager.GetIndexByName(serverArgs[1]),
				this.GetPoolCodeByName(serverArgs[2]),
				this.GetModeCodeByName(serverArgs[3]),
				"F",
				serverArgs[4],
				serverArgs[5],
				serverArgs[7],
				serverArgs[8],
				serverArgs[9],
				serverArgs[6],
				"0"
			});
			RoomServant.FromSolo = false;
			RoomServant.FromLocalHost = true;
			RoomServant.FromHandTest = false;
			YgoServer.StartServer(text);
			TcpHelper.LinkStart("127.0.0.1", Config.Get("DuelPlayerName0", "@ui"), port.ToString(), string.Empty, true, null);
		}

		// Token: 0x06008DE8 RID: 36328 RVA: 0x0012E724 File Offset: 0x0012C924
		private string GetPoolCodeByName(string pool)
		{
			for (int i = 1481; i < 1487; i++)
			{
				if (StringHelper.GetUnsafe(i, 0) == pool)
				{
					return (i - 1481).ToString();
				}
			}
			return "5";
		}

		// Token: 0x06008DE9 RID: 36329 RVA: 0x0012E76C File Offset: 0x0012C96C
		private string GetModeCodeByName(string mode)
		{
			for (int i = 1244; i < 1247; i++)
			{
				if (StringHelper.GetUnsafe(i, 0) == mode)
				{
					return (i - 1244).ToString();
				}
			}
			return "0";
		}

		// Token: 0x06008DEA RID: 36330 RVA: 0x0012E7B1 File Offset: 0x0012C9B1
		public void LoginMyCard(string name, string passwd)
		{
			base.StartCoroutine(this.LoginMyCardAsync(name, passwd));
		}

		// Token: 0x06008DEB RID: 36331 RVA: 0x0012E7C2 File Offset: 0x0012C9C2
		private IEnumerator LoginMyCardAsync(string account, string passwd)
		{
			this.GetUI<OnlineServantUI>().PageMyCard.PageLogin.SetActive(false);
			Task<MyCardAccount> task = MyCard.Login(account, passwd);
			while (!task.IsCompleted)
			{
				yield return null;
			}
			if (task.Result.user.id == 0)
			{
				MessageManager.Cast(InterString.Get("登录失败：", 0) + task.Result.user.username);
				this.GetUI<OnlineServantUI>().PageMyCard.PageLogin.SetActive(true);
				if (UserInput.NeedDefaultSelect())
				{
					this.Select(false);
				}
				yield break;
			}
			Config.Set("MyCardToken", task.Result.token);
			Config.Save();
			this.LoginSuccessEvent();
			yield break;
		}

		// Token: 0x06008DEC RID: 36332 RVA: 0x0012E7DF File Offset: 0x0012C9DF
		private void TryTokenIn()
		{
			base.StartCoroutine(this.TryTokenInAsync());
		}

		// Token: 0x06008DED RID: 36333 RVA: 0x0012E7EE File Offset: 0x0012C9EE
		private IEnumerator TryTokenInAsync()
		{
			string token = Config.Get("MyCardToken", "0");
			if (token == "0")
			{
				yield break;
			}
			Task<MyCardAccount> task = MyCard.TokenIn(token);
			while (!task.IsCompleted)
			{
				yield return null;
			}
			if (task.Result.user.id == 0)
			{
				MessageManager.Cast(InterString.Get("MyCard登录失败。", 0));
				yield break;
			}
			this.LoginSuccessEvent();
			yield break;
		}

		// Token: 0x06008DEE RID: 36334 RVA: 0x0012E7FD File Offset: 0x0012C9FD
		private void LoginSuccessEvent()
		{
			if (MyCard.account == null)
			{
				return;
			}
			base.StartCoroutine(this.SyncDecks());
			if (this.servantUI == null)
			{
				return;
			}
			base.StartCoroutine(this.RefreshMyCardAssets());
		}

		// Token: 0x06008DEF RID: 36335 RVA: 0x0012E830 File Offset: 0x0012CA30
		private IEnumerator RefreshMyCardAssets()
		{
			Task<MyCardUserExp> task = MyCard.GetExp();
			while (!task.IsCompleted)
			{
				yield return null;
			}
			this.GetUI<OnlineServantUI>().PageMyCard.UserProfile.SetProfile(task.Result);
			while (!Appearance.loaded)
			{
				yield return null;
			}
			this.GetUI<OnlineServantUI>().PageMyCard.UserProfile.Avatar.material = Appearance.duelFrameMat0;
			if (MyCard.avatar == null)
			{
				Task<Texture2D> avatarTask = Tools.DownloadImageAsync(MyCard.account.user.avatar);
				while (!avatarTask.IsCompleted)
				{
					yield return null;
				}
				MyCard.avatar = avatarTask.Result;
				this.GetUI<OnlineServantUI>().PageMyCard.UserProfile.Avatar.texture = MyCard.avatar;
				avatarTask = null;
			}
			this.GetUI<OnlineServantUI>().PageMyCard.ActivePageFunction();
			yield break;
		}

		// Token: 0x06008DF0 RID: 36336 RVA: 0x0012E83F File Offset: 0x0012CA3F
		private void RefreshDeckSelector()
		{
			this.GetUI<OnlineServantUI>().PageMyCard.ButtonDeckSelector.SetConfigDeck(InterString.Get("未选中有效卡组", 0));
		}

		// Token: 0x06008DF1 RID: 36337 RVA: 0x0012E864 File Offset: 0x0012CA64
		private string GetDeckNameWithType(string path)
		{
			string type = Path.GetFileName(Path.GetDirectoryName(path));
			if (type == "Deck" && type == Path.GetDirectoryName(path))
			{
				type = string.Empty;
			}
			return ((type == string.Empty) ? string.Empty : (type + "/")) + Path.GetFileNameWithoutExtension(path);
		}

		// Token: 0x06008DF2 RID: 36338 RVA: 0x0012E8C8 File Offset: 0x0012CAC8
		private string GetDeckTypeFromPath(string path)
		{
			string type = Path.GetFileName(Path.GetDirectoryName(path));
			if (type == "Deck" && type == Path.GetDirectoryName(path))
			{
				type = string.Empty;
			}
			return type;
		}

		// Token: 0x06008DF3 RID: 36339 RVA: 0x0012E903 File Offset: 0x0012CB03
		private string GetDeckTypeFromName(string deckName)
		{
			if (!deckName.Contains("/"))
			{
				return string.Empty;
			}
			return deckName.Split('/', StringSplitOptions.None)[0];
		}

		// Token: 0x06008DF4 RID: 36340 RVA: 0x0012E923 File Offset: 0x0012CB23
		private IEnumerator SyncDecks()
		{
			if (OnlineDeck.decks == null)
			{
				MessageManager.Cast(InterString.Get("同步卡组失败。", 0));
				yield break;
			}
			string[] deckFiles = Directory.GetFiles("Deck/", "*.ydk", SearchOption.AllDirectories);
			List<Deck> decks = new List<Deck>();
			foreach (string deckPath in deckFiles)
			{
				decks.Add(new Deck(deckPath));
			}
			Dictionary<string, Deck> decksNeedUpload = new Dictionary<string, Deck>();
			Dictionary<string, Deck> decksNeedUpdateToServer = new Dictionary<string, Deck>();
			Dictionary<string, Deck> decksNeedUpdateFromServer = new Dictionary<string, Deck>();
			List<string> localFoundIds = new List<string>();
			for (int i = 0; i < decks.Count; i++)
			{
				string deckName = this.GetDeckNameWithType(deckFiles[i]);
				string type = this.GetDeckTypeFromPath(deckFiles[i]);
				decks[i].type = type;
				bool deckIdFound = false;
				OnlineDeck.OnlineDeckData[] decks2 = OnlineDeck.decks;
				int j = 0;
				while (j < decks2.Length)
				{
					OnlineDeck.OnlineDeckData od3 = decks2[j];
					if (od3.deckId == decks[i].deckId)
					{
						deckIdFound = true;
						localFoundIds.Add(od3.deckId);
						if (od3.isDelete)
						{
							File.Delete(deckFiles[i]);
							break;
						}
						FileInfo fileInfo = new FileInfo(deckFiles[i]);
						DateTime serverTime = od3.GetUpdateUtcTime();
						TimeSpan diff = serverTime - fileInfo.LastWriteTimeUtc;
						if (diff.TotalSeconds > (double)this.timeError || diff.TotalSeconds < (double)(-(double)this.timeError))
						{
							if (fileInfo.LastWriteTimeUtc > serverTime)
							{
								decksNeedUpdateToServer.Add(deckName, decks[i]);
							}
							else
							{
								decksNeedUpdateFromServer.Add(deckName, decks[i]);
							}
						}
						if ((Path.GetFileName(deckName) != od3.deckName || this.GetDeckTypeFromName(deckName) != od3.GetType()) && !decksNeedUpdateFromServer.Keys.Contains(deckName) && !decksNeedUpdateToServer.Keys.Contains(deckName))
						{
							decksNeedUpdateFromServer.Add(deckName, decks[i]);
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (!deckIdFound)
				{
					decksNeedUpload.Add(deckName, decks[i]);
				}
			}
			foreach (KeyValuePair<string, Deck> deck in decksNeedUpdateToServer)
			{
				Debug.LogFormat("卡组[{0}]需要更新上传。", new object[] { deck.Key });
				DateTime time = DateTime.UtcNow;
				Task<bool> task = OnlineDeck.SyncDeck(deck.Value.deckId, Path.GetFileName(deck.Key), deck.Value, time, false);
				while (!task.IsCompleted)
				{
					yield return null;
				}
				task = null;
			}
			Dictionary<string, Deck>.Enumerator enumerator = default(Dictionary<string, Deck>.Enumerator);
			foreach (KeyValuePair<string, Deck> deck2 in decksNeedUpdateFromServer)
			{
				Debug.LogFormat("卡组[{0}]需要更新。", new object[] { deck2.Key });
				OnlineDeck.OnlineDeckData od2 = OnlineDeck.GetByID(deck2.Value.deckId);
				string oldPath = "Deck/" + deck2.Key + ".ydk";
				if (Path.GetFileName(deck2.Key) != od2.deckName || deck2.Value.type != od2.GetType())
				{
					File.Delete(oldPath);
				}
				string newPath = "Deck/" + ((od2.GetType() == string.Empty) ? string.Empty : (od2.GetType() + "/")) + od2.deckName + ".ydk";
				if (!Directory.Exists(Path.GetDirectoryName(newPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(newPath));
				}
				File.WriteAllText(newPath, od2.deckYdk);
				File.SetLastWriteTimeUtc(newPath, od2.GetUpdateUtcTime());
			}
			foreach (OnlineDeck.OnlineDeckData deck3 in OnlineDeck.decks.Where((OnlineDeck.OnlineDeckData od) => !od.isDelete && !localFoundIds.Contains(od.deckId)))
			{
				if (File.Exists("Deck/" + ((deck3.GetType() == string.Empty) ? string.Empty : (deck3.GetType() + "/")) + deck3.deckName + ".ydk"))
				{
					Debug.Log(string.Concat(new string[]
					{
						"删除服务器同名卡组 [",
						deck3.GetType(),
						"/",
						deck3.deckName,
						"]  [",
						deck3.deckId,
						"]。"
					}));
					OnlineDeck.DeleteDecks(new List<string> { deck3.deckId });
				}
				else
				{
					Debug.Log(string.Concat(new string[]
					{
						"卡组[",
						deck3.GetType(),
						" / ",
						deck3.deckName,
						"] [",
						deck3.deckId,
						"]需要下载。"
					}));
					new Deck(deck3.deckYdk, deck3.deckId, MyCard.account.user.username)
					{
						type = deck3.GetType()
					}.Save(Path.GetFileName(deck3.deckName), deck3.GetUpdateUtcTime(), false, true);
				}
			}
			if (decksNeedUpload.Count > 0)
			{
				List<Deck> decksToUp = new List<Deck>();
				List<string> deckNames = new List<string>();
				foreach (KeyValuePair<string, Deck> deck4 in decksNeedUpload)
				{
					Debug.Log(string.Format("卡组[{0}]需要上传：{1}。", deck4.Key, deck4.Value.deckId));
					deckNames.Add(Path.GetFileName(deck4.Key));
					decksToUp.Add(deck4.Value);
				}
				Task<bool> task = OnlineDeck.UploadDecks(decksToUp, deckNames);
				while (!task.IsCompleted)
				{
					yield return null;
				}
				task = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06008DF5 RID: 36341 RVA: 0x0012E934 File Offset: 0x0012CB34
		public void EntertainMatch()
		{
			if (OnlineServant.entertainMatch == null)
			{
				base.StartCoroutine(OnlineServant.entertainMatch = this.EntertainMatchAsync());
				if (OnlineServant.athleticMatch != null)
				{
					base.StopCoroutine(OnlineServant.athleticMatch);
					OnlineServant.athleticMatch = null;
				}
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonAMatch.SetButtonText(InterString.Get("竞技匹配", 0));
				return;
			}
			this.GetUI<OnlineServantUI>().PageMyCard.ButtonEMatch.SetButtonText(InterString.Get("娱乐匹配", 0));
			base.StopCoroutine(OnlineServant.entertainMatch);
			OnlineServant.entertainMatch = null;
		}

		// Token: 0x06008DF6 RID: 36342 RVA: 0x0012E9C8 File Offset: 0x0012CBC8
		public void AthleticMatch()
		{
			if (OnlineServant.athleticMatch == null)
			{
				base.StartCoroutine(OnlineServant.athleticMatch = this.AthleticMatchAsync());
				if (OnlineServant.entertainMatch != null)
				{
					base.StopCoroutine(OnlineServant.entertainMatch);
					OnlineServant.entertainMatch = null;
				}
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonEMatch.SetButtonText(InterString.Get("娱乐匹配", 0));
				return;
			}
			this.GetUI<OnlineServantUI>().PageMyCard.ButtonAMatch.SetButtonText(InterString.Get("竞技匹配", 0));
			base.StopCoroutine(OnlineServant.athleticMatch);
			OnlineServant.athleticMatch = null;
		}

		// Token: 0x06008DF7 RID: 36343 RVA: 0x0012EA59 File Offset: 0x0012CC59
		private IEnumerator EntertainMatchAsync()
		{
			Task<MyCardMatchInfo> task = MyCard.GetMatchInfo("entertain");
			DateTime startTime = DateTime.Now;
			while (!task.IsCompleted)
			{
				double totalSeconds = (DateTime.Now - startTime).TotalSeconds;
				int minutes = (int)Math.Floor(totalSeconds / 60.0);
				int seconds = (int)(totalSeconds % 60.0);
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonEMatch.SetButtonText(string.Format("{0:D2}:{1:D2}", minutes, seconds));
				yield return new WaitForSeconds(0.5f);
			}
			if (task.Result != null)
			{
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonEMatch.SetButtonText(InterString.Get("娱乐匹配", 0));
				TcpHelper.LinkStart(task.Result.address, MyCard.account.user.username, task.Result.port.ToString(), task.Result.password, false, null);
			}
			else
			{
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonEMatch.SetButtonText(InterString.Get("匹配失败", 0));
			}
			OnlineServant.entertainMatch = null;
			yield break;
		}

		// Token: 0x06008DF8 RID: 36344 RVA: 0x0012EA68 File Offset: 0x0012CC68
		private IEnumerator AthleticMatchAsync()
		{
			Task<MyCardMatchInfo> task = MyCard.GetMatchInfo("athletic");
			DateTime startTime = DateTime.Now;
			while (!task.IsCompleted)
			{
				double totalSeconds = (DateTime.Now - startTime).TotalSeconds;
				int minutes = (int)Math.Floor(totalSeconds / 60.0);
				int seconds = (int)(totalSeconds % 60.0);
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonAMatch.SetButtonText(string.Format("{0:D2}:{1:D2}", minutes, seconds));
				yield return new WaitForSeconds(0.5f);
			}
			if (task.Result != null)
			{
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonAMatch.SetButtonText(InterString.Get("竞技匹配", 0));
				TcpHelper.LinkStart(task.Result.address, MyCard.account.user.username, task.Result.port.ToString(), task.Result.password, false, null);
			}
			else
			{
				this.GetUI<OnlineServantUI>().PageMyCard.ButtonAMatch.SetButtonText(InterString.Get("匹配失败", 0));
			}
			OnlineServant.athleticMatch = null;
			yield break;
		}

		// Token: 0x06008DF9 RID: 36345 RVA: 0x0012EA77 File Offset: 0x0012CC77
		public void SetWatchRooms(List<MyCardRoom> rooms)
		{
			this.GetUI<OnlineServantUI>().PageMyCard.WatchList.SetRooms(rooms);
		}

		// Token: 0x06008DFA RID: 36346 RVA: 0x0012EA8F File Offset: 0x0012CC8F
		public void CreateWatchRoom(MyCardRoom room)
		{
			this.GetUI<OnlineServantUI>().PageMyCard.WatchList.CreateRoom(room);
		}

		// Token: 0x06008DFB RID: 36347 RVA: 0x0012EAA7 File Offset: 0x0012CCA7
		public void UpdateWatchRoom(MyCardRoom room)
		{
			this.GetUI<OnlineServantUI>().PageMyCard.WatchList.UpdateRoom(room);
		}

		// Token: 0x06008DFC RID: 36348 RVA: 0x0012EABF File Offset: 0x0012CCBF
		public void DeleteWatchRoom(string roomId)
		{
			this.GetUI<OnlineServantUI>().PageMyCard.WatchList.DeleteRoom(roomId);
		}

		// Token: 0x06008DFD RID: 36349 RVA: 0x0012EAD7 File Offset: 0x0012CCD7
		public void ClearWatchList()
		{
			this.GetUI<OnlineServantUI>().PageMyCard.WatchList.Clear();
		}

		// Token: 0x0400CC01 RID: 52225
		[HideInInspector]
		public SelectionToggle_Address lastSelectedAddressItem;

		// Token: 0x0400CC02 RID: 52226
		[HideInInspector]
		public SelectionToggle_Watch lastSelectedWatchItem;

		// Token: 0x0400CC03 RID: 52227
		public const int DEFAULT_TIME = 180;

		// Token: 0x0400CC04 RID: 52228
		public const int DEFAULT_PORT = 7911;

		// Token: 0x0400CC05 RID: 52229
		public const int DEFAULT_LP = 8000;

		// Token: 0x0400CC06 RID: 52230
		public const int DEFAULT_HAND = 5;

		// Token: 0x0400CC07 RID: 52231
		public const int DEFAULT_DRAW = 1;

		// Token: 0x0400CC08 RID: 52232
		private float timeError = 3f;

		// Token: 0x0400CC09 RID: 52233
		public static IEnumerator entertainMatch;

		// Token: 0x0400CC0A RID: 52234
		public static IEnumerator athleticMatch;
	}
}
