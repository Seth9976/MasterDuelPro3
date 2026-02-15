using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F93 RID: 3987
	public class PickupCardSelectionWidget : DeckBrowserOptionWidget
	{
		// Token: 0x0600754F RID: 30031 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public PickupCardSelectionWidget(ElementObjectManager eom, int id, int eventId, int deckcaseId, ProfileEditViewController.EditType editType)
			: base(null)
		{
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, int id, int eventId, ProfileEditViewController.EditType editType, int deckcaseId, Action<PickupCardSelectionWidget> onCreated)
		{
		}

		// Token: 0x06007551 RID: 30033 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeAccsesary()
		{
		}

		// Token: 0x06007552 RID: 30034 RVA: 0x0000216A File Offset: 0x0000036A
		private int[] DictToArray(Dictionary<string, object> dict)
		{
			return null;
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x0000216D File Offset: 0x0000036D
		public void SavePickups()
		{
		}

		// Token: 0x0400AE8C RID: 44684
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForPickupCardSelection";

		// Token: 0x0400AE8D RID: 44685
		private ShortcutKeySetter m_ShortcutSettings;

		// Token: 0x0400AE8E RID: 44686
		public Action popViewCallback;

		// Token: 0x0400AE8F RID: 44687
		private int m_DeckID;

		// Token: 0x0400AE90 RID: 44688
		private int m_EventID;

		// Token: 0x0400AE91 RID: 44689
		private ProfileEditViewController.EditType m_EditType;

		// Token: 0x0400AE92 RID: 44690
		private int m_DeckCaseId;

		// Token: 0x0400AE93 RID: 44691
		private int m_ProtecterId;

		// Token: 0x0400AE94 RID: 44692
		private int[] m_PickCardIds;

		// Token: 0x0400AE95 RID: 44693
		private int[] m_PickPremiumIds;

		// Token: 0x0400AE96 RID: 44694
		private readonly string k_ELabelDeckCase;

		// Token: 0x0400AE97 RID: 44695
		private GameObject m_DeckCaseImage;

		// Token: 0x0400AE98 RID: 44696
		public PickupCursorManager pickupCursorManager;

		// Token: 0x0400AE99 RID: 44697
		public Action<DeckBrowserViewController> pickupCardsActionCallback;
	}
}
