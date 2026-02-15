using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000114 RID: 276
	internal sealed class SqlInt64Storage : DataStorage
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x0004B640 File Offset: 0x00049840
		public SqlInt64Storage(DataColumn column)
			: base(column, typeof(SqlInt64), SqlInt64.Null, SqlInt64.Null, StorageType.SqlInt64)
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004B66C File Offset: 0x0004986C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlInt64 sqlInt = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlInt += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal sqlDecimal = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDecimal += this._values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return (sqlDecimal / (long)num2).ToSqlInt64();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlInt64 sqlInt2 = SqlInt64.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt64.LessThan(this._values[num4], sqlInt2).IsTrue)
							{
								sqlInt2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlInt64 sqlInt3 = SqlInt64.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt64.GreaterThan(this._values[num5], sqlInt3).IsTrue)
							{
								sqlInt3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt3;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num6 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (!this.IsNull(records[l]))
						{
							num6++;
						}
					}
					return num6;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num6 = 0;
					SqlDouble sqlDouble = 0.0;
					0.0;
					SqlDouble sqlDouble2 = 0.0;
					SqlDouble sqlDouble3 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble2 += this._values[num7].ToSqlDouble();
							sqlDouble3 += this._values[num7].ToSqlDouble() * this._values[num7].ToSqlDouble();
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble = (double)num6 * sqlDouble3 - sqlDouble2 * sqlDouble2;
					SqlBoolean sqlBoolean = sqlDouble / (sqlDouble2 * sqlDouble2) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | (sqlDouble < 0.0)))
					{
						sqlDouble = 0.0;
					}
					else
					{
						sqlDouble /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble.Value);
					}
					return sqlDouble;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlInt64));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0004BAA4 File Offset: 0x00049CA4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0004BAC3 File Offset: 0x00049CC3
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlInt64)value);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0004BADC File Offset: 0x00049CDC
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt64(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0004BAF3 File Offset: 0x00049CF3
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0004BB0D File Offset: 0x00049D0D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0004BB20 File Offset: 0x00049D20
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0004BB33 File Offset: 0x00049D33
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlInt64(value);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0004BB48 File Offset: 0x00049D48
		public override void SetCapacity(int capacity)
		{
			SqlInt64[] array = new SqlInt64[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0004BB88 File Offset: 0x00049D88
		public override object ConvertXmlToObject(string s)
		{
			SqlInt64 sqlInt = default(SqlInt64);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt64)xmlSerializable;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0004BC40 File Offset: 0x00049E40
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt64[recordCount];
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0004BC48 File Offset: 0x00049E48
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlInt64[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0004BC72 File Offset: 0x00049E72
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlInt64[])store;
		}

		// Token: 0x0400058C RID: 1420
		private SqlInt64[] _values;
	}
}
