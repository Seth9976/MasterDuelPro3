using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x0200146E RID: 5230
	public class MateViewerUI : ServantUI
	{
		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060097F9 RID: 38905 RVA: 0x00164C24 File Offset: 0x00162E24
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060097FA RID: 38906 RVA: 0x00164C60 File Offset: 0x00162E60
		private SelectionButton ButtonInteraction
		{
			get
			{
				return this.m_ButtonInteraction = ((this.m_ButtonInteraction != null) ? this.m_ButtonInteraction : base.Manager.GetElement<SelectionButton>("ButtonInteraction"));
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060097FB RID: 38907 RVA: 0x00164C9C File Offset: 0x00162E9C
		private TMP_InputField Input
		{
			get
			{
				return this.m_Input = ((this.m_Input != null) ? this.m_Input : base.Manager.GetElement<TMP_InputField>("InputField"));
			}
		}

		// Token: 0x060097FC RID: 38908 RVA: 0x00164CD8 File Offset: 0x00162ED8
		private void Awake()
		{
			this.Load();
		}

		// Token: 0x060097FD RID: 38909 RVA: 0x00164CE0 File Offset: 0x00162EE0
		public void Load()
		{
			MateViewerUI.cards.Clear();
			for (int i = 0; i < MateViewerUI.crossDuelMates.Count; i++)
			{
				Card card = CardsManager.Get(MateViewerUI.crossDuelMates[i], true);
				if (card.Id == 0)
				{
					card.Id = MateViewerUI.crossDuelMates[i];
					card.Name = MateViewerUI.GetRushDuelMateName(MateViewerUI.crossDuelMates[i]);
				}
				MateViewerUI.cards.Add(card);
			}
			MateViewerUI.cards.Sort(CardsManager.ComparisonOfCard());
			this.Print("");
		}

		// Token: 0x060097FE RID: 38910 RVA: 0x00164D74 File Offset: 0x00162F74
		public void Print(string search = "")
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			List<string[]> tasks = new List<string[]>();
			foreach (Card card in MateViewerUI.cards)
			{
				if (card.Name.Contains(search))
				{
					string[] task = new string[]
					{
						card.Id.ToString(),
						card.Name
					};
					tasks.Add(task);
				}
			}
			foreach (Items.Item mate in Program.items.mates)
			{
				if (!string.IsNullOrEmpty(mate.name) && mate.name.Contains(search) && !mate.notReady)
				{
					string[] array = new string[2];
					int num = 0;
					int id = mate.id;
					array[num] = id.ToString();
					array[1] = mate.name;
					string[] task2 = array;
					tasks.Add(task2);
				}
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemMate.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 460f : 360f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 80f : 40f);
				this.superScrollView = new SuperScrollView(1, itemWidth, itemHeight, 0f, 0f, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 4);
				this.superScrollView.Print(tasks);
				if (this.superScrollView.items.Count > 0)
				{
					Program.instance.mate.lastSelectedMateItem = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Mate>();
				}
			};
		}

		// Token: 0x060097FF RID: 38911 RVA: 0x00164EDC File Offset: 0x001630DC
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Mate component = item.GetComponent<SelectionToggle_Mate>();
			component.code = int.Parse(task[0]);
			component.mateName = task[1];
			component.Refresh();
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x00164F00 File Offset: 0x00163100
		public static string GetRushDuelMateName(int code)
		{
			if (code <= 120120018)
			{
				if (code <= 120110006)
				{
					if (code <= 120105010)
					{
						if (code == 120105001)
						{
							return InterString.Get("七星道魔术师", 0);
						}
						if (code == 120105010)
						{
							return InterString.Get("落单使魔", 0);
						}
					}
					else
					{
						if (code == 120110001)
						{
							return InterString.Get("连击龙 齿车戒龙", 0);
						}
						if (code == 120110006)
						{
							return InterString.Get("双刃龙", 0);
						}
					}
				}
				else if (code <= 120115001)
				{
					if (code == 120110010)
					{
						return InterString.Get("掌上小龙", 0);
					}
					if (code == 120115001)
					{
						return InterString.Get("七星道魔女", 0);
					}
				}
				else
				{
					if (code == 120120003)
					{
						return InterString.Get("古之守护龟", 0);
					}
					if (code == 120120018)
					{
						return InterString.Get("耳语妖精", 0);
					}
				}
			}
			else if (code <= 120130016)
			{
				if (code <= 120120025)
				{
					if (code == 120120024)
					{
						return InterString.Get("龙队布局投球手", 0);
					}
					if (code == 120120025)
					{
						return InterString.Get("龙队翻盘击球手", 0);
					}
				}
				else
				{
					if (code == 120120029)
					{
						return InterString.Get("魔将 雅灭鲁拉", 0);
					}
					if (code == 120130016)
					{
						return InterString.Get("七星道法师", 0);
					}
				}
			}
			else if (code <= 120140023)
			{
				if (code == 120130026)
				{
					return InterString.Get("斗将 难得斯", 0);
				}
				if (code == 120140023)
				{
					return InterString.Get("王家魔族·骨肉皮", 0);
				}
			}
			else
			{
				if (code == 120145014)
				{
					return InterString.Get("火星心少女", 0);
				}
				if (code == 120150002)
				{
					return InterString.Get("超魔机神 大霸道王", 0);
				}
				if (code == 120155019)
				{
					return InterString.Get("祭神 莫多丽娜", 0);
				}
			}
			return string.Empty;
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x00165138 File Offset: 0x00163338
		public void OnMateTap()
		{
			if (Program.instance.mate.mate == null)
			{
				return;
			}
			Program.instance.mate.mate.Play(Mate.MateAction.Tap);
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x00165167 File Offset: 0x00163367
		public void FocusOnInputField()
		{
			this.Input.ActivateInputField();
		}

		// Token: 0x06009803 RID: 38915 RVA: 0x00165174 File Offset: 0x00163374
		public void SelectButtonInteract()
		{
			this.ButtonInteraction.GetSelectable().Select();
		}

		// Token: 0x06009804 RID: 38916 RVA: 0x00165186 File Offset: 0x00163386
		public void SelectLastMateItem()
		{
			Program.instance.mate.lastSelectedMateItem.GetSelectable().Select();
		}

		// Token: 0x0400D615 RID: 54805
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D616 RID: 54806
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D617 RID: 54807
		private const string LABEL_SBN_INTERACTION = "ButtonInteraction";

		// Token: 0x0400D618 RID: 54808
		private SelectionButton m_ButtonInteraction;

		// Token: 0x0400D619 RID: 54809
		private const string LABEL_IPT = "InputField";

		// Token: 0x0400D61A RID: 54810
		private TMP_InputField m_Input;

		// Token: 0x0400D61B RID: 54811
		private static readonly List<int> crossDuelMates = new List<int>();

		// Token: 0x0400D61C RID: 54812
		private static readonly List<Card> cards = new List<Card>();

		// Token: 0x0400D61D RID: 54813
		public SuperScrollView superScrollView;
	}
}
