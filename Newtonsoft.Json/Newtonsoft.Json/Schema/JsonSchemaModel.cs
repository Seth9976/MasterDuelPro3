using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000148 RID: 328
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0003185F File Offset: 0x0002FA5F
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00031867 File Offset: 0x0002FA67
		public bool Required { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00031870 File Offset: 0x0002FA70
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00031878 File Offset: 0x0002FA78
		public JsonSchemaType Type { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00031881 File Offset: 0x0002FA81
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00031889 File Offset: 0x0002FA89
		public int? MinimumLength { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00031892 File Offset: 0x0002FA92
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0003189A File Offset: 0x0002FA9A
		public int? MaximumLength { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000318A3 File Offset: 0x0002FAA3
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x000318AB File Offset: 0x0002FAAB
		public double? DivisibleBy { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000318B4 File Offset: 0x0002FAB4
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x000318BC File Offset: 0x0002FABC
		public double? Minimum { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000318C5 File Offset: 0x0002FAC5
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x000318CD File Offset: 0x0002FACD
		public double? Maximum { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000318D6 File Offset: 0x0002FAD6
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000318DE File Offset: 0x0002FADE
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000318E7 File Offset: 0x0002FAE7
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000318EF File Offset: 0x0002FAEF
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000318F8 File Offset: 0x0002FAF8
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00031900 File Offset: 0x0002FB00
		public int? MinimumItems { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00031909 File Offset: 0x0002FB09
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00031911 File Offset: 0x0002FB11
		public int? MaximumItems { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003191A File Offset: 0x0002FB1A
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00031922 File Offset: 0x0002FB22
		public IList<string> Patterns { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0003192B File Offset: 0x0002FB2B
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00031933 File Offset: 0x0002FB33
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0003193C File Offset: 0x0002FB3C
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00031944 File Offset: 0x0002FB44
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0003194D File Offset: 0x0002FB4D
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00031955 File Offset: 0x0002FB55
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0003195E File Offset: 0x0002FB5E
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00031966 File Offset: 0x0002FB66
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0003196F File Offset: 0x0002FB6F
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00031977 File Offset: 0x0002FB77
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00031980 File Offset: 0x0002FB80
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00031988 File Offset: 0x0002FB88
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00031991 File Offset: 0x0002FB91
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00031999 File Offset: 0x0002FB99
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000319A2 File Offset: 0x0002FBA2
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000319AA File Offset: 0x0002FBAA
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000319B3 File Offset: 0x0002FBB3
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x000319BB File Offset: 0x0002FBBB
		public bool UniqueItems { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000319C4 File Offset: 0x0002FBC4
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000319CC File Offset: 0x0002FBCC
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000319D5 File Offset: 0x0002FBD5
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x000319DD File Offset: 0x0002FBDD
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000319E6 File Offset: 0x0002FBE6
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00031A0C File Offset: 0x0002FC0C
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00031A5C File Offset: 0x0002FC5C
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || schema.Required.GetValueOrDefault();
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault();
			model.ExclusiveMaximum = model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault();
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = model.PositionalItemsValidation || schema.PositionalItemsValidation;
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			model.AllowAdditionalItems = model.AllowAdditionalItems && schema.AllowAdditionalItems;
			model.UniqueItems = model.UniqueItems || schema.UniqueItems;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow.GetValueOrDefault();
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
