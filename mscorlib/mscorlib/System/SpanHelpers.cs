using System;
using System.Globalization;
using System.Numerics;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000143 RID: 323
	internal static class SpanHelpers
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00030978 File Offset: 0x0002EB78
		public static int IndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte b = value;
			ref byte ptr = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf(Unsafe.Add<byte>(ref searchSpace, num2), b, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num2 + 1), ref ptr, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public unsafe static int IndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00030A20 File Offset: 0x0002EC20
		public unsafe static int IndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					IL_014D:
					return (void*)intPtr;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					IL_0155:
					return (void*)(intPtr + 1);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					IL_0163:
					return (void*)(intPtr + 2);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					IL_0171:
					return (void*)(intPtr + 3);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
				{
					return (void*)(intPtr + 4);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
				{
					return (void*)(intPtr + 5);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
				{
					return (void*)(intPtr + 6);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_014D;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					goto IL_0155;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					goto IL_0163;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					goto IL_0171;
				}
				intPtr += 4;
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_014D;
				}
				intPtr += 1;
			}
			return -1;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		public unsafe static bool SequenceEqual(ref byte first, ref byte second, ulong length)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)length;
				if ((void*)intPtr2 >= sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += sizeof(UIntPtr);
					}
					return Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				while ((void*)intPtr2 != (void*)intPtr)
				{
					if (*Unsafe.AddByteOffset<byte>(ref first, intPtr) != *Unsafe.AddByteOffset<byte>(ref second, intPtr))
					{
						return false;
					}
					intPtr += 1;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		public unsafe static int SequenceCompareTo(ref char first, int firstLength, ref char second, int secondLength)
		{
			int num = firstLength - secondLength;
			if (!Unsafe.AreSame<char>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr2 = (IntPtr)0;
				if ((void*)intPtr >= sizeof(UIntPtr) / 2)
				{
					if (Vector.IsHardwareAccelerated && (void*)intPtr >= Vector<ushort>.Count)
					{
						IntPtr intPtr3 = intPtr - Vector<ushort>.Count;
						while (!(Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
						{
							intPtr2 += Vector<ushort>.Count;
							if ((void*)intPtr3 < (void*)intPtr2)
							{
								break;
							}
						}
					}
					while ((void*)intPtr >= (void*)(intPtr2 + sizeof(UIntPtr) / 2) && !(Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
					{
						intPtr2 += sizeof(UIntPtr) / 2;
					}
				}
				if (sizeof(UIntPtr) > 4 && (void*)intPtr >= (void*)(intPtr2 + 2) && Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) == Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2))))
				{
					intPtr2 += 2;
				}
				while ((void*)intPtr2 < (void*)intPtr)
				{
					int num2 = Unsafe.Add<char>(ref first, intPtr2).CompareTo(*Unsafe.Add<char>(ref second, intPtr2));
					if (num2 != 0)
					{
						return num2;
					}
					intPtr2 += 1;
				}
			}
			return num;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00030E34 File Offset: 0x0002F034
		public unsafe static int IndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + length;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					int num = (ptr3 & (Unsafe.SizeOf<Vector<ushort>>() - 1)) / 2;
					length = (Vector<ushort>.Count - num) & (Vector<ushort>.Count - 1);
				}
				Vector<ushort> vector2;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							if (*ptr3 == value)
							{
								goto IL_0127;
							}
							ptr3++;
						}
						if (!Vector.IsHardwareAccelerated || ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> vector = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							vector2 = Vector.Equals<ushort>(vector, Unsafe.Read<Vector<ushort>>((void*)ptr3));
							if (!Vector<ushort>.Zero.Equals(vector2))
							{
								goto IL_00F3;
							}
							ptr3 += Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3));
					}
					else
					{
						length -= 4;
						if (*ptr3 == value)
						{
							goto IL_0127;
						}
						if (ptr3[1] == value)
						{
							goto IL_0123;
						}
						if (ptr3[2] == value)
						{
							goto IL_011F;
						}
						if (ptr3[3] == value)
						{
							goto IL_011B;
						}
						ptr3 += 4;
					}
				}
				IL_00F3:
				return (int)((long)(ptr3 - ptr2)) + SpanHelpers.LocateFirstFoundChar(vector2);
				IL_011B:
				ptr3++;
				IL_011F:
				ptr3++;
				IL_0123:
				ptr3++;
				IL_0127:
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00030F70 File Offset: 0x0002F170
		public unsafe static int LastIndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + length;
				char* ptr4 = ptr2;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					length = (ptr3 & (Unsafe.SizeOf<Vector<ushort>>() - 1)) / 2;
				}
				char* ptr5;
				Vector<ushort> vector2;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							ptr3--;
							if (*ptr3 == value)
							{
								goto IL_011A;
							}
						}
						if (!Vector.IsHardwareAccelerated || ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> vector = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							ptr5 = ptr3 - Vector<ushort>.Count;
							vector2 = Vector.Equals<ushort>(vector, Unsafe.Read<Vector<ushort>>((void*)ptr5));
							if (!Vector<ushort>.Zero.Equals(vector2))
							{
								goto IL_00F1;
							}
							ptr3 -= Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4));
					}
					else
					{
						length -= 4;
						ptr3 -= 4;
						if (ptr3[3] == value)
						{
							goto IL_0136;
						}
						if (ptr3[2] == value)
						{
							goto IL_012C;
						}
						if (ptr3[1] == value)
						{
							goto IL_0122;
						}
						if (*ptr3 == value)
						{
							goto IL_011A;
						}
					}
				}
				IL_00F1:
				return (int)((long)(ptr5 - ptr4)) + SpanHelpers.LocateLastFoundChar(vector2);
				IL_011A:
				return (int)((long)(ptr3 - ptr4));
				IL_0122:
				return (int)((long)(ptr3 - ptr4)) + 1;
				IL_012C:
				return (int)((long)(ptr3 - ptr4)) + 2;
				IL_0136:
				return (int)((long)(ptr3 - ptr4)) + 3;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000310BC File Offset: 0x0002F2BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateFirstFoundChar(num);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000310F9 File Offset: 0x0002F2F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(ulong match)
		{
			return (int)((match ^ (match - 1UL)) * 4295098372UL >> 49);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00031110 File Offset: 0x0002F310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateLastFoundChar(num);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00031150 File Offset: 0x0002F350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(ulong match)
		{
			int num = 3;
			while (match > 0UL)
			{
				match <<= 16;
				num--;
			}
			return num;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00031174 File Offset: 0x0002F374
		public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T t = value;
			ref T ptr = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf<T>(Unsafe.Add<T>(ref searchSpace, num2), t, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num2 + 1), ref ptr, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000311E0 File Offset: 0x0002F3E0
		public unsafe static int IndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					IL_0202:
					return (void*)intPtr;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					IL_020A:
					return (void*)(intPtr + 1);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					IL_0218:
					return (void*)(intPtr + 2);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					IL_0226:
					return (void*)(intPtr + 3);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)))
				{
					return (void*)(intPtr + 4);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)))
				{
					return (void*)(intPtr + 5);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)))
				{
					return (void*)(intPtr + 6);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_0202;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					goto IL_020A;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					goto IL_0218;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					goto IL_0226;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_0202;
				}
				intPtr += 1;
				length--;
			}
			return -1;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00031458 File Offset: 0x0002F658
		public unsafe static int IndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0003149C File Offset: 0x0002F69C
		public unsafe static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : IEquatable<T>
		{
			if (!Unsafe.AreSame<T>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				while (length >= 8)
				{
					length -= 8;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)) || !Unsafe.Add<T>(ref first, intPtr + 4).Equals(*Unsafe.Add<T>(ref second, intPtr + 4)) || !Unsafe.Add<T>(ref first, intPtr + 5).Equals(*Unsafe.Add<T>(ref second, intPtr + 5)) || !Unsafe.Add<T>(ref first, intPtr + 6).Equals(*Unsafe.Add<T>(ref second, intPtr + 6)) || !Unsafe.Add<T>(ref first, intPtr + 7).Equals(*Unsafe.Add<T>(ref second, intPtr + 7)))
					{
						return false;
					}
					intPtr += 8;
				}
				if (length >= 4)
				{
					length -= 4;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)))
					{
						return false;
					}
					intPtr += 4;
				}
				while (length > 0)
				{
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)))
					{
						return false;
					}
					intPtr += 1;
					length--;
				}
			}
			return true;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00031735 File Offset: 0x0002F935
		public static bool EndsWithCultureHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return span.EndsWith(value);
			}
			return span.Length != 0 && compareInfo.IsSuffix(span, value, CompareOptions.None);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0003175A File Offset: 0x0002F95A
		public static bool EndsWithCultureIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return SpanHelpers.EndsWithOrdinalIgnoreCaseHelper(span, value);
			}
			return span.Length != 0 && compareInfo.IsSuffix(span, value, CompareOptions.IgnoreCase);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0003177F File Offset: 0x0002F97F
		public static bool EndsWithOrdinalIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length >= value.Length && CompareInfo.CompareOrdinalIgnoreCase(span.Slice(span.Length - value.Length), value) == 0;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000317B4 File Offset: 0x0002F9B4
		public unsafe static void ClearWithoutReferences(ref byte b, ulong byteLength)
		{
			if (byteLength == 0UL)
			{
				return;
			}
			ulong num = byteLength - 1UL;
			if (num <= 21UL)
			{
				switch ((uint)num)
				{
				case 0U:
					b = 0;
					return;
				case 1U:
					*Unsafe.As<byte, short>(ref b) = 0;
					return;
				case 2U:
					*Unsafe.As<byte, short>(ref b) = 0;
					*Unsafe.Add<byte>(ref b, 2) = 0;
					return;
				case 3U:
					*Unsafe.As<byte, int>(ref b) = 0;
					return;
				case 4U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.Add<byte>(ref b, 4) = 0;
					return;
				case 5U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 4)) = 0;
					return;
				case 6U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 4)) = 0;
					*Unsafe.Add<byte>(ref b, 6) = 0;
					return;
				case 7U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					return;
				case 8U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.Add<byte>(ref b, 8) = 0;
					return;
				case 9U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 8)) = 0;
					return;
				case 10U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.Add<byte>(ref b, 10) = 0;
					return;
				case 11U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					return;
				case 12U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.Add<byte>(ref b, 12) = 0;
					return;
				case 13U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 12)) = 0;
					return;
				case 14U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 12)) = 0;
					*Unsafe.Add<byte>(ref b, 14) = 0;
					return;
				case 15U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					return;
				case 16U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.Add<byte>(ref b, 16) = 0;
					return;
				case 17U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 16)) = 0;
					return;
				case 18U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.Add<byte>(ref b, 18) = 0;
					return;
				case 19U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					return;
				case 20U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.Add<byte>(ref b, 20) = 0;
					return;
				case 21U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 20)) = 0;
					return;
				}
			}
			if (byteLength < 512UL)
			{
				ulong num2 = 0UL;
				if ((*Unsafe.As<byte, int>(ref b) & 3) != 0)
				{
					if ((*Unsafe.As<byte, int>(ref b) & 1) != 0)
					{
						*Unsafe.AddByteOffset<byte>(ref b, num2) = 0;
						num2 += 1UL;
						if ((*Unsafe.As<byte, int>(ref b) & 2) != 0)
						{
							goto IL_0349;
						}
					}
					*Unsafe.As<byte, short>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 2UL;
				}
				IL_0349:
				if (((*Unsafe.As<byte, int>(ref b) - 1) & 4) == 0)
				{
					*Unsafe.As<byte, int>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 4UL;
				}
				ulong num3 = byteLength - 16UL;
				byteLength -= num2;
				ulong num4;
				do
				{
					num4 = num2 + 16UL;
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0L;
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2 + 8UL)) = 0L;
					num2 = num4;
				}
				while (num4 <= num3);
				if ((byteLength & 8UL) != 0UL)
				{
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0L;
					num2 += 8UL;
				}
				if ((byteLength & 4UL) != 0UL)
				{
					*Unsafe.As<byte, int>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 4UL;
				}
				if ((byteLength & 2UL) != 0UL)
				{
					*Unsafe.As<byte, short>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 2UL;
				}
				if ((byteLength & 1UL) != 0UL)
				{
					*Unsafe.AddByteOffset<byte>(ref b, num2) = 0;
				}
				return;
			}
			RuntimeImports.RhZeroMemory(ref b, byteLength);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		public unsafe static void ClearWithReferences(ref IntPtr ip, ulong pointerSizeLength)
		{
			while (pointerSizeLength >= 8UL)
			{
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -1) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -2) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -4) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -5) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -6) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -7) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -8) = 0;
				pointerSizeLength -= 8UL;
			}
			if (pointerSizeLength < 4UL)
			{
				if (pointerSizeLength < 2UL)
				{
					if (pointerSizeLength <= 0UL)
					{
						return;
					}
					goto IL_015B;
				}
			}
			else
			{
				*Unsafe.Add<IntPtr>(ref ip, 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, 3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -2) = 0;
			}
			*Unsafe.Add<IntPtr>(ref ip, 1) = 0;
			*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -1) = 0;
			IL_015B:
			ip = 0;
		}
	}
}
