using System;
using System.Collections.Generic;
using MDPro3.Servant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001454 RID: 5204
	public class AppearanceUI : ServantUI
	{
		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x060096C8 RID: 38600 RVA: 0x0015DD50 File Offset: 0x0015BF50
		public ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x060096C9 RID: 38601 RVA: 0x0015DD8C File Offset: 0x0015BF8C
		private CanvasGroup ScrollRectCG
		{
			get
			{
				return this.m_ScrollRectCG = ((this.m_ScrollRectCG != null) ? this.m_ScrollRectCG : this.ScrollRect.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060096CA RID: 38602 RVA: 0x0015DDC4 File Offset: 0x0015BFC4
		private TextMeshProUGUI TextDetailTitle
		{
			get
			{
				return this.m_TextDetailTitle = ((this.m_TextDetailTitle != null) ? this.m_TextDetailTitle : base.Manager.GetElement<TextMeshProUGUI>("TextDetailTitle"));
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x060096CB RID: 38603 RVA: 0x0015DE00 File Offset: 0x0015C000
		private TextMeshProUGUI TextDetailSetting
		{
			get
			{
				return this.m_TextDetailSetting = ((this.m_TextDetailSetting != null) ? this.m_TextDetailSetting : base.Manager.GetElement<TextMeshProUGUI>("TextDetailSetting"));
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x060096CC RID: 38604 RVA: 0x0015DE3C File Offset: 0x0015C03C
		private TextMeshProUGUI TextDetailDescription
		{
			get
			{
				return this.m_TextDetailDescription = ((this.m_TextDetailDescription != null) ? this.m_TextDetailDescription : base.Manager.GetElement<TextMeshProUGUI>("TextDetailDescription"));
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x060096CD RID: 38605 RVA: 0x0015DE78 File Offset: 0x0015C078
		public Image Image
		{
			get
			{
				return this.m_Image = ((this.m_Image != null) ? this.m_Image : base.Manager.GetElement<Image>("Image"));
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x060096CE RID: 38606 RVA: 0x0015DEB4 File Offset: 0x0015C0B4
		public RawImage RawImage
		{
			get
			{
				return this.m_RawImage = ((this.m_RawImage != null) ? this.m_RawImage : base.Manager.GetElement<RawImage>("RawImage"));
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x060096CF RID: 38607 RVA: 0x0015DEF0 File Offset: 0x0015C0F0
		private TextMeshProUGUI TextHover
		{
			get
			{
				return this.m_TextHover = ((this.m_TextHover != null) ? this.m_TextHover : base.Manager.GetElement<TextMeshProUGUI>("TextHover"));
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x060096D0 RID: 38608 RVA: 0x0015DF2C File Offset: 0x0015C12C
		private GameObject NameTable
		{
			get
			{
				return this.m_NameTable = ((this.m_NameTable != null) ? this.m_NameTable : base.Manager.GetElement("NameTable"));
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x060096D1 RID: 38609 RVA: 0x0015DF68 File Offset: 0x0015C168
		private SelectionToggle_AppearanceGenre Page00PlayerName
		{
			get
			{
				return this.m_Page00 = ((this.m_Page00 != null) ? this.m_Page00 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page00PlayerName"));
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x060096D2 RID: 38610 RVA: 0x0015DFA4 File Offset: 0x0015C1A4
		private SelectionToggle_AppearanceGenre Page01Wallpaper
		{
			get
			{
				return this.m_Page01 = ((this.m_Page01 != null) ? this.m_Page01 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page01Wallpaper"));
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060096D3 RID: 38611 RVA: 0x0015DFE0 File Offset: 0x0015C1E0
		private SelectionToggle_AppearanceGenre Page02Face
		{
			get
			{
				return this.m_Page02 = ((this.m_Page02 != null) ? this.m_Page02 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page02Face"));
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060096D4 RID: 38612 RVA: 0x0015E01C File Offset: 0x0015C21C
		private SelectionToggle_AppearanceGenre Page03Frame
		{
			get
			{
				return this.m_Page03 = ((this.m_Page03 != null) ? this.m_Page03 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page03Frame"));
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060096D5 RID: 38613 RVA: 0x0015E058 File Offset: 0x0015C258
		private SelectionToggle_AppearanceGenre Page04Case
		{
			get
			{
				return this.m_Page04 = ((this.m_Page04 != null) ? this.m_Page04 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page04Case"));
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060096D6 RID: 38614 RVA: 0x0015E094 File Offset: 0x0015C294
		private SelectionToggle_AppearanceGenre Page05Protector
		{
			get
			{
				return this.m_Page05 = ((this.m_Page05 != null) ? this.m_Page05 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page05Protector"));
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060096D7 RID: 38615 RVA: 0x0015E0D0 File Offset: 0x0015C2D0
		private SelectionToggle_AppearanceGenre Page06Field
		{
			get
			{
				return this.m_Page06 = ((this.m_Page06 != null) ? this.m_Page06 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page06Field"));
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060096D8 RID: 38616 RVA: 0x0015E10C File Offset: 0x0015C30C
		private SelectionToggle_AppearanceGenre Page07Grave
		{
			get
			{
				return this.m_Page07 = ((this.m_Page07 != null) ? this.m_Page07 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page07Grave"));
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060096D9 RID: 38617 RVA: 0x0015E148 File Offset: 0x0015C348
		private SelectionToggle_AppearanceGenre Page08Stand
		{
			get
			{
				return this.m_Page08 = ((this.m_Page08 != null) ? this.m_Page08 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page08Stand"));
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060096DA RID: 38618 RVA: 0x0015E184 File Offset: 0x0015C384
		private SelectionToggle_AppearanceGenre Page09Mate
		{
			get
			{
				return this.m_Page09 = ((this.m_Page09 != null) ? this.m_Page09 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page09Mate"));
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060096DB RID: 38619 RVA: 0x0015E1C0 File Offset: 0x0015C3C0
		private SelectionToggle_AppearanceGenre Page10Pickup
		{
			get
			{
				return this.m_Page10 = ((this.m_Page10 != null) ? this.m_Page10 : base.Manager.GetElement<SelectionToggle_AppearanceGenre>("Page10Pickup"));
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060096DC RID: 38620 RVA: 0x0015E1FC File Offset: 0x0015C3FC
		private SelectionToggle ToggleOverwrite
		{
			get
			{
				return this.m_ToggleOverwrite = ((this.m_ToggleOverwrite != null) ? this.m_ToggleOverwrite : base.Manager.GetElement<SelectionToggle>("ToggleOverwrite"));
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060096DD RID: 38621 RVA: 0x0015E238 File Offset: 0x0015C438
		private SelectionToggle_AppearancePlayer TogglePlayer0
		{
			get
			{
				return this.m_TogglePlayer0 = ((this.m_TogglePlayer0 != null) ? this.m_TogglePlayer0 : base.Manager.GetElement<SelectionToggle_AppearancePlayer>("TogglePlayer0"));
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060096DE RID: 38622 RVA: 0x0015E274 File Offset: 0x0015C474
		private TextMeshProUGUI TextInputHint
		{
			get
			{
				return this.m_TextInputHint = ((this.m_TextInputHint != null) ? this.m_TextInputHint : base.Manager.GetElement<TextMeshProUGUI>("TextInputHint"));
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060096DF RID: 38623 RVA: 0x0015E2B0 File Offset: 0x0015C4B0
		private DeckPickup DeckPickup
		{
			get
			{
				return this.m_DeckPickup = ((this.m_DeckPickup != null) ? this.m_DeckPickup : base.Manager.GetElement<DeckPickup>("DeckPickup"));
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060096E0 RID: 38624 RVA: 0x0015E2EC File Offset: 0x0015C4EC
		public TMP_InputField InputPlayerName
		{
			get
			{
				return this.m_InputPlayerName = ((this.m_InputPlayerName != null) ? this.m_InputPlayerName : base.Manager.GetElement<TMP_InputField>("InputFieldPlayerName"));
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060096E1 RID: 38625 RVA: 0x0015E328 File Offset: 0x0015C528
		private GameObject Template
		{
			get
			{
				return this.m_Template = ((this.m_Template != null) ? this.m_Template : base.Manager.GetElement("ItemAppearance"));
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060096E2 RID: 38626 RVA: 0x0015E364 File Offset: 0x0015C564
		public AppearanceDetail Detail
		{
			get
			{
				return this.m_Detail = ((this.m_Detail != null) ? this.m_Detail : base.Manager.GetElement<AppearanceDetail>("Details"));
			}
		}

		// Token: 0x060096E3 RID: 38627 RVA: 0x0015E3A0 File Offset: 0x0015C5A0
		private void Awake()
		{
			if (Config.GetBool("OverrideDeckAppearance", false))
			{
				this.ToggleOverwrite.SetToggleOn(true);
				return;
			}
			this.ToggleOverwrite.SetToggleOff(true);
		}

		// Token: 0x060096E4 RID: 38628 RVA: 0x0015E3C8 File Offset: 0x0015C5C8
		public override void SelectDefaultSelectable()
		{
			if (Appearance.condition == Appearance.Condition.DeckEditor)
			{
				this.Page04Case.GetSelectable().Select();
				return;
			}
			this.Page00PlayerName.GetSelectable().Select();
		}

		// Token: 0x060096E5 RID: 38629 RVA: 0x0015E3F4 File Offset: 0x0015C5F4
		public override void ShowEvent()
		{
			base.ShowEvent();
			switch (Appearance.condition)
			{
			case Appearance.Condition.Duel:
				base.Title.text = InterString.Get("决斗外观", 0);
				break;
			case Appearance.Condition.Watch:
				base.Title.text = InterString.Get("观战外观", 0);
				break;
			case Appearance.Condition.Replay:
				base.Title.text = InterString.Get("回放外观", 0);
				break;
			case Appearance.Condition.DeckEditor:
				base.Title.text = InterString.Get("卡组外观", 0);
				break;
			}
			this.Page00PlayerName.gameObject.SetActive(Appearance.condition != Appearance.Condition.DeckEditor);
			this.Page01Wallpaper.gameObject.SetActive(Appearance.condition != Appearance.Condition.DeckEditor);
			this.Page02Face.gameObject.SetActive(Appearance.condition != Appearance.Condition.DeckEditor);
			this.Page03Frame.gameObject.SetActive(Appearance.condition != Appearance.Condition.DeckEditor);
			this.Page04Case.gameObject.SetActive(Appearance.condition == Appearance.Condition.DeckEditor);
			this.Page10Pickup.gameObject.SetActive(Appearance.condition == Appearance.Condition.DeckEditor);
			if (Appearance.condition == Appearance.Condition.DeckEditor)
			{
				this.Page10Pickup.GetSelectable().Select();
				this.Page10Pickup.SetToggleOn(true);
			}
			else
			{
				this.Page00PlayerName.GetSelectable().Select();
				this.Page00PlayerName.SetToggleOn(true);
			}
			this.TogglePlayer0.SetToggleOn(true);
		}

		// Token: 0x060096E6 RID: 38630 RVA: 0x0015E570 File Offset: 0x0015C770
		protected override void HideEvent()
		{
			base.HideEvent();
			if (Appearance.condition != Appearance.Condition.DeckEditor)
			{
				Program.instance.setting.GetUI<SettingServantUI>().RefreshAppearanceModeText();
				string currentWallpaper = UIManager.currentWallpaper;
				string text = "Wallpaper";
				Items.Item item = Program.items.wallpapers[0];
				if (currentWallpaper != Config.Get(text, item.id.ToString()))
				{
					string text2 = "Wallpaper";
					item = Program.items.wallpapers[0];
					UIManager.currentWallpaper = Config.Get(text2, item.id.ToString());
					Program.instance.ui_.ChangeWallpaper(UIManager.currentWallpaper);
				}
			}
		}

		// Token: 0x060096E7 RID: 38631 RVA: 0x0015E618 File Offset: 0x0015C818
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
			{
				foreach (GameObject gameObject in pool.Value)
				{
					gameObject.GetComponent<SelectionToggle_AppearanceItem>().Dispose();
				}
				pool.Value.Clear();
			}
			Config.Save();
		}

		// Token: 0x060096E8 RID: 38632 RVA: 0x0015E6C0 File Offset: 0x0015C8C0
		public void SavePlayerName(string nameValue)
		{
			Config.Set(Appearance.condition.ToString() + "PlayerName" + Appearance.player, (nameValue == string.Empty) ? "@ui" : nameValue);
		}

		// Token: 0x060096E9 RID: 38633 RVA: 0x0015E6FB File Offset: 0x0015C8FB
		public bool CanSwitchPlayer()
		{
			return this.TogglePlayer0.gameObject.activeSelf;
		}

		// Token: 0x060096EA RID: 38634 RVA: 0x0015E70D File Offset: 0x0015C90D
		public void OnPlayerLeft()
		{
			this.TogglePlayer0.OnLeftSelection();
		}

		// Token: 0x060096EB RID: 38635 RVA: 0x0015E71A File Offset: 0x0015C91A
		public void OnPlayerRight()
		{
			this.TogglePlayer0.OnRightSelection();
		}

		// Token: 0x060096EC RID: 38636 RVA: 0x0015E727 File Offset: 0x0015C927
		public void SetHoverText(string hover)
		{
			this.TextHover.text = hover;
		}

		// Token: 0x060096ED RID: 38637 RVA: 0x0015E738 File Offset: 0x0015C938
		public void ShowItems(string type)
		{
			AppearanceUI.currentContent = type;
			this.pools.TryGetValue(AppearanceUI.currentContent, out AppearanceUI.currentList);
			if (Appearance.condition == Appearance.Condition.DeckEditor)
			{
				this.TogglePlayer0.transform.parent.gameObject.SetActive(false);
			}
			else
			{
				this.TogglePlayer0.transform.parent.gameObject.SetActive(true);
			}
			this.DeckPickup.gameObject.SetActive(AppearanceUI.currentContent == "Pickup");
			if (AppearanceUI.currentContent == "PlayerName")
			{
				this.ScrollRectCG.alpha = 0f;
				this.ScrollRectCG.blocksRaycasts = false;
				this.Detail.Hide();
				this.NameTable.SetActive(true);
				this.InputPlayerName.text = Config.Get(Appearance.condition.ToString() + AppearanceUI.currentContent + Appearance.player, "@ui");
				if (Appearance.player == "0")
				{
					this.TextInputHint.text = InterString.Get("请输入您的昵称：", 0);
					return;
				}
				if (Appearance.player == "1")
				{
					this.TextInputHint.text = InterString.Get("请输入对方的昵称，留空则显示真实昵称：", 0);
					return;
				}
				if (Appearance.player == "0Tag")
				{
					this.TextInputHint.text = InterString.Get("请输入您的队友的昵称，留空则显示真实昵称：", 0);
					return;
				}
				if (Appearance.player == "1Tag")
				{
					this.TextInputHint.text = InterString.Get("请输入对方的队友的昵称，留空则显示真实昵称：", 0);
				}
				return;
			}
			else
			{
				if (AppearanceUI.currentContent == "Pickup")
				{
					this.ScrollRectCG.alpha = 0f;
					this.ScrollRectCG.blocksRaycasts = false;
					this.Detail.Hide();
					this.DeckPickup.gameObject.SetActive(true);
					this.DeckPickup.SetDeck(DeckEditor.Deck);
					return;
				}
				if (AppearanceUI.currentContent == "Wallpaper")
				{
					this.TogglePlayer0.transform.parent.gameObject.SetActive(false);
				}
				this.ScrollRectCG.alpha = 1f;
				this.ScrollRectCG.blocksRaycasts = true;
				this.Detail.Show();
				this.NameTable.SetActive(false);
				this.DeckPickup.gameObject.SetActive(false);
				string text = AppearanceUI.currentContent;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				List<Items.Item> list;
				if (num <= 458327249U)
				{
					if (num <= 417664425U)
					{
						if (num != 263732487U)
						{
							if (num == 417664425U)
							{
								if (text == "Wallpaper")
								{
									list = Program.items.wallpapers;
									goto IL_041B;
								}
							}
						}
						else if (text == "Field")
						{
							list = Program.items.mats;
							goto IL_041B;
						}
					}
					else if (num != 453289155U)
					{
						if (num == 458327249U)
						{
							if (text == "Case")
							{
								list = Program.items.cases;
								goto IL_041B;
							}
						}
					}
					else if (text == "Stand")
					{
						list = Program.items.stands;
						goto IL_041B;
					}
				}
				else if (num <= 1483327592U)
				{
					if (num != 908340434U)
					{
						if (num == 1483327592U)
						{
							if (text == "Mate")
							{
								list = Program.items.mates;
								goto IL_041B;
							}
						}
					}
					else if (text == "Grave")
					{
						list = Program.items.graves;
						goto IL_041B;
					}
				}
				else if (num != 1585103174U)
				{
					if (num != 2484219132U)
					{
						if (num == 3664838481U)
						{
							if (text == "Protector")
							{
								list = Program.items.protectors;
								goto IL_041B;
							}
						}
					}
					else if (text == "Face")
					{
						list = Program.items.faces;
						goto IL_041B;
					}
				}
				else if (text == "Frame")
				{
					list = Program.items.frames;
					goto IL_041B;
				}
				list = Program.items.mates;
				IL_041B:
				AppearanceUI.targetItems = list;
				foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
				{
					if (pool.Key != AppearanceUI.currentContent)
					{
						foreach (GameObject gameObject in pool.Value)
						{
							gameObject.GetComponent<SelectionToggle_AppearanceItem>().Hide();
						}
					}
				}
				if (AppearanceUI.currentList.Count == 0)
				{
					int itemCount = 0;
					foreach (Items.Item itemInfo in AppearanceUI.targetItems)
					{
						if (!itemInfo.notReady)
						{
							GameObject item = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item.SetActive(true);
							SelectionToggle_AppearanceItem component = item.GetComponent<SelectionToggle_AppearanceItem>();
							component.index = itemCount++;
							component.itemID = itemInfo.id;
							component.description = itemInfo.description;
							component.itemName = itemInfo.name;
							component.path = Items.GetIconAddress(component.itemID.ToString(), 0);
							component.transform.SetParent(this.ScrollRect.content, false);
							component.Refresh();
							AppearanceUI.currentList.Add(item);
						}
					}
					if (Appearance.condition != Appearance.Condition.DeckEditor)
					{
						if (Program.items.ListHaveNone(AppearanceUI.targetItems))
						{
							GameObject item2 = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item2.SetActive(true);
							SelectionToggle_AppearanceItem component2 = item2.GetComponent<SelectionToggle_AppearanceItem>();
							component2.index = itemCount++;
							component2.itemID = 0;
							component2.description = InterString.Get("该项设置将设置为无。", 0);
							component2.itemName = InterString.Get("不设置", 0);
							component2.path = "Menu-NoImage";
							component2.transform.SetParent(this.ScrollRect.content, false);
							component2.Refresh();
							AppearanceUI.currentList.Add(item2);
						}
						if (Program.items.ListHaveRandom(AppearanceUI.targetItems))
						{
							GameObject item3 = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item3.SetActive(true);
							SelectionToggle_AppearanceItem component3 = item3.GetComponent<SelectionToggle_AppearanceItem>();
							component3.index = itemCount++;
							component3.itemID = 9999;
							component3.description = InterString.Get("该项设置将随机设置。", 0);
							component3.itemName = InterString.Get("随机", 0);
							component3.path = "Menu-Random";
							component3.transform.SetParent(this.ScrollRect.content, false);
							component3.Refresh();
							AppearanceUI.currentList.Add(item3);
						}
						if (Program.items.ListHaveSame(AppearanceUI.targetItems))
						{
							GameObject item4 = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item4.SetActive(true);
							SelectionToggle_AppearanceItem component4 = item4.GetComponent<SelectionToggle_AppearanceItem>();
							component4.index = itemCount++;
							component4.itemID = 8888;
							component4.description = InterString.Get("该项设置将与场地设置保持一致。", 0);
							component4.itemName = InterString.Get("一致", 0);
							component4.path = "Menu-Same";
							component4.transform.SetParent(this.ScrollRect.content, false);
							component4.Refresh();
							AppearanceUI.currentList.Add(item4);
						}
						if (Program.items.ListHaveDIY(AppearanceUI.targetItems))
						{
							GameObject item5 = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item5.SetActive(true);
							SelectionToggle_AppearanceItem itemMono = item5.GetComponent<SelectionToggle_AppearanceItem>();
							itemMono.index = itemCount++;
							itemMono.itemID = 9998;
							itemMono.description = string.Concat(new string[]
							{
								InterString.Get("我方头像：", 0),
								"Picture/DIY/Me.png\n",
								InterString.Get("对方头像：", 0),
								"Picture/DIY/Op.png\n",
								InterString.Get("我方队友头像：", 0),
								"Picture/DIY/MeTag.png\n",
								InterString.Get("对方队友头像：", 0),
								"Picture/DIY/OpTag.png"
							});
							itemMono.itemName = InterString.Get("自定义", 0);
							itemMono.path = "Menu-DIY";
							itemMono.transform.SetParent(this.ScrollRect.content, false);
							itemMono.Refresh();
							AppearanceUI.currentList.Add(item5);
						}
						if (AppearanceUI.targetItems == Program.items.mats)
						{
							GameObject item6 = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
							item6.SetActive(true);
							SelectionToggle_AppearanceItem component5 = item6.GetComponent<SelectionToggle_AppearanceItem>();
							component5.index = itemCount++;
							component5.itemID = 8888;
							component5.description = InterString.Get("该项设置将与我方场地设置保持一致。", 0);
							component5.itemName = InterString.Get("一致", 0);
							component5.path = "Menu-Same";
							component5.transform.SetParent(this.ScrollRect.content, false);
							component5.Refresh();
							AppearanceUI.currentList.Add(item6);
							AppearanceUI.onlyOpSideShowItems.Add(item6);
						}
					}
				}
				foreach (GameObject item7 in AppearanceUI.currentList)
				{
					if (Appearance.player.Contains("0") && AppearanceUI.onlyOpSideShowItems.Contains(item7))
					{
						item7.GetComponent<SelectionToggle_AppearanceItem>().Hide();
					}
					else
					{
						item7.GetComponent<SelectionToggle_AppearanceItem>().Show();
					}
				}
				foreach (GameObject item8 in AppearanceUI.currentList)
				{
					if (AppearanceUI.currentContent == "Wallpaper")
					{
						string text2 = item8.GetComponent<SelectionToggle_AppearanceItem>().itemID.ToString();
						string text3 = "Wallpaper";
						Items.Item item9 = AppearanceUI.targetItems[0];
						if (text2 == Config.Get(text3, item9.id.ToString()))
						{
							item8.GetComponent<SelectionToggle_AppearanceItem>().SetToggleOn(true);
							break;
						}
					}
					else
					{
						int itemID = item8.GetComponent<SelectionToggle_AppearanceItem>().itemID;
						if (Appearance.condition == Appearance.Condition.DeckEditor)
						{
							if (itemID == DeckEditor.Deck.Case || itemID == DeckEditor.Deck.Protector || itemID == DeckEditor.Deck.Field || itemID == DeckEditor.Deck.Grave || itemID == DeckEditor.Deck.Stand || itemID == DeckEditor.Deck.Mate)
							{
								item8.GetComponent<SelectionToggle_AppearanceItem>().SetToggleOn(true);
								break;
							}
						}
						else
						{
							string text4 = itemID.ToString();
							string text5 = Appearance.condition.ToString() + AppearanceUI.currentContent + Appearance.player;
							Items.Item item9 = AppearanceUI.targetItems[0];
							if (text4 == Config.Get(text5, item9.id.ToString()))
							{
								item8.GetComponent<SelectionToggle_AppearanceItem>().SetToggleOn(true);
								break;
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x060096EE RID: 38638 RVA: 0x0015F29C File Offset: 0x0015D49C
		public void SwitchPlayer(string player)
		{
			Appearance.player = player;
			if (Appearance.condition == Appearance.Condition.Duel && player == "0")
			{
				this.ToggleOverwrite.gameObject.SetActive(true);
			}
			else
			{
				this.ToggleOverwrite.gameObject.SetActive(false);
			}
			this.ShowItems(AppearanceUI.currentContent);
		}

		// Token: 0x060096EF RID: 38639 RVA: 0x0015F2F2 File Offset: 0x0015D4F2
		public void SetOverride(bool over)
		{
			Config.SetBool("OverrideDeckAppearance", over);
		}

		// Token: 0x060096F0 RID: 38640 RVA: 0x0015F300 File Offset: 0x0015D500
		public int GetCurrentGenreCount()
		{
			foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
			{
				if (pool.Key == AppearanceUI.currentContent)
				{
					return pool.Value.Count;
				}
			}
			return 0;
		}

		// Token: 0x060096F1 RID: 38641 RVA: 0x0015F374 File Offset: 0x0015D574
		public GameObject GetCurrentContentItem()
		{
			if (AppearanceUI.currentContent == "PlayerName")
			{
				return this.InputPlayerName.gameObject;
			}
			if (AppearanceUI.currentContent == "Pickup")
			{
				return null;
			}
			if (Program.instance.appearance.lastSelectedItem != null && Program.instance.appearance.lastSelectedItem.gameObject.activeSelf)
			{
				return Program.instance.appearance.lastSelectedItem.gameObject;
			}
			return this.ScrollRect.content.GetChild(0).gameObject;
		}

		// Token: 0x060096F2 RID: 38642 RVA: 0x0015F40E File Offset: 0x0015D60E
		public void SelectPlayerNameToggle()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Page00PlayerName.GetSelectable().Select();
		}

		// Token: 0x060096F3 RID: 38643 RVA: 0x0015F426 File Offset: 0x0015D626
		public bool InPickupPage()
		{
			return AppearanceUI.currentContent == "Pickup";
		}

		// Token: 0x060096F4 RID: 38644 RVA: 0x0015F437 File Offset: 0x0015D637
		public void OnPickupChange()
		{
			Program.instance.ShiftToServant(Program.instance.deckBrowser);
		}

		// Token: 0x0400D509 RID: 54537
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D50A RID: 54538
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D50B RID: 54539
		private CanvasGroup m_ScrollRectCG;

		// Token: 0x0400D50C RID: 54540
		private const string LABEL_TXT_DETAILTITLE = "TextDetailTitle";

		// Token: 0x0400D50D RID: 54541
		private TextMeshProUGUI m_TextDetailTitle;

		// Token: 0x0400D50E RID: 54542
		private const string LABEL_TXT_DETAILSETTING = "TextDetailSetting";

		// Token: 0x0400D50F RID: 54543
		private TextMeshProUGUI m_TextDetailSetting;

		// Token: 0x0400D510 RID: 54544
		private const string LABEL_TXT_DETAILDESCRIPTION = "TextDetailDescription";

		// Token: 0x0400D511 RID: 54545
		private TextMeshProUGUI m_TextDetailDescription;

		// Token: 0x0400D512 RID: 54546
		private const string LABEL_IMG = "Image";

		// Token: 0x0400D513 RID: 54547
		private Image m_Image;

		// Token: 0x0400D514 RID: 54548
		private const string LABEL_RIMG = "RawImage";

		// Token: 0x0400D515 RID: 54549
		private RawImage m_RawImage;

		// Token: 0x0400D516 RID: 54550
		private const string LABEL_TXT_HOVER = "TextHover";

		// Token: 0x0400D517 RID: 54551
		private TextMeshProUGUI m_TextHover;

		// Token: 0x0400D518 RID: 54552
		private const string LABEL_GO_NAMETABLE = "NameTable";

		// Token: 0x0400D519 RID: 54553
		private GameObject m_NameTable;

		// Token: 0x0400D51A RID: 54554
		private const string LABEL_STG_PAGE00 = "Page00PlayerName";

		// Token: 0x0400D51B RID: 54555
		private SelectionToggle_AppearanceGenre m_Page00;

		// Token: 0x0400D51C RID: 54556
		private const string LABEL_STG_PAGE01 = "Page01Wallpaper";

		// Token: 0x0400D51D RID: 54557
		private SelectionToggle_AppearanceGenre m_Page01;

		// Token: 0x0400D51E RID: 54558
		private const string LABEL_STG_PAGE02 = "Page02Face";

		// Token: 0x0400D51F RID: 54559
		private SelectionToggle_AppearanceGenre m_Page02;

		// Token: 0x0400D520 RID: 54560
		private const string LABEL_STG_PAGE03 = "Page03Frame";

		// Token: 0x0400D521 RID: 54561
		private SelectionToggle_AppearanceGenre m_Page03;

		// Token: 0x0400D522 RID: 54562
		private const string LABEL_STG_PAGE04 = "Page04Case";

		// Token: 0x0400D523 RID: 54563
		private SelectionToggle_AppearanceGenre m_Page04;

		// Token: 0x0400D524 RID: 54564
		private const string LABEL_STG_PAGE05 = "Page05Protector";

		// Token: 0x0400D525 RID: 54565
		private SelectionToggle_AppearanceGenre m_Page05;

		// Token: 0x0400D526 RID: 54566
		private const string LABEL_STG_PAGE06 = "Page06Field";

		// Token: 0x0400D527 RID: 54567
		private SelectionToggle_AppearanceGenre m_Page06;

		// Token: 0x0400D528 RID: 54568
		private const string LABEL_STG_PAGE07 = "Page07Grave";

		// Token: 0x0400D529 RID: 54569
		private SelectionToggle_AppearanceGenre m_Page07;

		// Token: 0x0400D52A RID: 54570
		private const string LABEL_STG_PAGE08 = "Page08Stand";

		// Token: 0x0400D52B RID: 54571
		private SelectionToggle_AppearanceGenre m_Page08;

		// Token: 0x0400D52C RID: 54572
		private const string LABEL_STG_PAGE09 = "Page09Mate";

		// Token: 0x0400D52D RID: 54573
		private SelectionToggle_AppearanceGenre m_Page09;

		// Token: 0x0400D52E RID: 54574
		private const string LABEL_STG_PAGE10 = "Page10Pickup";

		// Token: 0x0400D52F RID: 54575
		private SelectionToggle_AppearanceGenre m_Page10;

		// Token: 0x0400D530 RID: 54576
		private const string LABEL_STG_OVERWRITE = "ToggleOverwrite";

		// Token: 0x0400D531 RID: 54577
		private SelectionToggle m_ToggleOverwrite;

		// Token: 0x0400D532 RID: 54578
		private const string LABEL_STG_PLAYER0 = "TogglePlayer0";

		// Token: 0x0400D533 RID: 54579
		private SelectionToggle_AppearancePlayer m_TogglePlayer0;

		// Token: 0x0400D534 RID: 54580
		private const string LABEL_TXT_INPUTHINT = "TextInputHint";

		// Token: 0x0400D535 RID: 54581
		private TextMeshProUGUI m_TextInputHint;

		// Token: 0x0400D536 RID: 54582
		private const string LABEL_MONO_DECKPICKUP = "DeckPickup";

		// Token: 0x0400D537 RID: 54583
		private DeckPickup m_DeckPickup;

		// Token: 0x0400D538 RID: 54584
		private const string LABEL_IPT_PLAYERNAME = "InputFieldPlayerName";

		// Token: 0x0400D539 RID: 54585
		private TMP_InputField m_InputPlayerName;

		// Token: 0x0400D53A RID: 54586
		private const string LABEL_MONO_ITEM_APPEARANCE = "ItemAppearance";

		// Token: 0x0400D53B RID: 54587
		private GameObject m_Template;

		// Token: 0x0400D53C RID: 54588
		private const string LABEL_MONO_APPEARANCE_DETAIL = "Details";

		// Token: 0x0400D53D RID: 54589
		private AppearanceDetail m_Detail;

		// Token: 0x0400D53E RID: 54590
		public static string currentContent = "PlayerName";

		// Token: 0x0400D53F RID: 54591
		private static List<Items.Item> targetItems;

		// Token: 0x0400D540 RID: 54592
		private static List<GameObject> currentList;

		// Token: 0x0400D541 RID: 54593
		private static readonly List<GameObject> onlyOpSideShowItems = new List<GameObject>();

		// Token: 0x0400D542 RID: 54594
		private static readonly List<GameObject> wallpapers = new List<GameObject>();

		// Token: 0x0400D543 RID: 54595
		private static readonly List<GameObject> faces = new List<GameObject>();

		// Token: 0x0400D544 RID: 54596
		private static readonly List<GameObject> frames = new List<GameObject>();

		// Token: 0x0400D545 RID: 54597
		private static readonly List<GameObject> protectors = new List<GameObject>();

		// Token: 0x0400D546 RID: 54598
		private static readonly List<GameObject> mats = new List<GameObject>();

		// Token: 0x0400D547 RID: 54599
		private static readonly List<GameObject> graves = new List<GameObject>();

		// Token: 0x0400D548 RID: 54600
		private static readonly List<GameObject> stands = new List<GameObject>();

		// Token: 0x0400D549 RID: 54601
		private static readonly List<GameObject> mates = new List<GameObject>();

		// Token: 0x0400D54A RID: 54602
		private static readonly List<GameObject> cases = new List<GameObject>();

		// Token: 0x0400D54B RID: 54603
		private readonly Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>
		{
			{
				"Wallpaper",
				AppearanceUI.wallpapers
			},
			{
				"Face",
				AppearanceUI.faces
			},
			{
				"Frame",
				AppearanceUI.frames
			},
			{
				"Protector",
				AppearanceUI.protectors
			},
			{
				"Field",
				AppearanceUI.mats
			},
			{
				"Grave",
				AppearanceUI.graves
			},
			{
				"Stand",
				AppearanceUI.stands
			},
			{
				"Mate",
				AppearanceUI.mates
			},
			{
				"Case",
				AppearanceUI.cases
			}
		};
	}
}
