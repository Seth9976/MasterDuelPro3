using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200010E RID: 270
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x00049718 File Offset: 0x00047918
		public SqlDateTimeStorage(DataColumn column)
			: base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null, StorageType.SqlDateTime)
		{
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00049744 File Offset: 0x00047944
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlDateTime sqlDateTime = SqlDateTime.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							if (SqlDateTime.LessThan(this._values[num], sqlDateTime).IsTrue)
							{
								sqlDateTime = this._values[num];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDateTime sqlDateTime2 = SqlDateTime.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							if (SqlDateTime.GreaterThan(this._values[num2], sqlDateTime2).IsTrue)
							{
								sqlDateTime2 = this._values[num2];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime2;
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
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (!this.IsNull(records[k]))
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
				throw ExprException.Overflow(typeof(SqlDateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000498DC File Offset: 0x00047ADC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000498FB File Offset: 0x00047AFB
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00049914 File Offset: 0x00047B14
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0004992B File Offset: 0x00047B2B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00049945 File Offset: 0x00047B45
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00049958 File Offset: 0x00047B58
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0004996B File Offset: 0x00047B6B
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00049980 File Offset: 0x00047B80
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] array = new SqlDateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000499C0 File Offset: 0x00047BC0
		public override object ConvertXmlToObject(string s)
		{
			SqlDateTime sqlDateTime = default(SqlDateTime);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDateTime;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDateTime)xmlSerializable;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00049A28 File Offset: 0x00047C28
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00049A78 File Offset: 0x00047C78
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00049A80 File Offset: 0x00047C80
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDateTime[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00049AA9 File Offset: 0x00047CA9
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDateTime[])store;
		}

		// Token: 0x04000586 RID: 1414
		private SqlDateTime[] _values;
	}
}
