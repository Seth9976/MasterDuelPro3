using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;

namespace Willow.InGameField
{
	// Token: 0x02001569 RID: 5481
	[CreateAssetMenu]
	[Preserve]
	public class IntFieldEvent : BaseFieldEvent
	{
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06009EFD RID: 40701 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06009EFE RID: 40702 RVA: 0x0000216D File Offset: 0x0000036D
		private event Action<int> m_responseInt
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

		// Token: 0x06009EFF RID: 40703 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		private void OnDisable()
		{
		}

		// Token: 0x06009F00 RID: 40704 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void RegisterListener(Action<int> action)
		{
		}

		// Token: 0x06009F01 RID: 40705 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void UnregisterListener(Action<int> action)
		{
		}

		// Token: 0x06009F02 RID: 40706 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void RegisterListener(IntFieldEventListener listener)
		{
		}

		// Token: 0x06009F03 RID: 40707 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void UnregisterListener(IntFieldEventListener listener)
		{
		}

		// Token: 0x06009F04 RID: 40708 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void Raise(int value)
		{
		}

		// Token: 0x0400DE3F RID: 56895
		private readonly List<IntFieldEventListener> m_fieldEventListeners;
	}
}
