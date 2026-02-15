using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000115 RID: 277
	internal sealed class SqlMoneyStorage : DataStorage
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x0004BC80 File Offset: 0x00049E80
		public SqlMoneyStorage(DataColumn column)
			: base(column, typeof(SqlMoney), SqlMoney.Null, SqlMoney.Null, StorageType.SqlMoney)
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004BCAC File Offset: 0x00049EAC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDecimal sqlDecimal = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDecimal += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal sqlDecimal2 = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDecimal2 += this._values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return (sqlDecimal2 / (long)num2).ToSqlMoney();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlMoney sqlMoney = SqlMoney.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlMoney.LessThan(this._values[num4], sqlMoney).IsTrue)
							{
								sqlMoney = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlMoney sqlMoney2 = SqlMoney.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlMoney.GreaterThan(this._values[num5], sqlMoney2).IsTrue)
							{
								sqlMoney2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney2;
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
				throw ExprException.Overflow(typeof(SqlMoney));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0004C0EC File Offset: 0x0004A2EC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0004C10B File Offset: 0x0004A30B
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlMoney)value);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0004C124 File Offset: 0x0004A324
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlMoney(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0004C13B File Offset: 0x0004A33B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0004C155 File Offset: 0x0004A355
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0004C168 File Offset: 0x0004A368
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0004C17B File Offset: 0x0004A37B
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlMoney(value);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0004C190 File Offset: 0x0004A390
		public override void SetCapacity(int capacity)
		{
			SqlMoney[] array = new SqlMoney[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0004C1D0 File Offset: 0x0004A3D0
		public override object ConvertXmlToObject(string s)
		{
			SqlMoney sqlMoney = default(SqlMoney);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlMoney;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlMoney)xmlSerializable;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0004C238 File Offset: 0x0004A438
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004C288 File Offset: 0x0004A488
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlMoney[recordCount];
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0004C290 File Offset: 0x0004A490
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlMoney[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0004C2BA File Offset: 0x0004A4BA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlMoney[])store;
		}

		// Token: 0x0400058D RID: 1421
		private SqlMoney[] _values;
	}
}
