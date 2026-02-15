using System;

namespace System.Data
{
	/// <summary>Provides a means of reading one or more forward-only streams of result sets obtained by executing a command at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007A RID: 122
	public interface IDataReader : IDisposable, IDataRecord
	{
		/// <summary>Closes the <see cref="T:System.Data.IDataReader" /> Object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C3 RID: 1731
		void Close();

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.IDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C4 RID: 1732
		DataTable GetSchemaTable();

		/// <summary>Advances the <see cref="T:System.Data.IDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C5 RID: 1733
		bool Read();
	}
}
