using System;
using System.ComponentModel;
using System.Transactions;

namespace System.Data.Common
{
	/// <summary>Represents a connection to a database. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F4 RID: 244
	public abstract class DbConnection : Component, IDisposable, IAsyncDisposable
	{
		/// <summary>Gets or sets the string used to open the connection.</summary>
		/// <returns>The connection string used to establish the initial connection. The exact contents of the connection string depend on the specific data source for this connection. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000CFE RID: 3326
		// (set) Token: 0x06000CFF RID: 3327
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[RecommendedAsConfigurable(true)]
		public abstract string ConnectionString { get; set; }

		/// <summary>Gets a string that describes the state of the connection.</summary>
		/// <returns>The state of the connection. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000D00 RID: 3328
		[Browsable(false)]
		public abstract ConnectionState State { get; }

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		// Token: 0x06000D01 RID: 3329
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.Common.DbException">The connection-level error that occurred while opening the connection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D02 RID: 3330
		public abstract void Close();

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00044F17 File Offset: 0x00043117
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		// Token: 0x06000D04 RID: 3332
		protected abstract DbCommand CreateDbCommand();

		/// <summary>Enlists in the specified transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x000421A7 File Offset: 0x000403A7
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D06 RID: 3334 RVA: 0x000421A7 File Offset: 0x000403A7
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D07 RID: 3335 RVA: 0x000421A7 File Offset: 0x000403A7
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D08 RID: 3336
		public abstract void Open();
	}
}
