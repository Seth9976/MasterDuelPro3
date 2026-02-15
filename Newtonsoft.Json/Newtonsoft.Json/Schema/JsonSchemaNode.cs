using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200014A RID: 330
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000320C6 File Offset: 0x000302C6
		public string Id { get; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x000320CE File Offset: 0x000302CE
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000320D6 File Offset: 0x000302D6
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000320DE File Offset: 0x000302DE
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000320E6 File Offset: 0x000302E6
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000320EE File Offset: 0x000302EE
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x000320F6 File Offset: 0x000302F6
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x000320FF File Offset: 0x000302FF
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00032107 File Offset: 0x00030307
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x06000ABE RID: 2750 RVA: 0x00032110 File Offset: 0x00030310
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[] { schema });
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0003216C File Offset: 0x0003036C
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[] { schema }).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00032200 File Offset: 0x00030400
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003220C File Offset: 0x0003040C
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", schemata.Select((JsonSchema s) => s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}
	}
}
