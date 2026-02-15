using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000206B File Offset: 0x0000026B
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
