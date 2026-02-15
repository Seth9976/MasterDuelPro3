using System;
using System.Diagnostics;
using System.Globalization;

namespace AssetStudio
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	public struct Half : IComparable, IFormattable, IConvertible, IComparable<Half>, IEquatable<Half>
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0001618D File Offset: 0x0001438D
		public Half(float value)
		{
			this = HalfHelper.SingleToHalf(value);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001619B File Offset: 0x0001439B
		public Half(int value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001619B File Offset: 0x0001439B
		public Half(long value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001619B File Offset: 0x0001439B
		public Half(double value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000161A5 File Offset: 0x000143A5
		public Half(decimal value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000161B4 File Offset: 0x000143B4
		public Half(uint value)
		{
			this = new Half(value);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000161B4 File Offset: 0x000143B4
		public Half(ulong value)
		{
			this = new Half(value);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000161BF File Offset: 0x000143BF
		public static Half Negate(Half half)
		{
			return -half;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000161C7 File Offset: 0x000143C7
		public static Half Add(Half half1, Half half2)
		{
			return half1 + half2;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000161D0 File Offset: 0x000143D0
		public static Half Subtract(Half half1, Half half2)
		{
			return half1 - half2;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000161D9 File Offset: 0x000143D9
		public static Half Multiply(Half half1, Half half2)
		{
			return half1 * half2;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000161E2 File Offset: 0x000143E2
		public static Half Divide(Half half1, Half half2)
		{
			return half1 / half2;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000161EB File Offset: 0x000143EB
		public static Half operator +(Half half)
		{
			return half;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000161EE File Offset: 0x000143EE
		public static Half operator -(Half half)
		{
			return HalfHelper.Negate(half);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000161F6 File Offset: 0x000143F6
		public static Half operator ++(Half half)
		{
			return (Half)(half + 1f);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00016209 File Offset: 0x00014409
		public static Half operator --(Half half)
		{
			return (Half)(half - 1f);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001621C File Offset: 0x0001441C
		public static Half operator +(Half half1, Half half2)
		{
			return (Half)(half1 + half2);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016232 File Offset: 0x00014432
		public static Half operator -(Half half1, Half half2)
		{
			return (Half)(half1 - half2);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016248 File Offset: 0x00014448
		public static Half operator *(Half half1, Half half2)
		{
			return (Half)(half1 * half2);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001625E File Offset: 0x0001445E
		public static Half operator /(Half half1, Half half2)
		{
			return (Half)(half1 / half2);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016274 File Offset: 0x00014474
		public static bool operator ==(Half half1, Half half2)
		{
			return !Half.IsNaN(half1) && half1.value == half2.value;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001628E File Offset: 0x0001448E
		public static bool operator !=(Half half1, Half half2)
		{
			return half1.value != half2.value;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000162A1 File Offset: 0x000144A1
		public static bool operator <(Half half1, Half half2)
		{
			return half1 < half2;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000162B3 File Offset: 0x000144B3
		public static bool operator >(Half half1, Half half2)
		{
			return half1 > half2;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000162C5 File Offset: 0x000144C5
		public static bool operator <=(Half half1, Half half2)
		{
			return half1 == half2 || half1 < half2;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000162D9 File Offset: 0x000144D9
		public static bool operator >=(Half half1, Half half2)
		{
			return half1 == half2 || half1 > half2;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(byte value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(short value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(char value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(int value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(long value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000162ED File Offset: 0x000144ED
		public static explicit operator Half(float value)
		{
			return new Half(value);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000162ED File Offset: 0x000144ED
		public static explicit operator Half(double value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000162F6 File Offset: 0x000144F6
		public static explicit operator Half(decimal value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016304 File Offset: 0x00014504
		public static explicit operator byte(Half value)
		{
			return (byte)value;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001630E File Offset: 0x0001450E
		public static explicit operator char(Half value)
		{
			return (char)value;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00016318 File Offset: 0x00014518
		public static explicit operator short(Half value)
		{
			return (short)value;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00016322 File Offset: 0x00014522
		public static explicit operator int(Half value)
		{
			return (int)value;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001632C File Offset: 0x0001452C
		public static explicit operator long(Half value)
		{
			return (long)value;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016336 File Offset: 0x00014536
		public static implicit operator float(Half value)
		{
			return HalfHelper.HalfToSingle(value);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001633F File Offset: 0x0001453F
		public static implicit operator double(Half value)
		{
			return (double)value;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00016349 File Offset: 0x00014549
		public static explicit operator decimal(Half value)
		{
			return (decimal)value;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(sbyte value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000162ED File Offset: 0x000144ED
		public static implicit operator Half(ushort value)
		{
			return new Half((float)value);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00016357 File Offset: 0x00014557
		public static implicit operator Half(uint value)
		{
			return new Half(value);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00016357 File Offset: 0x00014557
		public static implicit operator Half(ulong value)
		{
			return new Half(value);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00016361 File Offset: 0x00014561
		public static explicit operator sbyte(Half value)
		{
			return (sbyte)value;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001630E File Offset: 0x0001450E
		public static explicit operator ushort(Half value)
		{
			return (ushort)value;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001636B File Offset: 0x0001456B
		public static explicit operator uint(Half value)
		{
			return (uint)value;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00016375 File Offset: 0x00014575
		public static explicit operator ulong(Half value)
		{
			return (ulong)value;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00016380 File Offset: 0x00014580
		public int CompareTo(Half other)
		{
			int result = 0;
			if (this < other)
			{
				result = -1;
			}
			else if (this > other)
			{
				result = 1;
			}
			else if (this != other)
			{
				if (!Half.IsNaN(this))
				{
					result = 1;
				}
				else if (!Half.IsNaN(other))
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000163E0 File Offset: 0x000145E0
		public int CompareTo(object obj)
		{
			int result;
			if (obj == null)
			{
				result = 1;
			}
			else
			{
				if (!(obj is Half))
				{
					throw new ArgumentException("Object must be of type Half.");
				}
				result = this.CompareTo((Half)obj);
			}
			return result;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00016419 File Offset: 0x00014619
		public bool Equals(Half other)
		{
			return other == this || (Half.IsNaN(other) && Half.IsNaN(this));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016440 File Offset: 0x00014640
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Half)
			{
				Half half = (Half)obj;
				if (half == this || (Half.IsNaN(half) && Half.IsNaN(this)))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00016484 File Offset: 0x00014684
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016491 File Offset: 0x00014691
		public TypeCode GetTypeCode()
		{
			return (TypeCode)255;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016498 File Offset: 0x00014698
		public static byte[] GetBytes(Half value)
		{
			return BitConverter.GetBytes(value.value);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000164A5 File Offset: 0x000146A5
		public static ushort GetBits(Half value)
		{
			return value.value;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000164AD File Offset: 0x000146AD
		public static Half ToHalf(byte[] value, int startIndex)
		{
			return Half.ToHalf((ushort)BitConverter.ToInt16(value, startIndex));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000164BC File Offset: 0x000146BC
		public static Half ToHalf(ushort bits)
		{
			return new Half
			{
				value = bits
			};
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000164DA File Offset: 0x000146DA
		public static int Sign(Half value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			if (value != 0)
			{
				throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
			}
			return 0;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00016516 File Offset: 0x00014716
		public static Half Abs(Half value)
		{
			return HalfHelper.Abs(value);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001651E File Offset: 0x0001471E
		public static Half Max(Half value1, Half value2)
		{
			if (!(value1 < value2))
			{
				return value1;
			}
			return value2;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001652C File Offset: 0x0001472C
		public static Half Min(Half value1, Half value2)
		{
			if (!(value1 < value2))
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001653A File Offset: 0x0001473A
		public static bool IsNaN(Half half)
		{
			return HalfHelper.IsNaN(half);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016542 File Offset: 0x00014742
		public static bool IsInfinity(Half half)
		{
			return HalfHelper.IsInfinity(half);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001654A File Offset: 0x0001474A
		public static bool IsNegativeInfinity(Half half)
		{
			return HalfHelper.IsNegativeInfinity(half);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016552 File Offset: 0x00014752
		public static bool IsPositiveInfinity(Half half)
		{
			return HalfHelper.IsPositiveInfinity(half);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001655A File Offset: 0x0001475A
		public static Half Parse(string value)
		{
			return (Half)float.Parse(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001656C File Offset: 0x0001476C
		public static Half Parse(string value, IFormatProvider provider)
		{
			return (Half)float.Parse(value, provider);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001657A File Offset: 0x0001477A
		public static Half Parse(string value, NumberStyles style)
		{
			return (Half)float.Parse(value, style, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001658D File Offset: 0x0001478D
		public static Half Parse(string value, NumberStyles style, IFormatProvider provider)
		{
			return (Half)float.Parse(value, style, provider);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001659C File Offset: 0x0001479C
		public static bool TryParse(string value, out Half result)
		{
			float f;
			if (float.TryParse(value, out f))
			{
				result = (Half)f;
				return true;
			}
			result = default(Half);
			return false;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000165CC File Offset: 0x000147CC
		public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out Half result)
		{
			bool parseResult = false;
			float f;
			if (float.TryParse(value, style, provider, out f))
			{
				result = (Half)f;
				parseResult = true;
			}
			else
			{
				result = default(Half);
			}
			return parseResult;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016600 File Offset: 0x00014800
		public override string ToString()
		{
			return this.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00016628 File Offset: 0x00014828
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001664C File Offset: 0x0001484C
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016674 File Offset: 0x00014874
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString(format, formatProvider);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016697 File Offset: 0x00014897
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000166A5 File Offset: 0x000148A5
		TypeCode IConvertible.GetTypeCode()
		{
			return this.GetTypeCode();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000166AD File Offset: 0x000148AD
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000166C0 File Offset: 0x000148C0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000166D3 File Offset: 0x000148D3
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "Half", "Char"));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000166F3 File Offset: 0x000148F3
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "Half", "DateTime"));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016713 File Offset: 0x00014913
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016726 File Offset: 0x00014926
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016739 File Offset: 0x00014939
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001674C File Offset: 0x0001494C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001675F File Offset: 0x0001495F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016772 File Offset: 0x00014972
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016785 File Offset: 0x00014985
		string IConvertible.ToString(IFormatProvider provider)
		{
			return Convert.ToString(this, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001679D File Offset: 0x0001499D
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this).ToType(conversionType, provider);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000167B7 File Offset: 0x000149B7
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000167CA File Offset: 0x000149CA
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000167DD File Offset: 0x000149DD
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0400098F RID: 2447
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal ushort value;

		// Token: 0x04000990 RID: 2448
		public static readonly Half Epsilon = Half.ToHalf(1);

		// Token: 0x04000991 RID: 2449
		public static readonly Half MaxValue = Half.ToHalf(31743);

		// Token: 0x04000992 RID: 2450
		public static readonly Half MinValue = Half.ToHalf(64511);

		// Token: 0x04000993 RID: 2451
		public static readonly Half NaN = Half.ToHalf(65024);

		// Token: 0x04000994 RID: 2452
		public static readonly Half NegativeInfinity = Half.ToHalf(64512);

		// Token: 0x04000995 RID: 2453
		public static readonly Half PositiveInfinity = Half.ToHalf(31744);
	}
}
