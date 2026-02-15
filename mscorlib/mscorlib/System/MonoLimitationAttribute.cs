using System;

namespace System
{
	// Token: 0x0200018A RID: 394
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x0003CE00 File Offset: 0x0003B000
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
