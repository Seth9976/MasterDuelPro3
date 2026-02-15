using System;

namespace UnityEngine
{
	// Token: 0x02000162 RID: 354
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x000201D1 File Offset: 0x0001E3D1
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x040005FC RID: 1532
		public readonly float min;
	}
}
