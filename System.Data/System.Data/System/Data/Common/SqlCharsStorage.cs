using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200010D RID: 269
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x000494EC File Offset: 0x000476EC
		public SqlCharsStorage(DataColumn column)
			: base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null, StorageType.SqlChars)
		{
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0004950C File Offset: 0x0004770C
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
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00049594 File Offset: 0x00047794
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000495A6 File Offset: 0x000477A6
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000495B0 File Offset: 0x000477B0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000495BF File Offset: 0x000477BF
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlChars.Null;
				return;
			}
			this._values[record] = (SqlChars)value;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000495E8 File Offset: 0x000477E8
		public override void SetCapacity(int capacity)
		{
			SqlChars[] array = new SqlChars[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00049628 File Offset: 0x00047828
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlChars((SqlString)xmlSerializable);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00049690 File Offset: 0x00047890
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000496E0 File Offset: 0x000478E0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000496E8 File Offset: 0x000478E8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlChars[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0004970A File Offset: 0x0004790A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlChars[])store;
		}

		// Token: 0x04000585 RID: 1413
		private SqlChars[] _values;
	}
}
