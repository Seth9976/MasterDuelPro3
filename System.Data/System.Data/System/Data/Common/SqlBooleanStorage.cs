using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000118 RID: 280
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0004CCFC File Offset: 0x0004AEFC
		public SqlBooleanStorage(DataColumn column)
			: base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null, StorageType.SqlBoolean)
		{
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0004CD28 File Offset: 0x0004AF28
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlBoolean sqlBoolean = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlBoolean = SqlBoolean.And(this._values[num], sqlBoolean);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlBoolean sqlBoolean2 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							sqlBoolean2 = SqlBoolean.Or(this._values[num2], sqlBoolean2);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean2;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return this._nullValue;
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
				throw ExprException.Overflow(typeof(SqlBoolean));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004CE98 File Offset: 0x0004B098
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004CEB7 File Offset: 0x0004B0B7
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0004CED0 File Offset: 0x0004B0D0
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004CEE7 File Offset: 0x0004B0E7
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0004CF01 File Offset: 0x0004B101
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0004CF14 File Offset: 0x0004B114
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0004CF27 File Offset: 0x0004B127
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0004CF3C File Offset: 0x0004B13C
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] array = new SqlBoolean[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0004CF7C File Offset: 0x0004B17C
		public override object ConvertXmlToObject(string s)
		{
			SqlBoolean sqlBoolean = default(SqlBoolean);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBoolean;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBoolean)xmlSerializable;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0004D034 File Offset: 0x0004B234
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0004D03C File Offset: 0x0004B23C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBoolean[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0004D066 File Offset: 0x0004B266
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBoolean[])store;
		}

		// Token: 0x04000590 RID: 1424
		private SqlBoolean[] _values;
	}
}
