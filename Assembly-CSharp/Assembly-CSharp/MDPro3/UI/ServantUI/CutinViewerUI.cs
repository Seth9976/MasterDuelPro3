using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001456 RID: 5206
	public class CutinViewerUI : ServantUI
	{
		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x0600970F RID: 38671 RVA: 0x0015FF20 File Offset: 0x0015E120
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06009710 RID: 38672 RVA: 0x0015FF5C File Offset: 0x0015E15C
		private TMP_InputField Input
		{
			get
			{
				return this.m_Input = ((this.m_Input != null) ? this.m_Input : base.Manager.GetElement<TMP_InputField>("InputField"));
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06009711 RID: 38673 RVA: 0x0015FF98 File Offset: 0x0015E198
		public SelectionButton ButtonAutoPlay
		{
			get
			{
				return this.m_ButtonAutoPlay = ((this.m_ButtonAutoPlay != null) ? this.m_ButtonAutoPlay : base.Manager.GetElement<SelectionButton>("ButtonAutoPlay"));
			}
		}

		// Token: 0x06009712 RID: 38674 RVA: 0x0015FFD4 File Offset: 0x0015E1D4
		private void Awake()
		{
			this.Print("");
		}

		// Token: 0x06009713 RID: 38675 RVA: 0x0015FFE4 File Offset: 0x0015E1E4
		public void Print(string search = "")
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			List<string[]> tasks = new List<string[]>();
			foreach (Card card in CutinViewer.cards)
			{
				if (card.Name.Contains(search) || card.Id.ToString() == search)
				{
					string code = card.Id.ToString();
					string cardName = card.Name;
					string[] task = new string[] { code, cardName };
					tasks.Add(task);
				}
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemCutin.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 460f : 360f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 80f : 40f);
				this.superScrollView = new SuperScrollView(1, itemWidth, itemHeight, 0f, 0f, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 4);
				this.superScrollView.Print(tasks);
				if (this.superScrollView.items.Count > 0)
				{
					Program.instance.cutin.lastSelectedCutinItem = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Cutin>();
				}
			};
		}

		// Token: 0x06009714 RID: 38676 RVA: 0x001600CC File Offset: 0x0015E2CC
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Cutin component = item.GetComponent<SelectionToggle_Cutin>();
			component.code = int.Parse(task[0]);
			component.cardName = task[1];
			component.Refresh();
		}

		// Token: 0x06009715 RID: 38677 RVA: 0x001600F0 File Offset: 0x0015E2F0
		public void FocusOnInputField()
		{
			this.Input.ActivateInputField();
		}

		// Token: 0x06009716 RID: 38678 RVA: 0x001600FD File Offset: 0x0015E2FD
		public void OnAutoPlay()
		{
			Program.instance.cutin.AutoPlay();
		}

		// Token: 0x06009717 RID: 38679 RVA: 0x0016010E File Offset: 0x0015E30E
		public void SelectLastCutinItem()
		{
			UserInput.NextSelectionIsAxis = true;
			Program.instance.cutin.lastSelectedCutinItem.GetSelectable().Select();
		}

		// Token: 0x0400D578 RID: 54648
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D579 RID: 54649
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D57A RID: 54650
		private const string LABEL_IPT = "InputField";

		// Token: 0x0400D57B RID: 54651
		private TMP_InputField m_Input;

		// Token: 0x0400D57C RID: 54652
		private const string LABEL_SBN_AUTOPLAY = "ButtonAutoPlay";

		// Token: 0x0400D57D RID: 54653
		private SelectionButton m_ButtonAutoPlay;

		// Token: 0x0400D57E RID: 54654
		public SuperScrollView superScrollView;
	}
}
