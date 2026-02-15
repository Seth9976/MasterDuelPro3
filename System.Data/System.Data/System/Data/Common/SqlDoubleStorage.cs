using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000110 RID: 272
	internal sealed class SqlDoubleStorage : DataStorage
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x0004A0E8 File Offset: 0x000482E8
		public SqlDoubleStorage(DataColumn column)
			: base(column, typeof(SqlDouble), SqlDouble.Null, SqlDouble.Null, StorageType.SqlDouble)
		{
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0004A114 File Offset: 0x00048314
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDouble sqlDouble = 0.0;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDouble += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDouble sqlDouble2 = 0.0;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDouble2 += this._values[num3];
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0.0;
						return sqlDouble2 / (double)num2;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlDouble sqlDouble3 = SqlDouble.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDouble.LessThan(this._values[num4], sqlDouble3).IsTrue)
							{
								sqlDouble3 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble3;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDouble sqlDouble4 = SqlDouble.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDouble.GreaterThan(this._values[num5], sqlDouble4).IsTrue)
							{
								sqlDouble4 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble4;
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
					SqlDouble sqlDouble5 = 0.0;
					0.0;
					SqlDouble sqlDouble6 = 0.0;
					SqlDouble sqlDouble7 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble6 += this._values[num7];
							sqlDouble7 += this._values[num7] * this._values[num7];
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble5 = (double)num6 * sqlDouble7 - sqlDouble6 * sqlDouble6;
					SqlBoolean sqlBoolean = sqlDouble5 / (sqlDouble6 * sqlDouble6) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | (sqlDouble5 < 0.0)))
					{
						sqlDouble5 = 0.0;
					}
					else
					{
						sqlDouble5 /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble5.Value);
					}
					return sqlDouble5;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDouble));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0004A544 File Offset: 0x00048744
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0004A563 File Offset: 0x00048763
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDouble)value);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0004A57C File Offset: 0x0004877C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDouble(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0004A593 File Offset: 0x00048793
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0004A5AD File Offset: 0x000487AD
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0004A5C0 File Offset: 0x000487C0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0004A5D3 File Offset: 0x000487D3
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDouble(value);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0004A5E8 File Offset: 0x000487E8
		public override void SetCapacity(int capacity)
		{
			SqlDouble[] array = new SqlDouble[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0004A628 File Offset: 0x00048828
		public override object ConvertXmlToObject(string s)
		{
			SqlDouble sqlDouble = default(SqlDouble);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDouble;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDouble)xmlSerializable;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0004A690 File Offset: 0x00048890
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0004A6E0 File Offset: 0x000488E0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDouble[recordCount];
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0004A6E8 File Offset: 0x000488E8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDouble[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0004A712 File Offset: 0x00048912
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDouble[])store;
		}

		// Token: 0x04000588 RID: 1416
		private SqlDouble[] _values;
	}
}
