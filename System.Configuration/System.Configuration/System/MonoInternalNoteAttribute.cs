using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020AB File Offset: 0x000002AB
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
