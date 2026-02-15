using System;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000037 RID: 55
	[GenerateTestsForBurstCompatibility]
	internal struct Bitwise
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004187 File Offset: 0x00002387
		internal static int AlignDown(int value, int alignPow2)
		{
			return value & ~(alignPow2 - 1);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000418F File Offset: 0x0000238F
		internal static int AlignUp(int value, int alignPow2)
		{
			return Bitwise.AlignDown(value + alignPow2 - 1, alignPow2);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000419C File Offset: 0x0000239C
		internal static int FromBool(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000041A4 File Offset: 0x000023A4
		internal static uint ExtractBits(uint input, int pos, uint mask)
		{
			return (input >> pos) & mask;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000041B0 File Offset: 0x000023B0
		internal static uint ReplaceBits(uint input, int pos, uint mask, uint value)
		{
			uint num = (value & mask) << pos;
			uint tmp = input & ~(mask << pos);
			return num | tmp;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000041D1 File Offset: 0x000023D1
		internal static uint SetBits(uint input, int pos, uint mask, bool value)
		{
			return Bitwise.ReplaceBits(input, pos, mask, (uint)(-(uint)Bitwise.FromBool(value)));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000041E2 File Offset: 0x000023E2
		internal static ulong ExtractBits(ulong input, int pos, ulong mask)
		{
			return (input >> pos) & mask;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000041EC File Offset: 0x000023EC
		internal static ulong ReplaceBits(ulong input, int pos, ulong mask, ulong value)
		{
			ulong num = (value & mask) << pos;
			ulong tmp = input & ~(mask << pos);
			return num | tmp;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000420D File Offset: 0x0000240D
		internal static ulong SetBits(ulong input, int pos, ulong mask, bool value)
		{
			return Bitwise.ReplaceBits(input, pos, mask, (ulong)(-(ulong)((long)Bitwise.FromBool(value))));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000421F File Offset: 0x0000241F
		internal static int lzcnt(byte value)
		{
			return math.lzcnt((uint)value) - 24;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000422A File Offset: 0x0000242A
		internal static int tzcnt(byte value)
		{
			return math.min(8, math.tzcnt((uint)value));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004238 File Offset: 0x00002438
		internal static int lzcnt(ushort value)
		{
			return math.lzcnt((uint)value) - 16;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004243 File Offset: 0x00002443
		internal static int tzcnt(ushort value)
		{
			return math.min(16, math.tzcnt((uint)value));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004254 File Offset: 0x00002454
		private unsafe static int FindUlong(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num2 = numBits + 63 >> 6;
			int numBitsPerStep = 64;
			int i = beginBit / numBitsPerStep;
			int end = Bitwise.AlignUp(endBit, numBitsPerStep) / numBitsPerStep;
			while (i < end)
			{
				if (ptr[i] == 0UL)
				{
					int idx = i * numBitsPerStep;
					int num = math.min(idx + numBitsPerStep, endBit) - idx;
					if (idx != beginBit)
					{
						ulong test = ptr[idx / numBitsPerStep - 1];
						int newIdx = math.max(idx - math.lzcnt(test), beginBit);
						num += idx - newIdx;
						idx = newIdx;
					}
					for (i++; i < end; i++)
					{
						if (num >= numBits)
						{
							return idx;
						}
						ulong test2 = ptr[i];
						int pos = i * numBitsPerStep;
						num += math.min(pos + math.tzcnt(test2), endBit) - pos;
						if (test2 != 0UL)
						{
							break;
						}
					}
					if (num >= numBits)
					{
						return idx;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004328 File Offset: 0x00002528
		private unsafe static int FindUint(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num2 = numBits + 31 >> 5;
			int numBitsPerStep = 32;
			int i = beginBit / numBitsPerStep;
			int end = Bitwise.AlignUp(endBit, numBitsPerStep) / numBitsPerStep;
			while (i < end)
			{
				if (*(uint*)(ptr + (IntPtr)i * 4 / 8) == 0U)
				{
					int idx = i * numBitsPerStep;
					int num = math.min(idx + numBitsPerStep, endBit) - idx;
					if (idx != beginBit)
					{
						uint test = *(uint*)(ptr + (IntPtr)(idx / numBitsPerStep - 1) * 4 / 8);
						int newIdx = math.max(idx - math.lzcnt(test), beginBit);
						num += idx - newIdx;
						idx = newIdx;
					}
					for (i++; i < end; i++)
					{
						if (num >= numBits)
						{
							return idx;
						}
						uint test2 = *(uint*)(ptr + (IntPtr)i * 4 / 8);
						int pos = i * numBitsPerStep;
						num += math.min(pos + math.tzcnt(test2), endBit) - pos;
						if (test2 != 0U)
						{
							break;
						}
					}
					if (num >= numBits)
					{
						return idx;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000043FC File Offset: 0x000025FC
		private unsafe static int FindUshort(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num2 = numBits + 15 >> 4;
			int numBitsPerStep = 16;
			int i = beginBit / numBitsPerStep;
			int end = Bitwise.AlignUp(endBit, numBitsPerStep) / numBitsPerStep;
			while (i < end)
			{
				if (*(ushort*)(ptr + (IntPtr)i * 2 / 8) == 0)
				{
					int idx = i * numBitsPerStep;
					int num = math.min(idx + numBitsPerStep, endBit) - idx;
					if (idx != beginBit)
					{
						ushort test = *(ushort*)(ptr + (IntPtr)(idx / numBitsPerStep - 1) * 2 / 8);
						int newIdx = math.max(idx - Bitwise.lzcnt(test), beginBit);
						num += idx - newIdx;
						idx = newIdx;
					}
					for (i++; i < end; i++)
					{
						if (num >= numBits)
						{
							return idx;
						}
						ushort test2 = *(ushort*)(ptr + (IntPtr)i * 2 / 8);
						int pos = i * numBitsPerStep;
						num += math.min(pos + Bitwise.tzcnt(test2), endBit) - pos;
						if (test2 != 0)
						{
							break;
						}
					}
					if (num >= numBits)
					{
						return idx;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000044D0 File Offset: 0x000026D0
		private unsafe static int FindByte(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num2 = numBits + 7 >> 3;
			int numBitsPerStep = 8;
			int i = beginBit / numBitsPerStep;
			int end = Bitwise.AlignUp(endBit, numBitsPerStep) / numBitsPerStep;
			while (i < end)
			{
				if (*(byte*)(ptr + i / 8) == 0)
				{
					int idx = i * numBitsPerStep;
					int num = math.min(idx + numBitsPerStep, endBit) - idx;
					if (idx != beginBit)
					{
						byte test = *(byte*)(ptr + (idx / numBitsPerStep - 1) / 8);
						int newIdx = math.max(idx - Bitwise.lzcnt(test), beginBit);
						num += idx - newIdx;
						idx = newIdx;
					}
					for (i++; i < end; i++)
					{
						if (num >= numBits)
						{
							return idx;
						}
						byte test2 = *(byte*)(ptr + i / 8);
						int pos = i * numBitsPerStep;
						num += math.min(pos + Bitwise.tzcnt(test2), endBit) - pos;
						if (test2 != 0)
						{
							break;
						}
					}
					if (num >= numBits)
					{
						return idx;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004598 File Offset: 0x00002798
		private unsafe static int FindUpto14bits(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			byte bit = (byte)(beginBit & 7);
			byte beginMask = (byte)(~(byte)(255 << (int)bit));
			int lz = 0;
			int begin = beginBit / 8;
			int end = Bitwise.AlignUp(endBit, 8) / 8;
			for (int i = begin; i < end; i++)
			{
				byte test = *(byte*)(ptr + i / 8);
				test |= ((i == begin) ? beginMask : 0);
				if (test != 255)
				{
					int pos = i * 8;
					int tz = math.min(pos + Bitwise.tzcnt(test), endBit) - pos;
					if (lz + tz >= numBits)
					{
						return pos - lz;
					}
					lz = Bitwise.lzcnt(test);
					int num = pos + 8;
					int newIdx = math.max(num - lz, beginBit);
					lz = math.min(num, endBit) - newIdx;
					if (lz >= numBits)
					{
						return newIdx;
					}
				}
			}
			return endBit;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004650 File Offset: 0x00002850
		private unsafe static int FindUpto6bits(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			byte beginMask = (byte)(~(byte)(255 << (beginBit & 7)));
			byte endMask = (byte)(~(byte)(255 >> ((8 - (endBit & 7)) & 7)));
			int mask = 1 << numBits - 1;
			int begin = beginBit / 8;
			int end = Bitwise.AlignUp(endBit, 8) / 8;
			for (int i = begin; i < end; i++)
			{
				byte test = *(byte*)(ptr + i / 8);
				test |= ((i == begin) ? beginMask : 0);
				test |= ((i == end - 1) ? endMask : 0);
				if (test != 255)
				{
					int pos = i * 8;
					int posEnd = pos + 7;
					while (pos < posEnd)
					{
						int tz = Bitwise.tzcnt(test ^ byte.MaxValue);
						test = (byte)(test >> tz);
						pos += tz;
						if (((int)test & mask) == 0)
						{
							return pos;
						}
						test = (byte)(test >> 1);
						pos++;
					}
				}
			}
			return endBit;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004734 File Offset: 0x00002934
		internal unsafe static int FindWithBeginEnd(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int idx;
			if (numBits >= 127)
			{
				idx = Bitwise.FindUlong(ptr, beginBit, endBit, numBits);
				if (idx != endBit)
				{
					return idx;
				}
			}
			if (numBits >= 63)
			{
				idx = Bitwise.FindUint(ptr, beginBit, endBit, numBits);
				if (idx != endBit)
				{
					return idx;
				}
			}
			if (numBits >= 128)
			{
				return int.MaxValue;
			}
			if (numBits >= 31)
			{
				idx = Bitwise.FindUshort(ptr, beginBit, endBit, numBits);
				if (idx != endBit)
				{
					return idx;
				}
			}
			if (numBits >= 64)
			{
				return int.MaxValue;
			}
			idx = Bitwise.FindByte(ptr, beginBit, endBit, numBits);
			if (idx != endBit)
			{
				return idx;
			}
			if (numBits < 15)
			{
				idx = Bitwise.FindUpto14bits(ptr, beginBit, endBit, numBits);
				if (idx != endBit)
				{
					return idx;
				}
				if (numBits < 7)
				{
					idx = Bitwise.FindUpto6bits(ptr, beginBit, endBit, numBits);
					if (idx != endBit)
					{
						return idx;
					}
				}
			}
			return int.MaxValue;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000047D7 File Offset: 0x000029D7
		internal unsafe static int Find(ulong* ptr, int pos, int count, int numBits)
		{
			return Bitwise.FindWithBeginEnd(ptr, pos, pos + count, numBits);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000047E4 File Offset: 0x000029E4
		internal unsafe static bool TestNone(ulong* ptr, int length, int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, length);
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue << shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			if (idxB == idxE)
			{
				ulong mask = maskB & maskE;
				return (ptr[idxB] & mask) == 0UL;
			}
			if ((ptr[idxB] & maskB) != 0UL)
			{
				return false;
			}
			for (int idx = idxB + 1; idx < idxE; idx++)
			{
				if (ptr[idx] != 0UL)
				{
					return false;
				}
			}
			return (ptr[idxE] & maskE) == 0UL;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004878 File Offset: 0x00002A78
		internal unsafe static bool TestAny(ulong* ptr, int length, int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, length);
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue << shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			if (idxB == idxE)
			{
				ulong mask = maskB & maskE;
				return (ptr[idxB] & mask) > 0UL;
			}
			if ((ptr[idxB] & maskB) != 0UL)
			{
				return true;
			}
			for (int idx = idxB + 1; idx < idxE; idx++)
			{
				if (ptr[idx] != 0UL)
				{
					return true;
				}
			}
			return (ptr[idxE] & maskE) > 0UL;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000490C File Offset: 0x00002B0C
		internal unsafe static bool TestAll(ulong* ptr, int length, int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, length);
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue << shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			if (idxB == idxE)
			{
				ulong mask = maskB & maskE;
				return mask == (ptr[idxB] & mask);
			}
			if (maskB != (ptr[idxB] & maskB))
			{
				return false;
			}
			for (int idx = idxB + 1; idx < idxE; idx++)
			{
				if (18446744073709551615UL != ptr[idx])
				{
					return false;
				}
			}
			return maskE == (ptr[idxE] & maskE);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000049A4 File Offset: 0x00002BA4
		internal unsafe static int CountBits(ulong* ptr, int length, int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, length);
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue << shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			if (idxB == idxE)
			{
				ulong mask = maskB & maskE;
				return math.countbits(ptr[idxB] & mask);
			}
			int count = math.countbits(ptr[idxB] & maskB);
			for (int idx = idxB + 1; idx < idxE; idx++)
			{
				count += math.countbits(ptr[idx]);
			}
			return count + math.countbits(ptr[idxE] & maskE);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004A48 File Offset: 0x00002C48
		internal unsafe static bool IsSet(ulong* ptr, int pos)
		{
			int idx = pos >> 6;
			int shift = pos & 63;
			ulong mask = 1UL << shift;
			return (ptr[idx] & mask) > 0UL;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004A74 File Offset: 0x00002C74
		internal unsafe static ulong GetBits(ulong* ptr, int length, int pos, int numBits = 1)
		{
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			if (shiftB + numBits <= 64)
			{
				ulong mask = ulong.MaxValue >> 64 - numBits;
				return Bitwise.ExtractBits(ptr[idxB], shiftB, mask);
			}
			int num = math.min(pos + numBits, length);
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue >> shiftB;
			ulong valueB = Bitwise.ExtractBits(ptr[idxB], shiftB, maskB);
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			return (Bitwise.ExtractBits(ptr[idxE], 0, maskE) << 64 - shiftB) | valueB;
		}
	}
}
