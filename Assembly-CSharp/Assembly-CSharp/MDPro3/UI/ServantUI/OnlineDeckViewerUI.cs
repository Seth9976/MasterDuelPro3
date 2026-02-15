using System;
using System.Collections.Generic;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001477 RID: 5239
	public class OnlineDeckViewerUI : ServantUI
	{
		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06009848 RID: 38984 RVA: 0x00166790 File Offset: 0x00164990
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06009849 RID: 38985 RVA: 0x001667CC File Offset: 0x001649CC
		private TextMeshProUGUI TextDeckNumValue
		{
			get
			{
				return this.m_TextDeckNumValue = ((this.m_TextDeckNumValue != null) ? this.m_TextDeckNumValue : base.Manager.GetElement<TextMeshProUGUI>("TextDeckNumValue"));
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x0600984A RID: 38986 RVA: 0x00166808 File Offset: 0x00164A08
		public TMP_InputField InputDeckName
		{
			get
			{
				return this.m_InputDeckName = ((this.m_InputDeckName != null) ? this.m_InputDeckName : base.Manager.GetElement<TMP_InputField>("InputFieldDeckName"));
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x0600984B RID: 38987 RVA: 0x00166844 File Offset: 0x00164A44
		public TMP_InputField InputDeckAuthor
		{
			get
			{
				return this.m_InputDeckAuthor = ((this.m_InputDeckAuthor != null) ? this.m_InputDeckAuthor : base.Manager.GetElement<TMP_InputField>("InputFieldDeckAuthor"));
			}
		}

		// Token: 0x0600984C RID: 38988 RVA: 0x00166880 File Offset: 0x00164A80
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView == null)
			{
				return;
			}
			superScrollView.Clear();
		}

		// Token: 0x0600984D RID: 38989 RVA: 0x00166898 File Offset: 0x00164A98
		public void OnSearchSubmit()
		{
			Program.instance.onlineDeckViewer.RefreshList();
		}

		// Token: 0x0600984E RID: 38990 RVA: 0x001668AC File Offset: 0x00164AAC
		public void Print()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			this.TextDeckNumValue.text = OnlineDeckViewer.decks.Length.ToString();
			Config.GetUIScale(1.5f);
			Addressables.LoadAssetAsync<GameObject>("UI/ItemDeckOnline.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 336f : 260f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 300f : 232f);
				float space = (PropertyOverrider.NeedMobileLayout() ? 30f : 24f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 196f : 150f) - space;
				this.superScrollView = new SuperScrollView(-1, itemWidth + space, itemHeight + space, 10f, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 2);
				List<string[]> tasks = new List<string[]>();
				foreach (OnlineDeck.OnlineDeckData deck in OnlineDeckViewer.decks)
				{
					string[] task = new string[]
					{
						deck.deckName,
						deck.deckContributor,
						deck.deckId,
						(deck.deckCase == 0) ? "1080001" : deck.deckCase.ToString(),
						deck.deckCoverCard1.ToString(),
						deck.deckCoverCard2.ToString(),
						deck.deckCoverCard3.ToString(),
						(deck.deckProtector == 0) ? "1070001" : deck.deckProtector.ToString(),
						deck.deckLike.ToString(),
						deck.GetOnlineDeckLocalTime().ToString()
					};
					tasks.Add(task);
				}
				this.superScrollView.Print(tasks);
				if (this.superScrollView.items.Count > 0)
				{
					Program.instance.onlineDeckViewer.lastSelectedDeckItem = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_DeckOnline>();
					if (Cursor.lockState == CursorLockMode.Locked)
					{
						Program.instance.onlineDeckViewer.Select(false);
					}
				}
			};
		}

		// Token: 0x0600984F RID: 38991 RVA: 0x00166910 File Offset: 0x00164B10
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_DeckOnline component = item.GetComponent<SelectionToggle_DeckOnline>();
			component.deckName = task[0];
			component.deckAuthor = task[1];
			component.deckId = task[2];
			component.deckCase = int.Parse(task[3]);
			component.card0 = int.Parse(task[4]);
			component.card1 = int.Parse(task[5]);
			component.card2 = int.Parse(task[6]);
			component.protector = task[7];
			component.like = int.Parse(task[8]);
			component.lastDate = task[9];
			component.Refresh();
		}

		// Token: 0x0400D667 RID: 54887
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D668 RID: 54888
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D669 RID: 54889
		private const string LABEL_TXT_DECKNUMVALUE = "TextDeckNumValue";

		// Token: 0x0400D66A RID: 54890
		private TextMeshProUGUI m_TextDeckNumValue;

		// Token: 0x0400D66B RID: 54891
		private const string LABEL_IPT_DECKNAME = "InputFieldDeckName";

		// Token: 0x0400D66C RID: 54892
		private TMP_InputField m_InputDeckName;

		// Token: 0x0400D66D RID: 54893
		private const string LABEL_IPT_DECKAUTHOR = "InputFieldDeckAuthor";

		// Token: 0x0400D66E RID: 54894
		private TMP_InputField m_InputDeckAuthor;

		// Token: 0x0400D66F RID: 54895
		public SuperScrollView superScrollView;
	}
}
