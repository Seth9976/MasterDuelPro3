using System;

namespace UnityEngine
{
	// Token: 0x02000161 RID: 353
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x000201B9 File Offset: 0x0001E3B9
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040005FA RID: 1530
		public readonly float min;

		// Token: 0x040005FB RID: 1531
		public readonly float max;
	}
}
