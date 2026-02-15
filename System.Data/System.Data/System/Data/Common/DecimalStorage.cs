using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000102 RID: 258
	internal sealed class DecimalStorage : DataStorage
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x000453FA File Offset: 0x000435FA
		internal DecimalStorage(DataColumn column)
			: base(column, typeof(decimal), DecimalStorage.s_defaultValue, StorageType.Decimal)
		{
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0004541C File Offset: 0x0004361C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = DecimalStorage.s_defaultValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							num += this._values[num2];
							flag = true;
						}
					}
					if (flag)
					{
						return num;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					decimal num3 = DecimalStorage.s_defaultValue;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							num3 += this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return num3 / num4;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					decimal num6 = decimal.MaxValue;
					foreach (int num7 in records)
					{
						if (base.HasValue(num7))
						{
							num6 = Math.Min(this._values[num7], num6);
							flag = true;
						}
					}
					if (flag)
					{
						return num6;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					decimal num8 = decimal.MinValue;
					foreach (int num9 in records)
					{
						if (base.HasValue(num9))
						{
							num8 = Math.Max(this._values[num9], num8);
							flag = true;
						}
					}
					if (flag)
					{
						return num8;
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
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num10 = 0;
					double num11 = (double)DecimalStorage.s_defaultValue;
					(double)DecimalStorage.s_defaultValue;
					double num12 = (double)DecimalStorage.s_defaultValue;
					double num13 = (double)DecimalStorage.s_defaultValue;
					foreach (int num14 in records)
					{
						if (base.HasValue(num14))
						{
							num12 += (double)this._values[num14];
							num13 += (double)this._values[num14] * (double)this._values[num14];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this._nullValue;
					}
					num11 = (double)num10 * num13 - num12 * num12;
					if (num11 / (num12 * num12) < 1E-15 || num11 < 0.0)
					{
						num11 = 0.0;
					}
					else
					{
						num11 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num11);
					}
					return num11;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(decimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0004577C File Offset: 0x0004397C
		public override int Compare(int recordNo1, int recordNo2)
		{
			decimal num = this._values[recordNo1];
			decimal num2 = this._values[recordNo2];
			if (num == DecimalStorage.s_defaultValue || num2 == DecimalStorage.s_defaultValue)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return decimal.Compare(num, num2);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000457D4 File Offset: 0x000439D4
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (!base.HasValue(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				decimal num = this._values[recordNo];
				if (DecimalStorage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return decimal.Compare(num, (decimal)value);
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00045828 File Offset: 0x00043A28
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDecimal(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00045859 File Offset: 0x00043A59
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0004587B File Offset: 0x00043A7B
		public override object Get(int record)
		{
			if (!base.HasValue(record))
			{
				return this._nullValue;
			}
			return this._values[record];
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000458A0 File Offset: 0x00043AA0
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DecimalStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToDecimal(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000458F8 File Offset: 0x00043AF8
		public override void SetCapacity(int capacity)
		{
			decimal[] array = new decimal[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0004593E File Offset: 0x00043B3E
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0004594B File Offset: 0x00043B4B
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((decimal)value);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00045958 File Offset: 0x00043B58
		protected override object GetEmptyStorage(int recordCount)
		{
			return new decimal[recordCount];
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00045960 File Offset: 0x00043B60
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((decimal[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0004598D File Offset: 0x00043B8D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (decimal[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000575 RID: 1397
		private static readonly decimal s_defaultValue;

		// Token: 0x04000576 RID: 1398
		private decimal[] _values;
	}
}
