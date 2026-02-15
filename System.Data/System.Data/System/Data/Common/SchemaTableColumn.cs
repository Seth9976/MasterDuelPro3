using System;

namespace System.Data.Common
{
	/// <summary>Describes the column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000119 RID: 281
	public static class SchemaTableColumn
	{
		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000591 RID: 1425
		public static readonly string ColumnName = "ColumnName";

		/// <summary>Specifies the ordinal of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000592 RID: 1426
		public static readonly string ColumnOrdinal = "ColumnOrdinal";

		/// <summary>Specifies the size of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000593 RID: 1427
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Specifies the precision of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000594 RID: 1428
		public static readonly string NumericPrecision = "NumericPrecision";

		/// <summary>Specifies the scale of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000595 RID: 1429
		public static readonly string NumericScale = "NumericScale";

		/// <summary>Specifies the type of data in the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000596 RID: 1430
		public static readonly string DataType = "DataType";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000597 RID: 1431
		public static readonly string ProviderType = "ProviderType";

		/// <summary>Specifies the non-versioned provider-specific data type of the column.</summary>
		// Token: 0x04000598 RID: 1432
		public static readonly string NonVersionedProviderType = "NonVersionedProviderType";

		/// <summary>Specifies whether this column contains long data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000599 RID: 1433
		public static readonly string IsLong = "IsLong";

		/// <summary>Specifies whether value DBNull is allowed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059A RID: 1434
		public static readonly string AllowDBNull = "AllowDBNull";

		/// <summary>Specifies whether this column is aliased.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059B RID: 1435
		public static readonly string IsAliased = "IsAliased";

		/// <summary>Specifies whether this column is an expression.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059C RID: 1436
		public static readonly string IsExpression = "IsExpression";

		/// <summary>Specifies whether this column is a key for the table. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059D RID: 1437
		public static readonly string IsKey = "IsKey";

		/// <summary>Specifies whether a unique constraint applies to this column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059E RID: 1438
		public static readonly string IsUnique = "IsUnique";

		/// <summary>Specifies the name of the schema in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400059F RID: 1439
		public static readonly string BaseSchemaName = "BaseSchemaName";

		/// <summary>Specifies the name of the table in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A0 RID: 1440
		public static readonly string BaseTableName = "BaseTableName";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A1 RID: 1441
		public static readonly string BaseColumnName = "BaseColumnName";
	}
}
