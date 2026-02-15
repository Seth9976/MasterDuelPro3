using System;

namespace System
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
