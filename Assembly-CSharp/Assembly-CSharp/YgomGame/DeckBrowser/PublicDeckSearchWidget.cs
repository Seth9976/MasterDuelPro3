using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Deck;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F96 RID: 3990
	public class PublicDeckSearchWidget : DeckBrowserOptionWidget
	{
		// Token: 0x0600756A RID: 30058 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public PublicDeckSearchWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<PublicDeckSearchWidget> onCreated)
		{
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeInfinityScrollView()
		{
		}

		// Token: 0x0600756D RID: 30061 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007570 RID: 30064 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCategories(List<int> categoryIds)
		{
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTags(List<int> tagIds)
		{
		}

		// Token: 0x06007572 RID: 30066 RVA: 0x0000216A File Offset: 0x0000036A
		public string LangKey(string lang)
		{
			return null;
		}

		// Token: 0x0400AEAF RID: 44719
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForPublicDeckSearch";

		// Token: 0x0400AEB0 RID: 44720
		private readonly string k_ELabelTagArea;

		// Token: 0x0400AEB1 RID: 44721
		private readonly Transform m_TagArea;

		// Token: 0x0400AEB2 RID: 44722
		private readonly InfinityScrollView m_TagScrollView;

		// Token: 0x0400AEB3 RID: 44723
		private List<CategoryReference> m_SelectedCategories;

		// Token: 0x0400AEB4 RID: 44724
		private List<CategoryReference> m_SelectedTags;

		// Token: 0x0400AEB5 RID: 44725
		private ShortcutKeySetter m_ShortcutSettings;
	}
}
