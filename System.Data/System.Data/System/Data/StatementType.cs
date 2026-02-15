using System;

namespace System.Data
{
	/// <summary>Specifies the type of SQL query to be used by the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" />, <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" />, <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" />, or <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A3 RID: 163
	public enum StatementType
	{
		/// <summary>An SQL query that is a SELECT statement.</summary>
		// Token: 0x0400032B RID: 811
		Select,
		/// <summary>An SQL query that is an INSERT statement.</summary>
		// Token: 0x0400032C RID: 812
		Insert,
		/// <summary>An SQL query that is an UPDATE statement.</summary>
		// Token: 0x0400032D RID: 813
		Update,
		/// <summary>An SQL query that is a DELETE statement.</summary>
		// Token: 0x0400032E RID: 814
		Delete,
		/// <summary>A SQL query that is a batch statement.</summary>
		// Token: 0x0400032F RID: 815
		Batch
	}
}
