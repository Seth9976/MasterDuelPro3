using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000117 RID: 279
	internal sealed class SqlStringStorage : DataStorage
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0004C918 File Offset: 0x0004AB18
		public SqlStringStorage(DataColumn column)
			: base(column, typeof(SqlString), SqlString.Null, SqlString.Null, StorageType.SqlString)
		{
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004C944 File Offset: 0x0004AB44
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					int num = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num = recordNos[i];
							break;
						}
					}
					if (num >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
							{
								num = recordNos[i];
							}
						}
						return this.Get(num);
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					int num2 = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num2 = recordNos[i];
							break;
						}
					}
					if (num2 >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (this.Compare(num2, recordNos[i]) < 0)
							{
								num2 = recordNos[i];
							}
						}
						return this.Get(num2);
					}
					return this._nullValue;
				}
				case AggregateType.Count:
				{
					int num3 = 0;
					for (int i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlString));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.Compare(this._values[recordNo1], this._values[recordNo2]);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0004CABC File Offset: 0x0004ACBC
		public int Compare(SqlString valueNo1, SqlString valueNo2)
		{
			if (valueNo1.IsNull && valueNo2.IsNull)
			{
				return 0;
			}
			if (valueNo1.IsNull)
			{
				return -1;
			}
			if (valueNo2.IsNull)
			{
				return 1;
			}
			return this._table.Compare(valueNo1.Value, valueNo2.Value);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0004CB0C File Offset: 0x0004AD0C
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.Compare(this._values[recordNo], (SqlString)value);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0004CB26 File Offset: 0x0004AD26
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlString(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0004CB3D File Offset: 0x0004AD3D
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0004CB57 File Offset: 0x0004AD57
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		public override int GetStringLength(int record)
		{
			SqlString sqlString = this._values[record];
			if (!sqlString.IsNull)
			{
				return sqlString.Value.Length;
			}
			return 0;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0004CB9D File Offset: 0x0004AD9D
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlString(value);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public override void SetCapacity(int capacity)
		{
			SqlString[] array = new SqlString[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0004CC04 File Offset: 0x0004AE04
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlString)xmlSerializable;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004CC6C File Offset: 0x0004AE6C
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0004CCBC File Offset: 0x0004AEBC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlString[recordCount];
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004CCC4 File Offset: 0x0004AEC4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlString[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0004CCEE File Offset: 0x0004AEEE
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlString[])store;
		}

		// Token: 0x0400058F RID: 1423
		private SqlString[] _values;
	}
}
