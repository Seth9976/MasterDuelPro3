using System;
using System.Collections;
using System.Globalization;
using System.Numerics;

namespace System.Data.Common
{
	// Token: 0x020000DD RID: 221
	internal sealed class BigIntegerStorage : DataStorage
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0004001D File Offset: 0x0003E21D
		internal BigIntegerStorage(DataColumn column)
			: base(column, typeof(BigInteger), BigInteger.Zero, StorageType.BigInteger)
		{
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003EAEE File Offset: 0x0003CCEE
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004003C File Offset: 0x0003E23C
		public override int Compare(int recordNo1, int recordNo2)
		{
			BigInteger bigInteger = this._values[recordNo1];
			BigInteger bigInteger2 = this._values[recordNo2];
			if (bigInteger.IsZero || bigInteger2.IsZero)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return bigInteger.CompareTo(bigInteger2);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004008C File Offset: 0x0003E28C
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
				BigInteger bigInteger = this._values[recordNo];
				if (bigInteger.IsZero && !base.HasValue(recordNo))
				{
					return -1;
				}
				return bigInteger.CompareTo((BigInteger)value);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000400E0 File Offset: 0x0003E2E0
		internal static BigInteger ConvertToBigInteger(object value, IFormatProvider formatProvider)
		{
			if (value.GetType() == typeof(BigInteger))
			{
				return (BigInteger)value;
			}
			if (value.GetType() == typeof(string))
			{
				return BigInteger.Parse((string)value, formatProvider);
			}
			if (value.GetType() == typeof(long))
			{
				return (long)value;
			}
			if (value.GetType() == typeof(int))
			{
				return (int)value;
			}
			if (value.GetType() == typeof(short))
			{
				return (short)value;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (value.GetType() == typeof(ulong))
			{
				return (ulong)value;
			}
			if (value.GetType() == typeof(uint))
			{
				return (uint)value;
			}
			if (value.GetType() == typeof(ushort))
			{
				return (ushort)value;
			}
			if (value.GetType() == typeof(byte))
			{
				return (byte)value;
			}
			throw ExceptionBuilder.ConvertFailed(value.GetType(), typeof(BigInteger));
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004025C File Offset: 0x0003E45C
		internal static object ConvertFromBigInteger(BigInteger value, Type type, IFormatProvider formatProvider)
		{
			if (type == typeof(string))
			{
				return value.ToString("D", formatProvider);
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (type == typeof(short))
			{
				return (short)value;
			}
			if (type == typeof(int))
			{
				return (int)value;
			}
			if (type == typeof(long))
			{
				return (long)value;
			}
			if (type == typeof(byte))
			{
				return (byte)value;
			}
			if (type == typeof(ushort))
			{
				return (ushort)value;
			}
			if (type == typeof(uint))
			{
				return (uint)value;
			}
			if (type == typeof(ulong))
			{
				return (ulong)value;
			}
			if (type == typeof(float))
			{
				return (float)value;
			}
			if (type == typeof(double))
			{
				return (double)value;
			}
			if (type == typeof(decimal))
			{
				return (decimal)value;
			}
			if (type == typeof(BigInteger))
			{
				return value;
			}
			throw ExceptionBuilder.ConvertFailed(typeof(BigInteger), type);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000403FE File Offset: 0x0003E5FE
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0004042A File Offset: 0x0003E62A
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004044C File Offset: 0x0003E64C
		public override object Get(int record)
		{
			BigInteger bigInteger = this._values[record];
			if (!bigInteger.IsZero)
			{
				return bigInteger;
			}
			return base.GetBits(record);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00040480 File Offset: 0x0003E680
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = BigInteger.Zero;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000404D0 File Offset: 0x0003E6D0
		public override void SetCapacity(int capacity)
		{
			BigInteger[] array = new BigInteger[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00040516 File Offset: 0x0003E716
		public override object ConvertXmlToObject(string s)
		{
			return BigInteger.Parse(s, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00040528 File Offset: 0x0003E728
		public override string ConvertObjectToXml(object value)
		{
			return ((BigInteger)value).ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004054D File Offset: 0x0003E74D
		protected override object GetEmptyStorage(int recordCount)
		{
			return new BigInteger[recordCount];
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00040555 File Offset: 0x0003E755
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((BigInteger[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00040582 File Offset: 0x0003E782
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (BigInteger[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040004AB RID: 1195
		private BigInteger[] _values;
	}
}
