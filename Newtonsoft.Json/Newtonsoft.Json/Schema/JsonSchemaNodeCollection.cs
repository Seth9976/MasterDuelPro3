using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200014C RID: 332
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x00032285 File Offset: 0x00030485
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
