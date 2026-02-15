using System;

namespace System.Data
{
	/// <summary>Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and update a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007D RID: 125
	public interface IDbDataAdapter
	{
		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060006D0 RID: 1744
		IDbCommand SelectCommand { get; }

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006D1 RID: 1745
		// (set) Token: 0x060006D2 RID: 1746
		IDbCommand InsertCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006D3 RID: 1747
		// (set) Token: 0x060006D4 RID: 1748
		IDbCommand UpdateCommand { get; set; }

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006D5 RID: 1749
		// (set) Token: 0x060006D6 RID: 1750
		IDbCommand DeleteCommand { get; set; }
	}
}
