using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000079 RID: 121
	[GenerateTestsForBurstCompatibility]
	[GenerateTestsForBurstCompatibility]
	[GenerateTestsForBurstCompatibility]
	[GenerateTestsForBurstCompatibility]
	public static class FixedStringMethods
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int len = fs.Length;
			int runeLen = rune.LengthInUtf8Bytes();
			if (!fs.TryResize(len + runeLen, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			return (ref fs).Write(ref len, rune);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000CA27 File Offset: 0x0000AC27
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, char ch) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append(ch);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000CA38 File Offset: 0x0000AC38
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError AppendRawByte<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte a) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int origLength = fs.Length;
			if (!fs.TryResize(origLength + 1, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			fs.GetUnsafePtr()[origLength] = a;
			return FormatError.None;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000CA78 File Offset: 0x0000AC78
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune, int count) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int origLength = fs.Length;
			if (!fs.TryResize(origLength + rune.LengthInUtf8Bytes() * count, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			int cap = fs.Capacity;
			byte* b = fs.GetUnsafePtr();
			int offset = origLength;
			for (int i = 0; i < count; i++)
			{
				if (Unicode.UcsToUtf8(b, ref offset, cap, rune) != ConversionError.None)
				{
					return FormatError.Overflow;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, long input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* temp = stackalloc byte[(UIntPtr)20];
			int offset = 20;
			if (input >= 0L)
			{
				do
				{
					byte digit = (byte)(input % 10L);
					temp[--offset] = 48 + digit;
					input /= 10L;
				}
				while (input != 0L);
			}
			else
			{
				do
				{
					byte digit2 = (byte)(input % 10L);
					temp[--offset] = 48 - digit2;
					input /= 10L;
				}
				while (input != 0L);
				temp[--offset] = 45;
			}
			return (ref fs).Append(temp + offset, 20 - offset);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, int input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append((long)input);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000CB68 File Offset: 0x0000AD68
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ulong input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* temp = stackalloc byte[(UIntPtr)20];
			int offset = 20;
			do
			{
				byte digit = (byte)(input % 10UL);
				temp[--offset] = 48 + digit;
				input /= 10UL;
			}
			while (input != 0UL);
			return (ref fs).Append(temp + offset, 20 - offset);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000CBA9 File Offset: 0x0000ADA9
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, uint input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append((ulong)input);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, float input, char decimalSeparator = '.') where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FixedStringUtils.UintFloatUnion ufu = new FixedStringUtils.UintFloatUnion
			{
				floatValue = input
			};
			uint sign = ufu.uintValue >> 31;
			ufu.uintValue &= 2147483647U;
			if ((ufu.uintValue & 2139095040U) == 2139095040U)
			{
				if (ufu.uintValue != 2139095040U)
				{
					return (ref fs).Append('N', 'a', 'N');
				}
				FormatError error;
				if (sign != 0U && (error = (ref fs).Append('-')) != FormatError.None)
				{
					return error;
				}
				return (ref fs).Append('I', 'n', 'f', 'i', 'n', 'i', 't', 'y');
			}
			else
			{
				FormatError error;
				if (sign != 0U && ufu.uintValue != 0U && (error = (ref fs).Append('-')) != FormatError.None)
				{
					return error;
				}
				ulong decimalMantissa = 0UL;
				int decimalExponent = 0;
				FixedStringUtils.Base2ToBase10(ref decimalMantissa, ref decimalExponent, ufu.floatValue);
				char* backwards = stackalloc char[(UIntPtr)18];
				int decimalDigits = 0;
				while (decimalDigits < 9)
				{
					ulong decimalDigit = decimalMantissa % 10UL;
					backwards[(IntPtr)(8 - decimalDigits++) * 2] = (char)(48UL + decimalDigit);
					decimalMantissa /= 10UL;
					if (decimalMantissa <= 0UL)
					{
						char* ascii = backwards + 9 - decimalDigits;
						int leadingZeroes = -decimalExponent - decimalDigits + 1;
						if (leadingZeroes > 0)
						{
							if (leadingZeroes > 4)
							{
								return (ref fs).AppendScientific(ascii, decimalDigits, decimalExponent, decimalSeparator);
							}
							if ((error = (ref fs).Append('0', decimalSeparator)) != FormatError.None)
							{
								return error;
							}
							for (leadingZeroes--; leadingZeroes > 0; leadingZeroes--)
							{
								if ((error = (ref fs).Append('0')) != FormatError.None)
								{
									return error;
								}
							}
							for (int i = 0; i < decimalDigits; i++)
							{
								if ((error = (ref fs).Append(ascii[i])) != FormatError.None)
								{
									return error;
								}
							}
							return FormatError.None;
						}
						else
						{
							int trailingZeroes = decimalExponent;
							if (trailingZeroes <= 0)
							{
								int indexOfSeparator = decimalDigits + decimalExponent;
								for (int j = 0; j < decimalDigits; j++)
								{
									if (j == indexOfSeparator && (error = (ref fs).Append(decimalSeparator)) != FormatError.None)
									{
										return error;
									}
									if ((error = (ref fs).Append(ascii[j])) != FormatError.None)
									{
										return error;
									}
								}
								return FormatError.None;
							}
							if (trailingZeroes > 4)
							{
								return (ref fs).AppendScientific(ascii, decimalDigits, decimalExponent, decimalSeparator);
							}
							for (int k = 0; k < decimalDigits; k++)
							{
								if ((error = (ref fs).Append(ascii[k])) != FormatError.None)
								{
									return error;
								}
							}
							while (trailingZeroes > 0)
							{
								if ((error = (ref fs).Append('0')) != FormatError.None)
								{
									return error;
								}
								trailingZeroes--;
							}
							return FormatError.None;
						}
					}
				}
				return FormatError.Overflow;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 inputRef = ref UnsafeUtilityExtensions.AsRef<T2>(in input);
			return (ref fs).Append(inputRef.GetUnsafePtr(), inputRef.Length);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000CE06 File Offset: 0x0000B006
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static CopyError CopyFrom<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			fs.Length = 0;
			if ((ref fs).Append(in input) != FormatError.None)
			{
				return CopyError.Truncation;
			}
			return CopyError.None;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000CE24 File Offset: 0x0000B024
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* utf8Bytes, int utf8BytesLength) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int origLength = fs.Length;
			if (!fs.TryResize(origLength + utf8BytesLength, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			UnsafeUtility.MemCpy((void*)(fs.GetUnsafePtr() + origLength), (void*)utf8Bytes, (long)utf8BytesLength);
			return FormatError.None;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000CE6C File Offset: 0x0000B06C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, string s) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int worstCaseCapacity = s.Length * 4;
			byte* utf8Bytes = stackalloc byte[(UIntPtr)worstCaseCapacity];
			int utf8Len;
			fixed (string text = s)
			{
				char* chars = text;
				if (chars != null)
				{
					chars += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (UTF8ArrayUnsafeUtility.Copy(utf8Bytes, out utf8Len, worstCaseCapacity, chars, s.Length) != CopyError.None)
				{
					return FormatError.Overflow;
				}
			}
			return (ref fs).Append(utf8Bytes, utf8Len);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static CopyError CopyFrom<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, string s) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			fs.Length = 0;
			if ((ref fs).Append(s) != FormatError.None)
			{
				return CopyError.Truncation;
			}
			return CopyError.None;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static CopyError CopyFromTruncated<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, string s) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			char* chars = s;
			if (chars != null)
			{
				chars += RuntimeHelpers.OffsetToStringData / 2;
			}
			int utf8Len;
			CopyError copyError = UTF8ArrayUnsafeUtility.Copy(fs.GetUnsafePtr(), out utf8Len, fs.Capacity, chars, s.Length);
			fs.Length = utf8Len;
			return copyError;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000CF24 File Offset: 0x0000B124
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static CopyError CopyFromTruncated<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 input) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int capacity = fs.Capacity;
			T2 t = input;
			byte* unsafePtr2 = t.GetUnsafePtr();
			t = input;
			int utf8Len;
			CopyError copyError = UTF8ArrayUnsafeUtility.Copy(unsafePtr, out utf8Len, capacity, unsafePtr2, t.Length);
			fs.Length = utf8Len;
			return copyError;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000CF88 File Offset: 0x0000B188
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0>(this T dest, in U format, in T0 arg0) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						if (currByte - 48 == 0)
						{
							err = (ref dest).Append(in arg0);
						}
						else
						{
							err = FormatError.BadFormatSpecifier;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000D068 File Offset: 0x0000B268
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1>(this T dest, in U format, in T0 arg0, in T1 arg1) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						int num = (int)(currByte - 48);
						if (num != 0)
						{
							if (num != 1)
							{
								err = FormatError.BadFormatSpecifier;
							}
							else
							{
								err = (ref dest).Append(in arg1);
							}
						}
						else
						{
							err = (ref dest).Append(in arg0);
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000D160 File Offset: 0x0000B360
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D270 File Offset: 0x0000B470
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D38C File Offset: 0x0000B58C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T5 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						case 53:
							err = (ref dest).Append(in arg5);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5, [global::System.Runtime.CompilerServices.IsUnmanaged] T6>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T5 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T6 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						case 53:
							err = (ref dest).Append(in arg5);
							break;
						case 54:
							err = (ref dest).Append(in arg6);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000D754 File Offset: 0x0000B954
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5, [global::System.Runtime.CompilerServices.IsUnmanaged] T6, [global::System.Runtime.CompilerServices.IsUnmanaged] T7>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T5 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T6 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T7 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						case 53:
							err = (ref dest).Append(in arg5);
							break;
						case 54:
							err = (ref dest).Append(in arg6);
							break;
						case 55:
							err = (ref dest).Append(in arg7);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5, [global::System.Runtime.CompilerServices.IsUnmanaged] T6, [global::System.Runtime.CompilerServices.IsUnmanaged] T7, [global::System.Runtime.CompilerServices.IsUnmanaged] T8>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T5 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T6 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T7 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T8 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						case 53:
							err = (ref dest).Append(in arg5);
							break;
						case 54:
							err = (ref dest).Append(in arg6);
							break;
						case 55:
							err = (ref dest).Append(in arg7);
							break;
						case 56:
							err = (ref dest).Append(in arg8);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000DA44 File Offset: 0x0000BC44
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static FormatError AppendFormat<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U, [global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5, [global::System.Runtime.CompilerServices.IsUnmanaged] T6, [global::System.Runtime.CompilerServices.IsUnmanaged] T7, [global::System.Runtime.CompilerServices.IsUnmanaged] T8, [global::System.Runtime.CompilerServices.IsUnmanaged] T9>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes where T0 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T1 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T3 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T4 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T5 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T6 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T7 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T8 : struct, ValueType, INativeList<byte>, IUTF8Bytes where T9 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int formatLength = ptr.Length;
			byte* formatBytes = ptr.GetUnsafePtr();
			int i = 0;
			while (i < formatLength)
			{
				byte currByte = formatBytes[i++];
				FormatError err;
				if (currByte == 123)
				{
					if (i >= formatLength)
					{
						return FormatError.BadFormatSpecifier;
					}
					currByte = formatBytes[i++];
					if (currByte >= 48 && currByte <= 57 && i < formatLength && formatBytes[i++] == 125)
					{
						switch (currByte)
						{
						case 48:
							err = (ref dest).Append(in arg0);
							break;
						case 49:
							err = (ref dest).Append(in arg1);
							break;
						case 50:
							err = (ref dest).Append(in arg2);
							break;
						case 51:
							err = (ref dest).Append(in arg3);
							break;
						case 52:
							err = (ref dest).Append(in arg4);
							break;
						case 53:
							err = (ref dest).Append(in arg5);
							break;
						case 54:
							err = (ref dest).Append(in arg6);
							break;
						case 55:
							err = (ref dest).Append(in arg7);
							break;
						case 56:
							err = (ref dest).Append(in arg8);
							break;
						case 57:
							err = (ref dest).Append(in arg9);
							break;
						default:
							err = FormatError.BadFormatSpecifier;
							break;
						}
					}
					else if (currByte == 123)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else if (currByte == 125)
				{
					if (i < formatLength)
					{
						currByte = formatBytes[i++];
					}
					if (currByte == 125)
					{
						err = (ref dest).AppendRawByte(currByte);
					}
					else
					{
						err = FormatError.BadFormatSpecifier;
					}
				}
				else
				{
					err = (ref dest).AppendRawByte(currByte);
				}
				if (err != FormatError.None)
				{
					return err;
				}
			}
			return FormatError.None;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, char a, char b) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append(a) | (ref fs).Append(b)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, char a, char b, char c) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append(a) | (ref fs).Append(b) | (ref fs).Append(c)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000DC28 File Offset: 0x0000BE28
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, char a, char b, char c, char d, char e, char f, char g, char h) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append(a) | (ref fs).Append(b) | (ref fs).Append(c) | (ref fs).Append(d) | (ref fs).Append(e) | (ref fs).Append(f) | (ref fs).Append(g) | (ref fs).Append(h)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static FormatError AppendScientific<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, char* source, int sourceLength, int decimalExponent, char decimalSeparator = '.') where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			FormatError error;
			if ((error = (ref fs).Append(*source)) != FormatError.None)
			{
				return error;
			}
			if (sourceLength > 1)
			{
				if ((error = (ref fs).Append(decimalSeparator)) != FormatError.None)
				{
					return error;
				}
				for (int i = 1; i < sourceLength; i++)
				{
					if ((error = (ref fs).Append(source[i])) != FormatError.None)
					{
						return error;
					}
				}
			}
			if ((error = (ref fs).Append('E')) != FormatError.None)
			{
				return error;
			}
			if (decimalExponent < 0)
			{
				if ((error = (ref fs).Append('-')) != FormatError.None)
				{
					return error;
				}
				decimalExponent *= -1;
				decimalExponent -= sourceLength - 1;
			}
			else
			{
				if ((error = (ref fs).Append('+')) != FormatError.None)
				{
					return error;
				}
				decimalExponent += sourceLength - 1;
			}
			char* ascii = stackalloc char[(UIntPtr)4];
			for (int j = 0; j < 2; j++)
			{
				int decimalDigit = decimalExponent % 10;
				ascii[1 - j] = (char)(48 + decimalDigit);
				decimalExponent /= 10;
			}
			for (int k = 0; k < 2; k++)
			{
				if ((error = (ref fs).Append(ascii[k])) != FormatError.None)
				{
					return error;
				}
			}
			return FormatError.None;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000DD88 File Offset: 0x0000BF88
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static bool Found<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int offset, char a, char b, char c) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int old = offset;
			if (((ref fs).Read(ref offset).value | 32) == (int)a && ((ref fs).Read(ref offset).value | 32) == (int)b && ((ref fs).Read(ref offset).value | 32) == (int)c)
			{
				return true;
			}
			offset = old;
			return false;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static bool Found<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int offset, char a, char b, char c, char d, char e, char f, char g, char h) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int old = offset;
			if (((ref fs).Read(ref offset).value | 32) == (int)a && ((ref fs).Read(ref offset).value | 32) == (int)b && ((ref fs).Read(ref offset).value | 32) == (int)c && ((ref fs).Read(ref offset).value | 32) == (int)d && ((ref fs).Read(ref offset).value | 32) == (int)e && ((ref fs).Read(ref offset).value | 32) == (int)f && ((ref fs).Read(ref offset).value | 32) == (int)g && ((ref fs).Read(ref offset).value | 32) == (int)h)
			{
				return true;
			}
			offset = old;
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000DE88 File Offset: 0x0000C088
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckSubstringInRange(int strLength, int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("startIndex {0} must be positive.", startIndex));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("length {0} cannot be negative.", length));
			}
			if (startIndex > strLength)
			{
				throw new ArgumentOutOfRangeException(string.Format("startIndex {0} cannot be larger than string length {1}.", startIndex, strLength));
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T Substring<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T str, int startIndex, int length) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			length = math.min(length, str.Length - startIndex);
			T substr = new T();
			(ref substr).Append(str.GetUnsafePtr() + startIndex, length);
			return substr;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000DF2D File Offset: 0x0000C12D
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T Substring<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T str, int startIndex) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (ref str).Substring(startIndex, str.Length - startIndex);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000DF44 File Offset: 0x0000C144
		public static NativeText Substring(this NativeText str, int startIndex, int length, AllocatorManager.AllocatorHandle allocator)
		{
			length = math.min(length, str.Length - startIndex);
			NativeText substr = new NativeText(length, allocator);
			(ref substr).Append(str.GetUnsafePtr() + startIndex, length);
			return substr;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public static NativeText Substring(this NativeText str, int startIndex, AllocatorManager.AllocatorHandle allocator)
		{
			return (ref str).Substring(startIndex, str.Length - startIndex);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000DF8D File Offset: 0x0000C18D
		public unsafe static NativeText Substring(this NativeText str, int startIndex, int length)
		{
			return (ref str).Substring(startIndex, length, str.m_Data->m_UntypedListData.Allocator);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public static NativeText Substring(this NativeText str, int startIndex)
		{
			return (ref str).Substring(startIndex, str.Length - startIndex);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int dstLen = fs.Length;
			int tempIndex;
			for (int index = 0; index < dstLen; index = tempIndex)
			{
				tempIndex = index;
				if ((ref fs).Read(ref tempIndex).value == rune.value)
				{
					return index;
				}
			}
			return -1;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* dst = fs.GetUnsafePtr();
			int dstLen = fs.Length;
			int i = 0;
			IL_003C:
			while (i <= dstLen - bytesLen)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (dst[i + j] != bytes[j])
					{
						i++;
						goto IL_003C;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000E038 File Offset: 0x0000C238
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen, int startIndex, int distance = 2147483647) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* dst = fs.GetUnsafePtr();
			int dstLen = fs.Length;
			int searchrange = Math.Min(distance - 1, dstLen - bytesLen);
			int i = startIndex;
			IL_004F:
			while (i <= searchrange)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (dst[i + j] != bytes[j])
					{
						i++;
						goto IL_004F;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000E09C File Offset: 0x0000C29C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).IndexOf(oref.GetUnsafePtr(), oref.Length);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other, int startIndex, int distance = 2147483647) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).IndexOf(oref.GetUnsafePtr(), oref.Length, startIndex, distance);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000E104 File Offset: 0x0000C304
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).IndexOf(in other) != -1;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000E114 File Offset: 0x0000C314
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static int LastIndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if (Unicode.IsValidCodePoint(rune.value))
			{
				for (int i = fs.Length - 1; i >= 0; i--)
				{
					Unicode.Rune runeAtIndex = (ref fs).Peek(i);
					if (Unicode.IsValidCodePoint(runeAtIndex.value) && runeAtIndex.value == rune.value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000E170 File Offset: 0x0000C370
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int LastIndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* dst = fs.GetUnsafePtr();
			int i = fs.Length - bytesLen;
			IL_003C:
			while (i >= 0)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (dst[i + j] != bytes[j])
					{
						i--;
						goto IL_003C;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int LastIndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen, int startIndex, int distance = 2147483647) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* dst = fs.GetUnsafePtr();
			startIndex = Math.Min(fs.Length - bytesLen, startIndex);
			int searchrange = Math.Max(0, startIndex - distance);
			int i = startIndex;
			IL_0050:
			while (i >= searchrange)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (dst[i + j] != bytes[j])
					{
						i--;
						goto IL_0050;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000E224 File Offset: 0x0000C424
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int LastIndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).LastIndexOf(oref.GetUnsafePtr(), oref.Length);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000E258 File Offset: 0x0000C458
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int LastIndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other, int startIndex, int distance = 2147483647) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).LastIndexOf(oref.GetUnsafePtr(), oref.Length, startIndex, distance);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000E28C File Offset: 0x0000C48C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int CompareTo<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* a = fs.GetUnsafePtr();
			int aa = fs.Length;
			int chars = ((aa < bytesLen) ? aa : bytesLen);
			for (int i = 0; i < chars; i++)
			{
				if (a[i] < bytes[i])
				{
					return -1;
				}
				if (a[i] > bytes[i])
				{
					return 1;
				}
			}
			if (aa < bytesLen)
			{
				return -1;
			}
			if (aa > bytesLen)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int CompareTo<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).CompareTo(oref.GetUnsafePtr(), oref.Length);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000E324 File Offset: 0x0000C524
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static bool Equals<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, byte* bytes, int bytesLen) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			byte* a = fs.GetUnsafePtr();
			return fs.Length == bytesLen && (a == bytes || (ref fs).CompareTo(bytes, bytesLen) == 0);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000E360 File Offset: 0x0000C560
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static bool Equals<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(this T fs, in T2 other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where T2 : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			ref T2 oref = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).Equals(oref.GetUnsafePtr(), oref.Length);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000E394 File Offset: 0x0000C594
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static Unicode.Rune Peek<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, int index) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return Unicode.BadRune;
			}
			Unicode.Rune rune;
			Unicode.Utf8ToUcs(out rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static Unicode.Rune Read<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int index) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return Unicode.BadRune;
			}
			Unicode.Rune rune;
			Unicode.Utf8ToUcs(out rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000E421 File Offset: 0x0000C621
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Write<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int index, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			if (Unicode.UcsToUtf8(fs.GetUnsafePtr(), ref index, fs.Capacity, rune) != ConversionError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000E448 File Offset: 0x0000C648
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public unsafe static string ConvertToString<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			char* c;
			int length;
			checked
			{
				c = stackalloc char[unchecked((UIntPtr)(fs.Length * 2)) * 2];
				length = 0;
			}
			Unicode.Utf8ToUtf16(fs.GetUnsafePtr(), fs.Length, c, out length, fs.Length * 2);
			return new string(c, 0, length);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000E4A2 File Offset: 0x0000C6A2
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int ComputeHashCode<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return (int)CollectionHelper.Hash((void*)fs.GetUnsafePtr(), fs.Length);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000E4C1 File Offset: 0x0000C6C1
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static int EffectiveSizeOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			return 2 + fs.Length + 1;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static bool StartsWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int len = rune.LengthInUtf8Bytes();
			return fs.Length >= len && UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr(), len, &rune, 1) == 0;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000E514 File Offset: 0x0000C714
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static bool StartsWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(this T fs, in U other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			U u = other;
			int len = u.Length;
			if (fs.Length >= len)
			{
				byte* unsafePtr = fs.GetUnsafePtr();
				int num = len;
				u = other;
				return UTF8ArrayUnsafeUtility.StrCmp(unsafePtr, num, u.GetUnsafePtr(), len) == 0;
			}
			return false;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000E574 File Offset: 0x0000C774
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static bool EndsWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, Unicode.Rune rune) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int len = rune.LengthInUtf8Bytes();
			return fs.Length >= len && UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr() + fs.Length - len, len, &rune, 1) == 0;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static bool EndsWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(this T fs, in U other) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes where U : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			U u = other;
			int len = u.Length;
			if (fs.Length >= len)
			{
				byte* ptr = fs.GetUnsafePtr() + fs.Length - len;
				int num = len;
				u = other;
				return UTF8ArrayUnsafeUtility.StrCmp(ptr, num, u.GetUnsafePtr(), len) == 0;
			}
			return false;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000E630 File Offset: 0x0000C830
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static int TrimStartIndex<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			int index = 0;
			int prev;
			Unicode.Rune rune;
			do
			{
				prev = index;
			}
			while (Unicode.Utf8ToUcs(out rune, ptr, ref index, lengthInBytes) == ConversionError.None && rune.IsWhiteSpace());
			index -= index - prev;
			return index;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000E678 File Offset: 0x0000C878
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static int TrimStartIndex<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ReadOnlySpan<Unicode.Rune> trimRunes) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			int index = 0;
			int prev;
			ConversionError error;
			bool doTrim;
			do
			{
				prev = index;
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref index, lengthInBytes);
				doTrim = false;
				int i = 0;
				int num = trimRunes.Length;
				while (i < num && !doTrim)
				{
					doTrim |= *trimRunes[i] == rune;
					i++;
				}
			}
			while (error == ConversionError.None && doTrim);
			index -= index - prev;
			return index;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static int TrimEndIndex<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			int index = lengthInBytes;
			int prev;
			Unicode.Rune rune;
			do
			{
				prev = index;
			}
			while (Unicode.Utf8ToUcsReverse(out rune, ptr, ref index, lengthInBytes) == ConversionError.None && rune.IsWhiteSpace());
			index += prev - index;
			return index;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000E744 File Offset: 0x0000C944
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static int TrimEndIndex<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ReadOnlySpan<Unicode.Rune> trimRunes) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			int index = lengthInBytes;
			int prev;
			ConversionError error;
			bool doTrim;
			do
			{
				prev = index;
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcsReverse(out rune, ptr, ref index, lengthInBytes);
				doTrim = false;
				int i = 0;
				int num = trimRunes.Length;
				while (i < num && !doTrim)
				{
					doTrim |= *trimRunes[i] == rune;
					i++;
				}
			}
			while (error == ConversionError.None && doTrim);
			index += prev - index;
			return index;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T TrimStart<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int index = (ref fs).TrimStartIndex<T>();
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr() + index, fs.Length - index);
			return result;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000E808 File Offset: 0x0000CA08
		public static UnsafeText TrimStart(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int index = (ref fs).TrimStartIndex<UnsafeText>();
			int lengthInBytes = fs.Length - index;
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + index, lengthInBytes);
			return result;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000E840 File Offset: 0x0000CA40
		public static NativeText TrimStart(this NativeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int index = (ref fs).TrimStartIndex<NativeText>();
			int lengthInBytes = fs.Length - index;
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + index, lengthInBytes);
			return result;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000E878 File Offset: 0x0000CA78
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T TrimStart<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ReadOnlySpan<Unicode.Rune> trimRunes) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int index = (ref fs).TrimStartIndex(trimRunes);
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr() + index, fs.Length - index);
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public static UnsafeText TrimStart(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int index = (ref fs).TrimStartIndex(trimRunes);
			int lengthInBytes = fs.Length - index;
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + index, lengthInBytes);
			return result;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public static NativeText TrimStart(this NativeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int index = (ref fs).TrimStartIndex(trimRunes);
			int lengthInBytes = fs.Length - index;
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + index, lengthInBytes);
			return result;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000E930 File Offset: 0x0000CB30
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T TrimEnd<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int index = (ref fs).TrimEndIndex<T>();
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr(), index);
			return result;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000E960 File Offset: 0x0000CB60
		public static UnsafeText TrimEnd(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = (ref fs).TrimEndIndex<UnsafeText>();
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr(), lengthInBytes);
			return result;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000E990 File Offset: 0x0000CB90
		public static NativeText TrimEnd(this NativeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = (ref fs).TrimEndIndex<NativeText>();
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr(), lengthInBytes);
			return result;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T TrimEnd<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ReadOnlySpan<Unicode.Rune> trimRunes) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int index = (ref fs).TrimEndIndex(trimRunes);
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr(), index);
			return result;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public static UnsafeText TrimEnd(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int lengthInBytes = (ref fs).TrimEndIndex(trimRunes);
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr(), lengthInBytes);
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public static NativeText TrimEnd(this NativeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int lengthInBytes = (ref fs).TrimEndIndex(trimRunes);
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr(), lengthInBytes);
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000EA54 File Offset: 0x0000CC54
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T Trim<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int start = (ref fs).TrimStartIndex<T>();
			if (start == fs.Length)
			{
				return new T();
			}
			int end = (ref fs).TrimEndIndex<T>();
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr() + start, end - start);
			return result;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public static UnsafeText Trim(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int start = (ref fs).TrimStartIndex<UnsafeText>();
			if (start == fs.Length)
			{
				return new UnsafeText(0, allocator);
			}
			int lengthInBytes = (ref fs).TrimEndIndex<UnsafeText>() - start;
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + start, lengthInBytes);
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public static NativeText Trim(this NativeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int start = (ref fs).TrimStartIndex<NativeText>();
			if (start == fs.Length)
			{
				return new NativeText(0, allocator);
			}
			int lengthInBytes = (ref fs).TrimEndIndex<NativeText>() - start;
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + start, lengthInBytes);
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static T Trim<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ReadOnlySpan<Unicode.Rune> trimRunes) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int start = (ref fs).TrimStartIndex(trimRunes);
			if (start == fs.Length)
			{
				return new T();
			}
			int end = (ref fs).TrimEndIndex(trimRunes);
			T result = new T();
			(ref result).Append(fs.GetUnsafePtr() + start, end - start);
			return result;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public static UnsafeText Trim(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int start = (ref fs).TrimStartIndex(trimRunes);
			if (start == fs.Length)
			{
				return new UnsafeText(0, allocator);
			}
			int lengthInBytes = (ref fs).TrimEndIndex<UnsafeText>() - start;
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + start, lengthInBytes);
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public static NativeText Trim(this NativeText fs, AllocatorManager.AllocatorHandle allocator, ReadOnlySpan<Unicode.Rune> trimRunes)
		{
			int start = (ref fs).TrimStartIndex(trimRunes);
			if (start == fs.Length)
			{
				return new NativeText(0, allocator);
			}
			int lengthInBytes = (ref fs).TrimEndIndex<NativeText>() - start;
			NativeText result = new NativeText(lengthInBytes, allocator);
			(ref result).Append(fs.GetUnsafePtr() + start, lengthInBytes);
			return result;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000EC28 File Offset: 0x0000CE28
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static T ToLowerAscii<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			T result = new T();
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToLowerAscii());
			}
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public unsafe static UnsafeText ToLowerAscii(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToLowerAscii());
			}
			return result;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public unsafe static NativeText ToLowerAscii(this NativeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			NativeText result = new NativeText(lengthInBytes, allocator);
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToLowerAscii());
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static T ToUpperAscii<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			T result = new T();
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToUpperAscii());
			}
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public unsafe static UnsafeText ToUpperAscii(this UnsafeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			UnsafeText result = new UnsafeText(lengthInBytes, allocator);
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToUpperAscii());
			}
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public unsafe static NativeText ToUpperAscii(this NativeText fs, AllocatorManager.AllocatorHandle allocator)
		{
			int lengthInBytes = fs.Length;
			byte* ptr = fs.GetUnsafePtr();
			NativeText result = new NativeText(lengthInBytes, allocator);
			ConversionError error = ConversionError.None;
			int i = 0;
			while (i < lengthInBytes && error == ConversionError.None)
			{
				Unicode.Rune rune;
				error = Unicode.Utf8ToUcs(out rune, ptr, ref i, lengthInBytes);
				(ref result).Append(rune.ToUpperAscii());
			}
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000EE30 File Offset: 0x0000D030
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool ParseLongInternal<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(ref T fs, ref int offset, out long value) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int resetOffset = offset;
			int sign = 1;
			if (offset < fs.Length)
			{
				if ((ref fs).Peek(offset).value == 43)
				{
					(ref fs).Read(ref offset);
				}
				else if ((ref fs).Peek(offset).value == 45)
				{
					sign = -1;
					(ref fs).Read(ref offset);
				}
			}
			int digitOffset = offset;
			value = 0L;
			while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
			{
				value *= 10L;
				value += (long)((ref fs).Read(ref offset).value - 48);
			}
			value = (long)sign * value;
			if (offset == digitOffset)
			{
				offset = resetOffset;
				return false;
			}
			return true;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int offset, ref int output) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			long value;
			if (!FixedStringMethods.ParseLongInternal<T>(ref fs, ref offset, out value))
			{
				return ParseError.Syntax;
			}
			if (value > 2147483647L)
			{
				return ParseError.Overflow;
			}
			if (value < -2147483648L)
			{
				return ParseError.Overflow;
			}
			output = (int)value;
			return ParseError.None;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000EF18 File Offset: 0x0000D118
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int offset, ref uint output) where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			long value;
			if (!FixedStringMethods.ParseLongInternal<T>(ref fs, ref offset, out value))
			{
				return ParseError.Syntax;
			}
			if (value > (long)((ulong)(-1)))
			{
				return ParseError.Overflow;
			}
			if (value < 0L)
			{
				return ParseError.Overflow;
			}
			output = (uint)value;
			return ParseError.None;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000EF48 File Offset: 0x0000D148
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this T fs, ref int offset, ref float output, char decimalSeparator = '.') where T : struct, ValueType, INativeList<byte>, IUTF8Bytes
		{
			int resetOffset = offset;
			int sign = 1;
			if (offset < fs.Length)
			{
				if ((ref fs).Peek(offset).value == 43)
				{
					(ref fs).Read(ref offset);
				}
				else if ((ref fs).Peek(offset).value == 45)
				{
					sign = -1;
					(ref fs).Read(ref offset);
				}
			}
			if ((ref fs).Found(ref offset, 'n', 'a', 'n'))
			{
				output = new FixedStringUtils.UintFloatUnion
				{
					uintValue = 4290772992U
				}.floatValue;
				return ParseError.None;
			}
			if ((ref fs).Found(ref offset, 'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'))
			{
				output = ((sign == 1) ? float.PositiveInfinity : float.NegativeInfinity);
				return ParseError.None;
			}
			ulong decimalMantissa = 0UL;
			int significantDigits = 0;
			int digitsAfterDot = 0;
			int mantissaDigits = 0;
			while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
			{
				mantissaDigits++;
				if (significantDigits < 9)
				{
					ulong num = decimalMantissa * 10UL + (ulong)((long)((ref fs).Peek(offset).value - 48));
					if (num > decimalMantissa)
					{
						significantDigits++;
					}
					decimalMantissa = num;
				}
				else
				{
					digitsAfterDot--;
				}
				(ref fs).Read(ref offset);
			}
			if (offset < fs.Length && (ref fs).Peek(offset).value == (int)decimalSeparator)
			{
				(ref fs).Read(ref offset);
				while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
				{
					mantissaDigits++;
					if (significantDigits < 9)
					{
						ulong num2 = decimalMantissa * 10UL + (ulong)((long)((ref fs).Peek(offset).value - 48));
						if (num2 > decimalMantissa)
						{
							significantDigits++;
						}
						decimalMantissa = num2;
						digitsAfterDot++;
					}
					(ref fs).Read(ref offset);
				}
			}
			if (mantissaDigits == 0)
			{
				offset = resetOffset;
				return ParseError.Syntax;
			}
			int decimalExponent = 0;
			int decimalExponentSign = 1;
			if (offset < fs.Length && ((ref fs).Peek(offset).value | 32) == 101)
			{
				(ref fs).Read(ref offset);
				if (offset < fs.Length)
				{
					if ((ref fs).Peek(offset).value == 43)
					{
						(ref fs).Read(ref offset);
					}
					else if ((ref fs).Peek(offset).value == 45)
					{
						decimalExponentSign = -1;
						(ref fs).Read(ref offset);
					}
				}
				int digitOffset = offset;
				while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
				{
					decimalExponent = decimalExponent * 10 + ((ref fs).Peek(offset).value - 48);
					(ref fs).Read(ref offset);
				}
				if (offset == digitOffset)
				{
					offset = resetOffset;
					return ParseError.Syntax;
				}
				if (decimalExponent > 38)
				{
					if (decimalExponentSign == 1)
					{
						return ParseError.Overflow;
					}
					return ParseError.Underflow;
				}
			}
			decimalExponent = decimalExponent * decimalExponentSign - digitsAfterDot;
			ParseError error = FixedStringUtils.Base10ToBase2(ref output, decimalMantissa, decimalExponent);
			if (error != ParseError.None)
			{
				return error;
			}
			output *= (float)sign;
			return ParseError.None;
		}
	}
}
