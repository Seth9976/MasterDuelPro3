using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C1 RID: 193
	[NullableContext(1)]
	[Nullable(0)]
	internal class EnumInfo
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x0001F9BB File Offset: 0x0001DBBB
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x0400043D RID: 1085
		public readonly bool IsFlags;

		// Token: 0x0400043E RID: 1086
		public readonly ulong[] Values;

		// Token: 0x0400043F RID: 1087
		public readonly string[] Names;

		// Token: 0x04000440 RID: 1088
		public readonly string[] ResolvedNames;
	}
}
