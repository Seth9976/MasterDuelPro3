using System;

namespace System
{
	// Token: 0x0200018B RID: 395
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x0003CE00 File Offset: 0x0003B000
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
