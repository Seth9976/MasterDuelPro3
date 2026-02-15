using System;

namespace System.Data
{
	/// <summary>Provides access to the column values within each row for a DataReader, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007B RID: 123
	public interface IDataRecord
	{
		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006C6 RID: 1734
		int FieldCount { get; }

		/// <summary>Gets the column located at the specified index.</summary>
		/// <returns>The column located at the specified index as an <see cref="T:System.Object" />.</returns>
		/// <param name="i">The zero-based index of the column to get. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000118 RID: 280
		object this[int i] { get; }

		/// <summary>Gets the name for the field to find.</summary>
		/// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C8 RID: 1736
		string GetName(int i);

		/// <summary>Gets the data type information for the specified field.</summary>
		/// <returns>The data type information for the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C9 RID: 1737
		string GetDataTypeName(int i);

		/// <summary>Gets the <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CA RID: 1738
		Type GetFieldType(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CB RID: 1739
		int GetValues(object[] values);

		/// <summary>Gets the 32-bit signed integer value of the specified field.</summary>
		/// <returns>The 32-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CC RID: 1740
		int GetInt32(int i);

		/// <summary>Gets the 64-bit signed integer value of the specified field.</summary>
		/// <returns>The 64-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CD RID: 1741
		long GetInt64(int i);

		/// <summary>Gets the string value of the specified field.</summary>
		/// <returns>The string value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CE RID: 1742
		string GetString(int i);
	}
}
