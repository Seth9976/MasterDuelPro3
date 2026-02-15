using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x02000138 RID: 312
	internal static class SpookyHash
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x00018CDC File Offset: 0x00016EDC
		private static bool AttemptDetectAllowUnalignedRead()
		{
			string processorType = SystemInfo.processorType;
			string text = processorType;
			return text == "x86" || text == "AMD64";
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00018D18 File Offset: 0x00016F18
		public unsafe static void Hash(void* message, ulong length, ulong* hash1, ulong* hash2)
		{
			bool flag = length < 192UL;
			if (flag)
			{
				SpookyHash.Short(message, length, hash1, hash2);
			}
			else
			{
				ulong* buf = stackalloc ulong[(UIntPtr)96];
				ulong h6;
				ulong h5;
				ulong h4;
				ulong h3 = (h4 = (h5 = (h6 = *hash1)));
				ulong h10;
				ulong h9;
				ulong h8;
				ulong h7 = (h8 = (h9 = (h10 = *hash2)));
				ulong h14;
				ulong h13;
				ulong h12;
				ulong h11 = (h12 = (h13 = (h14 = 16045690984833335023UL)));
				SpookyHash.U u = new SpookyHash.U((ushort*)message);
				ulong* end = u.p64 + length / 96UL * 12UL * 8UL / 8UL;
				bool flag2 = SpookyHash.AllowUnalignedRead || (u.i & 7UL) == 0UL;
				if (flag2)
				{
					while (u.p64 < end)
					{
						SpookyHash.Mix(u.p64, ref h4, ref h8, ref h12, ref h3, ref h7, ref h11, ref h5, ref h9, ref h13, ref h6, ref h10, ref h14);
						u.p64 += (IntPtr)12 * 8;
					}
				}
				else
				{
					while (u.p64 < end)
					{
						UnsafeUtility.MemCpy((void*)buf, (void*)u.p64, 96L);
						SpookyHash.Mix(buf, ref h4, ref h8, ref h12, ref h3, ref h7, ref h11, ref h5, ref h9, ref h13, ref h6, ref h10, ref h14);
						u.p64 += (IntPtr)12 * 8;
					}
				}
				ulong remainder = length - (ulong)((long)((byte*)end - (byte*)message));
				UnsafeUtility.MemCpy((void*)buf, (void*)end, (long)remainder);
				SpookyHash.memset((void*)(buf + remainder / 8UL), 0, 96UL - remainder);
				((byte*)buf)[95] = (byte)remainder;
				SpookyHash.End(buf, ref h4, ref h8, ref h12, ref h3, ref h7, ref h11, ref h5, ref h9, ref h13, ref h6, ref h10, ref h14);
				*hash1 = h4;
				*hash2 = h8;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00018EB4 File Offset: 0x000170B4
		private unsafe static void End(ulong* data, ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3, ref ulong h4, ref ulong h5, ref ulong h6, ref ulong h7, ref ulong h8, ref ulong h9, ref ulong h10, ref ulong h11)
		{
			h0 += *data;
			h1 += data[1];
			h2 += data[2];
			h3 += data[3];
			h4 += data[4];
			h5 += data[5];
			h6 += data[6];
			h7 += data[7];
			h8 += data[8];
			h9 += data[9];
			h10 += data[10];
			h11 += data[11];
			SpookyHash.EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
			SpookyHash.EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
			SpookyHash.EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00018FB0 File Offset: 0x000171B0
		private static void EndPartial(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3, ref ulong h4, ref ulong h5, ref ulong h6, ref ulong h7, ref ulong h8, ref ulong h9, ref ulong h10, ref ulong h11)
		{
			h11 += h1;
			h2 ^= h11;
			SpookyHash.Rot64(ref h1, 44);
			h0 += h2;
			h3 ^= h0;
			SpookyHash.Rot64(ref h2, 15);
			h1 += h3;
			h4 ^= h1;
			SpookyHash.Rot64(ref h3, 34);
			h2 += h4;
			h5 ^= h2;
			SpookyHash.Rot64(ref h4, 21);
			h3 += h5;
			h6 ^= h3;
			SpookyHash.Rot64(ref h5, 38);
			h4 += h6;
			h7 ^= h4;
			SpookyHash.Rot64(ref h6, 33);
			h5 += h7;
			h8 ^= h5;
			SpookyHash.Rot64(ref h7, 10);
			h6 += h8;
			h9 ^= h6;
			SpookyHash.Rot64(ref h8, 13);
			h7 += h9;
			h10 ^= h7;
			SpookyHash.Rot64(ref h9, 38);
			h8 += h10;
			h11 ^= h8;
			SpookyHash.Rot64(ref h10, 53);
			h9 += h11;
			h0 ^= h9;
			SpookyHash.Rot64(ref h11, 42);
			h10 += h0;
			h1 ^= h10;
			SpookyHash.Rot64(ref h0, 54);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00018C4B File Offset: 0x00016E4B
		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k) | (x >> 64 - k);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0001910C File Offset: 0x0001730C
		private unsafe static void Short(void* message, ulong length, ulong* hash1, ulong* hash2)
		{
			ulong* buf = stackalloc ulong[(UIntPtr)192];
			SpookyHash.U u = new SpookyHash.U((ushort*)message);
			bool flag = !SpookyHash.AllowUnalignedRead && (u.i & 7UL) > 0UL;
			if (flag)
			{
				UnsafeUtility.MemCpy((void*)buf, message, (long)length);
				u.p64 = buf;
			}
			ulong remainder = length % 32UL;
			ulong a = *hash1;
			ulong b = *hash2;
			ulong c = 16045690984833335023UL;
			ulong d = 16045690984833335023UL;
			bool flag2 = length > 15UL;
			if (flag2)
			{
				ulong* end = u.p64 + length / 32UL * 4UL * 8UL / 8UL;
				while (u.p64 < end)
				{
					c += *u.p64;
					d += u.p64[1];
					SpookyHash.ShortMix(ref a, ref b, ref c, ref d);
					a += u.p64[2];
					b += u.p64[3];
					u.p64 += (IntPtr)4 * 8;
				}
				bool flag3 = remainder >= 16UL;
				if (flag3)
				{
					c += *u.p64;
					d += u.p64[1];
					SpookyHash.ShortMix(ref a, ref b, ref c, ref d);
					u.p64 += (IntPtr)2 * 8;
					remainder -= 16UL;
				}
			}
			d += length << 56;
			ulong num = remainder;
			ulong num2 = num;
			ulong num3 = num2;
			if (num3 <= 15UL)
			{
				switch ((uint)num3)
				{
				case 0U:
					c += 16045690984833335023UL;
					d += 16045690984833335023UL;
					goto IL_02F9;
				case 1U:
					goto IL_02CC;
				case 2U:
					goto IL_02B9;
				case 3U:
					c += (ulong)u.p8[2] << 16;
					goto IL_02B9;
				case 4U:
					goto IL_0296;
				case 5U:
					goto IL_0282;
				case 6U:
					goto IL_026E;
				case 7U:
					c += (ulong)u.p8[6] << 48;
					goto IL_026E;
				case 8U:
					goto IL_0249;
				case 9U:
					goto IL_0238;
				case 10U:
					goto IL_0224;
				case 11U:
					d += (ulong)u.p8[10] << 16;
					goto IL_0224;
				case 12U:
					goto IL_01EC;
				case 13U:
					goto IL_01D7;
				case 14U:
					break;
				case 15U:
					d += (ulong)u.p8[14] << 48;
					break;
				default:
					goto IL_02F9;
				}
				d += (ulong)u.p8[13] << 40;
				IL_01D7:
				d += (ulong)u.p8[12] << 32;
				IL_01EC:
				d += (ulong)u.p32[2];
				c += *u.p64;
				goto IL_02F9;
				IL_0224:
				d += (ulong)u.p8[9] << 8;
				IL_0238:
				d += (ulong)u.p8[8];
				IL_0249:
				c += *u.p64;
				goto IL_02F9;
				IL_026E:
				c += (ulong)u.p8[5] << 40;
				IL_0282:
				c += (ulong)u.p8[4] << 32;
				IL_0296:
				c += (ulong)(*u.p32);
				goto IL_02F9;
				IL_02B9:
				c += (ulong)u.p8[1] << 8;
				IL_02CC:
				c += (ulong)(*u.p8);
			}
			IL_02F9:
			SpookyHash.ShortEnd(ref a, ref b, ref c, ref d);
			*hash1 = a;
			*hash2 = b;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00019428 File Offset: 0x00017628
		private static void ShortMix(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			SpookyHash.Rot64(ref h2, 50);
			h2 += h3;
			h0 ^= h2;
			SpookyHash.Rot64(ref h3, 52);
			h3 += h0;
			h1 ^= h3;
			SpookyHash.Rot64(ref h0, 30);
			h0 += h1;
			h2 ^= h0;
			SpookyHash.Rot64(ref h1, 41);
			h1 += h2;
			h3 ^= h1;
			SpookyHash.Rot64(ref h2, 54);
			h2 += h3;
			h0 ^= h2;
			SpookyHash.Rot64(ref h3, 48);
			h3 += h0;
			h1 ^= h3;
			SpookyHash.Rot64(ref h0, 38);
			h0 += h1;
			h2 ^= h0;
			SpookyHash.Rot64(ref h1, 37);
			h1 += h2;
			h3 ^= h1;
			SpookyHash.Rot64(ref h2, 62);
			h2 += h3;
			h0 ^= h2;
			SpookyHash.Rot64(ref h3, 34);
			h3 += h0;
			h1 ^= h3;
			SpookyHash.Rot64(ref h0, 5);
			h0 += h1;
			h2 ^= h0;
			SpookyHash.Rot64(ref h1, 36);
			h1 += h2;
			h3 ^= h1;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0001954C File Offset: 0x0001774C
		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			SpookyHash.Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			SpookyHash.Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			SpookyHash.Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			SpookyHash.Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			SpookyHash.Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			SpookyHash.Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			SpookyHash.Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			SpookyHash.Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			SpookyHash.Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			SpookyHash.Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			SpookyHash.Rot64(ref h0, 63);
			h1 += h0;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00019658 File Offset: 0x00017858
		private unsafe static void Mix(ulong* data, ref ulong s0, ref ulong s1, ref ulong s2, ref ulong s3, ref ulong s4, ref ulong s5, ref ulong s6, ref ulong s7, ref ulong s8, ref ulong s9, ref ulong s10, ref ulong s11)
		{
			s0 += *data;
			s2 ^= s10;
			s11 ^= s0;
			SpookyHash.Rot64(ref s0, 11);
			s11 += s1;
			s1 += data[1];
			s3 ^= s11;
			s0 ^= s1;
			SpookyHash.Rot64(ref s1, 32);
			s0 += s2;
			s2 += data[2];
			s4 ^= s0;
			s1 ^= s2;
			SpookyHash.Rot64(ref s2, 43);
			s1 += s3;
			s3 += data[3];
			s5 ^= s1;
			s2 ^= s3;
			SpookyHash.Rot64(ref s3, 31);
			s2 += s4;
			s4 += data[4];
			s6 ^= s2;
			s3 ^= s4;
			SpookyHash.Rot64(ref s4, 17);
			s3 += s5;
			s5 += data[5];
			s7 ^= s3;
			s4 ^= s5;
			SpookyHash.Rot64(ref s5, 28);
			s4 += s6;
			s6 += data[6];
			s8 ^= s4;
			s5 ^= s6;
			SpookyHash.Rot64(ref s6, 39);
			s5 += s7;
			s7 += data[7];
			s9 ^= s5;
			s6 ^= s7;
			SpookyHash.Rot64(ref s7, 57);
			s6 += s8;
			s8 += data[8];
			s10 ^= s6;
			s7 ^= s8;
			SpookyHash.Rot64(ref s8, 55);
			s7 += s9;
			s9 += data[9];
			s11 ^= s7;
			s8 ^= s9;
			SpookyHash.Rot64(ref s9, 54);
			s8 += s10;
			s10 += data[10];
			s0 ^= s8;
			s9 ^= s10;
			SpookyHash.Rot64(ref s10, 22);
			s9 += s11;
			s11 += data[11];
			s1 ^= s9;
			s10 ^= s11;
			SpookyHash.Rot64(ref s11, 46);
			s10 += s0;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000198C8 File Offset: 0x00017AC8
		private unsafe static void memset(void* dst, int value, ulong numberOfBytes)
		{
			ulong v = (ulong)(value | value);
			ulong* dst2 = (ulong*)dst;
			ulong count = numberOfBytes >> 3;
			for (ulong i = 0UL; i < count; i += 1UL)
			{
				dst2[i * 8UL / 8UL] = v;
			}
			dst = (void*)dst2;
			numberOfBytes -= count;
			byte* v2 = stackalloc byte[(UIntPtr)4];
			*v2 = (byte)(value & 15);
			v2[1] = (byte)(((uint)value >> 4) & 15U);
			v2[2] = (byte)(((uint)value >> 8) & 15U);
			v2[3] = (byte)(((uint)value >> 12) & 15U);
			byte* dst3 = (byte*)dst;
			ulong count2 = numberOfBytes;
			for (ulong j = 0UL; j < count2; j += 1UL)
			{
				dst3[j] = v2[j % 4UL];
			}
		}

		// Token: 0x04000413 RID: 1043
		private static readonly bool AllowUnalignedRead = SpookyHash.AttemptDetectAllowUnalignedRead();

		// Token: 0x02000139 RID: 313
		[StructLayout(LayoutKind.Explicit)]
		private struct U
		{
			// Token: 0x06000D33 RID: 3379 RVA: 0x00019979 File Offset: 0x00017B79
			public unsafe U(ushort* p8)
			{
				this.p32 = null;
				this.p64 = null;
				this.i = 0UL;
				this.p8 = (byte*)p8;
			}

			// Token: 0x04000414 RID: 1044
			[FieldOffset(0)]
			public unsafe byte* p8;

			// Token: 0x04000415 RID: 1045
			[FieldOffset(0)]
			public unsafe uint* p32;

			// Token: 0x04000416 RID: 1046
			[FieldOffset(0)]
			public unsafe ulong* p64;

			// Token: 0x04000417 RID: 1047
			[FieldOffset(0)]
			public ulong i;
		}
	}
}
