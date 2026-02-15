using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x0200005E RID: 94
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000400E File Offset: 0x0000220E
		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004039 File Offset: 0x00002239
		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000404C File Offset: 0x0000224C
		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004064 File Offset: 0x00002264
		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x17000025 RID: 37
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

		// Token: 0x0600011F RID: 287 RVA: 0x000040D7 File Offset: 0x000022D7
		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004109 File Offset: 0x00002309
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004114 File Offset: 0x00002314
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000412C File Offset: 0x0000232C
		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004144 File Offset: 0x00002344
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004164 File Offset: 0x00002364
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004184 File Offset: 0x00002384
		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000041C4 File Offset: 0x000023C4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000041FC File Offset: 0x000023FC
		public override int GetHashCode()
		{
			int hashCode = this.m_Buffer;
			hashCode = (hashCode * 397) ^ this.m_Stride;
			return (hashCode * 397) ^ this.m_Length;
		}

		// Token: 0x0400010A RID: 266
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x0400010B RID: 267
		internal int m_Stride;

		// Token: 0x0400010C RID: 268
		internal int m_Length;

		// Token: 0x0200005F RID: 95
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000128 RID: 296 RVA: 0x00004236 File Offset: 0x00002436
			public Enumerator(ref NativeSlice<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x06000129 RID: 297 RVA: 0x00003D56 File Offset: 0x00001F56
			public void Dispose()
			{
			}

			// Token: 0x0600012A RID: 298 RVA: 0x0000424C File Offset: 0x0000244C
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x0600012B RID: 299 RVA: 0x0000427F File Offset: 0x0000247F
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x0600012C RID: 300 RVA: 0x00004289 File Offset: 0x00002489
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600012D RID: 301 RVA: 0x0000429C File Offset: 0x0000249C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400010D RID: 269
			private NativeSlice<T> m_Array;

			// Token: 0x0400010E RID: 270
			private int m_Index;
		}
	}
}
