using System;

namespace System.Data.Common
{
	/// <summary>Provides static values that are used for the column names in the MetaDataCollection objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FC RID: 252
	public static class DbMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the CollectionName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000549 RID: 1353
		public static readonly string CollectionName = "CollectionName";

		/// <summary>Used by the GetSchema method to create the ColumnSize column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054A RID: 1354
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Used by the GetSchema method to create the CompositeIdentifierSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054B RID: 1355
		public static readonly string CompositeIdentifierSeparatorPattern = "CompositeIdentifierSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the CreateFormat column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054C RID: 1356
		public static readonly string CreateFormat = "CreateFormat";

		/// <summary>Used by the GetSchema method to create the CreateParameters column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054D RID: 1357
		public static readonly string CreateParameters = "CreateParameters";

		/// <summary>Used by the GetSchema method to create the DataSourceProductName column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054E RID: 1358
		public static readonly string DataSourceProductName = "DataSourceProductName";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersion column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400054F RID: 1359
		public static readonly string DataSourceProductVersion = "DataSourceProductVersion";

		/// <summary>Used by the GetSchema method to create the DataType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000550 RID: 1360
		public static readonly string DataType = "DataType";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersionNormalized column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000551 RID: 1361
		public static readonly string DataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		/// <summary>Used by the GetSchema method to create the GroupByBehavior column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000552 RID: 1362
		public static readonly string GroupByBehavior = "GroupByBehavior";

		/// <summary>Used by the GetSchema method to create the IdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000553 RID: 1363
		public static readonly string IdentifierCase = "IdentifierCase";

		/// <summary>Used by the GetSchema method to create the IdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000554 RID: 1364
		public static readonly string IdentifierPattern = "IdentifierPattern";

		/// <summary>Used by the GetSchema method to create the IsAutoIncrementable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000555 RID: 1365
		public static readonly string IsAutoIncrementable = "IsAutoIncrementable";

		/// <summary>Used by the GetSchema method to create the IsBestMatch column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000556 RID: 1366
		public static readonly string IsBestMatch = "IsBestMatch";

		/// <summary>Used by the GetSchema method to create the IsCaseSensitive column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000557 RID: 1367
		public static readonly string IsCaseSensitive = "IsCaseSensitive";

		/// <summary>Used by the GetSchema method to create the IsConcurrencyType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000558 RID: 1368
		public static readonly string IsConcurrencyType = "IsConcurrencyType";

		/// <summary>Used by the GetSchema method to create the IsFixedLength column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000559 RID: 1369
		public static readonly string IsFixedLength = "IsFixedLength";

		/// <summary>Used by the GetSchema method to create the IsFixedPrecisionScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055A RID: 1370
		public static readonly string IsFixedPrecisionScale = "IsFixedPrecisionScale";

		/// <summary>Used by the GetSchema method to create the IsLiteralSupported column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055B RID: 1371
		public static readonly string IsLiteralSupported = "IsLiteralSupported";

		/// <summary>Used by the GetSchema method to create the IsLong column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055C RID: 1372
		public static readonly string IsLong = "IsLong";

		/// <summary>Used by the GetSchema method to create the IsNullable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055D RID: 1373
		public static readonly string IsNullable = "IsNullable";

		/// <summary>Used by the GetSchema method to create the IsSearchable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055E RID: 1374
		public static readonly string IsSearchable = "IsSearchable";

		/// <summary>Used by the GetSchema method to create the IsSearchableWithLike column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400055F RID: 1375
		public static readonly string IsSearchableWithLike = "IsSearchableWithLike";

		/// <summary>Used by the GetSchema method to create the IsUnsigned column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000560 RID: 1376
		public static readonly string IsUnsigned = "IsUnsigned";

		/// <summary>Used by the GetSchema method to create the LiteralPrefix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000561 RID: 1377
		public static readonly string LiteralPrefix = "LiteralPrefix";

		/// <summary>Used by the GetSchema method to create the LiteralSuffix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000562 RID: 1378
		public static readonly string LiteralSuffix = "LiteralSuffix";

		/// <summary>Used by the GetSchema method to create the MaximumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000563 RID: 1379
		public static readonly string MaximumScale = "MaximumScale";

		/// <summary>Used by the GetSchema method to create the MinimumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000564 RID: 1380
		public static readonly string MinimumScale = "MinimumScale";

		/// <summary>Used by the GetSchema method to create the NumberOfIdentifierParts column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000565 RID: 1381
		public static readonly string NumberOfIdentifierParts = "NumberOfIdentifierParts";

		/// <summary>Used by the GetSchema method to create the NumberOfRestrictions column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000566 RID: 1382
		public static readonly string NumberOfRestrictions = "NumberOfRestrictions";

		/// <summary>Used by the GetSchema method to create the OrderByColumnsInSelect column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000567 RID: 1383
		public static readonly string OrderByColumnsInSelect = "OrderByColumnsInSelect";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerFormat column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000568 RID: 1384
		public static readonly string ParameterMarkerFormat = "ParameterMarkerFormat";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000569 RID: 1385
		public static readonly string ParameterMarkerPattern = "ParameterMarkerPattern";

		/// <summary>Used by the GetSchema method to create the ParameterNameMaxLength column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056A RID: 1386
		public static readonly string ParameterNameMaxLength = "ParameterNameMaxLength";

		/// <summary>Used by the GetSchema method to create the ParameterNamePattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056B RID: 1387
		public static readonly string ParameterNamePattern = "ParameterNamePattern";

		/// <summary>Used by the GetSchema method to create the ProviderDbType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056C RID: 1388
		public static readonly string ProviderDbType = "ProviderDbType";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056D RID: 1389
		public static readonly string QuotedIdentifierCase = "QuotedIdentifierCase";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056E RID: 1390
		public static readonly string QuotedIdentifierPattern = "QuotedIdentifierPattern";

		/// <summary>Used by the GetSchema method to create the ReservedWord column in the ReservedWords collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056F RID: 1391
		public static readonly string ReservedWord = "ReservedWord";

		/// <summary>Used by the GetSchema method to create the StatementSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000570 RID: 1392
		public static readonly string StatementSeparatorPattern = "StatementSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the StringLiteralPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000571 RID: 1393
		public static readonly string StringLiteralPattern = "StringLiteralPattern";

		/// <summary>Used by the GetSchema method to create the SupportedJoinOperators column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000572 RID: 1394
		public static readonly string SupportedJoinOperators = "SupportedJoinOperators";

		/// <summary>Used by the GetSchema method to create the TypeName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000573 RID: 1395
		public static readonly string TypeName = "TypeName";
	}
}
