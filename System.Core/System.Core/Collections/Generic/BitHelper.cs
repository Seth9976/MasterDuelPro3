using System;

namespace System.Collections.Generic
{
	// Token: 0x02000159 RID: 345
	internal sealed class BitHelper
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x0002C7C2 File Offset: 0x0002A9C2
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002C7DF File Offset: 0x0002A9DF
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._length && num >= 0)
			{
				int num2 = 1 << bitPosition % 32;
				if (this._useStackAlloc)
				{
					this._arrayPtr[num] |= num2;
					return;
				}
				this._array[num] |= num2;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002C84C File Offset: 0x0002AA4C
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num >= this._length || num < 0)
			{
				return false;
			}
			int num2 = 1 << bitPosition % 32;
			if (this._useStackAlloc)
			{
				return (this._arrayPtr[num] & num2) != 0;
			}
			return (this._array[num] & num2) != 0;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002C89E File Offset: 0x0002AA9E
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x0400036C RID: 876
		private readonly int _length;

		// Token: 0x0400036D RID: 877
		private unsafe readonly int* _arrayPtr;

		// Token: 0x0400036E RID: 878
		private readonly int[] _array;

		// Token: 0x0400036F RID: 879
		private readonly bool _useStackAlloc;
	}
}
