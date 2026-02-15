using System;

namespace System.Xml
{
	// Token: 0x0200002D RID: 45
	internal interface IDtdDefaultAttributeInfo : IDtdAttributeInfo
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000182 RID: 386
		string DefaultValueExpanded { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000183 RID: 387
		object DefaultValueTyped { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000184 RID: 388
		int ValueLineNumber { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000185 RID: 389
		int ValueLinePosition { get; }
	}
}
