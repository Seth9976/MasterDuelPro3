using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200014D RID: 333
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00032295 File Offset: 0x00030495
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x0003229D File Offset: 0x0003049D
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x06000ACA RID: 2762 RVA: 0x000322A6 File Offset: 0x000304A6
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000322BC File Offset: 0x000304BC
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Id, reference, StringComparison.Ordinal));
			if (jsonSchema == null)
			{
				jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Location, reference, StringComparison.Ordinal));
			}
			return jsonSchema;
		}
	}
}
