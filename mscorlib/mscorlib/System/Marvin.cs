using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000119 RID: 281
	internal static class Marvin
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00028435 File Offset: 0x00026635
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ComputeHash32(ReadOnlySpan<byte> data, ulong seed)
		{
			return Marvin.ComputeHash32(MemoryMarshal.GetReference<byte>(data), data.Length, seed);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002844C File Offset: 0x0002664C
		public unsafe static int ComputeHash32(ref byte data, int count, ulong seed)
		{
			ulong num = (ulong)((long)count);
			uint num2 = (uint)seed;
			uint num3 = (uint)(seed >> 32);
			ulong num4 = 0UL;
			while (num >= 8UL)
			{
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
				Marvin.Block(ref num2, ref num3);
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4 + 4UL));
				Marvin.Block(ref num2, ref num3);
				num4 += 8UL;
				num -= 8UL;
			}
			ulong num5 = num;
			if (num5 <= 7UL)
			{
				switch ((uint)num5)
				{
				case 0U:
					break;
				case 1U:
					goto IL_00CC;
				case 2U:
					goto IL_00FC;
				case 3U:
					goto IL_0130;
				case 4U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					Marvin.Block(ref num2, ref num3);
					break;
				case 5U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_00CC;
				case 6U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_00FC;
				case 7U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_0130;
				default:
					goto IL_0154;
				}
				num2 += 128U;
				goto IL_0154;
				IL_00CC:
				num2 += 32768U | (uint)(*Unsafe.AddByteOffset<byte>(ref data, num4));
				goto IL_0154;
				IL_00FC:
				num2 += 8388608U | (uint)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4));
				goto IL_0154;
				IL_0130:
				num2 += (uint)(int.MinValue | ((int)(*Unsafe.AddByteOffset<byte>(ref data, num4 + 2UL)) << 16) | (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4)));
			}
			IL_0154:
			Marvin.Block(ref num2, ref num3);
			Marvin.Block(ref num2, ref num3);
			return (int)(num3 ^ num2);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000285C4 File Offset: 0x000267C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Block(ref uint rp0, ref uint rp1)
		{
			uint num = rp0;
			uint num2 = rp1;
			num2 ^= num;
			num = Marvin._rotl(num, 20);
			num += num2;
			num2 = Marvin._rotl(num2, 9);
			num2 ^= num;
			num = Marvin._rotl(num, 27);
			num += num2;
			num2 = Marvin._rotl(num2, 19);
			rp0 = num;
			rp1 = num2;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002757B File Offset: 0x0002577B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _rotl(uint value, int shift)
		{
			return (value << shift) | (value >> 32 - shift);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00028611 File Offset: 0x00026811
		public static ulong DefaultSeed { get; } = Marvin.GenerateSeed();

		// Token: 0x06000931 RID: 2353 RVA: 0x00028618 File Offset: 0x00026818
		private static ulong GenerateSeed()
		{
			return 12874512UL;
		}
	}
}
