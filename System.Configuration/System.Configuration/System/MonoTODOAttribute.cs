using System;

namespace System
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x04000002 RID: 2
		private string comment;
	}
}
