using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000152 RID: 338
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum UndefinedSchemaIdHandling
	{
		// Token: 0x04000653 RID: 1619
		None,
		// Token: 0x04000654 RID: 1620
		UseTypeName,
		// Token: 0x04000655 RID: 1621
		UseAssemblyQualifiedName
	}
}
