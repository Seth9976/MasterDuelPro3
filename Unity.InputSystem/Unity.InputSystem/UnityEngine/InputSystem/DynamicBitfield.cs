using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000DF RID: 223
	internal struct DynamicBitfield
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003D8A0 File Offset: 0x0003BAA0
		public void SetLength(int newLength)
		{
			int ulongCount = DynamicBitfield.BitCountToULongCount(newLength);
			if (this.array.length < ulongCount)
			{
				this.array.SetLength(ulongCount);
			}
			this.length = newLength;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		public void SetBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] |= 1UL << bitIndex % 64;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003D90A File Offset: 0x0003BB0A
		public bool TestBit(int bitIndex)
		{
			return (this.array[bitIndex / 64] & (1UL << bitIndex % 64)) > 0UL;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003D92C File Offset: 0x0003BB2C
		public void ClearBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] &= ~(1UL << bitIndex % 64);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003D960 File Offset: 0x0003BB60
		public bool AnyBitIsSet()
		{
			for (int i = 0; i < this.array.length; i++)
			{
				if (this.array[i] != 0UL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003D994 File Offset: 0x0003BB94
		private static int BitCountToULongCount(int bitCount)
		{
			return (bitCount + 63) / 64;
		}

		// Token: 0x0400053A RID: 1338
		public InlinedArray<ulong> array;

		// Token: 0x0400053B RID: 1339
		public int length;
	}
}
