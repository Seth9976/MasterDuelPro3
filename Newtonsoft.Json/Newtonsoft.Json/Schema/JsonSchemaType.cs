using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200014F RID: 335
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x04000645 RID: 1605
		None = 0,
		// Token: 0x04000646 RID: 1606
		String = 1,
		// Token: 0x04000647 RID: 1607
		Float = 2,
		// Token: 0x04000648 RID: 1608
		Integer = 4,
		// Token: 0x04000649 RID: 1609
		Boolean = 8,
		// Token: 0x0400064A RID: 1610
		Object = 16,
		// Token: 0x0400064B RID: 1611
		Array = 32,
		// Token: 0x0400064C RID: 1612
		Null = 64,
		// Token: 0x0400064D RID: 1613
		Any = 127
	}
}
