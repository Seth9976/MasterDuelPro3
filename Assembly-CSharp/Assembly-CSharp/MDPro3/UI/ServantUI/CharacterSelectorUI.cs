using System;
using System.Collections.Generic;
using MDPro3.Servant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001455 RID: 5205
	public class CharacterSelectorUI : ServantUI
	{
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060096F7 RID: 38647 RVA: 0x0015F57C File Offset: 0x0015D77C
		public ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060096F8 RID: 38648 RVA: 0x0015F5B8 File Offset: 0x0015D7B8
		private TextMeshProUGUI TextHover
		{
			get
			{
				return this.m_TextHover = ((this.m_TextHover != null) ? this.m_TextHover : base.Manager.GetElement<TextMeshProUGUI>("TextHover"));
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060096F9 RID: 38649 RVA: 0x0015F5F4 File Offset: 0x0015D7F4
		public Image ImageDetail
		{
			get
			{
				return this.m_ImageDetail = ((this.m_ImageDetail != null) ? this.m_ImageDetail : base.Manager.GetElement<Image>("ImageDetail"));
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060096FA RID: 38650 RVA: 0x0015F630 File Offset: 0x0015D830
		public TextMeshProUGUI TextDetailName
		{
			get
			{
				return this.m_TextDetailName = ((this.m_TextDetailName != null) ? this.m_TextDetailName : base.Manager.GetElement<TextMeshProUGUI>("TextDetailName"));
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060096FB RID: 38651 RVA: 0x0015F66C File Offset: 0x0015D86C
		public TextMeshProUGUI TextDetailDescription
		{
			get
			{
				return this.m_TextDetailDescription = ((this.m_TextDetailDescription != null) ? this.m_TextDetailDescription : base.Manager.GetElement<TextMeshProUGUI>("TextDetailDescription"));
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060096FC RID: 38652 RVA: 0x0015F6A8 File Offset: 0x0015D8A8
		private SelectionToggle_CharacterSeries TogglePage00
		{
			get
			{
				return this.m_TogglePage00 = ((this.m_TogglePage00 != null) ? this.m_TogglePage00 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page00"));
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060096FD RID: 38653 RVA: 0x0015F6E4 File Offset: 0x0015D8E4
		private SelectionToggle_CharacterSeries TogglePage01
		{
			get
			{
				return this.m_TogglePage01 = ((this.m_TogglePage01 != null) ? this.m_TogglePage01 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page01"));
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060096FE RID: 38654 RVA: 0x0015F720 File Offset: 0x0015D920
		private SelectionToggle_CharacterSeries TogglePage02
		{
			get
			{
				return this.m_TogglePage02 = ((this.m_TogglePage02 != null) ? this.m_TogglePage02 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page02"));
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060096FF RID: 38655 RVA: 0x0015F75C File Offset: 0x0015D95C
		private SelectionToggle_CharacterSeries TogglePage03
		{
			get
			{
				return this.m_TogglePage03 = ((this.m_TogglePage03 != null) ? this.m_TogglePage03 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page03"));
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06009700 RID: 38656 RVA: 0x0015F798 File Offset: 0x0015D998
		private SelectionToggle_CharacterSeries TogglePage04
		{
			get
			{
				return this.m_TogglePage04 = ((this.m_TogglePage04 != null) ? this.m_TogglePage04 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page04"));
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06009701 RID: 38657 RVA: 0x0015F7D4 File Offset: 0x0015D9D4
		private SelectionToggle_CharacterSeries TogglePage05
		{
			get
			{
				return this.m_TogglePage05 = ((this.m_TogglePage05 != null) ? this.m_TogglePage05 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page05"));
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06009702 RID: 38658 RVA: 0x0015F810 File Offset: 0x0015DA10
		private SelectionToggle_CharacterSeries TogglePage06
		{
			get
			{
				return this.m_TogglePage06 = ((this.m_TogglePage06 != null) ? this.m_TogglePage06 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page06"));
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06009703 RID: 38659 RVA: 0x0015F84C File Offset: 0x0015DA4C
		private SelectionToggle_CharacterSeries TogglePage07
		{
			get
			{
				return this.m_TogglePage07 = ((this.m_TogglePage07 != null) ? this.m_TogglePage07 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page07"));
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06009704 RID: 38660 RVA: 0x0015F888 File Offset: 0x0015DA88
		private SelectionToggle_CharacterSeries TogglePage08
		{
			get
			{
				return this.m_TogglePage08 = ((this.m_TogglePage08 != null) ? this.m_TogglePage08 : base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page08"));
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06009705 RID: 38661 RVA: 0x0015F8C4 File Offset: 0x0015DAC4
		public SelectionToggle_CharacterPlayer TogglePlayer0
		{
			get
			{
				return this.m_TogglePlayer0 = ((this.m_TogglePlayer0 != null) ? this.m_TogglePlayer0 : base.Manager.GetElement<SelectionToggle_CharacterPlayer>("TogglePlayer0"));
			}
		}

		// Token: 0x06009706 RID: 38662 RVA: 0x0015F900 File Offset: 0x0015DB00
		public override void ShowEvent()
		{
			base.ShowEvent();
			switch (CharacterSelector.condition)
			{
			case CharacterSelector.Condition.Duel:
				base.Title.text = InterString.Get("决斗角色", 0);
				break;
			case CharacterSelector.Condition.Watch:
				base.Title.text = InterString.Get("观战角色", 0);
				break;
			case CharacterSelector.Condition.Replay:
				base.Title.text = InterString.Get("回放角色", 0);
				break;
			}
			this.TogglePlayer0.SetToggleOn(true);
		}

		// Token: 0x06009707 RID: 38663 RVA: 0x0015F980 File Offset: 0x0015DB80
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
			{
				foreach (GameObject gameObject in pool.Value)
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}
			foreach (KeyValuePair<string, List<GameObject>> pool2 in this.pools)
			{
				pool2.Value.Clear();
			}
		}

		// Token: 0x06009708 RID: 38664 RVA: 0x0015FA5C File Offset: 0x0015DC5C
		public void SwitchPlayer(string player)
		{
			CharacterSelectorUI.player = player;
			string configCharacter = Config.Get(CharacterSelector.condition.ToString() + "Character" + player, "0001");
			string configSeries = CharacterSelector.characters.GetCharacterSeries(configCharacter);
			base.Manager.GetElement<SelectionToggle_CharacterSeries>("Page" + configSeries).SetToggleOn(true);
		}

		// Token: 0x06009709 RID: 38665 RVA: 0x0015FAC0 File Offset: 0x0015DCC0
		public void ShowCharacters(string serial)
		{
			this.currentSerial = serial;
			if (CharacterSelector.characters == null || CharacterSelector.characterItem == null)
			{
				return;
			}
			foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
			{
				if (pool.Key != this.currentSerial)
				{
					using (List<GameObject>.Enumerator enumerator2 = pool.Value.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GameObject gameObject = enumerator2.Current;
							gameObject.SetActive(false);
						}
						continue;
					}
				}
				this.targetItems = pool.Value;
			}
			if (this.targetItems.Count == 0)
			{
				List<Characters.SeriesCharacter> targetCharacters = CharacterSelector.characters.GetSeriesCharacters(this.currentSerial);
				int count = 0;
				for (int i = 0; i < targetCharacters.Count; i++)
				{
					if (!targetCharacters[i].notReady)
					{
						GameObject item = global::UnityEngine.Object.Instantiate<GameObject>(CharacterSelector.characterItem);
						SelectionToggle_CharacterItem component = item.GetComponent<SelectionToggle_CharacterItem>();
						component.index = count;
						component.characterID = targetCharacters[i].id;
						component.Refresh();
						item.transform.SetParent(base.Manager.GetElement<ScrollRect>("ScrollRect").content, false);
						this.targetItems.Add(item);
						count++;
					}
				}
			}
			foreach (GameObject gameObject2 in this.targetItems)
			{
				gameObject2.SetActive(true);
				SelectionToggle_CharacterItem mono = gameObject2.GetComponent<SelectionToggle_CharacterItem>();
				string config = Config.Get(CharacterSelector.condition.ToString() + "Character" + CharacterSelectorUI.player, "0001");
				if (mono.characterID == config)
				{
					mono.SetToggleOn(true);
				}
			}
		}

		// Token: 0x0600970A RID: 38666 RVA: 0x0015FCD0 File Offset: 0x0015DED0
		public int GetCurrentSerialCount()
		{
			if (CharacterSelector.characters == null || CharacterSelector.characterItem == null)
			{
				return 0;
			}
			foreach (KeyValuePair<string, List<GameObject>> pool in this.pools)
			{
				if (pool.Key == this.currentSerial)
				{
					return pool.Value.Count;
				}
			}
			return 0;
		}

		// Token: 0x0600970B RID: 38667 RVA: 0x0015FD60 File Offset: 0x0015DF60
		public void SetHoverText(string txt)
		{
			this.TextHover.text = txt;
		}

		// Token: 0x0600970C RID: 38668 RVA: 0x0015FD70 File Offset: 0x0015DF70
		public GameObject GetFirstActiveCharacterItem()
		{
			for (int i = 0; i < this.ScrollRect.content.childCount; i++)
			{
				if (this.ScrollRect.content.GetChild(i).gameObject.activeSelf)
				{
					return this.ScrollRect.content.GetChild(i).gameObject;
				}
			}
			return null;
		}

		// Token: 0x0400D54C RID: 54604
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D54D RID: 54605
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D54E RID: 54606
		private const string LABEL_TXT_HOVER = "TextHover";

		// Token: 0x0400D54F RID: 54607
		private TextMeshProUGUI m_TextHover;

		// Token: 0x0400D550 RID: 54608
		private const string LABEL_IMG_DETAIL = "ImageDetail";

		// Token: 0x0400D551 RID: 54609
		private Image m_ImageDetail;

		// Token: 0x0400D552 RID: 54610
		private const string LABEL_TXT_DETAILNAME = "TextDetailName";

		// Token: 0x0400D553 RID: 54611
		private TextMeshProUGUI m_TextDetailName;

		// Token: 0x0400D554 RID: 54612
		private const string LABEL_TXT_DETAILDESCRIPTION = "TextDetailDescription";

		// Token: 0x0400D555 RID: 54613
		private TextMeshProUGUI m_TextDetailDescription;

		// Token: 0x0400D556 RID: 54614
		private const string LABEL_STG_PAGE00 = "Page00";

		// Token: 0x0400D557 RID: 54615
		private SelectionToggle_CharacterSeries m_TogglePage00;

		// Token: 0x0400D558 RID: 54616
		private const string LABEL_STG_PAGE01 = "Page01";

		// Token: 0x0400D559 RID: 54617
		private SelectionToggle_CharacterSeries m_TogglePage01;

		// Token: 0x0400D55A RID: 54618
		private const string LABEL_STG_PAGE02 = "Page02";

		// Token: 0x0400D55B RID: 54619
		private SelectionToggle_CharacterSeries m_TogglePage02;

		// Token: 0x0400D55C RID: 54620
		private const string LABEL_STG_PAGE03 = "Page03";

		// Token: 0x0400D55D RID: 54621
		private SelectionToggle_CharacterSeries m_TogglePage03;

		// Token: 0x0400D55E RID: 54622
		private const string LABEL_STG_PAGE04 = "Page04";

		// Token: 0x0400D55F RID: 54623
		private SelectionToggle_CharacterSeries m_TogglePage04;

		// Token: 0x0400D560 RID: 54624
		private const string LABEL_STG_PAGE05 = "Page05";

		// Token: 0x0400D561 RID: 54625
		private SelectionToggle_CharacterSeries m_TogglePage05;

		// Token: 0x0400D562 RID: 54626
		private const string LABEL_STG_PAGE06 = "Page06";

		// Token: 0x0400D563 RID: 54627
		private SelectionToggle_CharacterSeries m_TogglePage06;

		// Token: 0x0400D564 RID: 54628
		private const string LABEL_STG_PAGE07 = "Page07";

		// Token: 0x0400D565 RID: 54629
		private SelectionToggle_CharacterSeries m_TogglePage07;

		// Token: 0x0400D566 RID: 54630
		private const string LABEL_STG_PAGE08 = "Page08";

		// Token: 0x0400D567 RID: 54631
		private SelectionToggle_CharacterSeries m_TogglePage08;

		// Token: 0x0400D568 RID: 54632
		private const string LABEL_STG_PLAYER0 = "TogglePlayer0";

		// Token: 0x0400D569 RID: 54633
		private SelectionToggle_CharacterPlayer m_TogglePlayer0;

		// Token: 0x0400D56A RID: 54634
		public static string player = "0";

		// Token: 0x0400D56B RID: 54635
		private string currentSerial = "00";

		// Token: 0x0400D56C RID: 54636
		private static readonly List<GameObject> dm = new List<GameObject>();

		// Token: 0x0400D56D RID: 54637
		private static readonly List<GameObject> gx = new List<GameObject>();

		// Token: 0x0400D56E RID: 54638
		private static readonly List<GameObject> _5ds = new List<GameObject>();

		// Token: 0x0400D56F RID: 54639
		private static readonly List<GameObject> dsod = new List<GameObject>();

		// Token: 0x0400D570 RID: 54640
		private static readonly List<GameObject> zexal = new List<GameObject>();

		// Token: 0x0400D571 RID: 54641
		private static readonly List<GameObject> arcv = new List<GameObject>();

		// Token: 0x0400D572 RID: 54642
		private static readonly List<GameObject> vrains = new List<GameObject>();

		// Token: 0x0400D573 RID: 54643
		private static readonly List<GameObject> sevens = new List<GameObject>();

		// Token: 0x0400D574 RID: 54644
		private static readonly List<GameObject> npc = new List<GameObject>();

		// Token: 0x0400D575 RID: 54645
		private static readonly List<GameObject> gorush = new List<GameObject>();

		// Token: 0x0400D576 RID: 54646
		private List<GameObject> targetItems = new List<GameObject>();

		// Token: 0x0400D577 RID: 54647
		private readonly Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>
		{
			{
				"00",
				CharacterSelectorUI.dm
			},
			{
				"01",
				CharacterSelectorUI.gx
			},
			{
				"02",
				CharacterSelectorUI._5ds
			},
			{
				"03",
				CharacterSelectorUI.dsod
			},
			{
				"04",
				CharacterSelectorUI.zexal
			},
			{
				"05",
				CharacterSelectorUI.arcv
			},
			{
				"06",
				CharacterSelectorUI.vrains
			},
			{
				"07",
				CharacterSelectorUI.sevens
			},
			{
				"08",
				CharacterSelectorUI.npc
			},
			{
				"09",
				CharacterSelectorUI.gorush
			}
		};
	}
}
