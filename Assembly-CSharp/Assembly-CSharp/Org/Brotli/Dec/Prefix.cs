using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000081 RID: 129
	internal sealed class Prefix
	{
		// Token: 0x040002F1 RID: 753
		internal static readonly int[] BlockLengthOffset = new int[]
		{
			1, 5, 9, 13, 17, 25, 33, 41, 49, 65,
			81, 97, 113, 145, 177, 209, 241, 305, 369, 497,
			753, 1265, 2289, 4337, 8433, 16625
		};

		// Token: 0x040002F2 RID: 754
		internal static readonly int[] BlockLengthNBits = new int[]
		{
			2, 2, 2, 2, 3, 3, 3, 3, 4, 4,
			4, 4, 5, 5, 5, 5, 6, 6, 7, 8,
			9, 10, 11, 12, 13, 24
		};

		// Token: 0x040002F3 RID: 755
		internal static readonly int[] InsertLengthOffset = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 8, 10, 14,
			18, 26, 34, 50, 66, 98, 130, 194, 322, 578,
			1090, 2114, 6210, 22594
		};

		// Token: 0x040002F4 RID: 756
		internal static readonly int[] InsertLengthNBits = new int[]
		{
			0, 0, 0, 0, 0, 0, 1, 1, 2, 2,
			3, 3, 4, 4, 5, 5, 6, 7, 8, 9,
			10, 12, 14, 24
		};

		// Token: 0x040002F5 RID: 757
		internal static readonly int[] CopyLengthOffset = new int[]
		{
			2, 3, 4, 5, 6, 7, 8, 9, 10, 12,
			14, 18, 22, 30, 38, 54, 70, 102, 134, 198,
			326, 582, 1094, 2118
		};

		// Token: 0x040002F6 RID: 758
		internal static readonly int[] CopyLengthNBits = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			2, 2, 3, 3, 4, 4, 5, 5, 6, 7,
			8, 9, 10, 24
		};

		// Token: 0x040002F7 RID: 759
		internal static readonly int[] InsertRangeLut = new int[] { 0, 0, 8, 8, 0, 16, 8, 16, 16 };

		// Token: 0x040002F8 RID: 760
		internal static readonly int[] CopyRangeLut = new int[] { 0, 8, 0, 8, 16, 0, 16, 8, 16 };
	}
}
