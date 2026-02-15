using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F97 RID: 3991
	public class SoloDeckOptionWidget : DeckBrowserOptionWidget
	{
		// Token: 0x06007573 RID: 30067 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public SoloDeckOptionWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007574 RID: 30068 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<SoloDeckOptionWidget> onCreated)
		{
		}

		// Token: 0x0400AEB6 RID: 44726
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForSoloDeck";

		// Token: 0x0400AEB7 RID: 44727
		private ShortcutKeySetter m_ShortcutSettings;
	}
}
