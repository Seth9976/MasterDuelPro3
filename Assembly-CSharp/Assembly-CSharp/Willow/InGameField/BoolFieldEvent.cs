using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x0200155D RID: 5469
	[CreateAssetMenu]
	public class BoolFieldEvent : BaseFieldEvent
	{
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06009ECB RID: 40651 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06009ECC RID: 40652 RVA: 0x0000216D File Offset: 0x0000036D
		private event Action<bool> m_responseBool
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

		// Token: 0x06009ECD RID: 40653 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009ECE RID: 40654 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterListener(Action<bool> action)
		{
		}

		// Token: 0x06009ECF RID: 40655 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterListener(Action<bool> action)
		{
		}

		// Token: 0x06009ED0 RID: 40656 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterListener(BoolFieldEventListener listener)
		{
		}

		// Token: 0x06009ED1 RID: 40657 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterListener(BoolFieldEventListener listener)
		{
		}

		// Token: 0x06009ED2 RID: 40658 RVA: 0x0000216D File Offset: 0x0000036D
		public void Raise(bool value)
		{
		}

		// Token: 0x0400DE2D RID: 56877
		private readonly List<BoolFieldEventListener> m_fieldEventListeners;
	}
}
