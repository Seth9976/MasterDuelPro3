using System;

namespace UnityEngine
{
	// Token: 0x02000160 RID: 352
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x000201A8 File Offset: 0x0001E3A8
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x040005F9 RID: 1529
		public readonly string header;
	}
}
