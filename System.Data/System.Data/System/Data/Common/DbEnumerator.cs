using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F8 RID: 248
	public class DbEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader, and indicates whether to automatically close the DataReader after iterating through its data.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		/// <param name="closeReader">true to automatically close the DataReader after iterating through its data; otherwise, false. </param>
		// Token: 0x06000D3F RID: 3391 RVA: 0x00044FD4 File Offset: 0x000431D4
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this._closeReader = closeReader;
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00044FF8 File Offset: 0x000431F8
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D41 RID: 3393 RVA: 0x00045000 File Offset: 0x00043200
		public bool MoveNext()
		{
			if (this._schemaInfo == null)
			{
				this.BuildSchemaInfo();
			}
			this._current = null;
			if (this._reader.Read())
			{
				object[] array = new object[this._schemaInfo.Length];
				this._reader.GetValues(array);
				this._current = new DataRecordInternal(this._schemaInfo, array, this._descriptors, this._fieldNameLookup);
				return true;
			}
			if (this._closeReader)
			{
				this._reader.Close();
			}
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D42 RID: 3394 RVA: 0x000421A7 File Offset: 0x000403A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00045080 File Offset: 0x00043280
		private void BuildSchemaInfo()
		{
			int fieldCount = this._reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = this._reader.GetName(i);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
			SchemaInfo[] array2 = new SchemaInfo[fieldCount];
			PropertyDescriptor[] array3 = new PropertyDescriptor[this._reader.FieldCount];
			for (int j = 0; j < array2.Length; j++)
			{
				SchemaInfo schemaInfo = default(SchemaInfo);
				schemaInfo.name = this._reader.GetName(j);
				schemaInfo.type = this._reader.GetFieldType(j);
				schemaInfo.typeName = this._reader.GetDataTypeName(j);
				array3[j] = new DbEnumerator.DbColumnDescriptor(j, array[j], schemaInfo.type);
				array2[j] = schemaInfo;
			}
			this._schemaInfo = array2;
			this._fieldNameLookup = new FieldNameLookup(this._reader, -1);
			this._descriptors = new PropertyDescriptorCollection(array3);
		}

		// Token: 0x0400053C RID: 1340
		internal IDataReader _reader;

		// Token: 0x0400053D RID: 1341
		internal DbDataRecord _current;

		// Token: 0x0400053E RID: 1342
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x0400053F RID: 1343
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x04000540 RID: 1344
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000541 RID: 1345
		private bool _closeReader;

		// Token: 0x020000F9 RID: 249
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x06000D44 RID: 3396 RVA: 0x0004517A File Offset: 0x0004337A
			internal DbColumnDescriptor(int ordinal, string name, Type type)
				: base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00045192 File Offset: 0x00043392
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0000593A File Offset: 0x00003B3A
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0004519E File Offset: 0x0004339E
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x00011ED5 File Offset: 0x000100D5
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x000451A6 File Offset: 0x000433A6
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x000421A7 File Offset: 0x000403A7
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06000D4B RID: 3403 RVA: 0x000421A7 File Offset: 0x000403A7
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x00011ED5 File Offset: 0x000100D5
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04000542 RID: 1346
			private int _ordinal;

			// Token: 0x04000543 RID: 1347
			private Type _type;
		}
	}
}
