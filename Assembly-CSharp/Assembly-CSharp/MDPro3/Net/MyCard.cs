using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MDPro3.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityWebSocket;

namespace MDPro3.Net
{
	// Token: 0x02001311 RID: 4881
	public static class MyCard
	{
		// Token: 0x06008EE1 RID: 36577 RVA: 0x001327C8 File Offset: 0x001309C8
		public static async Task<MyCardAccount> Login(string account, string password)
		{
			string json = JsonConvert.SerializeObject(new { account, password });
			MyCardAccount myCardAccount;
			using (UnityWebRequest request = UnityWebRequest.Post("https://sapi.moecube.com:444/accounts/signin", json, "application/json"))
			{
				request.SetRequestHeader("Content-Type", "application/json");
				request.SetRequestHeader("Origin", "https://accounts.moecube.com");
				request.SetRequestHeader("Referer", "https://accounts.moecube.com/");
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					myCardAccount = null;
				}
				else if (request.result == UnityWebRequest.Result.Success)
				{
					MyCard.account = JsonUtility.FromJson<MyCardAccount>(request.downloadHandler.text);
					Task<OnlineDeck.OnlineDeckData[]> response = OnlineDeck.GetAllDecks();
					await TaskUtility.WaitUntil(() => response.IsCompleted);
					if (!Application.isPlaying)
					{
						myCardAccount = null;
					}
					else
					{
						myCardAccount = MyCard.account;
					}
				}
				else
				{
					myCardAccount = new MyCardAccount
					{
						user = new MyCardUserInfo(),
						user = 
						{
							username = JsonUtility.FromJson<MyCardMessage>(request.downloadHandler.text).message
						}
					};
				}
			}
			return myCardAccount;
		}

		// Token: 0x06008EE2 RID: 36578 RVA: 0x00132814 File Offset: 0x00130A14
		public static async Task<MyCardAccount> TokenIn(string token)
		{
			MyCardAccount myCardAccount;
			using (UnityWebRequest request = UnityWebRequest.Get("https://sapi.moecube.com:444/accounts/authUser"))
			{
				request.SetRequestHeader("Authorization", "Bearer " + token);
				await request.SendWebRequest();
				if (request.result == UnityWebRequest.Result.Success)
				{
					MyCardUserInfo user = JsonUtility.FromJson<MyCardUserInfo>(request.downloadHandler.text);
					MyCard.account = new MyCardAccount
					{
						user = user,
						token = token
					};
					Task<OnlineDeck.OnlineDeckData[]> response = OnlineDeck.GetAllDecks();
					await response;
					await TaskUtility.WaitUntil(() => response.IsCompleted);
					if (!Application.isPlaying)
					{
						myCardAccount = null;
					}
					else
					{
						myCardAccount = MyCard.account;
					}
				}
				else
				{
					MyCardAccount myCardAccount2 = new MyCardAccount();
					myCardAccount2.user = new MyCardUserInfo();
					if (JsonUtility.FromJson<MyCardMessage>(request.downloadHandler.text) != null)
					{
						myCardAccount2.user.username = JsonUtility.FromJson<MyCardMessage>(request.downloadHandler.text).message;
					}
					else
					{
						myCardAccount2.user.username = InterString.Get("请检查网络连接", 0);
					}
					myCardAccount = myCardAccount2;
				}
			}
			return myCardAccount;
		}

		// Token: 0x06008EE3 RID: 36579 RVA: 0x00132858 File Offset: 0x00130A58
		public static async UniTask<Texture2D> GetAvatarAsync(string userName)
		{
			if (!Directory.Exists("Picture/MyCardAvatars/"))
			{
				Directory.CreateDirectory("Picture/MyCardAvatars/");
			}
			string fullPath = string.Empty;
			string avatarName = string.Empty;
			bool cached = false;
			Dictionary<string, string> dictionary = MyCard.cachedAvatarAddress;
			lock (dictionary)
			{
				if (MyCard.cachedAvatarAddress.TryGetValue(userName, out avatarName))
				{
					fullPath = "Picture/MyCardAvatars/" + avatarName + ".png";
					cached = true;
				}
			}
			Texture2D texture2D;
			if (cached)
			{
				Dictionary<string, Texture2D> dictionary2 = MyCard.cachedAvatars;
				lock (dictionary2)
				{
					if (MyCard.cachedAvatars.ContainsKey(avatarName))
					{
						return MyCard.cachedAvatars[avatarName];
					}
				}
				Texture2D pic = await TextureManager.LoadPicFromFileAsync(fullPath);
				dictionary2 = MyCard.cachedAvatars;
				lock (dictionary2)
				{
					if (!MyCard.cachedAvatars.ContainsKey(avatarName))
					{
						MyCard.cachedAvatars[avatarName] = pic;
					}
				}
				texture2D = pic;
			}
			else
			{
				string avatarAddress;
				using (UnityWebRequest request = UnityWebRequest.Get("https://sapi.moecube.com:444/accounts/users/{username}.json".Replace("{username}", userName)))
				{
					await request.SendWebRequest();
					if (request.result != UnityWebRequest.Result.Success)
					{
						Debug.LogError("Failed to get user info: " + request.error);
						return null;
					}
					avatarAddress = JsonUtility.FromJson<MyCardRoomUserInfo>(request.downloadHandler.text).user.avatar;
				}
				UnityWebRequest request = null;
				Task<Texture2D> requestAvatar = Tools.DownloadImageAsync(avatarAddress);
				await requestAvatar;
				Texture2D downloadImage = requestAvatar.Result;
				if (downloadImage == null)
				{
					texture2D = null;
				}
				else
				{
					Texture2D returnValue = downloadImage;
					try
					{
						if (downloadImage != null)
						{
							string fileName = Path.GetFileNameWithoutExtension(avatarAddress);
							fullPath = "Picture/MyCardAvatars/" + fileName + ".png";
							if (downloadImage.width > 256)
							{
								returnValue = TextureManager.ResizeTexture2D(downloadImage, 256, 256);
								global::UnityEngine.Object.Destroy(downloadImage);
							}
							File.WriteAllBytes(fullPath, returnValue.EncodeToPNG());
							dictionary = MyCard.cachedAvatarAddress;
							lock (dictionary)
							{
								if (!MyCard.cachedAvatarAddress.ContainsKey(userName))
								{
									MyCard.cachedAvatarAddress[userName] = fileName;
								}
							}
							Dictionary<string, Texture2D> dictionary2 = MyCard.cachedAvatars;
							lock (dictionary2)
							{
								if (!MyCard.cachedAvatars.ContainsKey(avatarName))
								{
									MyCard.cachedAvatars[avatarName] = returnValue;
								}
							}
						}
					}
					catch
					{
					}
					texture2D = returnValue;
				}
			}
			return texture2D;
		}

		// Token: 0x06008EE4 RID: 36580 RVA: 0x0013289C File Offset: 0x00130A9C
		public static async Task<MyCardNews> GetNews()
		{
			MyCardNews myCardNews;
			using (UnityWebRequest request = UnityWebRequest.Get("https://cdntx.moecube.com/apps.json"))
			{
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					myCardNews = null;
				}
				else if (request.result == UnityWebRequest.Result.Success)
				{
					foreach (MyCardApp app in JsonConvert.DeserializeObject<MyCardApp[]>(request.downloadHandler.text.Replace("\"news\":[]", "\"news\":{}")))
					{
						if (app.id == "ygopro")
						{
							MyCard.ygopro = app;
							return app.news;
						}
					}
					myCardNews = null;
				}
				else
				{
					Debug.LogError("Failed to get apps: " + request.error);
					myCardNews = null;
				}
			}
			return myCardNews;
		}

		// Token: 0x06008EE5 RID: 36581 RVA: 0x001328D8 File Offset: 0x00130AD8
		public static async Task<MyCardUserExp> GetExp()
		{
			MyCardUserExp myCardUserExp;
			if (MyCard.account == null)
			{
				myCardUserExp = null;
			}
			else
			{
				using (UnityWebRequest request = UnityWebRequest.Get("https://sapi.moecube.com:444/ygopro/arena/user?username=" + MyCard.account.user.username))
				{
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						myCardUserExp = null;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						myCardUserExp = JsonConvert.DeserializeObject<MyCardUserExp>(request.downloadHandler.text);
					}
					else
					{
						Debug.LogError("Failed to get Exp: " + request.error);
						myCardUserExp = null;
					}
				}
			}
			return myCardUserExp;
		}

		// Token: 0x06008EE6 RID: 36582 RVA: 0x00132914 File Offset: 0x00130B14
		public static async Task<MyCardMatchInfo> GetMatchInfo(string arena)
		{
			MyCardMatchInfo myCardMatchInfo;
			using (UnityWebRequest request = UnityWebRequest.PostWwwForm("https://sapi.moecube.com:444/ygopro/match?arena=" + arena, "application/json"))
			{
				int u16Secret = await MyCard.GetUserU16SecretAsync();
				request.SetRequestHeader("Content-Type", "application/json");
				request.SetRequestHeader("Authorization", "Basic " + MyCard.CustomBase64Encode(MyCard.account.user.username + ":" + u16Secret.ToString()));
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					myCardMatchInfo = null;
				}
				else if (request.result == UnityWebRequest.Result.Success)
				{
					myCardMatchInfo = JsonUtility.FromJson<MyCardMatchInfo>(request.downloadHandler.text);
				}
				else
				{
					Debug.LogError(arena + " Match error: " + request.error);
					myCardMatchInfo = null;
				}
			}
			return myCardMatchInfo;
		}

		// Token: 0x06008EE7 RID: 36583 RVA: 0x00132957 File Offset: 0x00130B57
		private static string CustomBase64Encode(string input)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}

		// Token: 0x06008EE8 RID: 36584 RVA: 0x0013296C File Offset: 0x00130B6C
		public static void ConnectToAthleticWatchListWebSocket()
		{
			MyCard.socket = new WebSocket("wss://tiramisu.moenext.com:8923?filter=started");
			MyCard.socket.OnOpen += MyCard.OnSocketOpen;
			MyCard.socket.OnClose += MyCard.OnSocketClose;
			MyCard.socket.OnMessage += MyCard.OnSocketMessage;
			MyCard.socket.OnError += MyCard.OnSocketError;
			MyCard.socket.ConnectAsync();
		}

		// Token: 0x06008EE9 RID: 36585 RVA: 0x001329EC File Offset: 0x00130BEC
		public static void CloseAthleticWatchListWebSocket()
		{
			try
			{
				WebSocket webSocket = MyCard.socket;
				if (webSocket != null)
				{
					webSocket.CloseAsync();
				}
				Program.instance.online.ClearWatchList();
			}
			catch
			{
			}
			MyCard.socket = null;
		}

		// Token: 0x06008EEA RID: 36586 RVA: 0x00132A34 File Offset: 0x00130C34
		private static void OnSocketOpen(object sender, OpenEventArgs e)
		{
			Debug.Log(string.Format("Socket Connected: {0}", "wss://tiramisu.moenext.com:8923?filter=started"));
		}

		// Token: 0x06008EEB RID: 36587 RVA: 0x00132A4A File Offset: 0x00130C4A
		private static void OnSocketClose(object sender, CloseEventArgs e)
		{
			Debug.Log(string.Format("Socket Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
		}

		// Token: 0x06008EEC RID: 36588 RVA: 0x00132A6C File Offset: 0x00130C6C
		private static void OnSocketError(object sender, global::UnityWebSocket.ErrorEventArgs e)
		{
			Debug.Log(string.Format("Socket Error: {0}", e.Message));
		}

		// Token: 0x06008EED RID: 36589 RVA: 0x00132A83 File Offset: 0x00130C83
		private static void OnSocketMessage(object sender, MessageEventArgs e)
		{
			MyCard.HandleWatchInfo(e.Data);
		}

		// Token: 0x06008EEE RID: 36590 RVA: 0x00132A90 File Offset: 0x00130C90
		private static void HandleWatchInfo(string json)
		{
			MyCardWatchInfo info = JsonConvert.DeserializeObject<MyCardWatchInfo>(json);
			MyCard.currentEventType = info.eventType;
			string text = MyCard.currentEventType;
			if (!(text == "init") && !(text == "create") && !(text == "update"))
			{
				if (!(text == "delete"))
				{
					Debug.LogWarning("Unknown event type: " + info.eventType);
					return;
				}
				MyCard.HandleDelete(info.data as string);
				return;
			}
			else
			{
				JToken roomsOrSingleRoom = JsonConvert.DeserializeObject<JToken>(info.data.ToString());
				JArray array = roomsOrSingleRoom as JArray;
				if (array != null)
				{
					MyCard.HandleRooms(array);
					return;
				}
				MyCard.HandleSingleRoom((JObject)roomsOrSingleRoom);
				return;
			}
		}

		// Token: 0x06008EEF RID: 36591 RVA: 0x00132B40 File Offset: 0x00130D40
		private static void HandleRooms(JArray rooms)
		{
			List<MyCardRoom> list = new List<MyCardRoom>();
			foreach (JToken jtoken in rooms)
			{
				MyCardRoom roomInstance = jtoken.ToObject<MyCardRoom>();
				list.Add(roomInstance);
			}
			Program.instance.online.SetWatchRooms(list);
		}

		// Token: 0x06008EF0 RID: 36592 RVA: 0x00132BA4 File Offset: 0x00130DA4
		private static void HandleSingleRoom(JObject room)
		{
			MyCardRoom roomInstance = room.ToObject<MyCardRoom>();
			string text = MyCard.currentEventType;
			if (text == "create")
			{
				Program.instance.online.CreateWatchRoom(roomInstance);
				return;
			}
			if (!(text == "update"))
			{
				return;
			}
			Program.instance.online.UpdateWatchRoom(roomInstance);
		}

		// Token: 0x06008EF1 RID: 36593 RVA: 0x00132BFA File Offset: 0x00130DFA
		private static void HandleDelete(string roomId)
		{
			Program.instance.online.DeleteWatchRoom(roomId);
		}

		// Token: 0x06008EF2 RID: 36594 RVA: 0x00132C0C File Offset: 0x00130E0C
		public static async UniTask<string> GetJoinRoomPassword(MyCardRoomOptions options, string roomId, int userId, bool _private = false)
		{
			byte[] optionsBuffer = new byte[6];
			optionsBuffer[1] = (byte)((_private ? 5 : 3) << 4);
			await MyCard.EncryptBuffer(optionsBuffer);
			return Convert.ToBase64String(optionsBuffer) + roomId;
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x00132C58 File Offset: 0x00130E58
		public static async Task<string> GetCreateRoomPasswd(MyCardRoomOptions options, string roomID, int userId, bool _private = false)
		{
			byte[] optionsBuffer = new byte[6];
			optionsBuffer[1] = (byte)(((int)(_private ? 2 : 1) << 4) | (int)((byte)(options.duel_rule << 1)) | (options.auto_death ? 1 : 0));
			optionsBuffer[2] = (byte)(((int)((byte)options.rule) << 5) | ((int)((byte)options.mode) << 3) | (options.no_check_deck ? 2 : 0) | (options.no_shuffle_deck ? 1 : 0));
			MyCard.WriteUInt16LE(optionsBuffer, 3, (ushort)options.start_lp);
			optionsBuffer[5] = (byte)(((int)((byte)options.start_hand) << 4) | options.draw_count);
			await MyCard.EncryptBuffer(optionsBuffer);
			return Convert.ToBase64String(optionsBuffer) + roomID;
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x00132CAC File Offset: 0x00130EAC
		private static async UniTask EncryptBuffer(byte[] buffer)
		{
			int checksum = 0;
			for (int i = 1; i < buffer.Length; i++)
			{
				checksum -= (int)buffer[i];
			}
			buffer[0] = (byte)(checksum & 255);
			UniTask<int>.Awaiter awaiter = MyCard.GetUserU16SecretAsync().GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				UniTask<int>.Awaiter awaiter2;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<int>.Awaiter);
			}
			int secret = awaiter.GetResult() % 65535 + 1;
			for (int j = 0; j < buffer.Length; j += 2)
			{
				MyCard.WriteUInt16LE(buffer, j, (ushort)((int)MyCard.ReadUInt16LE(buffer, j) ^ secret));
			}
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x00132CEF File Offset: 0x00130EEF
		public static int GetPrivateRoomID(int external_id)
		{
			return external_id ^ 344865;
		}

		// Token: 0x06008EF6 RID: 36598 RVA: 0x00132CF8 File Offset: 0x00130EF8
		private static ushort ReadUInt16LE(byte[] buffer, int offset)
		{
			return (ushort)(((int)buffer[offset + 1] << 8) | (int)buffer[offset]);
		}

		// Token: 0x06008EF7 RID: 36599 RVA: 0x00132D06 File Offset: 0x00130F06
		private static void WriteUInt16LE(byte[] buffer, int offset, ushort value)
		{
			buffer[offset] = (byte)(value & 255);
			buffer[offset + 1] = (byte)((value >> 8) & 255);
		}

		// Token: 0x06008EF8 RID: 36600 RVA: 0x00132D24 File Offset: 0x00130F24
		private static async UniTask<int> GetUserU16SecretAsync()
		{
			if (MyCard.account == null)
			{
				MyCard.<GetUserU16SecretAsync>g__Bad|48_0("No account info");
			}
			if (string.IsNullOrEmpty(MyCard.account.token))
			{
				MyCard.<GetUserU16SecretAsync>g__Bad|48_0("no token");
			}
			string token = MyCard.account.token;
			int num;
			using (UnityWebRequest request = UnityWebRequest.Get("https://sapi.moecube.com:444/accounts/authUser"))
			{
				request.SetRequestHeader("Authorization", "Bearer " + token);
				request.SetRequestHeader("Content-Type", "application/json");
				await request.SendWebRequest();
				if (request.result != UnityWebRequest.Result.Success)
				{
					MyCard.<GetUserU16SecretAsync>g__Bad|48_0(request.error);
					num = 0;
				}
				else
				{
					try
					{
						MyCard.AuthResponse authInfo = JsonUtility.FromJson<MyCard.AuthResponse>(request.downloadHandler.text);
						if (authInfo == null || authInfo.u16Secret == 0)
						{
							MyCard.<GetUserU16SecretAsync>g__Bad|48_0("no secret or invalid response");
							num = 0;
						}
						else
						{
							num = authInfo.u16Secret;
						}
					}
					catch (Exception ex)
					{
						MyCard.<GetUserU16SecretAsync>g__Bad|48_0(ex.ToString());
						num = 0;
					}
				}
			}
			return num;
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x00132D75 File Offset: 0x00130F75
		[CompilerGenerated]
		internal static void <GetUserU16SecretAsync>g__Bad|48_0(string message)
		{
			MessageManager.Cast(InterString.Get("MyCard: 获取用户密钥失败。请重新登录。([?])", message, 0));
			throw new Exception("Get U16 secret failed: " + message);
		}

		// Token: 0x0400CC8B RID: 52363
		public const string duelUrl = "tiramisu.moenext.com";

		// Token: 0x0400CC8C RID: 52364
		public const int entertainPort = 7911;

		// Token: 0x0400CC8D RID: 52365
		public const int athleticPort = 8911;

		// Token: 0x0400CC8E RID: 52366
		private const string loginUrl = "https://sapi.moecube.com:444/accounts/signin";

		// Token: 0x0400CC8F RID: 52367
		private const string authUrl = "https://sapi.moecube.com:444/accounts/authUser";

		// Token: 0x0400CC90 RID: 52368
		private const string appsUrl = "https://cdntx.moecube.com/apps.json";

		// Token: 0x0400CC91 RID: 52369
		private const string expUrl = "https://sapi.moecube.com:444/ygopro/arena/user?username=";

		// Token: 0x0400CC92 RID: 52370
		private const string matchUrl = "https://sapi.moecube.com:444/ygopro/match";

		// Token: 0x0400CC93 RID: 52371
		private const string userUrl = "https://sapi.moecube.com:444/accounts/users/{username}.json";

		// Token: 0x0400CC94 RID: 52372
		private const string athleticWatchUrl = "wss://tiramisu.moenext.com:8923?filter=started";

		// Token: 0x0400CC95 RID: 52373
		private const string entertainWatchUrl = "wss://tiramisu.moenext.com:7923?filter=started";

		// Token: 0x0400CC96 RID: 52374
		private const string contentTypeHeader = "Content-Type";

		// Token: 0x0400CC97 RID: 52375
		private const string jsonHeader = "application/json";

		// Token: 0x0400CC98 RID: 52376
		private const string authHeader = "Authorization";

		// Token: 0x0400CC99 RID: 52377
		public static MyCardAccount account;

		// Token: 0x0400CC9A RID: 52378
		public static MyCardApp ygopro;

		// Token: 0x0400CC9B RID: 52379
		public static Texture2D avatar;

		// Token: 0x0400CC9C RID: 52380
		private const string avatarSavePath = "Picture/MyCardAvatars/";

		// Token: 0x0400CC9D RID: 52381
		private static readonly Dictionary<string, string> cachedAvatarAddress = new Dictionary<string, string>();

		// Token: 0x0400CC9E RID: 52382
		private static readonly Dictionary<string, Texture2D> cachedAvatars = new Dictionary<string, Texture2D>();

		// Token: 0x0400CC9F RID: 52383
		private const int avatarSize = 256;

		// Token: 0x0400CCA0 RID: 52384
		private static string currentEventType;

		// Token: 0x0400CCA1 RID: 52385
		private static WebSocket socket;

		// Token: 0x02001312 RID: 4882
		public enum RoomAction
		{
			// Token: 0x0400CCA3 RID: 52387
			CreatePublic = 1,
			// Token: 0x0400CCA4 RID: 52388
			CreatePrivate,
			// Token: 0x0400CCA5 RID: 52389
			JoinPublic,
			// Token: 0x0400CCA6 RID: 52390
			JoinPrivate = 5
		}

		// Token: 0x02001313 RID: 4883
		[Serializable]
		private class AuthResponse
		{
			// Token: 0x0400CCA7 RID: 52391
			public int u16Secret;
		}
	}
}
