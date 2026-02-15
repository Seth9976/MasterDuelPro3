using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200010C RID: 268
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x000492BF File Offset: 0x000474BF
		public SqlBytesStorage(DataColumn column)
			: base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null, StorageType.SqlBytes)
		{
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000492E0 File Offset: 0x000474E0
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
					{
						int num = 0;
						for (int i = 0; i < records.Length; i++)
						{
							if (!this.IsNull(records[i]))
							{
								num++;
							}
						}
						return num;
					}
				}
				else
				{
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00049368 File Offset: 0x00047568
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0004937A File Offset: 0x0004757A
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00049384 File Offset: 0x00047584
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00049393 File Offset: 0x00047593
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlBytes.Null;
				return;
			}
			this._values[record] = (SqlBytes)value;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000493BC File Offset: 0x000475BC
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] array = new SqlBytes[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000493FC File Offset: 0x000475FC
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlBytes((SqlBinary)xmlSerializable);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00049464 File Offset: 0x00047664
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000494B4 File Offset: 0x000476B4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000494BC File Offset: 0x000476BC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBytes[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000494DE File Offset: 0x000476DE
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBytes[])store;
		}

		// Token: 0x04000584 RID: 1412
		private SqlBytes[] _values;
	}
}
