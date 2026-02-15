using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020000F1 RID: 241
	internal sealed class DateTimeOffsetStorage : DataStorage
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x00044548 File Offset: 0x00042748
		internal DateTimeOffsetStorage(DataColumn column)
			: base(column, typeof(DateTimeOffset), DateTimeOffsetStorage.s_defaultValue, StorageType.DateTimeOffset)
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00044568 File Offset: 0x00042768
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTimeOffset = ((DateTimeOffset.Compare(this._values[num], dateTimeOffset) < 0) ? this._values[num] : dateTimeOffset);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					DateTimeOffset dateTimeOffset2 = DateTimeOffset.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTimeOffset2 = ((DateTimeOffset.Compare(this._values[num2], dateTimeOffset2) >= 0) ? this._values[num2] : dateTimeOffset2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset2;
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
						if (base.HasValue(records[k]))
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
				throw ExprException.Overflow(typeof(DateTimeOffset));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000446F8 File Offset: 0x000428F8
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTimeOffset dateTimeOffset = this._values[recordNo1];
			DateTimeOffset dateTimeOffset2 = this._values[recordNo2];
			if (dateTimeOffset == DateTimeOffsetStorage.s_defaultValue || dateTimeOffset2 == DateTimeOffsetStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTimeOffset.Compare(dateTimeOffset, dateTimeOffset2);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00044750 File Offset: 0x00042950
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
				DateTimeOffset dateTimeOffset = this._values[recordNo];
				if (DateTimeOffsetStorage.s_defaultValue == dateTimeOffset && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTimeOffset.Compare(dateTimeOffset, (DateTimeOffset)value);
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000447A4 File Offset: 0x000429A4
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = (DateTimeOffset)value;
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000447CA File Offset: 0x000429CA
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000447EC File Offset: 0x000429EC
		public override object Get(int record)
		{
			DateTimeOffset dateTimeOffset = this._values[record];
			if (dateTimeOffset != DateTimeOffsetStorage.s_defaultValue || base.HasValue(record))
			{
				return dateTimeOffset;
			}
			return this._nullValue;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00044829 File Offset: 0x00042A29
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DateTimeOffsetStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = (DateTimeOffset)value;
			base.SetNullBit(record, false);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00044868 File Offset: 0x00042A68
		public override void SetCapacity(int capacity)
		{
			DateTimeOffset[] array = new DateTimeOffset[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000448AE File Offset: 0x00042AAE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDateTimeOffset(s);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000448BB File Offset: 0x00042ABB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((DateTimeOffset)value);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000448C8 File Offset: 0x00042AC8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTimeOffset[recordCount];
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000448D0 File Offset: 0x00042AD0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((DateTimeOffset[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000448FD File Offset: 0x00042AFD
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (DateTimeOffset[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000536 RID: 1334
		private static readonly DateTimeOffset s_defaultValue = DateTimeOffset.MinValue;

		// Token: 0x04000537 RID: 1335
		private DateTimeOffset[] _values;
	}
}
