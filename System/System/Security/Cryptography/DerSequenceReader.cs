using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	// Token: 0x020001A4 RID: 420
	internal class DerSequenceReader
	{
		// Token: 0x170001B4 RID: 436
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x00033B2C File Offset: 0x00031D2C
		private int ContentLength
		{
			[CompilerGenerated]
			set
			{
				this.<ContentLength>k__BackingField = value;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00033B35 File Offset: 0x00031D35
		internal DerSequenceReader(byte[] data)
			: this(data, 0, data.Length)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00033B42 File Offset: 0x00031D42
		internal DerSequenceReader(byte[] data, int offset, int length)
			: this(DerSequenceReader.DerTag.Sequence, data, offset, length)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00033B50 File Offset: 0x00031D50
		private DerSequenceReader(DerSequenceReader.DerTag tagToEat, byte[] data, int offset, int length)
		{
			if (offset < 0 || length < 2 || length > data.Length - offset)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this._data = data;
			this._end = offset + length;
			this._position = offset;
			this.EatTag(tagToEat);
			int num = this.EatLength();
			this.ContentLength = num;
			this._end = this._position + num;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00033BBB File Offset: 0x00031DBB
		internal bool HasData
		{
			get
			{
				return this._position < this._end;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00033BCB File Offset: 0x00031DCB
		internal byte PeekTag()
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = this._data[this._position];
			if ((b & 31) == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return b;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00033C00 File Offset: 0x00031E00
		internal void SkipValue()
		{
			this.EatTag((DerSequenceReader.DerTag)this.PeekTag());
			int num = this.EatLength();
			this._position += num;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00033C30 File Offset: 0x00031E30
		internal byte[] ReadNextEncodedValue()
		{
			this.PeekTag();
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(this._data, this._position, array, 0, num3);
			this._position += num3;
			return array;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00033C90 File Offset: 0x00031E90
		internal bool ReadBoolean()
		{
			this.EatTag(DerSequenceReader.DerTag.Boolean);
			int num = this.EatLength();
			if (num != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bool flag = this._data[this._position] > 0;
			this._position += num;
			return flag;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00033CD8 File Offset: 0x00031ED8
		internal int ReadInteger()
		{
			byte[] array = this.ReadIntegerBytes();
			Array.Reverse<byte>(array);
			return (int)new BigInteger(array);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00033CFD File Offset: 0x00031EFD
		internal byte[] ReadIntegerBytes()
		{
			this.EatTag(DerSequenceReader.DerTag.Integer);
			return this.ReadContentAsBytes();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00033D0C File Offset: 0x00031F0C
		internal byte[] ReadBitString()
		{
			this.EatTag(DerSequenceReader.DerTag.BitString);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (this._data[this._position] > 7)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			num--;
			this._position++;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00033D8D File Offset: 0x00031F8D
		internal byte[] ReadOctetString()
		{
			this.EatTag(DerSequenceReader.DerTag.OctetString);
			return this.ReadContentAsBytes();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00033D9C File Offset: 0x00031F9C
		internal string ReadOidAsString()
		{
			this.EatTag(DerSequenceReader.DerTag.ObjectIdentifier);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			StringBuilder stringBuilder = new StringBuilder(num * 4);
			byte b = this._data[this._position];
			byte b2 = b / 40;
			byte b3 = b % 40;
			stringBuilder.Append(b2);
			stringBuilder.Append('.');
			stringBuilder.Append(b3);
			bool flag = true;
			BigInteger bigInteger = new BigInteger(0);
			for (int i = 1; i < num; i++)
			{
				byte b4 = this._data[this._position + i];
				byte b5 = b4 & 127;
				if (flag)
				{
					stringBuilder.Append('.');
					flag = false;
				}
				bigInteger <<= 7;
				bigInteger += b5;
				if (b4 == b5)
				{
					stringBuilder.Append(bigInteger);
					bigInteger = 0;
					flag = true;
				}
			}
			this._position += num;
			return stringBuilder.ToString();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00033E90 File Offset: 0x00032090
		internal string ReadUtf8String()
		{
			this.EatTag(DerSequenceReader.DerTag.UTF8String);
			int num = this.EatLength();
			string @string = Encoding.UTF8.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00033ED8 File Offset: 0x000320D8
		private DerSequenceReader ReadCollectionWithTag(DerSequenceReader.DerTag expected)
		{
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			DerSequenceReader derSequenceReader = new DerSequenceReader(expected, this._data, this._position, num3);
			this._position += num3;
			return derSequenceReader;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00033F3A File Offset: 0x0003213A
		internal DerSequenceReader ReadSequence()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Sequence);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00033F44 File Offset: 0x00032144
		internal DerSequenceReader ReadSet()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Set);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00033F50 File Offset: 0x00032150
		internal string ReadPrintableString()
		{
			this.EatTag(DerSequenceReader.DerTag.PrintableString);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00033F98 File Offset: 0x00032198
		internal string ReadIA5String()
		{
			this.EatTag(DerSequenceReader.DerTag.IA5String);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00033FE0 File Offset: 0x000321E0
		internal string ReadT61String()
		{
			this.EatTag(DerSequenceReader.DerTag.T61String);
			int num = this.EatLength();
			Encoding encoding = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_utf8EncodingWithExceptionFallback, () => new UTF8Encoding(false, true));
			Encoding encoding2 = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_latin1Encoding, () => Encoding.GetEncoding("iso-8859-1"));
			string text;
			try
			{
				text = encoding.GetString(this._data, this._position, num);
			}
			catch (DecoderFallbackException)
			{
				text = encoding2.GetString(this._data, this._position, num);
			}
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(text);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000340A4 File Offset: 0x000322A4
		internal DateTime ReadX509Date()
		{
			DerSequenceReader.DerTag derTag = (DerSequenceReader.DerTag)this.PeekTag();
			if (derTag == DerSequenceReader.DerTag.UTCTime)
			{
				return this.ReadUtcTime();
			}
			if (derTag != DerSequenceReader.DerTag.GeneralizedTime)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return this.ReadGeneralizedTime();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000340DC File Offset: 0x000322DC
		internal DateTime ReadUtcTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.UTCTime, "yyMMddHHmmss'Z'");
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000340EB File Offset: 0x000322EB
		internal DateTime ReadGeneralizedTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.GeneralizedTime, "yyyyMMddHHmmss'Z'");
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000340FC File Offset: 0x000322FC
		internal string ReadBMPString()
		{
			this.EatTag(DerSequenceReader.DerTag.BMPString);
			int num = this.EatLength();
			string @string = Encoding.BigEndianUnicode.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00034144 File Offset: 0x00032344
		private static string TrimTrailingNulls(string value)
		{
			if (value != null && value.Length > 0)
			{
				int num = value.Length;
				while (num > 0 && value[num - 1] == '\0')
				{
					num--;
				}
				if (num != value.Length)
				{
					return value.Substring(0, num);
				}
			}
			return value;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0003418C File Offset: 0x0003238C
		private DateTime ReadTime(DerSequenceReader.DerTag timeTag, string formatString)
		{
			this.EatTag(timeTag);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			DateTimeFormatInfo dateTimeFormatInfo = LazyInitializer.EnsureInitialized<DateTimeFormatInfo>(ref DerSequenceReader.s_validityDateTimeFormatInfo, delegate
			{
				DateTimeFormatInfo dateTimeFormatInfo2 = (DateTimeFormatInfo)CultureInfo.InvariantCulture.DateTimeFormat.Clone();
				dateTimeFormatInfo2.Calendar.TwoDigitYearMax = 2049;
				return dateTimeFormatInfo2;
			});
			DateTime dateTime;
			if (!DateTime.TryParseExact(@string, formatString, dateTimeFormatInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return dateTime;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00034210 File Offset: 0x00032410
		private byte[] ReadContentAsBytes()
		{
			int num = this.EatLength();
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0003424E File Offset: 0x0003244E
		private void EatTag(DerSequenceReader.DerTag expected)
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			this._position++;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00034284 File Offset: 0x00032484
		private static void CheckTag(DerSequenceReader.DerTag expected, byte[] data, int position)
		{
			if (position >= data.Length)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[position];
			byte b2 = b & 31;
			if (b2 == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if ((b & 128) != 0)
			{
				return;
			}
			if ((byte)(expected & (DerSequenceReader.DerTag)31) != b2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000342D8 File Offset: 0x000324D8
		private int EatLength()
		{
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position, this._end, out num2);
			this._position += num2;
			return num;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003430C File Offset: 0x0003250C
		private static int ScanContentLength(byte[] data, int offset, int end, out int bytesConsumed)
		{
			if (offset >= end)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[offset];
			if (b < 128)
			{
				bytesConsumed = 1;
				if ((int)b > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (int)b;
			}
			else
			{
				int num = (int)(b & 127);
				if (num > 4)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bytesConsumed = 1 + num;
				if (bytesConsumed > end - offset)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (bytesConsumed == 1)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				int num2 = offset + bytesConsumed;
				int num3 = 0;
				for (int i = offset + 1; i < num2; i++)
				{
					num3 <<= 8;
					num3 |= (int)data[i];
				}
				if (num3 < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (num3 > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return num3;
			}
		}

		// Token: 0x04000767 RID: 1895
		internal static DateTimeFormatInfo s_validityDateTimeFormatInfo;

		// Token: 0x04000768 RID: 1896
		private static Encoding s_utf8EncodingWithExceptionFallback;

		// Token: 0x04000769 RID: 1897
		private static Encoding s_latin1Encoding;

		// Token: 0x0400076A RID: 1898
		private readonly byte[] _data;

		// Token: 0x0400076B RID: 1899
		private readonly int _end;

		// Token: 0x0400076C RID: 1900
		private int _position;

		// Token: 0x020001A5 RID: 421
		internal enum DerTag : byte
		{
			// Token: 0x0400076F RID: 1903
			Boolean = 1,
			// Token: 0x04000770 RID: 1904
			Integer,
			// Token: 0x04000771 RID: 1905
			BitString,
			// Token: 0x04000772 RID: 1906
			OctetString,
			// Token: 0x04000773 RID: 1907
			Null,
			// Token: 0x04000774 RID: 1908
			ObjectIdentifier,
			// Token: 0x04000775 RID: 1909
			UTF8String = 12,
			// Token: 0x04000776 RID: 1910
			Sequence = 16,
			// Token: 0x04000777 RID: 1911
			Set,
			// Token: 0x04000778 RID: 1912
			PrintableString = 19,
			// Token: 0x04000779 RID: 1913
			T61String,
			// Token: 0x0400077A RID: 1914
			IA5String = 22,
			// Token: 0x0400077B RID: 1915
			UTCTime,
			// Token: 0x0400077C RID: 1916
			GeneralizedTime,
			// Token: 0x0400077D RID: 1917
			BMPString = 30
		}
	}
}
