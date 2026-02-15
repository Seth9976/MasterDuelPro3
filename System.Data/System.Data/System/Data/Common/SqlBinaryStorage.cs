using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200010A RID: 266
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x000489E7 File Offset: 0x00046BE7
		public SqlBinaryStorage(DataColumn column)
			: base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null, StorageType.SqlBinary)
		{
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00048A10 File Offset: 0x00046C10
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
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00048AA0 File Offset: 0x00046CA0
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00048ABF File Offset: 0x00046CBF
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00048AD8 File Offset: 0x00046CD8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00048AEF File Offset: 0x00046CEF
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00048B09 File Offset: 0x00046D09
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00048B1C File Offset: 0x00046D1C
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00048B2F File Offset: 0x00046D2F
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00048B44 File Offset: 0x00046D44
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] array = new SqlBinary[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00048B84 File Offset: 0x00046D84
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBinary)xmlSerializable;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00048BEC File Offset: 0x00046DEC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00048C3C File Offset: 0x00046E3C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00048C44 File Offset: 0x00046E44
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBinary[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00048C6E File Offset: 0x00046E6E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBinary[])store;
		}

		// Token: 0x04000582 RID: 1410
		private SqlBinary[] _values;
	}
}
