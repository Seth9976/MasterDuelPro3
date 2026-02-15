using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.Networking;

namespace MDPro3.Net
{
	// Token: 0x02001335 RID: 4917
	public static class OnlineDeck
	{
		// Token: 0x06008F37 RID: 36663 RVA: 0x00134644 File Offset: 0x00132844
		public static async Task<OnlineDeck.OnlineDeckData[]> FetchSimpleDeckList(int size, string keyWord = "", string contributor = "", bool sortLike = true)
		{
			string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/deck/list/lite" + string.Format("?size={0}&keyWord={1}&contributor={2}&sortLike={3}", new object[] { size, keyWord, contributor, sortLike });
			OnlineDeck.OnlineDeckData[] array;
			using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
			{
				request.SetRequestHeader("ReqSource", "MDPro3");
				try
				{
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						array = null;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						array = JsonUtility.FromJson<OnlineDeck.ResponseMultiSimpleData>(request.downloadHandler.text).data;
					}
					else
					{
						MessageManager.Cast("FetchSimpleDeckList Error : " + request.error);
						array = null;
					}
				}
				catch (Exception e)
				{
					Debug.Log("FetchSimpleDeckList Error: " + ((e != null) ? e.ToString() : null));
					array = null;
				}
				finally
				{
					request.Dispose();
					if (request.downloadHandler != null)
					{
						request.downloadHandler.Dispose();
					}
				}
			}
			return array;
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x001346A0 File Offset: 0x001328A0
		public static async Task<OnlineDeck.OnlineDeckData> GetDeck(string deckID)
		{
			string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/deck/" + deckID;
			OnlineDeck.OnlineDeckData onlineDeckData;
			using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
			{
				request.SetRequestHeader("ReqSource", "MDPro3");
				try
				{
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						onlineDeckData = null;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						onlineDeckData = JsonUtility.FromJson<OnlineDeck.ResponseSingleData>(request.downloadHandler.text).data;
					}
					else
					{
						MessageManager.Cast("FetchSimpleDeckList Error: " + request.error);
						onlineDeckData = null;
					}
				}
				catch (Exception e)
				{
					Debug.Log("FetchSimpleDeckList Error: " + ((e != null) ? e.ToString() : null));
					onlineDeckData = null;
				}
				finally
				{
					request.Dispose();
					if (request.downloadHandler != null)
					{
						request.downloadHandler.Dispose();
					}
				}
			}
			return onlineDeckData;
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x001346E4 File Offset: 0x001328E4
		public static async Task<OnlineDeck.OnlineDeckData[]> GetAllDecks()
		{
			OnlineDeck.OnlineDeckData[] array;
			if (MyCard.account == null)
			{
				array = null;
			}
			else
			{
				int userId = MyCard.account.user.id;
				string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/sync/" + userId.ToString();
				using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
				{
					request.SetRequestHeader("ReqSource", "MDPro3");
					request.SetRequestHeader("token", MyCard.account.token);
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						array = null;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						OnlineDeck.decks = JsonUtility.FromJson<OnlineDeck.ResponseMultiSimpleData>(request.downloadHandler.text).data;
						array = OnlineDeck.decks;
					}
					else
					{
						MessageManager.Cast(InterString.Get("获取MyCard卡组失败：", 0) + request.error);
						array = null;
					}
				}
			}
			return array;
		}

		// Token: 0x06008F3A RID: 36666 RVA: 0x00134720 File Offset: 0x00132920
		public static async void LikeDeck(string deckId)
		{
			string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/deck/like/" + deckId;
			using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, "application/json"))
			{
				request.SetRequestHeader("ReqSource", "MDPro3");
				request.SetRequestHeader("Content-Type", "application/json");
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (Application.isPlaying)
				{
					if (request.result == UnityWebRequest.Result.Success)
					{
						OnlineDeck.ResponseSingleData responseData = JsonUtility.FromJson<OnlineDeck.ResponseSingleData>(request.downloadHandler.text);
						if (responseData.code == 0)
						{
							MessageManager.Cast(InterString.Get("点赞卡组成功。", 0));
						}
						else
						{
							MessageManager.Cast(InterString.Get("点赞卡组失败：", 0) + responseData.message);
						}
					}
					else
					{
						MessageManager.Cast(InterString.Get("点赞卡组失败：", 0) + request.error);
					}
				}
			}
		}

		// Token: 0x06008F3B RID: 36667 RVA: 0x00134758 File Offset: 0x00132958
		public static async Task<bool> UploadDecks(List<Deck> decks, List<string> deckNames)
		{
			string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/deck/deckIds?count=" + decks.Count.ToString();
			bool flag;
			using (UnityWebRequest getIDs = UnityWebRequest.Get(apiUrl))
			{
				getIDs.SetRequestHeader("ReqSource", "MDPro3");
				UnityWebRequestAsyncOperation send = getIDs.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					flag = false;
				}
				else if (getIDs.result == UnityWebRequest.Result.Success)
				{
					string[] ids = JsonUtility.FromJson<OnlineDeck.ResponseDeckIDs>(getIDs.downloadHandler.text).data;
					apiUrl = "http://rarnu.xyz:38383/api/mdpro3/sync/multi";
					OnlineDeck.PostAllDecksBody body = new OnlineDeck.PostAllDecksBody
					{
						deckContributor = MyCard.account.user.username,
						userId = MyCard.account.user.id,
						decks = new OnlineDeck.PostDeck[decks.Count]
					};
					for (int i = 0; i < decks.Count; i++)
					{
						string oldName = deckNames[i];
						string newName = deckNames[i];
						if (OnlineDeck.DeckNameExist(oldName, decks[i].type))
						{
							newName = newName + " - " + InterString.Get("复制", 0);
							while (File.Exists("Deck/" + ((decks[i].type == string.Empty) ? string.Empty : (decks[i].type + "/")) + newName + ".ydk"))
							{
								newName = newName + " - " + InterString.Get("复制", 0);
							}
							File.Delete("Deck/" + ((decks[i].type == string.Empty) ? string.Empty : (decks[i].type + "/")) + oldName + ".ydk");
						}
						body.decks[i] = new OnlineDeck.PostDeck
						{
							deckId = ids[i],
							deckName = newName,
							deckType = decks[i].type,
							deckCoverCard1 = ((decks[i].Pickup.Count > 0) ? decks[i].Pickup[0] : 0),
							deckCoverCard2 = ((decks[i].Pickup.Count > 1) ? decks[i].Pickup[1] : 0),
							deckCoverCard3 = ((decks[i].Pickup.Count > 2) ? decks[i].Pickup[2] : 0),
							deckCase = decks[i].Case,
							deckProtector = decks[i].Protector,
							isDelete = false,
							deckYdk = decks[i].GetYDK()
						};
						if (body.decks[i].deckType == "/")
						{
							body.decks[i].deckType = string.Empty;
						}
						decks[i].deckId = ids[i];
						decks[i].userId = MyCard.account.user.id.ToString();
						decks[i].Save(newName, DateTime.UtcNow, false, true);
					}
					string json = JsonUtility.ToJson(body);
					using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, json, "application/json"))
					{
						request.SetRequestHeader("ReqSource", "MDPro3");
						request.SetRequestHeader("Content-Type", "application/json");
						request.SetRequestHeader("token", MyCard.account.token);
						send = request.SendWebRequest();
						await TaskUtility.WaitUntil(() => send.isDone);
						if (!Application.isPlaying)
						{
							flag = false;
						}
						else if (request.result == UnityWebRequest.Result.Success)
						{
							await OnlineDeck.GetAllDecks();
							flag = true;
						}
						else
						{
							MessageManager.Cast(InterString.Get("上传卡组失败：", 0) + request.error);
							flag = false;
						}
					}
				}
				else
				{
					MessageManager.Cast(InterString.Get("上传卡组失败：", 0) + getIDs.error);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06008F3C RID: 36668 RVA: 0x001347A4 File Offset: 0x001329A4
		public static async Task<bool> SyncDeck(string deckId, string deckName, Deck deck, DateTime time, bool showHint = true)
		{
			deck.deckId = deckId;
			deck.userId = MyCard.account.user.id.ToString();
			string ydk = deck.GetYDK();
			OnlineDeck.OnlineDeckData od = OnlineDeck.GetByID(deckId);
			bool flag;
			if (od == null || od.isDelete)
			{
				flag = await OnlineDeck.UploadDecks(new List<Deck> { deck }, new List<string> { deckName });
			}
			else
			{
				od.deckYdk = ydk;
				od.deckName = deckName;
				string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/sync/single";
				string json = JsonUtility.ToJson(new OnlineDeck.PostDeckBody
				{
					userId = MyCard.account.user.id,
					deckContributor = MyCard.account.user.username,
					deck = new OnlineDeck.PostDeck(deck, deckId, deckName, ydk)
				});
				using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, json, "application/json"))
				{
					request.SetRequestHeader("ReqSource", "MDPro3");
					request.SetRequestHeader("Content-Type", "application/json");
					request.SetRequestHeader("token", MyCard.account.token);
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						flag = false;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						JsonUtility.FromJson<OnlineDeck.SyncResponseSingleData>(request.downloadHandler.text);
						if (showHint)
						{
							MessageManager.Cast(InterString.Get("云端卡组「[?]」已同步。", deckName, 0));
						}
						deck.Save(deckName, DateTime.UtcNow, false, true);
						flag = true;
					}
					else
					{
						MessageManager.Cast(InterString.Get("云端卡组同步失败：", 0) + request.error);
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x00134800 File Offset: 0x00132A00
		public static async Task<bool> DeleteDecks(List<string> ids)
		{
			bool flag;
			if (ids == null || ids.Count == 0)
			{
				flag = false;
			}
			else
			{
				List<string> toDelete = new List<string>();
				foreach (string id in ids)
				{
					foreach (OnlineDeck.OnlineDeckData deck in OnlineDeck.decks)
					{
						if (deck.deckId == id)
						{
							toDelete.Add(id);
							deck.isDelete = true;
						}
					}
				}
				string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/sync/multi";
				OnlineDeck.PostAllDecksBody body = new OnlineDeck.PostAllDecksBody();
				body.deckContributor = MyCard.account.user.username;
				body.userId = MyCard.account.user.id;
				body.decks = new OnlineDeck.PostDeck[toDelete.Count];
				for (int i = 0; i < toDelete.Count; i++)
				{
					body.decks[i] = new OnlineDeck.PostDeck
					{
						deckId = toDelete[i],
						isDelete = true
					};
				}
				string json = JsonUtility.ToJson(body);
				using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, json, "application/json"))
				{
					request.SetRequestHeader("ReqSource", "MDPro3");
					request.SetRequestHeader("Content-Type", "application/json");
					request.SetRequestHeader("token", MyCard.account.token);
					UnityWebRequestAsyncOperation send = request.SendWebRequest();
					await TaskUtility.WaitUntil(() => send.isDone);
					if (!Application.isPlaying)
					{
						flag = false;
					}
					else if (request.result == UnityWebRequest.Result.Success)
					{
						await OnlineDeck.GetAllDecks();
						flag = true;
					}
					else
					{
						MessageManager.Cast(InterString.Get("删除云端卡组失败：", 0) + request.error);
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06008F3E RID: 36670 RVA: 0x00134844 File Offset: 0x00132A44
		public static async Task<bool> UpdatePublicState(string deckId, bool isPublic)
		{
			string apiUrl = "http://rarnu.xyz:38383/api/mdpro3/deck/public";
			string json = JsonUtility.ToJson(new OnlineDeck.PostPublicBody
			{
				deckId = deckId,
				isPublic = isPublic,
				userId = MyCard.account.user.id
			});
			bool flag;
			using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, json, "application/json"))
			{
				request.SetRequestHeader("ReqSource", "MDPro3");
				request.SetRequestHeader("Content-Type", "application/json");
				request.SetRequestHeader("token", MyCard.account.token);
				UnityWebRequestAsyncOperation send = request.SendWebRequest();
				await TaskUtility.WaitUntil(() => send.isDone);
				if (!Application.isPlaying)
				{
					flag = false;
				}
				else if (request.result == UnityWebRequest.Result.Success)
				{
					Debug.Log("UpdatePublicState Success: " + isPublic.ToString());
					flag = true;
				}
				else
				{
					Debug.Log("UpdatePublicState Failed: " + request.error);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06008F3F RID: 36671 RVA: 0x00134890 File Offset: 0x00132A90
		public static OnlineDeck.OnlineDeckData GetByID(string deckId)
		{
			if (OnlineDeck.decks == null)
			{
				return null;
			}
			foreach (OnlineDeck.OnlineDeckData deck in OnlineDeck.decks)
			{
				if (deck.deckId == deckId)
				{
					return deck;
				}
			}
			return null;
		}

		// Token: 0x06008F40 RID: 36672 RVA: 0x001348D0 File Offset: 0x00132AD0
		public static bool GetDeckPublicState(string deckId)
		{
			if (OnlineDeck.decks == null)
			{
				return false;
			}
			foreach (OnlineDeck.OnlineDeckData deck in OnlineDeck.decks)
			{
				if (deck.deckId == deckId)
				{
					return deck.isPublic;
				}
			}
			return false;
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x00134914 File Offset: 0x00132B14
		public static bool StringIsIdFormat(string deckId)
		{
			return !string.IsNullOrEmpty(deckId);
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x00134920 File Offset: 0x00132B20
		private static bool DeckNameExist(string deckName, string deckType)
		{
			return OnlineDeck.decks != null && OnlineDeck.decks.Any((OnlineDeck.OnlineDeckData deck) => !deck.isDelete && deck.deckName == deckName && deck.GetType() == deckType);
		}

		// Token: 0x0400CD39 RID: 52537
		public static OnlineDeck.OnlineDeckData[] decks;

		// Token: 0x0400CD3A RID: 52538
		private const string url = "http://rarnu.xyz:38383";

		// Token: 0x0400CD3B RID: 52539
		private const string liteAPI = "/api/mdpro3/deck/list/lite";

		// Token: 0x0400CD3C RID: 52540
		private const string getAPI = "/api/mdpro3/deck/";

		// Token: 0x0400CD3D RID: 52541
		private const string likeAPI = "/api/mdpro3/deck/like/";

		// Token: 0x0400CD3E RID: 52542
		private const string getAllAPI = "/api/mdpro3/sync/";

		// Token: 0x0400CD3F RID: 52543
		private const string syncAllAPI = "/api/mdpro3/sync/multi";

		// Token: 0x0400CD40 RID: 52544
		private const string getIdsAPI = "/api/mdpro3/deck/deckIds?count=";

		// Token: 0x0400CD41 RID: 52545
		private const string getIdAPI = "/api/mdpro3/deck/deckId";

		// Token: 0x0400CD42 RID: 52546
		private const string syncSigleAPI = "/api/mdpro3/sync/single";

		// Token: 0x0400CD43 RID: 52547
		private const string publicAPI = "/api/mdpro3/deck/public";

		// Token: 0x0400CD44 RID: 52548
		private const string reqHeader = "ReqSource";

		// Token: 0x0400CD45 RID: 52549
		private const string reqValue = "MDPro3";

		// Token: 0x0400CD46 RID: 52550
		private const string contentTypeHeader = "Content-Type";

		// Token: 0x0400CD47 RID: 52551
		private const string jsonHeader = "application/json";

		// Token: 0x0400CD48 RID: 52552
		private const string tokenHeader = "token";

		// Token: 0x02001336 RID: 4918
		[Serializable]
		public class OnlineDeckData
		{
			// Token: 0x06008F44 RID: 36676 RVA: 0x00134960 File Offset: 0x00132B60
			public DateTime GetUpdateUtcTime()
			{
				return DateTimeOffset.FromUnixTimeMilliseconds(this.deckUpdateDate - 28800000L).UtcDateTime;
			}

			// Token: 0x06008F45 RID: 36677 RVA: 0x00134988 File Offset: 0x00132B88
			public DateTime GetOnlineDeckLocalTime()
			{
				return DateTimeOffset.FromUnixTimeMilliseconds(this.lastDate).LocalDateTime;
			}

			// Token: 0x06008F46 RID: 36678 RVA: 0x001349A8 File Offset: 0x00132BA8
			public new string GetType()
			{
				if (string.IsNullOrEmpty(this.deckType) || this.deckType == "/")
				{
					return string.Empty;
				}
				return this.deckType;
			}

			// Token: 0x0400CD49 RID: 52553
			public string deckId;

			// Token: 0x0400CD4A RID: 52554
			public string deckContributor;

			// Token: 0x0400CD4B RID: 52555
			public string deckName;

			// Token: 0x0400CD4C RID: 52556
			public string deckType;

			// Token: 0x0400CD4D RID: 52557
			public int deckRank;

			// Token: 0x0400CD4E RID: 52558
			public int deckLike;

			// Token: 0x0400CD4F RID: 52559
			public long deckUploadDate;

			// Token: 0x0400CD50 RID: 52560
			public long deckUpdateDate;

			// Token: 0x0400CD51 RID: 52561
			public int deckCoverCard1;

			// Token: 0x0400CD52 RID: 52562
			public int deckCoverCard2;

			// Token: 0x0400CD53 RID: 52563
			public int deckCoverCard3;

			// Token: 0x0400CD54 RID: 52564
			public int deckCase;

			// Token: 0x0400CD55 RID: 52565
			public int deckProtector;

			// Token: 0x0400CD56 RID: 52566
			public string deckMainSerial;

			// Token: 0x0400CD57 RID: 52567
			public string deckYdk;

			// Token: 0x0400CD58 RID: 52568
			public int userid;

			// Token: 0x0400CD59 RID: 52569
			public bool isPublic;

			// Token: 0x0400CD5A RID: 52570
			public string description;

			// Token: 0x0400CD5B RID: 52571
			public bool isDelete;

			// Token: 0x0400CD5C RID: 52572
			public long lastDate;
		}

		// Token: 0x02001337 RID: 4919
		[Serializable]
		public class ResponseSingleData
		{
			// Token: 0x0400CD5D RID: 52573
			public int code;

			// Token: 0x0400CD5E RID: 52574
			public string message;

			// Token: 0x0400CD5F RID: 52575
			public OnlineDeck.OnlineDeckData data;
		}

		// Token: 0x02001338 RID: 4920
		[Serializable]
		public class ResponseMultiData
		{
			// Token: 0x0400CD60 RID: 52576
			public int code;

			// Token: 0x0400CD61 RID: 52577
			public string message;

			// Token: 0x0400CD62 RID: 52578
			public int data;
		}

		// Token: 0x02001339 RID: 4921
		[Serializable]
		public class SyncResponseSingleData
		{
			// Token: 0x0400CD63 RID: 52579
			public int code;

			// Token: 0x0400CD64 RID: 52580
			public string message;

			// Token: 0x0400CD65 RID: 52581
			public bool data;
		}

		// Token: 0x0200133A RID: 4922
		[Serializable]
		public class ResponseMultiSimpleData
		{
			// Token: 0x0400CD66 RID: 52582
			public int code;

			// Token: 0x0400CD67 RID: 52583
			public string message;

			// Token: 0x0400CD68 RID: 52584
			public string messageValue;

			// Token: 0x0400CD69 RID: 52585
			public OnlineDeck.OnlineDeckData[] data;
		}

		// Token: 0x0200133B RID: 4923
		[Serializable]
		public class ResponseRecords
		{
			// Token: 0x0400CD6A RID: 52586
			public int current;

			// Token: 0x0400CD6B RID: 52587
			public int size;

			// Token: 0x0400CD6C RID: 52588
			public int total;

			// Token: 0x0400CD6D RID: 52589
			public int pages;

			// Token: 0x0400CD6E RID: 52590
			public OnlineDeck.OnlineDeckData[] records;
		}

		// Token: 0x0200133C RID: 4924
		[Serializable]
		public class ResponseDeckID
		{
			// Token: 0x0400CD6F RID: 52591
			public int code;

			// Token: 0x0400CD70 RID: 52592
			public int message;

			// Token: 0x0400CD71 RID: 52593
			public string data;
		}

		// Token: 0x0200133D RID: 4925
		[Serializable]
		public class ResponseDeckIDs
		{
			// Token: 0x0400CD72 RID: 52594
			public int code;

			// Token: 0x0400CD73 RID: 52595
			public int message;

			// Token: 0x0400CD74 RID: 52596
			public string[] data;
		}

		// Token: 0x0200133E RID: 4926
		[Serializable]
		public class PostAllDecksBody
		{
			// Token: 0x0400CD75 RID: 52597
			public string deckContributor;

			// Token: 0x0400CD76 RID: 52598
			public int userId;

			// Token: 0x0400CD77 RID: 52599
			public OnlineDeck.PostDeck[] decks;
		}

		// Token: 0x0200133F RID: 4927
		[Serializable]
		public class PostDeckBody
		{
			// Token: 0x0400CD78 RID: 52600
			public string deckContributor;

			// Token: 0x0400CD79 RID: 52601
			public int userId;

			// Token: 0x0400CD7A RID: 52602
			public OnlineDeck.PostDeck deck;
		}

		// Token: 0x02001340 RID: 4928
		[Serializable]
		public class PostDeck
		{
			// Token: 0x06008F50 RID: 36688 RVA: 0x00002739 File Offset: 0x00000939
			public PostDeck()
			{
			}

			// Token: 0x06008F51 RID: 36689 RVA: 0x001349D8 File Offset: 0x00132BD8
			public PostDeck(Deck deck, string deckId, string deckName, string ydk)
			{
				this.deckId = deckId;
				this.deckName = deckName;
				this.deckType = deck.type;
				if (this.deckType == "/")
				{
					this.deckType = string.Empty;
				}
				if (deck.Pickup.Count > 0)
				{
					this.deckCoverCard1 = deck.Pickup[0];
				}
				if (deck.Pickup.Count > 1)
				{
					this.deckCoverCard1 = deck.Pickup[1];
				}
				if (deck.Pickup.Count > 2)
				{
					this.deckCoverCard1 = deck.Pickup[2];
				}
				this.deckCase = deck.Case;
				this.deckProtector = deck.Protector;
				this.deckYdk = ydk;
				this.isDelete = false;
			}

			// Token: 0x0400CD7B RID: 52603
			public string deckId;

			// Token: 0x0400CD7C RID: 52604
			public string deckName;

			// Token: 0x0400CD7D RID: 52605
			public string deckType;

			// Token: 0x0400CD7E RID: 52606
			public int deckCoverCard1;

			// Token: 0x0400CD7F RID: 52607
			public int deckCoverCard2;

			// Token: 0x0400CD80 RID: 52608
			public int deckCoverCard3;

			// Token: 0x0400CD81 RID: 52609
			public int deckCase;

			// Token: 0x0400CD82 RID: 52610
			public int deckProtector;

			// Token: 0x0400CD83 RID: 52611
			public string deckYdk;

			// Token: 0x0400CD84 RID: 52612
			public bool isDelete;
		}

		// Token: 0x02001341 RID: 4929
		[Serializable]
		public class PostPublicBody
		{
			// Token: 0x0400CD85 RID: 52613
			public int userId;

			// Token: 0x0400CD86 RID: 52614
			public string deckId;

			// Token: 0x0400CD87 RID: 52615
			public bool isPublic;
		}
	}
}
