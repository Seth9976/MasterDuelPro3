using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200009A RID: 154
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(ArraySliceDebugView<>))]
	internal struct ArraySlice<T> : IEquatable<ArraySlice<T>> where T : struct
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0001A0F0 File Offset: 0x000182F0
		public unsafe ArraySlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.GetUnsafePtr<T>() + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001A126 File Offset: 0x00018326
		public bool Equals(ArraySlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001A154 File Offset: 0x00018354
		public override bool Equals(object obj)
		{
			return obj != null && obj is ArraySlice<T> && this.Equals((ArraySlice<T>)obj);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001A171 File Offset: 0x00018371
		public override int GetHashCode()
		{
			return (((this.m_Buffer * 397) ^ this.m_Stride) * 397) ^ this.m_Length;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001A194 File Offset: 0x00018394
		public static bool operator ==(ArraySlice<T> left, ArraySlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001A19E File Offset: 0x0001839E
		public static bool operator !=(ArraySlice<T> left, ArraySlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001A1AC File Offset: 0x000183AC
		public unsafe static ArraySlice<T> ConvertExistingDataToArraySlice(void* dataPointer, int stride, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			if (stride < 0)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new ArraySlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x170000A3 RID: 163
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001A241 File Offset: 0x00018441
		internal unsafe void* GetUnsafeReadOnlyPtr()
		{
			return (void*)this.m_Buffer;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001A24C File Offset: 0x0001844C
		internal unsafe void CopyTo(T[] array)
		{
			GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = handle.AddrOfPinnedObject();
			int sizeOf = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, sizeOf, this.GetUnsafeReadOnlyPtr(), this.Stride, sizeOf, this.m_Length);
			handle.Free();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001A294 File Offset: 0x00018494
		internal T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001A2B5 File Offset: 0x000184B5
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001A2BD File Offset: 0x000184BD
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x040002F1 RID: 753
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x040002F2 RID: 754
		internal int m_Stride;

		// Token: 0x040002F3 RID: 755
		internal int m_Length;
	}
}
