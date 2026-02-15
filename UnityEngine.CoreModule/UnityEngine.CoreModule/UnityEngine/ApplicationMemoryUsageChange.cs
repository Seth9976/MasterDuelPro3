using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000096 RID: 150
	public struct ApplicationMemoryUsageChange
	{
		// Token: 0x17000063 RID: 99
		// (set) Token: 0x0600028B RID: 651 RVA: 0x000064BF File Offset: 0x000046BF
		private ApplicationMemoryUsage memoryUsage
		{
			[CompilerGenerated]
			set
			{
				this.<memoryUsage>k__BackingField = value;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000064C8 File Offset: 0x000046C8
		public ApplicationMemoryUsageChange(ApplicationMemoryUsage usage)
		{
			this.memoryUsage = usage;
		}
	}
}
