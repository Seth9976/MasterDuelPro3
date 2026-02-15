using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F92 RID: 3986
	public class OptionalStructureWidget : DeckBrowserOptionWidget
	{
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600754A RID: 30026 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600754B RID: 30027 RVA: 0x0000216D File Offset: 0x0000036D
		public bool getFirstStructureButtonEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public OptionalStructureWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(int structureId, Transform parent, Action<OptionalStructureWidget> onCreated)
		{
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetStructureId(int structureId)
		{
		}

		// Token: 0x0400AE84 RID: 44676
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForStructureDeck";

		// Token: 0x0400AE85 RID: 44677
		private readonly string k_ELabelDeckCaseIcon;

		// Token: 0x0400AE86 RID: 44678
		private readonly Image m_DeckCaseIcon;

		// Token: 0x0400AE87 RID: 44679
		private ShortcutKeySetter m_ShortcutSettings;

		// Token: 0x0400AE88 RID: 44680
		private int m_structureId;

		// Token: 0x0400AE89 RID: 44681
		protected string k_ELabelGetFirstStructureButton;

		// Token: 0x0400AE8A RID: 44682
		protected SelectionButton m_GetFirstStructureButton;

		// Token: 0x0400AE8B RID: 44683
		public Action onGetFirstStructureCallback;
	}
}
