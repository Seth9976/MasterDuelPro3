using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009F RID: 159
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001BA63 File Offset: 0x00019C63
		public Type Type { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001BA6B File Offset: 0x00019C6B
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x06000535 RID: 1333 RVA: 0x0001BA73 File Offset: 0x00019C73
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}
