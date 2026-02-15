using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x0200011E RID: 286
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0004DB36 File Offset: 0x0004BD36
		public StringStorage(DataColumn column)
			: base(column, typeof(string), string.Empty, StorageType.String)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0004DB50 File Offset: 0x0004BD50
		public override object Aggregate(int[] recordNos, AggregateType kind)
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
					if (this._values[recordNos[i]] != null)
					{
						num3++;
					}
				}
				return num3;
			}
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0004DC64 File Offset: 0x0004BE64
		public override int Compare(int recordNo1, int recordNo2)
		{
			string text = this._values[recordNo1];
			string text2 = this._values[recordNo2];
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return this._table.Compare(text, text2);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		public override int CompareValueTo(int recordNo, object value)
		{
			string text = this._values[recordNo];
			if (text == null)
			{
				if (this._nullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this._nullValue == value)
				{
					return 1;
				}
				return this._table.Compare(text, (string)value);
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0004DCE3 File Offset: 0x0004BEE3
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = value.ToString();
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0004DD04 File Offset: 0x0004BF04
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0004DD18 File Offset: 0x0004BF18
		public override object Get(int recordNo)
		{
			string text = this._values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this._nullValue;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0004DD3C File Offset: 0x0004BF3C
		public override int GetStringLength(int record)
		{
			string text = this._values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0004DD5D File Offset: 0x0004BF5D
		public override bool IsNull(int record)
		{
			return this._values[record] == null;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0004DD6A File Offset: 0x0004BF6A
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = null;
				return;
			}
			this._values[record] = value.ToString();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0004DD90 File Offset: 0x0004BF90
		public override void SetCapacity(int capacity)
		{
			string[] array = new string[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00043A70 File Offset: 0x00041C70
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0004DDCF File Offset: 0x0004BFCF
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0004DDD7 File Offset: 0x0004BFD7
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0004DDDF File Offset: 0x0004BFDF
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((string[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0004DE01 File Offset: 0x0004C001
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (string[])store;
		}

		// Token: 0x040005B6 RID: 1462
		private string[] _values;
	}
}
