using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Deck
{
	// Token: 0x02000FC9 RID: 4041
	public class DeckEditFooter
	{
		// Token: 0x060078F7 RID: 30967 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(ElementObjectManager template, Transform parent, KeyConfigContainer keyConfig)
		{
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateFooterDescription(DeckEditFooter.FooterType footerType, string keyLabel, string text)
		{
		}

		// Token: 0x060078F9 RID: 30969 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateFooterDescription(DeckEditFooter.FooterType footerType, SelectorManager.KeyType keyType, string text)
		{
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateFooterDescription(DeckEditFooter.FooterType footerType, int buttonID, string text)
		{
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisp(DeckEditFooter.FooterType footerType, bool disp)
		{
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x0000216D File Offset: 0x0000036D
		public void RequestDisplay(DeckEditFooter.FooterType footerType, bool disp)
		{
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x0000216D File Offset: 0x0000036D
		public void RequestHideAll()
		{
		}

		// Token: 0x0400B0C2 RID: 45250
		private ElementObjectManager template;

		// Token: 0x0400B0C3 RID: 45251
		private Transform parent;

		// Token: 0x0400B0C4 RID: 45252
		private KeyConfigContainer keyConfig;

		// Token: 0x0400B0C5 RID: 45253
		private Dictionary<DeckEditFooter.FooterType, GameObject> footerList;

		// Token: 0x0400B0C6 RID: 45254
		private Dictionary<DeckEditFooter.FooterType, DeckEditFooter.DisplayRequest> dispRequest;

		// Token: 0x0400B0C7 RID: 45255
		private bool requestUpdateDisplay;

		// Token: 0x02000FCA RID: 4042
		public enum FooterType
		{
			// Token: 0x0400B0C9 RID: 45257
			CursorJump,
			// Token: 0x0400B0CA RID: 45258
			OptionL1,
			// Token: 0x0400B0CB RID: 45259
			OptionR1,
			// Token: 0x0400B0CC RID: 45260
			AddDeckCard,
			// Token: 0x0400B0CD RID: 45261
			RemoveDeckCard,
			// Token: 0x0400B0CE RID: 45262
			CardDetail,
			// Token: 0x0400B0CF RID: 45263
			ChangeActiveWindow
		}

		// Token: 0x02000FCB RID: 4043
		private class DisplayRequest
		{
			// Token: 0x0400B0D0 RID: 45264
			public bool display;
		}
	}
}
