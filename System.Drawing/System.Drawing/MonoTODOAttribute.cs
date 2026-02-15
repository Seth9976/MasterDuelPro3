using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000205C File Offset: 0x0000025C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x04000001 RID: 1
		private string comment;
	}
}
