using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000044 RID: 68
	internal class ConcurrentMask
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005260 File Offset: 0x00003460
		internal static long AtomicOr(ref long destination, long source)
		{
			long readValue = Interlocked.Read(ref destination);
			long writtenValue;
			long oldReadValue;
			do
			{
				writtenValue = readValue | source;
				oldReadValue = readValue;
				readValue = Interlocked.CompareExchange(ref destination, writtenValue, oldReadValue);
			}
			while (readValue != oldReadValue);
			return writtenValue;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005288 File Offset: 0x00003488
		internal static long AtomicAnd(ref long destination, long source)
		{
			long readValue = Interlocked.Read(ref destination);
			long writtenValue;
			long oldReadValue;
			do
			{
				writtenValue = readValue & source;
				oldReadValue = readValue;
				readValue = Interlocked.CompareExchange(ref destination, writtenValue, oldReadValue);
			}
			while (readValue != oldReadValue);
			return writtenValue;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000052B0 File Offset: 0x000034B0
		internal static void longestConsecutiveOnes(long value, out int offset, out int count)
		{
			count = 0;
			long newvalue = value;
			while (newvalue != 0L)
			{
				value = newvalue;
				newvalue = value & (long)((ulong)value >> 1);
				count++;
			}
			offset = math.tzcnt(value);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000052DE File Offset: 0x000034DE
		internal static bool foundAtLeastThisManyConsecutiveOnes(long value, int minimum, out int offset, out int count)
		{
			if (minimum == 1)
			{
				offset = math.tzcnt(value);
				count = 1;
				return offset != 64;
			}
			ConcurrentMask.longestConsecutiveOnes(value, out offset, out count);
			return count >= minimum;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005309 File Offset: 0x00003509
		internal static bool foundAtLeastThisManyConsecutiveZeroes(long value, int minimum, out int offset, out int count)
		{
			return ConcurrentMask.foundAtLeastThisManyConsecutiveOnes(~value, minimum, out offset, out count);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005315 File Offset: 0x00003515
		internal static bool Succeeded(int error)
		{
			return error >= 0;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000531E File Offset: 0x0000351E
		internal static long MakeMask(int offset, int bits)
		{
			return (long)((long)(ulong.MaxValue >> 64 - bits) << offset);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005330 File Offset: 0x00003530
		internal static int TryAllocate(ref long l, int offset, int bits)
		{
			long mask = ConcurrentMask.MakeMask(offset, bits);
			long readValue = Interlocked.Read(ref l);
			while ((readValue & mask) == 0L)
			{
				long writtenValue = readValue | mask;
				long oldReadValue = readValue;
				readValue = Interlocked.CompareExchange(ref l, writtenValue, oldReadValue);
				if (readValue == oldReadValue)
				{
					return math.countbits(readValue);
				}
			}
			return -2;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005370 File Offset: 0x00003570
		internal static int TryFree(ref long l, int offset, int bits)
		{
			long mask = ConcurrentMask.MakeMask(offset, bits);
			long readValue = Interlocked.Read(ref l);
			while ((readValue & mask) == mask)
			{
				long writtenValue = readValue & ~mask;
				long oldReadValue = readValue;
				readValue = Interlocked.CompareExchange(ref l, writtenValue, oldReadValue);
				if (readValue == oldReadValue)
				{
					return math.countbits(writtenValue);
				}
			}
			return -1;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000053B0 File Offset: 0x000035B0
		internal static int TryAllocate(ref long l, out int offset, int bits)
		{
			long readValue = Interlocked.Read(ref l);
			int num;
			while (ConcurrentMask.foundAtLeastThisManyConsecutiveZeroes(readValue, bits, out offset, out num))
			{
				long mask = ConcurrentMask.MakeMask(offset, bits);
				long writtenValue = readValue | mask;
				long oldReadValue = readValue;
				readValue = Interlocked.CompareExchange(ref l, writtenValue, oldReadValue);
				if (readValue == oldReadValue)
				{
					return math.countbits(readValue);
				}
			}
			return -2;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000053F8 File Offset: 0x000035F8
		internal static int TryAllocate<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int wordOffset = offset >> 6;
			int bitOffset = offset & 63;
			if (bitOffset + bits > 64)
			{
				return -3;
			}
			return ConcurrentMask.TryAllocate(t.ElementAt(wordOffset), bitOffset, bits);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000542C File Offset: 0x0000362C
		internal static int TryFree<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int wordOffset = offset >> 6;
			int bitOffset = offset & 63;
			return ConcurrentMask.TryFree(t.ElementAt(wordOffset), bitOffset, bits);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005458 File Offset: 0x00003658
		internal unsafe static int TryAllocate<T>(ref T t, out int offset, int begin, int end, int bits) where T : IIndexable<long>
		{
			for (int wordOffset = begin; wordOffset < end; wordOffset++)
			{
				if (*t.ElementAt(wordOffset) != -1L)
				{
					IL_004D:
					while (wordOffset < end)
					{
						int bitOffset;
						int error = ConcurrentMask.TryAllocate(t.ElementAt(wordOffset), out bitOffset, bits);
						if (ConcurrentMask.Succeeded(error))
						{
							offset = wordOffset * 64 + bitOffset;
							return error;
						}
						wordOffset++;
					}
					offset = -1;
					return -2;
				}
			}
			goto IL_004D;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000054BC File Offset: 0x000036BC
		internal static int TryAllocate<T>(ref T t, out int offset, int begin, int bits) where T : IIndexable<long>
		{
			int error = ConcurrentMask.TryAllocate<T>(ref t, out offset, begin, t.Length, bits);
			if (ConcurrentMask.Succeeded(error))
			{
				return error;
			}
			return ConcurrentMask.TryAllocate<T>(ref t, out offset, 0, begin, bits);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000054F3 File Offset: 0x000036F3
		internal static int TryAllocate<T>(ref T t, out int offset, int bits) where T : IIndexable<long>
		{
			return ConcurrentMask.TryAllocate<T>(ref t, out offset, 0, t.Length, bits);
		}

		// Token: 0x040000A3 RID: 163
		internal const int ErrorFailedToFree = -1;

		// Token: 0x040000A4 RID: 164
		internal const int ErrorFailedToAllocate = -2;

		// Token: 0x040000A5 RID: 165
		internal const int ErrorAllocationCrossesWordBoundary = -3;

		// Token: 0x040000A6 RID: 166
		internal const int EmptyBeforeAllocation = 0;

		// Token: 0x040000A7 RID: 167
		internal const int EmptyAfterFree = 0;
	}
}
