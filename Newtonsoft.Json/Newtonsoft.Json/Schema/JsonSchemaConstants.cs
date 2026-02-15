using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000143 RID: 323
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal static class JsonSchemaConstants
	{
		// Token: 0x040005F2 RID: 1522
		public const string TypePropertyName = "type";

		// Token: 0x040005F3 RID: 1523
		public const string PropertiesPropertyName = "properties";

		// Token: 0x040005F4 RID: 1524
		public const string ItemsPropertyName = "items";

		// Token: 0x040005F5 RID: 1525
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x040005F6 RID: 1526
		public const string RequiredPropertyName = "required";

		// Token: 0x040005F7 RID: 1527
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x040005F8 RID: 1528
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x040005F9 RID: 1529
		public const string RequiresPropertyName = "requires";

		// Token: 0x040005FA RID: 1530
		public const string MinimumPropertyName = "minimum";

		// Token: 0x040005FB RID: 1531
		public const string MaximumPropertyName = "maximum";

		// Token: 0x040005FC RID: 1532
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x040005FD RID: 1533
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x040005FE RID: 1534
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x040005FF RID: 1535
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x04000600 RID: 1536
		public const string PatternPropertyName = "pattern";

		// Token: 0x04000601 RID: 1537
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x04000602 RID: 1538
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x04000603 RID: 1539
		public const string EnumPropertyName = "enum";

		// Token: 0x04000604 RID: 1540
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x04000605 RID: 1541
		public const string TitlePropertyName = "title";

		// Token: 0x04000606 RID: 1542
		public const string DescriptionPropertyName = "description";

		// Token: 0x04000607 RID: 1543
		public const string FormatPropertyName = "format";

		// Token: 0x04000608 RID: 1544
		public const string DefaultPropertyName = "default";

		// Token: 0x04000609 RID: 1545
		public const string TransientPropertyName = "transient";

		// Token: 0x0400060A RID: 1546
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x0400060B RID: 1547
		public const string HiddenPropertyName = "hidden";

		// Token: 0x0400060C RID: 1548
		public const string DisallowPropertyName = "disallow";

		// Token: 0x0400060D RID: 1549
		public const string ExtendsPropertyName = "extends";

		// Token: 0x0400060E RID: 1550
		public const string IdPropertyName = "id";

		// Token: 0x0400060F RID: 1551
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x04000610 RID: 1552
		public const string OptionValuePropertyName = "value";

		// Token: 0x04000611 RID: 1553
		public const string OptionLabelPropertyName = "label";

		// Token: 0x04000612 RID: 1554
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping = new Dictionary<string, JsonSchemaType>
		{
			{
				"string",
				JsonSchemaType.String
			},
			{
				"object",
				JsonSchemaType.Object
			},
			{
				"integer",
				JsonSchemaType.Integer
			},
			{
				"number",
				JsonSchemaType.Float
			},
			{
				"null",
				JsonSchemaType.Null
			},
			{
				"boolean",
				JsonSchemaType.Boolean
			},
			{
				"array",
				JsonSchemaType.Array
			},
			{
				"any",
				JsonSchemaType.Any
			}
		};
	}
}
