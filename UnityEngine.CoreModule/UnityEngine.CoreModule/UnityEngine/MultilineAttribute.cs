using System;

namespace UnityEngine
{
	// Token: 0x02000163 RID: 355
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x000201E2 File Offset: 0x0001E3E2
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x040005FD RID: 1533
		public readonly int lines;
	}
}
