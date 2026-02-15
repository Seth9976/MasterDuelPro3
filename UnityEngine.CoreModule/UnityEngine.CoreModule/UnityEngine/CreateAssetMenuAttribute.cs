using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000179 RID: 377
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x1700027D RID: 637
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00020D2B File Offset: 0x0001EF2B
		public string menuName
		{
			[CompilerGenerated]
			set
			{
				this.<menuName>k__BackingField = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00020D34 File Offset: 0x0001EF34
		public string fileName
		{
			[CompilerGenerated]
			set
			{
				this.<fileName>k__BackingField = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00020D3D File Offset: 0x0001EF3D
		public int order
		{
			[CompilerGenerated]
			set
			{
				this.<order>k__BackingField = value;
			}
		}
	}
}
