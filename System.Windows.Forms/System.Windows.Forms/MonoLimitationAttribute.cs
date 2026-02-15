using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
