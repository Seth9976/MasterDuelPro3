using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but inherit most of the functionality needed to fully implement a DataAdapter. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F5 RID: 245
	public abstract class DbDataAdapter : DataAdapter
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0000207F File Offset: 0x0000027F
		private IDbDataAdapter _IDbDataAdapter
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets a command for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00044F1F File Offset: 0x0004311F
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00044F31 File Offset: 0x00043131
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand DeleteCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.DeleteCommand;
			}
			set
			{
				this._IDbDataAdapter.DeleteCommand = value;
			}
		}

		/// <summary>Gets or sets a command used to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00044F3F File Offset: 0x0004313F
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00044F51 File Offset: 0x00043151
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand InsertCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.InsertCommand;
			}
			set
			{
				this._IDbDataAdapter.InsertCommand = value;
			}
		}

		/// <summary>Gets or sets a command used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00044F5F File Offset: 0x0004315F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand SelectCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.SelectCommand;
			}
		}

		/// <summary>Gets or sets a command used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00044F71 File Offset: 0x00043171
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00044F83 File Offset: 0x00043183
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand UpdateCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.UpdateCommand;
			}
			set
			{
				this._IDbDataAdapter.UpdateCommand = value;
			}
		}

		// Token: 0x0400053A RID: 1338
		internal static readonly object s_parameterValueNonNullValue = 0;

		// Token: 0x0400053B RID: 1339
		internal static readonly object s_parameterValueNullValue = 1;
	}
}
