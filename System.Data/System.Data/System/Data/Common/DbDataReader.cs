using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Reads a forward-only stream of rows from a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F6 RID: 246
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable, IAsyncDisposable
	{
		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>The number of columns in the current row.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000D13 RID: 3347
		public abstract int FieldCount { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement. </summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000D14 RID: 3348
		public abstract int RecordsAffected { get; }

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.Common.DbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00044FA9 File Offset: 0x000431A9
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000200 RID: 512
		public abstract object this[int ordinal] { get; }

		/// <summary>Closes the <see cref="T:System.Data.Common.DbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00003FD2 File Offset: 0x000021D2
		public virtual void Close()
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00044FB1 File Offset: 0x000431B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the managed resources used by the <see cref="T:System.Data.Common.DbDataReader" /> and optionally releases the unmanaged resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00044FBA File Offset: 0x000431BA
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Gets name of the data type of the specified column.</summary>
		/// <returns>A string representing the name of the data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1A RID: 3354
		public abstract string GetDataTypeName(int ordinal);

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1B RID: 3355
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets the data type of the specified column.</summary>
		/// <returns>The data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1C RID: 3356
		public abstract Type GetFieldType(int ordinal);

		/// <summary>Gets the name of the column, given the zero-based column ordinal.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1D RID: 3357
		public abstract string GetName(int ordinal);

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00044FC5 File Offset: 0x000431C5
		public virtual DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1F RID: 3359
		public abstract bool GetBoolean(int ordinal);

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D20 RID: 3360
		public abstract int GetInt32(int ordinal);

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D21 RID: 3361
		public abstract long GetInt64(int ordinal);

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D22 RID: 3362
		public abstract string GetString(int ordinal);

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D23 RID: 3363
		public abstract object GetValue(int ordinal);

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D24 RID: 3364
		public abstract int GetValues(object[] values);

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D25 RID: 3365
		public abstract bool IsDBNull(int ordinal);

		/// <summary>Advances the reader to the next result when reading the results of a batch of statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D26 RID: 3366
		public abstract bool NextResult();

		/// <summary>Advances the reader to the next record in a result set.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D27 RID: 3367
		public abstract bool Read();
	}
}
