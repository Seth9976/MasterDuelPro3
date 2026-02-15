using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200010B RID: 267
	internal sealed class SqlByteStorage : DataStorage
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x00048C7C File Offset: 0x00046E7C
		public SqlByteStorage(DataColumn column)
			: base(column, typeof(SqlByte), SqlByte.Null, SqlByte.Null, StorageType.SqlByte)
		{
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00048CA8 File Offset: 0x00046EA8
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
					SqlInt64 sqlInt2 = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlInt2 += this._values[num3].ToSqlInt64();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0;
						return (sqlInt2 / (long)num2).ToSqlByte();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlByte sqlByte = SqlByte.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlByte.LessThan(this._values[num4], sqlByte).IsTrue)
							{
								sqlByte = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlByte sqlByte2 = SqlByte.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlByte.GreaterThan(this._values[num5], sqlByte2).IsTrue)
							{
								sqlByte2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte2;
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
				throw ExprException.Overflow(typeof(SqlByte));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000490E4 File Offset: 0x000472E4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00049103 File Offset: 0x00047303
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlByte)value);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0004911C File Offset: 0x0004731C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlByte(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00049133 File Offset: 0x00047333
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0004914D File Offset: 0x0004734D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00049160 File Offset: 0x00047360
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00049173 File Offset: 0x00047373
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlByte(value);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00049188 File Offset: 0x00047388
		public override void SetCapacity(int capacity)
		{
			SqlByte[] array = new SqlByte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000491C8 File Offset: 0x000473C8
		public override object ConvertXmlToObject(string s)
		{
			SqlByte sqlByte = default(SqlByte);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlByte;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlByte)xmlSerializable;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00049230 File Offset: 0x00047430
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00049280 File Offset: 0x00047480
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlByte[recordCount];
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00049288 File Offset: 0x00047488
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlByte[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000492B1 File Offset: 0x000474B1
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlByte[])store;
		}

		// Token: 0x04000583 RID: 1411
		private SqlByte[] _values;
	}
}
