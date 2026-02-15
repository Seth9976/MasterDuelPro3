using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000246 RID: 582
	internal static class MemoryHelpers
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0006126D File Offset: 0x0005F46D
		public unsafe static bool Compare(void* ptr1, void* ptr2, MemoryHelpers.BitRegion region)
		{
			if (region.sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit(ptr1, region.bitOffset) == MemoryHelpers.ReadSingleBit(ptr2, region.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion(ptr1, ptr2, region.bitOffset, region.sizeInBits, null);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x000612A8 File Offset: 0x0005F4A8
		public static uint ComputeFollowingByteOffset(uint byteOffset, uint sizeInBits)
		{
			return (uint)((ulong)(byteOffset + sizeInBits / 8U) + (ulong)((sizeInBits % 8U > 0U) ? 1L : 0L));
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000612C0 File Offset: 0x0005F4C0
		public unsafe static void WriteSingleBit(void* ptr, uint bitOffset, bool value)
		{
			uint byteOffset = bitOffset >> 3;
			bitOffset &= 7U;
			if (value)
			{
				byte* ptr2 = (byte*)ptr + byteOffset;
				*ptr2 |= (byte)(1 << (int)bitOffset);
				return;
			}
			byte* ptr3 = (byte*)ptr + byteOffset;
			*ptr3 &= (byte)(~(byte)(1 << (int)bitOffset));
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000612FC File Offset: 0x0005F4FC
		public unsafe static bool ReadSingleBit(void* ptr, uint bitOffset)
		{
			uint byteOffset = bitOffset >> 3;
			bitOffset &= 7U;
			return ((int)((byte*)ptr)[byteOffset] & (1 << (int)bitOffset)) != 0;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00061324 File Offset: 0x0005F524
		public unsafe static void MemCpyBitRegion(void* destination, void* source, uint bitOffset, uint bitCount)
		{
			byte* destPtr = (byte*)destination;
			byte* sourcePtr = (byte*)source;
			if (bitOffset >= 8U)
			{
				uint skipBytes = bitOffset / 8U;
				destPtr += skipBytes;
				sourcePtr += skipBytes;
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int byteMask = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					byteMask &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				*destPtr = (byte)((((int)(*destPtr) & ~byteMask) | ((int)(*sourcePtr) & byteMask)) & 255);
				if (bitCount + bitOffset <= 8U)
				{
					return;
				}
				destPtr++;
				sourcePtr++;
				bitCount -= 8U - bitOffset;
			}
			uint byteCount = bitCount / 8U;
			if (byteCount >= 1U)
			{
				UnsafeUtility.MemCpy((void*)destPtr, (void*)sourcePtr, (long)((ulong)byteCount));
			}
			uint remainingBitCount = bitCount % 8U;
			if (remainingBitCount > 0U)
			{
				destPtr += byteCount;
				sourcePtr += byteCount;
				int byteMask2 = 255 >> (int)(8U - remainingBitCount);
				*destPtr = (byte)((((int)(*destPtr) & ~byteMask2) | ((int)(*sourcePtr) & byteMask2)) & 255);
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000613EC File Offset: 0x0005F5EC
		public unsafe static bool MemCmpBitRegion(void* ptr1, void* ptr2, uint bitOffset, uint bitCount, void* mask = null)
		{
			byte* bytePtr = (byte*)ptr1;
			byte* bytePtr2 = (byte*)ptr2;
			byte* maskPtr = (byte*)mask;
			if (bitOffset >= 8U)
			{
				uint skipBytes = bitOffset / 8U;
				bytePtr += skipBytes;
				bytePtr2 += skipBytes;
				if (maskPtr != null)
				{
					maskPtr += skipBytes;
				}
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int byteMask = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					byteMask &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				if (maskPtr != null)
				{
					byteMask &= (int)(*maskPtr);
					maskPtr++;
				}
				int num = (int)(*bytePtr) & byteMask;
				int byte2 = (int)(*bytePtr2) & byteMask;
				if (num != byte2)
				{
					return false;
				}
				if (bitCount + bitOffset <= 8U)
				{
					return true;
				}
				bytePtr++;
				bytePtr2++;
				bitCount -= 8U - bitOffset;
			}
			uint byteCount = bitCount / 8U;
			if (byteCount >= 1U)
			{
				if (maskPtr != null)
				{
					int i = 0;
					while ((long)i < (long)((ulong)byteCount))
					{
						byte b = bytePtr[i];
						byte byte3 = bytePtr2[i];
						byte byteMask2 = maskPtr[i];
						if ((b & byteMask2) != (byte3 & byteMask2))
						{
							return false;
						}
						i++;
					}
				}
				else if (UnsafeUtility.MemCmp((void*)bytePtr, (void*)bytePtr2, (long)((ulong)byteCount)) != 0)
				{
					return false;
				}
			}
			uint remainingBitCount = bitCount % 8U;
			if (remainingBitCount > 0U)
			{
				bytePtr += byteCount;
				bytePtr2 += byteCount;
				int byteMask3 = 255 >> (int)(8U - remainingBitCount);
				if (maskPtr != null)
				{
					maskPtr += byteCount;
					byteMask3 &= (int)(*maskPtr);
				}
				int num2 = (int)(*bytePtr) & byteMask3;
				int byte4 = (int)(*bytePtr2) & byteMask3;
				if (num2 != byte4)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0006151C File Offset: 0x0005F71C
		public unsafe static void MemSet(void* destination, int numBytes, byte value)
		{
			int pos = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + pos) = (long)(((ulong)value << 56) | ((ulong)value << 48) | ((ulong)value << 40) | ((ulong)value << 32) | ((ulong)value << 24) | ((ulong)value << 16) | ((ulong)value << 8) | (ulong)value);
				numBytes -= 8;
				pos += 8;
			}
			while (numBytes >= 4)
			{
				*(int*)((byte*)destination + pos) = ((int)value << 24) | ((int)value << 16) | ((int)value << 8) | (int)value;
				numBytes -= 4;
				pos += 4;
			}
			while (numBytes > 0)
			{
				((byte*)destination)[pos] = value;
				numBytes--;
				pos++;
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000615A4 File Offset: 0x0005F7A4
		public unsafe static void MemCpyMasked(void* destination, void* source, int numBytes, void* mask)
		{
			int pos = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + pos) &= ~(*(long*)((byte*)mask + pos));
				*(long*)((byte*)destination + pos) |= *(long*)((byte*)source + pos) & *(long*)((byte*)mask + pos);
				numBytes -= 8;
				pos += 8;
			}
			while (numBytes >= 4)
			{
				*(uint*)((byte*)destination + pos) &= ~(*(uint*)((byte*)mask + pos));
				*(uint*)((byte*)destination + pos) |= *(uint*)((byte*)source + pos) & *(uint*)((byte*)mask + pos);
				numBytes -= 4;
				pos += 4;
			}
			while (numBytes > 0)
			{
				byte* ptr = (byte*)destination + pos;
				*ptr &= ~((byte*)mask)[pos];
				byte* ptr2 = (byte*)destination + pos;
				*ptr2 |= ((byte*)source)[pos] & ((byte*)mask)[pos];
				numBytes--;
				pos++;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00061640 File Offset: 0x0005F840
		public unsafe static uint ReadMultipleBitsAsUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to read more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int newBitOffset = (int)(bitOffset % 32U);
				int intOffset = (int)((bitOffset - (uint)newBitOffset) / 32U);
				ptr = (void*)((byte*)ptr + intOffset * 4);
				bitOffset = (uint)newBitOffset;
			}
			if (bitOffset + bitCount <= 8U)
			{
				uint num = (uint)((byte)(*(byte*)ptr >> (int)bitOffset));
				uint mask = 255U >> (int)(8U - bitCount);
				return num & mask;
			}
			if (bitOffset + bitCount <= 16U)
			{
				uint num2 = (uint)((ushort)(*(ushort*)ptr >> (int)bitOffset));
				uint mask2 = 65535U >> (int)(16U - bitCount);
				return num2 & mask2;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint num3 = *(uint*)ptr >> (int)bitOffset;
				uint mask3 = uint.MaxValue >> (int)(32U - bitCount);
				return num3 & mask3;
			}
			throw new NotImplementedException("Reading int straddling int boundary");
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000616F4 File Offset: 0x0005F8F4
		public unsafe static void WriteUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, uint value)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to write more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int newBitOffset = (int)(bitOffset % 32U);
				int intOffset = (int)((bitOffset - (uint)newBitOffset) / 32U);
				ptr = (void*)((byte*)ptr + intOffset * 4);
				bitOffset = (uint)newBitOffset;
			}
			if (bitOffset + bitCount <= 8U)
			{
				byte byteValue = (byte)value;
				byteValue = (byte)(byteValue << (int)bitOffset);
				uint mask = ~(255U >> (int)(8U - bitCount) << (int)bitOffset);
				*(byte*)ptr = (byte)(((uint)(*(byte*)ptr) & mask) | (uint)byteValue);
				return;
			}
			if (bitOffset + bitCount <= 16U)
			{
				ushort ushortValue = (ushort)value;
				ushortValue = (ushort)(ushortValue << (int)bitOffset);
				uint mask2 = ~(65535U >> (int)(16U - bitCount) << (int)bitOffset);
				*(short*)ptr = (short)((ushort)(((uint)(*(ushort*)ptr) & mask2) | (uint)ushortValue));
				return;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint uintValue = value << (int)bitOffset;
				uint mask3 = ~(uint.MaxValue >> (int)(32U - bitCount) << (int)bitOffset);
				*(int*)ptr = (int)((*(uint*)ptr & mask3) | uintValue);
				return;
			}
			throw new NotImplementedException("Writing int straddling int boundary");
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000617DE File Offset: 0x0005F9DE
		public unsafe static int ReadTwosComplementMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			return (int)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000617E8 File Offset: 0x0005F9E8
		public unsafe static void WriteIntAsTwosComplementMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)value);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000617F4 File Offset: 0x0005F9F4
		public unsafe static int ReadExcessKMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			long num = (long)((ulong)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount));
			long halfMax = (1L << (int)bitCount) / 2L;
			return (int)(num - halfMax);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00061818 File Offset: 0x0005FA18
		public unsafe static void WriteIntAsExcessKMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			long unsignedValue = (1L << (int)bitCount) / 2L + (long)value;
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)unsignedValue);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00061840 File Offset: 0x0005FA40
		public unsafe static float ReadMultipleBitsAsNormalizedUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			uint num = MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
			uint maxValue = (uint)((1L << (int)bitCount) - 1L);
			return NumberHelpers.UIntToNormalizedFloat(num, 0U, maxValue);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00061868 File Offset: 0x0005FA68
		public unsafe static void WriteNormalizedUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, float value)
		{
			uint maxValue = (uint)((1L << (int)bitCount) - 1L);
			uint uintValue = NumberHelpers.NormalizedFloatToUInt(value, 0U, maxValue);
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, uintValue);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00061894 File Offset: 0x0005FA94
		public unsafe static void SetBitsInBuffer(void* buffer, int byteOffset, int bitOffset, int sizeInBits, bool value)
		{
			if (buffer == null)
			{
				throw new ArgumentException("A buffer must be provided to apply the bitmask on", "buffer");
			}
			if (sizeInBits < 0)
			{
				throw new ArgumentException("Negative sizeInBits", "sizeInBits");
			}
			if (bitOffset < 0)
			{
				throw new ArgumentException("Negative bitOffset", "bitOffset");
			}
			if (byteOffset < 0)
			{
				throw new ArgumentException("Negative byteOffset", "byteOffset");
			}
			if (bitOffset >= 8)
			{
				int skipBytes = bitOffset / 8;
				byteOffset += skipBytes;
				bitOffset %= 8;
			}
			byte* bytePos = (byte*)buffer + byteOffset;
			int sizeRemainingInBits = sizeInBits;
			if (bitOffset != 0)
			{
				int mask = 255 << bitOffset;
				if (sizeRemainingInBits + bitOffset < 8)
				{
					mask &= 255 >> 8 - (sizeRemainingInBits + bitOffset);
				}
				if (value)
				{
					byte* ptr = bytePos;
					*ptr |= (byte)mask;
				}
				else
				{
					byte* ptr2 = bytePos;
					*ptr2 &= (byte)(~(byte)mask);
				}
				bytePos++;
				sizeRemainingInBits -= 8 - bitOffset;
			}
			while (sizeRemainingInBits >= 8)
			{
				*bytePos = (value ? byte.MaxValue : 0);
				bytePos++;
				sizeRemainingInBits -= 8;
			}
			if (sizeRemainingInBits > 0)
			{
				byte mask2 = (byte)(255 >> 8 - sizeRemainingInBits);
				if (value)
				{
					byte* ptr3 = bytePos;
					*ptr3 |= mask2;
					return;
				}
				byte* ptr4 = bytePos;
				*ptr4 &= ~mask2;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00061998 File Offset: 0x0005FB98
		public static void Swap<TValue>(ref TValue a, ref TValue b)
		{
			TValue temp = a;
			a = b;
			b = temp;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000619C0 File Offset: 0x0005FBC0
		public static uint AlignNatural(uint offset, uint sizeInBytes)
		{
			uint alignment = Math.Min(8U, sizeInBytes);
			return offset.AlignToMultipleOf(alignment);
		}

		// Token: 0x02000247 RID: 583
		public struct BitRegion
		{
			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001555 RID: 5461 RVA: 0x000619DC File Offset: 0x0005FBDC
			public bool isEmpty
			{
				get
				{
					return this.sizeInBits == 0U;
				}
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x000619E7 File Offset: 0x0005FBE7
			public BitRegion(uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x000619F7 File Offset: 0x0005FBF7
			public BitRegion(uint byteOffset, uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = byteOffset * 8U + bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x06001558 RID: 5464 RVA: 0x00061A0C File Offset: 0x0005FC0C
			public MemoryHelpers.BitRegion Overlap(MemoryHelpers.BitRegion other)
			{
				uint thisEnd = this.bitOffset + this.sizeInBits;
				uint otherEnd = other.bitOffset + other.sizeInBits;
				if (thisEnd <= other.bitOffset || otherEnd <= this.bitOffset)
				{
					return default(MemoryHelpers.BitRegion);
				}
				uint end = Math.Min(thisEnd, otherEnd);
				uint start = Math.Max(this.bitOffset, other.bitOffset);
				return new MemoryHelpers.BitRegion(start, end - start);
			}

			// Token: 0x04000C69 RID: 3177
			public uint bitOffset;

			// Token: 0x04000C6A RID: 3178
			public uint sizeInBits;
		}
	}
}
