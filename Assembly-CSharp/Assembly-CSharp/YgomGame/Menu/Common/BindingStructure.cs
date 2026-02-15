using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B21 RID: 2849
	public class BindingStructure : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005302 RID: 21250 RVA: 0x0000216D File Offset: 0x0000036D
		public int itemId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06005304 RID: 21252 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005305 RID: 21253 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingStructure Binding(GameObject target, int itemId)
		{
			return null;
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0000216D File Offset: 0x0000036D
		private void SourceChange()
		{
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04009101 RID: 37121
		[SerializeField]
		private int m_ItemId;

		// Token: 0x04009102 RID: 37122
		private uint m_PrefCrc;

		// Token: 0x04009103 RID: 37123
		private DeckCaseWidget m_DeckCaseWidget;

		// Token: 0x04009104 RID: 37124
		private bool m_WidgetDirty;
	}
}
