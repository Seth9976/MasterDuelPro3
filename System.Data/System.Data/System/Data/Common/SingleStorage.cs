using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200011B RID: 283
	internal sealed class SingleStorage : DataStorage
	{
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004D1C5 File Offset: 0x0004B3C5
		public SingleStorage(DataColumn column)
			: base(column, typeof(float), 0f, StorageType.Single)
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004D1E4 File Offset: 0x0004B3E4
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					float num = 0f;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
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
					double num3 = 0.0;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += (double)this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return (float)(num3 / (double)num4);
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					float num6 = float.MaxValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
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
					float num8 = float.MinValue;
					foreach (int num9 in records)
					{
						if (!this.IsNull(num9))
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
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (!this.IsNull(num13))
						{
							num11 += (double)this._values[num13];
							num12 += (double)this._values[num13] * (double)this._values[num13];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this._nullValue;
					}
					double num14 = (double)num10 * num12 - num11 * num11;
					if (num14 / (num11 * num11) < 1E-15 || num14 < 0.0)
					{
						num14 = 0.0;
					}
					else
					{
						num14 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num14);
					}
					return num14;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(float));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004D4F0 File Offset: 0x0004B6F0
		public override int Compare(int recordNo1, int recordNo2)
		{
			float num = this._values[recordNo1];
			float num2 = this._values[recordNo2];
			if (num == 0f || num2 == 0f)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0004D538 File Offset: 0x0004B738
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
				float num = this._values[recordNo];
				if (0f == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((float)value);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0004D584 File Offset: 0x0004B784
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSingle(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004D5B5 File Offset: 0x0004B7B5
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0004D5D0 File Offset: 0x0004B7D0
		public override object Get(int record)
		{
			float num = this._values[record];
			if (num != 0f)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0004D5FC File Offset: 0x0004B7FC
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0f;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToSingle(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004D64C File Offset: 0x0004B84C
		public override void SetCapacity(int capacity)
		{
			float[] array = new float[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0004D692 File Offset: 0x0004B892
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0004D69F File Offset: 0x0004B89F
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((float)value);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new float[recordCount];
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((float[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0004D6D6 File Offset: 0x0004B8D6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (float[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040005B0 RID: 1456
		private float[] _values;
	}
}
