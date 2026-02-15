using System;

namespace System
{
	// Token: 0x02000189 RID: 393
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003CDF1 File Offset: 0x0003AFF1
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x040005B8 RID: 1464
		private string comment;
	}
}
