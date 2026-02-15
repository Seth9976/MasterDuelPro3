using System;

namespace System.Data
{
	/// <summary>Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007C RID: 124
	public interface IDbCommand : IDisposable
	{
		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CF RID: 1743
		IDataReader ExecuteReader();
	}
}
