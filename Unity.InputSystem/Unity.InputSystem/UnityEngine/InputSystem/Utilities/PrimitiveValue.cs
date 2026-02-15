using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200025C RID: 604
	[StructLayout(LayoutKind.Explicit)]
	public struct PrimitiveValue : IEquatable<PrimitiveValue>, IConvertible
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00062D6E File Offset: 0x00060F6E
		internal unsafe byte* valuePtr
		{
			get
			{
				return (byte*)UnsafeUtility.AddressOf<PrimitiveValue>(ref this) + 4;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00062D78 File Offset: 0x00060F78
		public TypeCode type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00062D80 File Offset: 0x00060F80
		public bool isEmpty
		{
			get
			{
				return this.type == TypeCode.Empty;
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00062D8B File Offset: 0x00060F8B
		public PrimitiveValue(bool value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Boolean;
			this.m_BoolValue = value;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00062DA2 File Offset: 0x00060FA2
		public PrimitiveValue(char value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Char;
			this.m_CharValue = value;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00062DB9 File Offset: 0x00060FB9
		public PrimitiveValue(byte value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Byte;
			this.m_ByteValue = value;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00062DD0 File Offset: 0x00060FD0
		public PrimitiveValue(sbyte value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.SByte;
			this.m_SByteValue = value;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00062DE7 File Offset: 0x00060FE7
		public PrimitiveValue(short value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int16;
			this.m_ShortValue = value;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00062DFE File Offset: 0x00060FFE
		public PrimitiveValue(ushort value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt16;
			this.m_UShortValue = value;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00062E15 File Offset: 0x00061015
		public PrimitiveValue(int value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int32;
			this.m_IntValue = value;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00062E2D File Offset: 0x0006102D
		public PrimitiveValue(uint value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt32;
			this.m_UIntValue = value;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00062E45 File Offset: 0x00061045
		public PrimitiveValue(long value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int64;
			this.m_LongValue = value;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00062E5D File Offset: 0x0006105D
		public PrimitiveValue(ulong value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt64;
			this.m_ULongValue = value;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00062E75 File Offset: 0x00061075
		public PrimitiveValue(float value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Single;
			this.m_FloatValue = value;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00062E8D File Offset: 0x0006108D
		public PrimitiveValue(double value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Double;
			this.m_DoubleValue = value;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00062EA8 File Offset: 0x000610A8
		public PrimitiveValue ConvertTo(TypeCode type)
		{
			switch (type)
			{
			case TypeCode.Empty:
				return default(PrimitiveValue);
			case TypeCode.Boolean:
				return this.ToBoolean(null);
			case TypeCode.Char:
				return this.ToChar(null);
			case TypeCode.SByte:
				return this.ToSByte(null);
			case TypeCode.Byte:
				return this.ToByte(null);
			case TypeCode.Int16:
				return this.ToInt16(null);
			case TypeCode.UInt16:
				return this.ToInt16(null);
			case TypeCode.Int32:
				return this.ToInt32(null);
			case TypeCode.UInt32:
				return this.ToInt32(null);
			case TypeCode.Int64:
				return this.ToInt64(null);
			case TypeCode.UInt64:
				return this.ToUInt64(null);
			case TypeCode.Single:
				return this.ToSingle(null);
			case TypeCode.Double:
				return this.ToDouble(null);
			}
			throw new ArgumentException(string.Format("Don't know how to convert PrimitiveValue to '{0}'", type), "type");
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00062FBC File Offset: 0x000611BC
		public unsafe bool Equals(PrimitiveValue other)
		{
			if (this.m_Type != other.m_Type)
			{
				return false;
			}
			void* ptr = UnsafeUtility.AddressOf<double>(ref this.m_DoubleValue);
			void* otherValuePtr = UnsafeUtility.AddressOf<double>(ref other.m_DoubleValue);
			return UnsafeUtility.MemCmp(ptr, otherValuePtr, 8L) == 0;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00062FFC File Offset: 0x000611FC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is PrimitiveValue)
			{
				PrimitiveValue value = (PrimitiveValue)obj;
				return this.Equals(value);
			}
			return (obj is bool || obj is char || obj is byte || obj is sbyte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double) && this.Equals(PrimitiveValue.FromObject(obj));
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00063093 File Offset: 0x00061293
		public static bool operator ==(PrimitiveValue left, PrimitiveValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0006309D File Offset: 0x0006129D
		public static bool operator !=(PrimitiveValue left, PrimitiveValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000630AC File Offset: 0x000612AC
		public unsafe override int GetHashCode()
		{
			fixed (double* ptr = &this.m_DoubleValue)
			{
				double* valuePtr = ptr;
				return (this.m_Type.GetHashCode() * 397) ^ valuePtr->GetHashCode();
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000630E4 File Offset: 0x000612E4
		public override string ToString()
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (!this.m_BoolValue)
				{
					return "false";
				}
				return "true";
			case TypeCode.Char:
				return "'" + this.m_CharValue.ToString() + "'";
			case TypeCode.SByte:
				return this.m_SByteValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Byte:
				return this.m_ByteValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int16:
				return this.m_ShortValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt16:
				return this.m_UShortValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int32:
				return this.m_IntValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt32:
				return this.m_UIntValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int64:
				return this.m_LongValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt64:
				return this.m_ULongValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Single:
				return this.m_FloatValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Double:
				return this.m_DoubleValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			default:
				return string.Empty;
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00063248 File Offset: 0x00061448
		public static PrimitiveValue FromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(PrimitiveValue);
			}
			if (value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
			{
				return new PrimitiveValue(true);
			}
			if (value.Equals("false", StringComparison.InvariantCultureIgnoreCase))
			{
				return new PrimitiveValue(false);
			}
			double doubleResult;
			if ((value.Contains('.') || value.Contains("e") || value.Contains("E") || value.Contains("infinity", StringComparison.InvariantCultureIgnoreCase)) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out doubleResult))
			{
				return new PrimitiveValue(doubleResult);
			}
			long longResult;
			if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out longResult))
			{
				return new PrimitiveValue(longResult);
			}
			if (value.IndexOf("0x", StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				string hexDigits = value.TrimStart();
				if (hexDigits.StartsWith("0x"))
				{
					hexDigits = hexDigits.Substring(2);
				}
				long hexResult;
				if (long.TryParse(hexDigits, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hexResult))
				{
					return new PrimitiveValue(hexResult);
				}
			}
			throw new NotImplementedException();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00063341 File Offset: 0x00061541
		public TypeCode GetTypeCode()
		{
			return this.type;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0006334C File Offset: 0x0006154C
		public bool ToBoolean(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				return this.m_BoolValue;
			case TypeCode.Char:
				return this.m_CharValue > '\0';
			case TypeCode.SByte:
				return this.m_SByteValue != 0;
			case TypeCode.Byte:
				return this.m_ByteValue > 0;
			case TypeCode.Int16:
				return this.m_ShortValue != 0;
			case TypeCode.UInt16:
				return this.m_UShortValue > 0;
			case TypeCode.Int32:
				return this.m_IntValue != 0;
			case TypeCode.UInt32:
				return this.m_UIntValue > 0U;
			case TypeCode.Int64:
				return this.m_LongValue != 0L;
			case TypeCode.UInt64:
				return this.m_ULongValue > 0UL;
			case TypeCode.Single:
				return !Mathf.Approximately(this.m_FloatValue, 0f);
			case TypeCode.Double:
				return !NumberHelpers.Approximately(this.m_DoubleValue, 0.0);
			default:
				return false;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0006342D File Offset: 0x0006162D
		public byte ToByte(IFormatProvider provider = null)
		{
			return (byte)this.ToInt64(provider);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00063438 File Offset: 0x00061638
		public char ToChar(IFormatProvider provider = null)
		{
			TypeCode type = this.type;
			if (type == TypeCode.Char)
			{
				return this.m_CharValue;
			}
			if (type - TypeCode.Int16 > 5)
			{
				return '\0';
			}
			return (char)this.ToInt64(provider);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00063469 File Offset: 0x00061669
		public DateTime ToDateTime(IFormatProvider provider = null)
		{
			throw new NotSupportedException("Converting PrimitiveValue to DateTime");
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00063475 File Offset: 0x00061675
		public decimal ToDecimal(IFormatProvider provider = null)
		{
			return new decimal(this.ToDouble(provider));
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00063484 File Offset: 0x00061684
		public double ToDouble(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1.0;
				}
				return 0.0;
			case TypeCode.Char:
				return (double)this.m_CharValue;
			case TypeCode.SByte:
				return (double)this.m_SByteValue;
			case TypeCode.Byte:
				return (double)this.m_ByteValue;
			case TypeCode.Int16:
				return (double)this.m_ShortValue;
			case TypeCode.UInt16:
				return (double)this.m_UShortValue;
			case TypeCode.Int32:
				return (double)this.m_IntValue;
			case TypeCode.UInt32:
				return this.m_UIntValue;
			case TypeCode.Int64:
				return (double)this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return (double)this.m_FloatValue;
			case TypeCode.Double:
				return this.m_DoubleValue;
			default:
				return 0.0;
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00063550 File Offset: 0x00061750
		public short ToInt16(IFormatProvider provider = null)
		{
			return (short)this.ToInt64(provider);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0006355A File Offset: 0x0006175A
		public int ToInt32(IFormatProvider provider = null)
		{
			return (int)this.ToInt64(provider);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00063564 File Offset: 0x00061764
		public long ToInt64(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1L;
				}
				return 0L;
			case TypeCode.Char:
				return (long)((ulong)this.m_CharValue);
			case TypeCode.SByte:
				return (long)this.m_SByteValue;
			case TypeCode.Byte:
				return (long)((ulong)this.m_ByteValue);
			case TypeCode.Int16:
				return (long)this.m_ShortValue;
			case TypeCode.UInt16:
				return (long)((ulong)this.m_UShortValue);
			case TypeCode.Int32:
				return (long)this.m_IntValue;
			case TypeCode.UInt32:
				return (long)((ulong)this.m_UIntValue);
			case TypeCode.Int64:
				return this.m_LongValue;
			case TypeCode.UInt64:
				return (long)this.m_ULongValue;
			case TypeCode.Single:
				return (long)this.m_FloatValue;
			case TypeCode.Double:
				return (long)this.m_DoubleValue;
			default:
				return 0L;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00063618 File Offset: 0x00061818
		public sbyte ToSByte(IFormatProvider provider = null)
		{
			return (sbyte)this.ToInt64(provider);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00063622 File Offset: 0x00061822
		public float ToSingle(IFormatProvider provider = null)
		{
			return (float)this.ToDouble(provider);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0006362C File Offset: 0x0006182C
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00004C6C File Offset: 0x00002E6C
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0006363A File Offset: 0x0006183A
		public ushort ToUInt16(IFormatProvider provider = null)
		{
			return (ushort)this.ToUInt64(null);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00063644 File Offset: 0x00061844
		public uint ToUInt32(IFormatProvider provider = null)
		{
			return (uint)this.ToUInt64(null);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00063650 File Offset: 0x00061850
		public ulong ToUInt64(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1UL;
				}
				return 0UL;
			case TypeCode.Char:
				return (ulong)this.m_CharValue;
			case TypeCode.SByte:
				return (ulong)((long)this.m_SByteValue);
			case TypeCode.Byte:
				return (ulong)this.m_ByteValue;
			case TypeCode.Int16:
				return (ulong)((long)this.m_ShortValue);
			case TypeCode.UInt16:
				return (ulong)this.m_UShortValue;
			case TypeCode.Int32:
				return (ulong)((long)this.m_IntValue);
			case TypeCode.UInt32:
				return (ulong)this.m_UIntValue;
			case TypeCode.Int64:
				return (ulong)this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return (ulong)this.m_FloatValue;
			case TypeCode.Double:
				return (ulong)this.m_DoubleValue;
			default:
				return 0UL;
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00063704 File Offset: 0x00061904
		public object ToObject()
		{
			switch (this.m_Type)
			{
			case TypeCode.Boolean:
				return this.m_BoolValue;
			case TypeCode.Char:
				return this.m_CharValue;
			case TypeCode.SByte:
				return this.m_SByteValue;
			case TypeCode.Byte:
				return this.m_ByteValue;
			case TypeCode.Int16:
				return this.m_ShortValue;
			case TypeCode.UInt16:
				return this.m_UShortValue;
			case TypeCode.Int32:
				return this.m_IntValue;
			case TypeCode.UInt32:
				return this.m_UIntValue;
			case TypeCode.Int64:
				return this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return this.m_FloatValue;
			case TypeCode.Double:
				return this.m_DoubleValue;
			default:
				return null;
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000637E8 File Offset: 0x000619E8
		public static PrimitiveValue From<TValue>(TValue value) where TValue : struct
		{
			Type type = typeof(TValue);
			if (type.IsEnum)
			{
				type = type.GetEnumUnderlyingType();
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return new PrimitiveValue(Convert.ToBoolean(value));
			case TypeCode.Char:
				return new PrimitiveValue(Convert.ToChar(value));
			case TypeCode.SByte:
				return new PrimitiveValue(Convert.ToSByte(value));
			case TypeCode.Byte:
				return new PrimitiveValue(Convert.ToByte(value));
			case TypeCode.Int16:
				return new PrimitiveValue(Convert.ToInt16(value));
			case TypeCode.UInt16:
				return new PrimitiveValue(Convert.ToUInt16(value));
			case TypeCode.Int32:
				return new PrimitiveValue(Convert.ToInt32(value));
			case TypeCode.UInt32:
				return new PrimitiveValue(Convert.ToUInt32(value));
			case TypeCode.Int64:
				return new PrimitiveValue(Convert.ToInt64(value));
			case TypeCode.UInt64:
				return new PrimitiveValue(Convert.ToUInt64(value));
			case TypeCode.Single:
				return new PrimitiveValue(Convert.ToSingle(value));
			case TypeCode.Double:
				return new PrimitiveValue(Convert.ToDouble(value));
			default:
				throw new ArgumentException(string.Format("Cannot convert value '{0}' of type '{1}' to PrimitiveValue", value, typeof(TValue).Name), "value");
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00063948 File Offset: 0x00061B48
		public static PrimitiveValue FromObject(object value)
		{
			if (value == null)
			{
				return default(PrimitiveValue);
			}
			string stringValue = value as string;
			if (stringValue != null)
			{
				return PrimitiveValue.FromString(stringValue);
			}
			if (value is bool)
			{
				bool b = (bool)value;
				return new PrimitiveValue(b);
			}
			if (value is char)
			{
				char ch = (char)value;
				return new PrimitiveValue(ch);
			}
			if (value is byte)
			{
				byte bt = (byte)value;
				return new PrimitiveValue(bt);
			}
			if (value is sbyte)
			{
				sbyte sbt = (sbyte)value;
				return new PrimitiveValue(sbt);
			}
			if (value is short)
			{
				short s = (short)value;
				return new PrimitiveValue(s);
			}
			if (value is ushort)
			{
				ushort us = (ushort)value;
				return new PrimitiveValue(us);
			}
			if (value is int)
			{
				int i = (int)value;
				return new PrimitiveValue(i);
			}
			if (value is uint)
			{
				uint ui = (uint)value;
				return new PrimitiveValue(ui);
			}
			if (value is long)
			{
				long j = (long)value;
				return new PrimitiveValue(j);
			}
			if (value is ulong)
			{
				ulong ul = (ulong)value;
				return new PrimitiveValue(ul);
			}
			if (value is float)
			{
				float f = (float)value;
				return new PrimitiveValue(f);
			}
			if (value is double)
			{
				double d = (double)value;
				return new PrimitiveValue(d);
			}
			if (value is Enum)
			{
				switch (Type.GetTypeCode(value.GetType().GetEnumUnderlyingType()))
				{
				case TypeCode.SByte:
					return new PrimitiveValue((sbyte)value);
				case TypeCode.Byte:
					return new PrimitiveValue((byte)value);
				case TypeCode.Int16:
					return new PrimitiveValue((short)value);
				case TypeCode.UInt16:
					return new PrimitiveValue((ushort)value);
				case TypeCode.Int32:
					return new PrimitiveValue((int)value);
				case TypeCode.UInt32:
					return new PrimitiveValue((uint)value);
				case TypeCode.Int64:
					return new PrimitiveValue((long)value);
				case TypeCode.UInt64:
					return new PrimitiveValue((ulong)value);
				}
			}
			throw new ArgumentException(string.Format("Cannot convert '{0}' to primitive value", value), "value");
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00063B4B File Offset: 0x00061D4B
		public static implicit operator PrimitiveValue(bool value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00063B53 File Offset: 0x00061D53
		public static implicit operator PrimitiveValue(char value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00063B5B File Offset: 0x00061D5B
		public static implicit operator PrimitiveValue(byte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00063B63 File Offset: 0x00061D63
		public static implicit operator PrimitiveValue(sbyte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00063B6B File Offset: 0x00061D6B
		public static implicit operator PrimitiveValue(short value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00063B73 File Offset: 0x00061D73
		public static implicit operator PrimitiveValue(ushort value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00063B7B File Offset: 0x00061D7B
		public static implicit operator PrimitiveValue(int value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00063B83 File Offset: 0x00061D83
		public static implicit operator PrimitiveValue(uint value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00063B8B File Offset: 0x00061D8B
		public static implicit operator PrimitiveValue(long value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00063B93 File Offset: 0x00061D93
		public static implicit operator PrimitiveValue(ulong value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00063B9B File Offset: 0x00061D9B
		public static implicit operator PrimitiveValue(float value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00063BA3 File Offset: 0x00061DA3
		public static implicit operator PrimitiveValue(double value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00063B4B File Offset: 0x00061D4B
		public static PrimitiveValue FromBoolean(bool value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00063B53 File Offset: 0x00061D53
		public static PrimitiveValue FromChar(char value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00063B5B File Offset: 0x00061D5B
		public static PrimitiveValue FromByte(byte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00063B63 File Offset: 0x00061D63
		public static PrimitiveValue FromSByte(sbyte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00063B6B File Offset: 0x00061D6B
		public static PrimitiveValue FromInt16(short value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00063B73 File Offset: 0x00061D73
		public static PrimitiveValue FromUInt16(ushort value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00063B7B File Offset: 0x00061D7B
		public static PrimitiveValue FromInt32(int value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00063B83 File Offset: 0x00061D83
		public static PrimitiveValue FromUInt32(uint value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00063B8B File Offset: 0x00061D8B
		public static PrimitiveValue FromInt64(long value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00063B93 File Offset: 0x00061D93
		public static PrimitiveValue FromUInt64(ulong value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00063B9B File Offset: 0x00061D9B
		public static PrimitiveValue FromSingle(float value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00063BA3 File Offset: 0x00061DA3
		public static PrimitiveValue FromDouble(double value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x04000C98 RID: 3224
		[FieldOffset(0)]
		private TypeCode m_Type;

		// Token: 0x04000C99 RID: 3225
		[FieldOffset(4)]
		private bool m_BoolValue;

		// Token: 0x04000C9A RID: 3226
		[FieldOffset(4)]
		private char m_CharValue;

		// Token: 0x04000C9B RID: 3227
		[FieldOffset(4)]
		private byte m_ByteValue;

		// Token: 0x04000C9C RID: 3228
		[FieldOffset(4)]
		private sbyte m_SByteValue;

		// Token: 0x04000C9D RID: 3229
		[FieldOffset(4)]
		private short m_ShortValue;

		// Token: 0x04000C9E RID: 3230
		[FieldOffset(4)]
		private ushort m_UShortValue;

		// Token: 0x04000C9F RID: 3231
		[FieldOffset(4)]
		private int m_IntValue;

		// Token: 0x04000CA0 RID: 3232
		[FieldOffset(4)]
		private uint m_UIntValue;

		// Token: 0x04000CA1 RID: 3233
		[FieldOffset(4)]
		private long m_LongValue;

		// Token: 0x04000CA2 RID: 3234
		[FieldOffset(4)]
		private ulong m_ULongValue;

		// Token: 0x04000CA3 RID: 3235
		[FieldOffset(4)]
		private float m_FloatValue;

		// Token: 0x04000CA4 RID: 3236
		[FieldOffset(4)]
		private double m_DoubleValue;
	}
}
