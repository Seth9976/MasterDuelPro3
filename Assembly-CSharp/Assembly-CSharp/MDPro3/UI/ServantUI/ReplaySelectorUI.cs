using System;
using System.Collections.Generic;
using System.IO;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using Percy;
using SevenZip.Compression.LZMA;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x0200147C RID: 5244
	public class ReplaySelectorUI : ServantUI
	{
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x0600986F RID: 39023 RVA: 0x00167408 File Offset: 0x00165608
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06009870 RID: 39024 RVA: 0x00167444 File Offset: 0x00165644
		public TextMeshProUGUI TextOverview
		{
			get
			{
				return this.m_TextOverview = ((this.m_TextOverview != null) ? this.m_TextOverview : base.Manager.GetElement<TextMeshProUGUI>("TextOverview"));
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06009871 RID: 39025 RVA: 0x00167480 File Offset: 0x00165680
		public SelectionButton ButtonPlayer0
		{
			get
			{
				return this.m_ButtonPlayer0 = ((this.m_ButtonPlayer0 != null) ? this.m_ButtonPlayer0 : base.Manager.GetElement<SelectionButton>("ButtonPlayer0"));
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06009872 RID: 39026 RVA: 0x001674BC File Offset: 0x001656BC
		public SelectionButton ButtonPlayer1
		{
			get
			{
				return this.m_ButtonPlayer1 = ((this.m_ButtonPlayer1 != null) ? this.m_ButtonPlayer1 : base.Manager.GetElement<SelectionButton>("ButtonPlayer1"));
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06009873 RID: 39027 RVA: 0x001674F8 File Offset: 0x001656F8
		public SelectionButton ButtonPlayer2
		{
			get
			{
				return this.m_ButtonPlayer2 = ((this.m_ButtonPlayer2 != null) ? this.m_ButtonPlayer2 : base.Manager.GetElement<SelectionButton>("ButtonPlayer2"));
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06009874 RID: 39028 RVA: 0x00167534 File Offset: 0x00165734
		public SelectionButton ButtonPlayer3
		{
			get
			{
				return this.m_ButtonPlayer3 = ((this.m_ButtonPlayer3 != null) ? this.m_ButtonPlayer3 : base.Manager.GetElement<SelectionButton>("ButtonPlayer3"));
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06009875 RID: 39029 RVA: 0x00167570 File Offset: 0x00165770
		private SelectionButton ButtonSort
		{
			get
			{
				return this.m_ButtonSort = ((this.m_ButtonSort != null) ? this.m_ButtonSort : base.Manager.GetElement<SelectionButton>("ButtonSort"));
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06009876 RID: 39030 RVA: 0x001675AC File Offset: 0x001657AC
		public SelectionButton ButtonGodView
		{
			get
			{
				return this.m_ButtonGodView = ((this.m_ButtonGodView != null) ? this.m_ButtonGodView : base.Manager.GetElement<SelectionButton>("ButtonGodView"));
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06009877 RID: 39031 RVA: 0x001675E8 File Offset: 0x001657E8
		public Image ImageHover
		{
			get
			{
				return this.m_ImageHover = ((this.m_ImageHover != null) ? this.m_ImageHover : base.Manager.GetElement<Image>("ButtonHover"));
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06009878 RID: 39032 RVA: 0x00167624 File Offset: 0x00165824
		public Image ImageOut
		{
			get
			{
				return this.m_ImageOut = ((this.m_ImageOut != null) ? this.m_ImageOut : base.Manager.GetElement<Image>("ButtonOut"));
			}
		}

		// Token: 0x06009879 RID: 39033 RVA: 0x00167660 File Offset: 0x00165860
		public void KF_Replay(string name, bool god = false)
		{
			string fileName = "Replay/" + name + (name.EndsWith(".yrp") ? string.Empty : ".yrp3d");
			if (!File.Exists(fileName))
			{
				fileName = fileName.Replace(".yrp3d", ".yrp");
				if (!File.Exists(fileName))
				{
					return;
				}
			}
			bool yrp3d = fileName.Length > 6 && fileName.ToLower().Substring(fileName.Length - 6, 6) == ".yrp3d";
			try
			{
				if (yrp3d)
				{
					if (god)
					{
						MessageManager.Cast(InterString.Get("您正在观看旧版的回放（上帝视角），不保证稳定性。", 0));
						PercyOCG percyOCG = this.percy;
						if (percyOCG != null)
						{
							percyOCG.Dispose();
						}
						this.percy = new PercyOCG();
						List<byte[]> replays = this.GetYRPBuffer(fileName);
						Ygopro ygopro = this.percy.ygopro;
						List<byte[]> list = replays;
						List<Package> collections = TcpHelper.GetPackages(ygopro.GetYRP3dBuffer(this.GetYRP(list[list.Count - 1])));
						this.PushCollection(collections);
					}
					else
					{
						List<byte[]> replays2 = this.GetYRPBuffer(fileName);
						if (replays2.Count == 0)
						{
							OcgCore.CurrentReplayUseYRP2 = true;
						}
						else
						{
							List<byte[]> list2 = replays2;
							OcgCore.CurrentReplayUseYRP2 = this.GetYRP(list2[list2.Count - 1]).IsNew();
						}
						List<Package> collection = TcpHelper.ReadPackagesInRecord(fileName);
						this.PushCollection(collection);
					}
				}
				else
				{
					MessageManager.Cast(InterString.Get("您正在观看旧版的回放（上帝视角），不保证稳定性。", 0));
					PercyOCG percyOCG2 = this.percy;
					if (percyOCG2 != null)
					{
						percyOCG2.Dispose();
					}
					this.percy = new PercyOCG();
					List<Package> collections2 = TcpHelper.GetPackages(this.percy.ygopro.GetYRP3dBuffer(this.GetYRP(File.ReadAllBytes(fileName))));
					this.PushCollection(collections2);
				}
			}
			catch (Exception)
			{
				MessageManager.Cast(InterString.Get("回放没有录制完整。", 0));
			}
		}

		// Token: 0x0600987A RID: 39034 RVA: 0x00167828 File Offset: 0x00165A28
		private List<byte[]> GetYRPBuffer(string path)
		{
			if (path.EndsWith(".yrp"))
			{
				return new List<byte[]> { File.ReadAllBytes(path) };
			}
			List<byte[]> returnValue = new List<byte[]>();
			try
			{
				foreach (Package item in TcpHelper.ReadPackagesInRecord(path))
				{
					if (item.Function == 231)
					{
						byte[] replay = item.Data.reader.ReadToEnd();
						returnValue.Add(replay);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			return returnValue;
		}

		// Token: 0x0600987B RID: 39035 RVA: 0x001678D4 File Offset: 0x00165AD4
		private YRP GetYRP(byte[] buffer)
		{
			YRP returnValue = new YRP();
			try
			{
				BinaryReader reader = new BinaryReader(new MemoryStream(buffer));
				returnValue.ID = reader.ReadInt32();
				returnValue.Version = reader.ReadInt32();
				returnValue.Flag = reader.ReadInt32();
				returnValue.Seed = reader.ReadUInt32();
				returnValue.DataSize = (long)reader.ReadInt32();
				returnValue.Hash = reader.ReadInt32();
				returnValue.Props = reader.ReadBytes(8);
				if (returnValue.ID == 846230137)
				{
					for (int i = 0; i < 8; i++)
					{
						returnValue.SeedsV2[i] = reader.ReadUInt32();
					}
					for (int j = 0; j < 4; j++)
					{
						reader.ReadUInt32();
					}
				}
				byte[] raw = reader.ReadToEnd();
				if ((returnValue.Flag & 1) > 0)
				{
					SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
					decoder.SetDecoderProperties(returnValue.Props);
					MemoryStream decompressed = new MemoryStream();
					decoder.Code(new MemoryStream(raw), decompressed, (long)raw.Length, returnValue.DataSize, null);
					raw = decompressed.ToArray();
				}
				reader = new BinaryReader(new MemoryStream(raw));
				if ((returnValue.Flag & 2) > 0)
				{
					RoomServant.Mode = 2;
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData[0].name = reader.ReadUnicode(20);
					returnValue.playerData[1].name = reader.ReadUnicode(20);
					returnValue.playerData[2].name = reader.ReadUnicode(20);
					returnValue.playerData[3].name = reader.ReadUnicode(20);
					returnValue.StartLp = reader.ReadInt32();
					returnValue.StartHand = reader.ReadInt32();
					returnValue.DrawCount = reader.ReadInt32();
					returnValue.opt = reader.ReadUInt32();
					OcgCore.MasterRule = (int)(returnValue.opt >> 16);
					for (int k = 0; k < 4; k++)
					{
						int count = reader.ReadInt32();
						for (int i2 = 0; i2 < count; i2++)
						{
							returnValue.playerData[k].main.Add(reader.ReadInt32());
						}
						count = reader.ReadInt32();
						for (int i3 = 0; i3 < count; i3++)
						{
							returnValue.playerData[k].extra.Add(reader.ReadInt32());
						}
					}
				}
				else
				{
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData.Add(new YRP.PlayerData());
					returnValue.playerData[0].name = reader.ReadUnicode(20);
					returnValue.playerData[1].name = reader.ReadUnicode(20);
					returnValue.StartLp = reader.ReadInt32();
					returnValue.StartHand = reader.ReadInt32();
					returnValue.DrawCount = reader.ReadInt32();
					returnValue.opt = reader.ReadUInt32();
					OcgCore.MasterRule = (int)(returnValue.opt >> 16);
					for (int l = 0; l < 2; l++)
					{
						int count2 = reader.ReadInt32();
						for (int i4 = 0; i4 < count2; i4++)
						{
							returnValue.playerData[l].main.Add(reader.ReadInt32());
						}
						count2 = reader.ReadInt32();
						for (int i5 = 0; i5 < count2; i5++)
						{
							returnValue.playerData[l].extra.Add(reader.ReadInt32());
						}
					}
				}
				while (reader.BaseStream.Position < reader.BaseStream.Length)
				{
					returnValue.gameData.Add(reader.ReadBytes((int)reader.ReadByte()));
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
			return returnValue;
		}

		// Token: 0x0600987C RID: 39036 RVA: 0x00167CB8 File Offset: 0x00165EB8
		public YRP CacheYRP(string replay)
		{
			if (this.cachedYRPs.ContainsKey(replay))
			{
				return this.cachedYRPs[replay];
			}
			YRP yrp;
			if (File.Exists("Replay/" + replay))
			{
				yrp = this.GetYRP(File.ReadAllBytes("Replay/" + replay));
			}
			else
			{
				List<byte[]> buffer = this.GetYRPBuffer("Replay/" + replay + ".yrp3d");
				if (buffer.Count == 0)
				{
					yrp = null;
				}
				else
				{
					yrp = this.GetYRP(buffer[0]);
				}
			}
			if (yrp != null)
			{
				this.cachedYRPs.Add(replay, yrp);
			}
			return yrp;
		}

		// Token: 0x0600987D RID: 39037 RVA: 0x00167D50 File Offset: 0x00165F50
		private void PushCollection(List<Package> collection)
		{
			Program.instance.ocgcore.returnServant = Program.instance.replay;
			OcgCore.handler = delegate(byte[] a)
			{
			};
			OcgCore.name_0 = Config.Get("ReplayPlayerName0", "@ui");
			OcgCore.name_0_tag = Config.Get("ReplayPlayerName0Tag", "@ui");
			OcgCore.name_0_c = OcgCore.name_0;
			OcgCore.name_1 = Config.Get("ReplayPlayerName1", "@ui");
			OcgCore.name_1_tag = Config.Get("ReplayPlayerName1Tag", "@ui");
			OcgCore.name_1_c = OcgCore.name_1;
			OcgCore.timeLimit = 240;
			OcgCore.lpLimit = 8000;
			OcgCore.isFirst = true;
			OcgCore.condition = OcgCore.Condition.Replay;
			Program.instance.ShiftToServant(Program.instance.ocgcore);
			Program.instance.ocgcore.FlushPackages(collection);
		}

		// Token: 0x0600987E RID: 39038 RVA: 0x00167E44 File Offset: 0x00166044
		private void SelectZero()
		{
			SelectionToggle_Replay item0 = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Replay>();
			item0.SetToggleOn(true);
			Program.instance.replay.lastSelectedReplayItem = item0;
		}

		// Token: 0x0600987F RID: 39039 RVA: 0x00167E84 File Offset: 0x00166084
		public void Print()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			if (!Directory.Exists("Replay/"))
			{
				Directory.CreateDirectory("Replay/");
			}
			FileInfo[] fileInfos = new DirectoryInfo("Replay/").GetFiles();
			if (this.sortByName)
			{
				Array.Sort<FileInfo>(fileInfos, new Comparison<FileInfo>(Tools.CompareName));
			}
			else
			{
				Array.Sort<FileInfo>(fileInfos, new Comparison<FileInfo>(Tools.CompareTime));
			}
			List<string[]> tasks = new List<string[]>();
			int count = 0;
			for (int i = 0; i < fileInfos.Length; i++)
			{
				if (fileInfos[i].Name.EndsWith(".yrp3d"))
				{
					string[] task = new string[]
					{
						count.ToString(),
						fileInfos[i].Name.Replace(".yrp3d", string.Empty)
					};
					tasks.Add(task);
					count++;
				}
				else if (fileInfos[i].Name.EndsWith(".yrp"))
				{
					string[] task2 = new string[]
					{
						count.ToString(),
						fileInfos[i].Name
					};
					tasks.Add(task2);
					count++;
				}
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemReplay.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 180f : 150f);
				float topPadding = (PropertyOverrider.NeedMobileLayout() ? 148f : 134f);
				float space = itemHeight - (PropertyOverrider.NeedMobileLayout() ? 152f : 122f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 64f : 54f) - space;
				this.superScrollView = new SuperScrollView(1, 700f, itemHeight, topPadding, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.Manager.GetElement<ScrollRect>("ScrollRect"), 2);
				this.superScrollView.Print(tasks);
				if (tasks.Count > 0)
				{
					this.SelectZero();
				}
			};
		}

		// Token: 0x06009880 RID: 39040 RVA: 0x00167FE0 File Offset: 0x001661E0
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Replay component = item.GetComponent<SelectionToggle_Replay>();
			component.index = int.Parse(task[0]);
			component.replayName = task[1];
			component.Refresh();
		}

		// Token: 0x06009881 RID: 39041 RVA: 0x00168004 File Offset: 0x00166204
		public void OnRename()
		{
			UIManager.ShowPopupInput(new List<string>
			{
				InterString.Get("请输入新的回放名称", 0),
				this.superScrollView.items[this.superScrollView.selected].args[1].Replace(".yrp", string.Empty)
			}, new Action<string>(this.ReplayRename), null, TmpInputValidation.ValidationType.Path);
		}

		// Token: 0x06009882 RID: 39042 RVA: 0x00168074 File Offset: 0x00166274
		private void ReplayRename(string newName)
		{
			string replay = this.superScrollView.items[this.superScrollView.selected].args[1];
			if (replay.EndsWith(".yrp"))
			{
				File.Move("Replay/" + replay, "Replay/" + newName + ".yrp");
			}
			else
			{
				File.Move("Replay/" + replay + ".yrp3d", "Replay/" + newName + ".yrp3d");
			}
			this.Print();
		}

		// Token: 0x06009883 RID: 39043 RVA: 0x001680FE File Offset: 0x001662FE
		public void OnPlay()
		{
			this.KF_Replay(this.superScrollView.items[this.superScrollView.selected].args[1], false);
		}

		// Token: 0x06009884 RID: 39044 RVA: 0x00168129 File Offset: 0x00166329
		public void OnGodView()
		{
			this.KF_Replay(this.superScrollView.items[this.superScrollView.selected].args[1], true);
		}

		// Token: 0x06009885 RID: 39045 RVA: 0x00168154 File Offset: 0x00166354
		public void OnDelete()
		{
			string replay = this.superScrollView.items[this.superScrollView.selected].args[1];
			if (File.Exists("Replay/" + replay))
			{
				File.Delete("Replay/" + replay);
			}
			else
			{
				File.Delete("Replay/" + replay + ".yrp3d");
			}
			MessageManager.Cast(InterString.Get("已删除回放「[?]」。", replay, 0));
			this.Print();
		}

		// Token: 0x06009886 RID: 39046 RVA: 0x001681D4 File Offset: 0x001663D4
		public void OnSort()
		{
			this.sortByName = !this.sortByName;
			if (this.sortByName)
			{
				this.ButtonSort.SetButtonText(InterString.Get("名称排序", 0));
			}
			else
			{
				this.ButtonSort.SetButtonText(InterString.Get("时间排序", 0));
			}
			this.Print();
		}

		// Token: 0x06009887 RID: 39047 RVA: 0x0016822C File Offset: 0x0016642C
		public void OnDeck(int player)
		{
			string replay = this.superScrollView.items[this.superScrollView.selected].args[1];
			YRP yrp = this.cachedYRPs[replay];
			replay = replay.Replace(".yrp", string.Empty);
			string deckName = replay + "_" + yrp.playerData[player].name;
			MDPro3.Duel.YGOSharp.Deck deck = new MDPro3.Duel.YGOSharp.Deck(yrp.playerData[player].main, yrp.playerData[player].extra, new List<int>());
			Program.instance.deckEditor.SwitchCondition(DeckEditor.Condition.ReplayDeck, deckName, deck);
			Program.instance.ShiftToServant(Program.instance.deckEditor);
		}

		// Token: 0x06009888 RID: 39048 RVA: 0x001682EA File Offset: 0x001664EA
		public void SelectLastReplayItem()
		{
			UserInput.NextSelectionIsAxis = true;
			Program.instance.replay.lastSelectedReplayItem.GetSelectable().Select();
		}

		// Token: 0x0400D690 RID: 54928
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D691 RID: 54929
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D692 RID: 54930
		private const string LABEL_TXT_OVERVIEW = "TextOverview";

		// Token: 0x0400D693 RID: 54931
		private TextMeshProUGUI m_TextOverview;

		// Token: 0x0400D694 RID: 54932
		private const string LABEL_SBN_PLAYER0 = "ButtonPlayer0";

		// Token: 0x0400D695 RID: 54933
		private SelectionButton m_ButtonPlayer0;

		// Token: 0x0400D696 RID: 54934
		private const string LABEL_SBN_PLAYER1 = "ButtonPlayer1";

		// Token: 0x0400D697 RID: 54935
		private SelectionButton m_ButtonPlayer1;

		// Token: 0x0400D698 RID: 54936
		private const string LABEL_SBN_PLAYER2 = "ButtonPlayer2";

		// Token: 0x0400D699 RID: 54937
		private SelectionButton m_ButtonPlayer2;

		// Token: 0x0400D69A RID: 54938
		private const string LABEL_SBN_PLAYER3 = "ButtonPlayer3";

		// Token: 0x0400D69B RID: 54939
		private SelectionButton m_ButtonPlayer3;

		// Token: 0x0400D69C RID: 54940
		private const string LABEL_SBN_SORT = "ButtonSort";

		// Token: 0x0400D69D RID: 54941
		private SelectionButton m_ButtonSort;

		// Token: 0x0400D69E RID: 54942
		private const string LABEL_SBN_GODVIEW = "ButtonGodView";

		// Token: 0x0400D69F RID: 54943
		private SelectionButton m_ButtonGodView;

		// Token: 0x0400D6A0 RID: 54944
		private const string LABEL_IMG_HOVER = "ButtonHover";

		// Token: 0x0400D6A1 RID: 54945
		private Image m_ImageHover;

		// Token: 0x0400D6A2 RID: 54946
		private const string LABEL_IMG_OUT = "ButtonOut";

		// Token: 0x0400D6A3 RID: 54947
		private Image m_ImageOut;

		// Token: 0x0400D6A4 RID: 54948
		public SuperScrollView superScrollView;

		// Token: 0x0400D6A5 RID: 54949
		private readonly Dictionary<string, YRP> cachedYRPs = new Dictionary<string, YRP>();

		// Token: 0x0400D6A6 RID: 54950
		private bool sortByName = true;

		// Token: 0x0400D6A7 RID: 54951
		private PercyOCG percy;
	}
}
