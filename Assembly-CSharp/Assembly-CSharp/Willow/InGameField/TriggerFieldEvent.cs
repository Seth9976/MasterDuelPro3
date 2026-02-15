using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001577 RID: 5495
	[CreateAssetMenu]
	public class TriggerFieldEvent : BaseFieldEvent
	{
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06009F35 RID: 40757 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06009F36 RID: 40758 RVA: 0x0000216D File Offset: 0x0000036D
		private event Action m_responseTrigger
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

		// Token: 0x06009F37 RID: 40759 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterListener(Action action)
		{
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterListener(Action action)
		{
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterListener(TriggerFieldEventListener listener)
		{
		}

		// Token: 0x06009F3B RID: 40763 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterListener(TriggerFieldEventListener listener)
		{
		}

		// Token: 0x06009F3C RID: 40764 RVA: 0x0000216D File Offset: 0x0000036D
		public void Raise()
		{
		}

		// Token: 0x0400DE5E RID: 56926
		private readonly List<TriggerFieldEventListener> m_fieldEventListeners;
	}
}
