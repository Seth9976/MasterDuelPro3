using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000073 RID: 115
	public static class NativeSliceUnsafeUtility
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00004434 File Offset: 0x00002634
		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			bool flag2 = stride < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new NativeSlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000044B4 File Offset: 0x000026B4
		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000044CC File Offset: 0x000026CC
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}
	}
}
