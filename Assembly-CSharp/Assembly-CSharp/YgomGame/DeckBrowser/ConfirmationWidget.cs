using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F8B RID: 3979
	public class ConfirmationWidget : DeckBrowserOptionWidget
	{
		// Token: 0x060074B0 RID: 29872 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public ConfirmationWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<ConfirmationWidget> onCreated)
		{
		}

		// Token: 0x060074B2 RID: 29874 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int deckId, int eventId, DeckSelectViewController2.DeckEventType deckType, Dictionary<string, object> accsesories = null)
		{
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeleteDeckDialog()
		{
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetNeuronToken()
		{
		}

		// Token: 0x060074B5 RID: 29877 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExportMyDeck(bool isFirst = false)
		{
		}

		// Token: 0x060074B6 RID: 29878 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeleteDeck()
		{
		}

		// Token: 0x0400ADAD RID: 44461
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForConfirmation";

		// Token: 0x0400ADAE RID: 44462
		private ShortcutKeySetter m_ShortcutSettings;

		// Token: 0x0400ADAF RID: 44463
		private int m_DeckID;

		// Token: 0x0400ADB0 RID: 44464
		private int m_EventID;

		// Token: 0x0400ADB1 RID: 44465
		private DeckSelectViewController2.DeckEventType m_DeckType;

		// Token: 0x0400ADB2 RID: 44466
		private Dictionary<string, object> m_Accsesories;

		// Token: 0x0400ADB3 RID: 44467
		public Action popViewCallback;

		// Token: 0x0400ADB4 RID: 44468
		public Action deleteWCSFinalDeckCallback;
	}
}
