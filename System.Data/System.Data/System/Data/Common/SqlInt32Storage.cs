using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000113 RID: 275
	internal sealed class SqlInt32Storage : DataStorage
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x0004AFFC File Offset: 0x000491FC
		public SqlInt32Storage(DataColumn column)
			: base(column, typeof(SqlInt32), SqlInt32.Null, SqlInt32.Null, StorageType.SqlInt32)
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0004B028 File Offset: 0x00049228
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
						return (sqlInt2 / (long)num2).ToSqlInt32();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlInt32 sqlInt3 = SqlInt32.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt32.LessThan(this._values[num4], sqlInt3).IsTrue)
							{
								sqlInt3 = this._values[num4];
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
				case AggregateType.Max:
				{
					SqlInt32 sqlInt4 = SqlInt32.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt32.GreaterThan(this._values[num5], sqlInt4).IsTrue)
							{
								sqlInt4 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt4;
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
				throw ExprException.Overflow(typeof(SqlInt32));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0004B464 File Offset: 0x00049664
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0004B483 File Offset: 0x00049683
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlInt32)value);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0004B49C File Offset: 0x0004969C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt32(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0004B4B3 File Offset: 0x000496B3
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0004B4CD File Offset: 0x000496CD
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0004B4E0 File Offset: 0x000496E0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0004B4F3 File Offset: 0x000496F3
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlInt32(value);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0004B508 File Offset: 0x00049708
		public override void SetCapacity(int capacity)
		{
			SqlInt32[] array = new SqlInt32[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0004B548 File Offset: 0x00049748
		public override object ConvertXmlToObject(string s)
		{
			SqlInt32 sqlInt = default(SqlInt32);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt32)xmlSerializable;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0004B5B0 File Offset: 0x000497B0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0004B600 File Offset: 0x00049800
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt32[recordCount];
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0004B608 File Offset: 0x00049808
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlInt32[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0004B632 File Offset: 0x00049832
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlInt32[])store;
		}

		// Token: 0x0400058B RID: 1419
		private SqlInt32[] _values;
	}
}
