using System;

namespace Unity.Collections
{
	// Token: 0x0200005D RID: 93
	public static class NativeSliceExtensions
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisArray, start, length);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003FF4 File Offset: 0x000021F4
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start, length);
		}
	}
}
