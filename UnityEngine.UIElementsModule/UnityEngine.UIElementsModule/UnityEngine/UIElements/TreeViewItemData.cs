using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000163 RID: 355
	public readonly struct TreeViewItemData<T>
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00033998 File Offset: 0x00031B98
		public int id { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000339A0 File Offset: 0x00031BA0
		public T data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000339A8 File Offset: 0x00031BA8
		public IEnumerable<TreeViewItemData<T>> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x040006E8 RID: 1768
		[CreateProperty]
		private readonly T m_Data;

		// Token: 0x040006E9 RID: 1769
		private readonly IList<TreeViewItemData<T>> m_Children;
	}
}
