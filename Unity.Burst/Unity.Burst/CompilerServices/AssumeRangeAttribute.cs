using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class AssumeRangeAttribute : Attribute
	{
		// Token: 0x06000E31 RID: 3633 RVA: 0x00002050 File Offset: 0x00000250
		public AssumeRangeAttribute(long min, long max)
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00002050 File Offset: 0x00000250
		public AssumeRangeAttribute(ulong min, ulong max)
		{
		}
	}
}
