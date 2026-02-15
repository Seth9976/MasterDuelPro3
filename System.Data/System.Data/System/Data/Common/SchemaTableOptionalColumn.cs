using System;

namespace System.Data.Common
{
	/// <summary>Describes optional column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011A RID: 282
	public static class SchemaTableOptionalColumn
	{
		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A2 RID: 1442
		public static readonly string ProviderSpecificDataType = "ProviderSpecificDataType";

		/// <summary>Specifies whether the column values in the column are automatically incremented.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A3 RID: 1443
		public static readonly string IsAutoIncrement = "IsAutoIncrement";

		/// <summary>Specifies whether this column is hidden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A4 RID: 1444
		public static readonly string IsHidden = "IsHidden";

		/// <summary>Specifies whether this column is read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A5 RID: 1445
		public static readonly string IsReadOnly = "IsReadOnly";

		/// <summary>Specifies whether this column contains row version information.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A6 RID: 1446
		public static readonly string IsRowVersion = "IsRowVersion";

		/// <summary>The server name of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A7 RID: 1447
		public static readonly string BaseServerName = "BaseServerName";

		/// <summary>The name of the catalog associated with the results of the latest query.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A8 RID: 1448
		public static readonly string BaseCatalogName = "BaseCatalogName";

		/// <summary>Specifies the value at which the series for new identity columns is assigned.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005A9 RID: 1449
		public static readonly string AutoIncrementSeed = "AutoIncrementSeed";

		/// <summary>Specifies the increment between values in the identity column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005AA RID: 1450
		public static readonly string AutoIncrementStep = "AutoIncrementStep";

		/// <summary>The default value for the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005AB RID: 1451
		public static readonly string DefaultValue = "DefaultValue";

		/// <summary>The expression used to compute the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005AC RID: 1452
		public static readonly string Expression = "Expression";

		/// <summary>The namespace for the table that contains the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005AD RID: 1453
		public static readonly string BaseTableNamespace = "BaseTableNamespace";

		/// <summary>The namespace of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040005AE RID: 1454
		public static readonly string BaseColumnNamespace = "BaseColumnNamespace";

		/// <summary>Specifies the mapping for the column.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040005AF RID: 1455
		public static readonly string ColumnMapping = "ColumnMapping";
	}
}
