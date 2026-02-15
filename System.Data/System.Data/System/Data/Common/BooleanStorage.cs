using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020000DE RID: 222
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00040597 File Offset: 0x0003E797
		internal BooleanStorage(DataColumn column)
			: base(column, typeof(bool), false, StorageType.Boolean)
		{
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000405B4 File Offset: 0x0003E7B4
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = this._values[num] && flag2;
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = this._values[num2] || flag3;
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
					return base.Aggregate(records, kind);
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000406D0 File Offset: 0x0003E8D0
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this._values[recordNo1];
			bool flag2 = this._values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004070C File Offset: 0x0003E90C
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (this.IsNull(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				bool flag = this._values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00040753 File Offset: 0x0003E953
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00040784 File Offset: 0x0003E984
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000407A0 File Offset: 0x0003E9A0
		public override object Get(int record)
		{
			bool flag = this._values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000407C7 File Offset: 0x0003E9C7
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00040808 File Offset: 0x0003EA08
		public override void SetCapacity(int capacity)
		{
			bool[] array = new bool[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004084E File Offset: 0x0003EA4E
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004085B File Offset: 0x0003EA5B
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00040868 File Offset: 0x0003EA68
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00040870 File Offset: 0x0003EA70
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((bool[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00040892 File Offset: 0x0003EA92
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040004AC RID: 1196
		private bool[] _values;
	}
}
