using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x02000055 RID: 85
	[NativeContainerSupportsDeallocateOnJobCompletion]
	[NativeContainerSupportsDeferredConvertListToArray]
	[DebuggerTypeProxy(typeof(NativeArrayDebugView<>))]
	[NativeContainer]
	[DebuggerDisplay("Length = {m_Length}")]
	[NativeContainerSupportsMinMaxWriteRestriction]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEnumerable, IEquatable<NativeArray<T>> where T : struct
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000036F0 File Offset: 0x000018F0
		public NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeArray<T>.Allocate(length, allocator, out this);
			bool flag = (options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory;
			if (flag)
			{
				UnsafeUtility.MemClear(this.m_Buffer, (long)this.Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000372B File Offset: 0x0000192B
		public NativeArray(T[] array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003746 File Offset: 0x00001946
		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, 0, this, 0, array.Length);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003770 File Offset: 0x00001970
		private static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long totalSize = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			array = default(NativeArray<T>);
			array.m_Buffer = UnsafeUtility.MallocTracked(totalSize, UnsafeUtility.AlignOf<T>(), allocator, 0);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000037B0 File Offset: 0x000019B0
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700001D RID: 29
		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
			}
			[WriteAccessRequired]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000037F7 File Offset: 0x000019F7
		public bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003808 File Offset: 0x00001A08
		[WriteAccessRequired]
		public void Dispose()
		{
			bool flag = !this.IsCreated;
			if (!flag)
			{
				bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
				if (flag2)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
				}
				bool flag3 = this.m_AllocatorLabel >= Allocator.FirstUserIndex;
				if (flag3)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was allocated with a custom allocator, use CollectionHelper.Dispose in com.unity.collections package.");
				}
				bool flag4 = this.m_AllocatorLabel > Allocator.None;
				if (flag4)
				{
					UnsafeUtility.FreeTracked(this.m_Buffer, this.m_AllocatorLabel);
					this.m_AllocatorLabel = Allocator.Invalid;
				}
				this.m_Buffer = null;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000388C File Offset: 0x00001A8C
		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = !this.IsCreated;
			JobHandle jobHandle2;
			if (flag)
			{
				jobHandle2 = inputDeps;
			}
			else
			{
				bool flag2 = this.m_AllocatorLabel >= Allocator.FirstUserIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was allocated with a custom allocator, use CollectionHelper.Dispose in com.unity.collections package.");
				}
				bool flag3 = this.m_AllocatorLabel > Allocator.None;
				if (flag3)
				{
					JobHandle jobHandle = new NativeArrayDisposeJob
					{
						Data = new NativeArrayDispose
						{
							m_Buffer = this.m_Buffer,
							m_AllocatorLabel = this.m_AllocatorLabel
						}
					}.Schedule(inputDeps);
					this.m_Buffer = null;
					this.m_AllocatorLabel = Allocator.Invalid;
					jobHandle2 = jobHandle;
				}
				else
				{
					this.m_Buffer = null;
					jobHandle2 = inputDeps;
				}
			}
			return jobHandle2;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003938 File Offset: 0x00001B38
		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003948 File Offset: 0x00001B48
		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003958 File Offset: 0x00001B58
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003968 File Offset: 0x00001B68
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			NativeArray<T>.Copy(this, array, this.Length);
			return array;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000399C File Offset: 0x00001B9C
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000039B4 File Offset: 0x00001BB4
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000039D4 File Offset: 0x00001BD4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000039F4 File Offset: 0x00001BF4
		public bool Equals(NativeArray<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003A28 File Offset: 0x00001C28
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeArray<T> && this.Equals((NativeArray<T>)obj);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003A60 File Offset: 0x00001C60
		public override int GetHashCode()
		{
			return (this.m_Buffer * 397) ^ this.m_Length;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003A88 File Offset: 0x00001C88
		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003AA2 File Offset: 0x00001CA2
		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003AB6 File Offset: 0x00001CB6
		public static void Copy(T[] src, NativeArray<T> dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003AC6 File Offset: 0x00001CC6
		public static void Copy(NativeArray<T> src, T[] dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003ADA File Offset: 0x00001CDA
		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003B05 File Offset: 0x00001D05
		public static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003B14 File Offset: 0x00001D14
		private unsafe static void CopySafe(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003B44 File Offset: 0x00001D44
		private unsafe static void CopySafe(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			GCHandle handle = GCHandle.Alloc(src, GCHandleType.Pinned);
			IntPtr addr = handle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)(void*)addr + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			handle.Free();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003B98 File Offset: 0x00001D98
		private unsafe static void CopySafe(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle handle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr addr = handle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)addr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			handle.Free();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003BEC File Offset: 0x00001DEC
		private NativeArray<U> InternalReinterpret<U>(int length) where U : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(this.m_Buffer, length, this.m_AllocatorLabel);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003C14 File Offset: 0x00001E14
		public NativeArray<U> Reinterpret<U>() where U : struct
		{
			return this.InternalReinterpret<U>(this.Length);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003C34 File Offset: 0x00001E34
		public NativeArray<U> Reinterpret<U>(int expectedTypeSize) where U : struct
		{
			long tSize = (long)UnsafeUtility.SizeOf<T>();
			long uSize = (long)UnsafeUtility.SizeOf<U>();
			long byteLen = (long)this.Length * tSize;
			long uLen = byteLen / uSize;
			return this.InternalReinterpret<U>((int)uLen);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003C6C File Offset: 0x00001E6C
		public unsafe NativeArray<T> GetSubArray(int start, int length)
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)((byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)start), length, Allocator.None);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003C98 File Offset: 0x00001E98
		public NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly(this.m_Buffer, this.m_Length);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003CBC File Offset: 0x00001EBC
		[WriteAccessRequired]
		public readonly Span<T> AsSpan()
		{
			return new Span<T>(this.m_Buffer, this.m_Length);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public readonly ReadOnlySpan<T> AsReadOnlySpan()
		{
			return new ReadOnlySpan<T>(this.m_Buffer, this.m_Length);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003D04 File Offset: 0x00001F04
		public static implicit operator Span<T>(in NativeArray<T> source)
		{
			return source.AsSpan();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static implicit operator ReadOnlySpan<T>(in NativeArray<T> source)
		{
			return source.AsReadOnlySpan();
		}

		// Token: 0x040000FC RID: 252
		[NativeDisableUnsafePtrRestriction]
		[VisibleToOtherModules(new string[] { "UnityEngine.ContentLoadModule", "UnityEngine.TilemapModule" })]
		internal unsafe void* m_Buffer;

		// Token: 0x040000FD RID: 253
		internal int m_Length;

		// Token: 0x040000FE RID: 254
		internal Allocator m_AllocatorLabel;

		// Token: 0x02000056 RID: 86
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060000FF RID: 255 RVA: 0x00003D34 File Offset: 0x00001F34
			public Enumerator(ref NativeArray<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
				this.value = default(T);
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00003D56 File Offset: 0x00001F56
			public void Dispose()
			{
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00003D5C File Offset: 0x00001F5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				bool flag = this.m_Index < this.m_Array.m_Length;
				bool flag2;
				if (flag)
				{
					this.value = UnsafeUtility.ReadArrayElement<T>(this.m_Array.m_Buffer, this.m_Index);
					flag2 = true;
				}
				else
				{
					this.value = default(T);
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00003DC1 File Offset: 0x00001FC1
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00003DCC File Offset: 0x00001FCC
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000104 RID: 260 RVA: 0x00003DE4 File Offset: 0x00001FE4
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000FF RID: 255
			private NativeArray<T> m_Array;

			// Token: 0x04000100 RID: 256
			private int m_Index;

			// Token: 0x04000101 RID: 257
			private T value;
		}

		// Token: 0x02000057 RID: 87
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[DebuggerTypeProxy(typeof(NativeArrayReadOnlyDebugView<>))]
		[DebuggerDisplay("Length = {Length}")]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000105 RID: 261 RVA: 0x00003E01 File Offset: 0x00002001
			internal unsafe ReadOnly(void* buffer, int length)
			{
				this.m_Buffer = buffer;
				this.m_Length = length;
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000106 RID: 262 RVA: 0x00003E14 File Offset: 0x00002014
			public int Length
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x17000022 RID: 34
			public T this[int index]
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
				}
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00003E4C File Offset: 0x0000204C
			public readonly ref T UnsafeElementAt(int index)
			{
				return UnsafeUtility.ArrayElementAsRef<T>(this.m_Buffer, index);
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00003E6C File Offset: 0x0000206C
			public NativeArray<T>.ReadOnly.Enumerator GetEnumerator()
			{
				return new NativeArray<T>.ReadOnly.Enumerator(in this);
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00003E84 File Offset: 0x00002084
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600010B RID: 267 RVA: 0x00003EA4 File Offset: 0x000020A4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00003EC4 File Offset: 0x000020C4
			public readonly ReadOnlySpan<T> AsReadOnlySpan()
			{
				return new ReadOnlySpan<T>(this.m_Buffer, this.m_Length);
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00003EE8 File Offset: 0x000020E8
			public static implicit operator ReadOnlySpan<T>(in NativeArray<T>.ReadOnly source)
			{
				return source.AsReadOnlySpan();
			}

			// Token: 0x04000102 RID: 258
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* m_Buffer;

			// Token: 0x04000103 RID: 259
			internal int m_Length;

			// Token: 0x02000058 RID: 88
			[ExcludeFromDocs]
			public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x0600010E RID: 270 RVA: 0x00003F00 File Offset: 0x00002100
				public Enumerator(in NativeArray<T>.ReadOnly array)
				{
					this.m_Array = array;
					this.m_Index = -1;
					this.value = default(T);
				}

				// Token: 0x0600010F RID: 271 RVA: 0x00003D56 File Offset: 0x00001F56
				public void Dispose()
				{
				}

				// Token: 0x06000110 RID: 272 RVA: 0x00003F24 File Offset: 0x00002124
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public bool MoveNext()
				{
					this.m_Index++;
					bool flag = this.m_Index < this.m_Array.m_Length;
					bool flag2;
					if (flag)
					{
						this.value = UnsafeUtility.ReadArrayElement<T>(this.m_Array.m_Buffer, this.m_Index);
						flag2 = true;
					}
					else
					{
						this.value = default(T);
						flag2 = false;
					}
					return flag2;
				}

				// Token: 0x06000111 RID: 273 RVA: 0x00003F89 File Offset: 0x00002189
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x17000023 RID: 35
				// (get) Token: 0x06000112 RID: 274 RVA: 0x00003F93 File Offset: 0x00002193
				public T Current
				{
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					get
					{
						return this.value;
					}
				}

				// Token: 0x17000024 RID: 36
				// (get) Token: 0x06000113 RID: 275 RVA: 0x00003F9B File Offset: 0x0000219B
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x04000104 RID: 260
				private NativeArray<T>.ReadOnly m_Array;

				// Token: 0x04000105 RID: 261
				private int m_Index;

				// Token: 0x04000106 RID: 262
				private T value;
			}
		}
	}
}
