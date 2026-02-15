using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class IgnoreWarningAttribute : Attribute
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x00002050 File Offset: 0x00000250
		public IgnoreWarningAttribute(int warning)
		{
		}
	}
}
