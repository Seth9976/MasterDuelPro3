using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for database-specific classes that represent commands. <see cref="Overload:System.Data.Common.DbCommand.ExecuteNonQueryAsync" /></summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F3 RID: 243
	public abstract class DbCommand : Component, IDbCommand, IDisposable, IAsyncDisposable
	{
		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CE4 RID: 3300
		// (set) Token: 0x06000CE5 RID: 3301
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CE6 RID: 3302
		// (set) Token: 0x06000CE7 RID: 3303
		public abstract int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001ED RID: 493
		// (set) Token: 0x06000CE8 RID: 3304
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract CommandType CommandType { set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00044ED3 File Offset: 0x000430D3
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00044EDB File Offset: 0x000430DB
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CEB RID: 3307
		// (set) Token: 0x06000CEC RID: 3308
		protected abstract DbConnection DbConnection { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000CED RID: 3309
		protected abstract DbParameterCollection DbParameterCollection { get; }

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000CEE RID: 3310
		// (set) Token: 0x06000CEF RID: 3311
		protected abstract DbTransaction DbTransaction { get; set; }

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000CF0 RID: 3312
		// (set) Token: 0x06000CF1 RID: 3313
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool DesignTimeVisible { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00044EE4 File Offset: 0x000430E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00044EEC File Offset: 0x000430EC
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00044EF4 File Offset: 0x000430F4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000CF5 RID: 3317
		// (set) Token: 0x06000CF6 RID: 3318
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00044EFD File Offset: 0x000430FD
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x06000CF8 RID: 3320
		protected abstract DbParameter CreateDbParameter();

		/// <summary>Executes the command text against the connection.</summary>
		/// <returns>A task representing the operation.</returns>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06000CF9 RID: 3321
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		/// <summary>Executes a SQL statement against a connection object.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CFA RID: 3322
		public abstract int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06000CFB RID: 3323 RVA: 0x00044F05 File Offset: 0x00043105
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values. </summary>
		/// <returns>An <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CFC RID: 3324 RVA: 0x00044F0E File Offset: 0x0004310E
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}
	}
}
