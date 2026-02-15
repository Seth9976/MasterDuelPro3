using System;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Servant;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200142C RID: 5164
	public class ChatPanel : SidePanel
	{
		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x060095F3 RID: 38387 RVA: 0x0000763C File Offset: 0x0000583C
		protected override bool Permanent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x060095F4 RID: 38388 RVA: 0x00130723 File Offset: 0x0012E923
		protected override float TransitionTime
		{
			get
			{
				return 0.4f;
			}
		}

		// Token: 0x060095F5 RID: 38389 RVA: 0x0015AC3C File Offset: 0x00158E3C
		protected override void Awake()
		{
			base.Awake();
			Addressables.LoadAssetAsync<GameObject>("UI/ChatItemMe.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.chatItemMe = result.Result;
			};
			Addressables.LoadAssetAsync<GameObject>("UI/ChatItemOp.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.chatItemOp = result.Result;
			};
			Addressables.LoadAssetAsync<GameObject>("UI/ChatItemSystem.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.chatItemSystem = result.Result;
			};
		}

		// Token: 0x060095F6 RID: 38390 RVA: 0x0015ACA9 File Offset: 0x00158EA9
		public void Switch()
		{
			if (this.showing)
			{
				if (!this.input.isFocused)
				{
					this.HideWithSound();
					return;
				}
			}
			else
			{
				this.Show(true);
			}
		}

		// Token: 0x060095F7 RID: 38391 RVA: 0x0015ACD0 File Offset: 0x00158ED0
		public void Show(bool takeOver = true)
		{
			base.Show();
			if (Program.instance.currentServant != Program.instance.room && !DeviceInfo.OnMobile())
			{
				this.input.Select();
			}
			this.scrollRect.DOVerticalNormalizedPos(0f, 0f, false);
		}

		// Token: 0x060095F8 RID: 38392 RVA: 0x0015AD28 File Offset: 0x00158F28
		protected override void Update()
		{
			if (!this.showing)
			{
				return;
			}
			if (!this.NeedResponse())
			{
				return;
			}
			if ((UserInput.WasCancelPressed || UserInput.MouseRightDown) && Program.instance.currentServant == Program.instance.ocgcore)
			{
				this.HideWithSound();
			}
		}

		// Token: 0x060095F9 RID: 38393 RVA: 0x0015AD76 File Offset: 0x00158F76
		protected override bool NeedResponse()
		{
			return !this.input.isFocused && (base.NeedResponse() || Program.instance.room.showing);
		}

		// Token: 0x060095FA RID: 38394 RVA: 0x0015ADA0 File Offset: 0x00158FA0
		public void OnSend()
		{
			if (this.input.text == string.Empty)
			{
				this.Hide();
				return;
			}
			this.OnChat(this.input.text);
		}

		// Token: 0x060095FB RID: 38395 RVA: 0x0015ADD1 File Offset: 0x00158FD1
		public void OnChat(string content)
		{
			if (content == string.Empty)
			{
				return;
			}
			TcpHelper.CtosMessage_Chat(content);
			Program.instance.ui_.chatPanel.ClearInputField();
		}

		// Token: 0x060095FC RID: 38396 RVA: 0x0015ADFB File Offset: 0x00158FFB
		public string GetInputFieldText()
		{
			return this.input.text;
		}

		// Token: 0x060095FD RID: 38397 RVA: 0x0015AE08 File Offset: 0x00159008
		public void ClearInputField()
		{
			this.input.text = string.Empty;
		}

		// Token: 0x060095FE RID: 38398 RVA: 0x0015AE1C File Offset: 0x0015901C
		public void AddChatItem(int player, string content)
		{
			if (RoomServant.CoreShowing == 1)
			{
				this.cachedMessages.Add(new ChatPanel.ChatMessage
				{
					player = player,
					content = content
				});
				return;
			}
			if (RoomServant.CoreShowing == 2 && this.cachedMessages.Count > 0)
			{
				List<ChatPanel.ChatMessage> cacehd = new List<ChatPanel.ChatMessage>(this.cachedMessages);
				this.cachedMessages.Clear();
				for (int i = 0; i < cacehd.Count; i++)
				{
					this.AddChatItem(cacehd[i].player, cacehd[i].content);
				}
			}
			if (player == -2)
			{
				return;
			}
			string nickName = ChatPanel.GetPlayerName(player);
			GameObject item = null;
			ChatPanel.PlayerPosition position = ChatPanel.GetPlayerPosition(player);
			switch (position)
			{
			case ChatPanel.PlayerPosition.Me:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemMe);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.duelFrameMat0;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.duelFace0;
				break;
			case ChatPanel.PlayerPosition.MyTag:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemMe);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.duelFrameMat0Tag;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.duelFace0Tag;
				break;
			case ChatPanel.PlayerPosition.Op:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemOp);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.duelFrameMat1;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.duelFace1;
				break;
			case ChatPanel.PlayerPosition.OpTag:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemOp);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.duelFrameMat1Tag;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.duelFace1Tag;
				break;
			case ChatPanel.PlayerPosition.WatchMe:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemMe);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.watchFrameMat0;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.watchFace0;
				break;
			case ChatPanel.PlayerPosition.WatchMyTag:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemMe);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.watchFrameMat0Tag;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.watchFace0Tag;
				break;
			case ChatPanel.PlayerPosition.WatchOp:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemOp);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.watchFrameMat1;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.watchFace1;
				break;
			case ChatPanel.PlayerPosition.WatchOpTag:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemOp);
				item.transform.GetChild(2).GetComponent<Image>().material = Appearance.watchFrameMat1Tag;
				item.transform.GetChild(2).GetComponent<Image>().sprite = Appearance.watchFace1Tag;
				break;
			case ChatPanel.PlayerPosition.Other:
				item = global::UnityEngine.Object.Instantiate<GameObject>(this.chatItemSystem);
				break;
			}
			item.transform.GetChild(0).GetComponent<Text>().text = nickName + ":";
			item.transform.GetChild(1).GetComponent<Text>().text = content;
			if (position == ChatPanel.PlayerPosition.Other)
			{
				item.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
				item.transform.GetChild(1).GetComponent<Text>().text = string.Empty;
				item.transform.GetChild(2).GetComponent<Text>().text = content;
			}
			item.transform.SetParent(this.scrollRect.content, false);
			item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (float)(-(float)this.chatItems.Count * 150));
			this.chatItems.Add(item);
			this.scrollRect.content.sizeDelta = new Vector2(0f, (float)(this.chatItems.Count * 150));
			this.scrollRect.DOVerticalNormalizedPos(0f, 0.2f, false);
			Package package = new Package();
			package.Function = 230;
			package.Data = new BinaryMaster(null);
			package.Data.writer.Write(player);
			package.Data.writer.WriteUnicode(content, content.Length + 1);
			TcpHelper.AddRecordLine(package);
			if (Program.instance.ocgcore.showing)
			{
				Program.instance.ocgcore.Chat(player, content);
			}
			if (Program.instance.online.showing)
			{
				MessageManager.Cast(content);
			}
		}

		// Token: 0x060095FF RID: 38399 RVA: 0x0015B2DC File Offset: 0x001594DC
		private static int GetRoomPlayerIndex(int player)
		{
			if (!Program.instance.ocgcore.showing)
			{
				return player;
			}
			if (player > -1 && player < 4)
			{
				int swapMask = ((RoomServant.Mode == 2) ? 2 : 1);
				if (RoomServant.SelfType == 7)
				{
					if (!OcgCore.isFirst)
					{
						return player ^ swapMask;
					}
					return player;
				}
				else
				{
					if (ChatPanel.InFirst() && !OcgCore.isFirst)
					{
						return player ^ swapMask;
					}
					if (!ChatPanel.InFirst() && OcgCore.isFirst)
					{
						return player ^ swapMask;
					}
				}
			}
			return player;
		}

		// Token: 0x06009600 RID: 38400 RVA: 0x0015B34B File Offset: 0x0015954B
		private static bool InFirst()
		{
			if (RoomServant.Mode < 2)
			{
				return RoomServant.SelfType == 0;
			}
			return RoomServant.SelfType == 0 || RoomServant.SelfType == 1;
		}

		// Token: 0x06009601 RID: 38401 RVA: 0x0015B370 File Offset: 0x00159570
		private static string GetPlayerConfigName(ChatPanel.PlayerPosition position)
		{
			string text;
			switch (position)
			{
			case ChatPanel.PlayerPosition.Me:
				text = Config.Get("DuelPlayerName0", "@ui");
				break;
			case ChatPanel.PlayerPosition.MyTag:
				text = Config.Get("DuelPlayerName0Tag", "@ui");
				break;
			case ChatPanel.PlayerPosition.Op:
				text = Config.Get("DuelPlayerName1", "@ui");
				break;
			case ChatPanel.PlayerPosition.OpTag:
				text = Config.Get("DuelPlayerName1Tag", "@ui");
				break;
			case ChatPanel.PlayerPosition.WatchMe:
				text = Config.Get("WatchPlayerName0", "@ui");
				break;
			case ChatPanel.PlayerPosition.WatchMyTag:
				text = Config.Get("WatchPlayerName0Tag", "@ui");
				break;
			case ChatPanel.PlayerPosition.WatchOp:
				text = Config.Get("WatchPlayerName1", "@ui");
				break;
			case ChatPanel.PlayerPosition.WatchOpTag:
				text = Config.Get("WatchPlayerName1Tag", "@ui");
				break;
			default:
				text = string.Empty;
				break;
			}
			return text;
		}

		// Token: 0x06009602 RID: 38402 RVA: 0x0015B444 File Offset: 0x00159644
		public static string GetPlayerName(int player)
		{
			ChatPanel.PlayerPosition playerPosition = ChatPanel.GetPlayerPosition(player);
			player = ChatPanel.GetRoomPlayerIndex(player);
			string nickName = string.Empty;
			switch (player)
			{
			case -1:
				return Config.Get("DuelPlayerName0", "@ui");
			case 0:
			case 1:
			case 2:
			case 3:
			{
				nickName = RoomServant.players[player].name;
				string configName = ChatPanel.GetPlayerConfigName(playerPosition);
				if (configName.Length > 0)
				{
					return configName;
				}
				return nickName;
			}
			case 7:
				return nickName + InterString.Get("观战者", 0);
			case 8:
				return nickName + "[System]";
			case 9:
				return nickName + "[Script error]";
			}
			nickName += "[---]";
			return nickName;
		}

		// Token: 0x06009603 RID: 38403 RVA: 0x0015B50C File Offset: 0x0015970C
		public static ChatPanel.PlayerPosition GetPlayerPosition(int player)
		{
			player = ChatPanel.GetRoomPlayerIndex(player);
			ChatPanel.PlayerPosition position;
			if (player < 4)
			{
				if (RoomServant.Mode < 2)
				{
					if (RoomServant.SelfType != 7)
					{
						if (RoomServant.SelfType == player)
						{
							position = ChatPanel.PlayerPosition.Me;
						}
						else
						{
							position = ChatPanel.PlayerPosition.Op;
						}
					}
					else if (player == 0)
					{
						position = ChatPanel.PlayerPosition.WatchMe;
					}
					else
					{
						position = ChatPanel.PlayerPosition.WatchOp;
					}
				}
				else if (RoomServant.SelfType != 7)
				{
					if (RoomServant.SelfType == player)
					{
						position = ChatPanel.PlayerPosition.Me;
					}
					else if ((RoomServant.SelfType + player) % 4 == 1)
					{
						position = ChatPanel.PlayerPosition.MyTag;
					}
					else if (player == 0 || player == 2)
					{
						position = ChatPanel.PlayerPosition.Op;
					}
					else
					{
						position = ChatPanel.PlayerPosition.OpTag;
					}
				}
				else if (player == 0)
				{
					position = ChatPanel.PlayerPosition.WatchMe;
				}
				else if (player == 1)
				{
					position = ChatPanel.PlayerPosition.WatchMyTag;
				}
				else if (player == 2)
				{
					position = ChatPanel.PlayerPosition.WatchOp;
				}
				else
				{
					position = ChatPanel.PlayerPosition.WatchOpTag;
				}
			}
			else
			{
				position = ChatPanel.PlayerPosition.Other;
			}
			return position;
		}

		// Token: 0x0400D44A RID: 54346
		[Header("Chat Panel")]
		public TMP_InputField input;

		// Token: 0x0400D44B RID: 54347
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x0400D44C RID: 54348
		private GameObject chatItemMe;

		// Token: 0x0400D44D RID: 54349
		private GameObject chatItemOp;

		// Token: 0x0400D44E RID: 54350
		private GameObject chatItemSystem;

		// Token: 0x0400D44F RID: 54351
		private List<GameObject> chatItems = new List<GameObject>();

		// Token: 0x0400D450 RID: 54352
		private readonly List<ChatPanel.ChatMessage> cachedMessages = new List<ChatPanel.ChatMessage>();

		// Token: 0x0200142D RID: 5165
		private struct ChatMessage
		{
			// Token: 0x0400D451 RID: 54353
			public int player;

			// Token: 0x0400D452 RID: 54354
			public string content;
		}

		// Token: 0x0200142E RID: 5166
		public enum PlayerPosition
		{
			// Token: 0x0400D454 RID: 54356
			Me,
			// Token: 0x0400D455 RID: 54357
			MyTag,
			// Token: 0x0400D456 RID: 54358
			Op,
			// Token: 0x0400D457 RID: 54359
			OpTag,
			// Token: 0x0400D458 RID: 54360
			WatchMe,
			// Token: 0x0400D459 RID: 54361
			WatchMyTag,
			// Token: 0x0400D45A RID: 54362
			WatchOp,
			// Token: 0x0400D45B RID: 54363
			WatchOpTag,
			// Token: 0x0400D45C RID: 54364
			Other
		}
	}
}
