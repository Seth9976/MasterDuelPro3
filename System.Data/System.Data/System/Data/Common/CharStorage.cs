using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020000E0 RID: 224
	internal sealed class CharStorage : DataStorage
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x00040D9B File Offset: 0x0003EF9B
		internal CharStorage(DataColumn column)
			: base(column, typeof(char), '\0', StorageType.Char)
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00040DB8 File Offset: 0x0003EFB8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					char c = char.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							c = ((this._values[num] < c) ? this._values[num] : c);
							flag = true;
						}
					}
					if (flag)
					{
						return c;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					char c2 = '\0';
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							c2 = ((this._values[num2] > c2) ? this._values[num2] : c2);
							flag = true;
						}
					}
					if (flag)
					{
						return c2;
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
				throw ExprException.Overflow(typeof(char));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00040EF0 File Offset: 0x0003F0F0
		public override int Compare(int recordNo1, int recordNo2)
		{
			char c = this._values[recordNo1];
			char c2 = this._values[recordNo2];
			if (c == '\0' || c2 == '\0')
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return c.CompareTo(c2);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00040F2C File Offset: 0x0003F12C
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
				char c = this._values[recordNo];
				if (c == '\0' && this.IsNull(recordNo))
				{
					return -1;
				}
				return c.CompareTo((char)value);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00040F73 File Offset: 0x0003F173
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToChar(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00040FA4 File Offset: 0x0003F1A4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00040FC0 File Offset: 0x0003F1C0
		public override object Get(int record)
		{
			char c = this._values[record];
			if (c != '\0')
			{
				return c;
			}
			return base.GetBits(record);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00040FE8 File Offset: 0x0003F1E8
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = '\0';
				base.SetNullBit(record, true);
				return;
			}
			char c = ((IConvertible)value).ToChar(base.FormatProvider);
			if ((c >= '\ud800' && c <= '\udfff') || (c < '!' && (c == '\t' || c == '\n' || c == '\r')))
			{
				throw ExceptionBuilder.ProblematicChars(c);
			}
			this._values[record] = c;
			base.SetNullBit(record, false);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00041060 File Offset: 0x0003F260
		public override void SetCapacity(int capacity)
		{
			char[] array = new char[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000410A6 File Offset: 0x0003F2A6
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000410B3 File Offset: 0x0003F2B3
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((char)value);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000410C0 File Offset: 0x0003F2C0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new char[recordCount];
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000410C8 File Offset: 0x0003F2C8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((char[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000410EA File Offset: 0x0003F2EA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (char[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040004AE RID: 1198
		private char[] _values;
	}
}
