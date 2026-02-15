using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyDescriptor.h")]
	public struct HierarchyPropertyDescriptor
	{
		// Token: 0x1700001D RID: 29
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000038AB File Offset: 0x00001AAB
		public int Size
		{
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000038B4 File Offset: 0x00001AB4
		public HierarchyPropertyStorageType Type
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x04000052 RID: 82
		private int m_Size;

		// Token: 0x04000053 RID: 83
		private HierarchyPropertyStorageType m_Type;
	}
}
