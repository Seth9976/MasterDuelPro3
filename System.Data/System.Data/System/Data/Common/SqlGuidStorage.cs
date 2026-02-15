using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000111 RID: 273
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x06000E56 RID: 3670 RVA: 0x0004A720 File Offset: 0x00048920
		public SqlGuidStorage(DataColumn column)
			: base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null, StorageType.SqlGuid)
		{
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0004A74C File Offset: 0x0004894C
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
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0004A7DC File Offset: 0x000489DC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0004A7FB File Offset: 0x000489FB
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0004A814 File Offset: 0x00048A14
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this._nullValue;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0004A82B File Offset: 0x00048A2B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0004A845 File Offset: 0x00048A45
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0004A858 File Offset: 0x00048A58
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0004A86B File Offset: 0x00048A6B
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0004A880 File Offset: 0x00048A80
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] array = new SqlGuid[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0004A8C0 File Offset: 0x00048AC0
		public override object ConvertXmlToObject(string s)
		{
			SqlGuid sqlGuid = default(SqlGuid);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlGuid;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlGuid)xmlSerializable;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0004A928 File Offset: 0x00048B28
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0004A978 File Offset: 0x00048B78
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0004A980 File Offset: 0x00048B80
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlGuid[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004A9AA File Offset: 0x00048BAA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlGuid[])store;
		}

		// Token: 0x04000589 RID: 1417
		private SqlGuid[] _values;
	}
}
