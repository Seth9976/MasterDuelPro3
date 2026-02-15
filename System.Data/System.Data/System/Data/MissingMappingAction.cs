using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when a mapping is missing from a source table or a source column.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000083 RID: 131
	public enum MissingMappingAction
	{
		/// <summary>The source column or source table is created and added to the <see cref="T:System.Data.DataSet" /> using its original name.</summary>
		// Token: 0x04000299 RID: 665
		Passthrough = 1,
		/// <summary>The column or table not having a mapping is ignored. Returns null.</summary>
		// Token: 0x0400029A RID: 666
		Ignore,
		/// <summary>An <see cref="T:System.InvalidOperationException" /> is generated if the specified column mapping is missing.</summary>
		// Token: 0x0400029B RID: 667
		Error
	}
}
