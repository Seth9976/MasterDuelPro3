using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000121 RID: 289
	internal sealed class UInt32Storage : DataStorage
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x0004E9B6 File Offset: 0x0004CBB6
		public UInt32Storage(DataColumn column)
			: base(column, typeof(uint), UInt32Storage.s_defaultValue, StorageType.UInt32)
		{
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0004E9D8 File Offset: 0x0004CBD8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = (ulong)UInt32Storage.s_defaultValue;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += unchecked((ulong)this._values[num2]);
								flag = true;
							}
						}
						if (flag)
						{
							return num;
						}
						return this._nullValue;
					}
				}
				case AggregateType.Mean:
				{
					long num3 = (long)((ulong)UInt32Storage.s_defaultValue);
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += (long)(unchecked((ulong)this._values[num5]));
							}
							num4++;
							flag = true;
						}
					}
					checked
					{
						if (flag)
						{
							return (uint)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
				}
				case AggregateType.Min:
				{
					uint num6 = uint.MaxValue;
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
					uint num8 = 0U;
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
				{
					int num10 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (base.HasValue(records[l]))
						{
							num10++;
						}
					}
					return num10;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num10 = 0;
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (base.HasValue(num13))
						{
							num11 += this._values[num13];
							num12 += this._values[num13] * this._values[num13];
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
				throw ExprException.Overflow(typeof(uint));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0004ED04 File Offset: 0x0004CF04
		public override int Compare(int recordNo1, int recordNo2)
		{
			uint num = this._values[recordNo1];
			uint num2 = this._values[recordNo2];
			if (num == UInt32Storage.s_defaultValue || num2 == UInt32Storage.s_defaultValue)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			if (num < num2)
			{
				return -1;
			}
			if (num <= num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004ED50 File Offset: 0x0004CF50
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
				uint num = this._values[recordNo];
				if (UInt32Storage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((uint)value);
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004ED9C File Offset: 0x0004CF9C
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt32(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0004EDCD File Offset: 0x0004CFCD
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004EDE8 File Offset: 0x0004CFE8
		public override object Get(int record)
		{
			uint num = this._values[record];
			if (!num.Equals(UInt32Storage.s_defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004EE1C File Offset: 0x0004D01C
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = UInt32Storage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToUInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0004EE6C File Offset: 0x0004D06C
		public override void SetCapacity(int capacity)
		{
			uint[] array = new uint[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0004EEB2 File Offset: 0x0004D0B2
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0004EEBF File Offset: 0x0004D0BF
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((uint)value);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004EECC File Offset: 0x0004D0CC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new uint[recordCount];
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0004EED4 File Offset: 0x0004D0D4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((uint[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0004EEF9 File Offset: 0x0004D0F9
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (uint[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040005BB RID: 1467
		private static readonly uint s_defaultValue;

		// Token: 0x040005BC RID: 1468
		private uint[] _values;
	}
}
