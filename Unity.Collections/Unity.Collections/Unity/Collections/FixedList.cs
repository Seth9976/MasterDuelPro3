using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Properties;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200004C RID: 76
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(FixedBytes32Align8)
	})]
	[Serializable]
	internal struct FixedList<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U> : INativeList<T>, IIndexable<T> where T : struct, ValueType where U : struct, ValueType
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006B1C File Offset: 0x00004D1C
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006B38 File Offset: 0x00004D38
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (U* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (U* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006B58 File Offset: 0x00004D58
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (U* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006B76 File Offset: 0x00004D76
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00006B7E File Offset: 0x00004D7E
		[CreateProperty]
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return (int)this.length;
			}
			set
			{
				this.length = (ushort)value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006B88 File Offset: 0x00004D88
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006B90 File Offset: 0x00004D90
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006B9B File Offset: 0x00004D9B
		internal readonly int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006BA9 File Offset: 0x00004DA9
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006BB7 File Offset: 0x00004DB7
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<U, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000042 RID: 66
		public unsafe T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return UnsafeUtility.ReadArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index));
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				UnsafeUtility.WriteArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index), value);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006BE5 File Offset: 0x00004DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006BF3 File Offset: 0x00004DF3
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006C06 File Offset: 0x00004E06
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006C0F File Offset: 0x00004E0F
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006C1C File Offset: 0x00004E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006C48 File Offset: 0x00004E48
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006C88 File Offset: 0x00004E88
		public unsafe void AddReplicate(in T value, int count)
		{
			int idx = this.Length;
			this.Length += count;
			fixed (T* ptr2 = &value)
			{
				T* ptr = ptr2;
				UnsafeUtility.MemCpyReplicate((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), (void*)ptr, UnsafeUtility.SizeOf<T>(), count);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006CCE File Offset: 0x00004ECE
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int items = end - begin;
			if (items < 1)
			{
				return;
			}
			int itemsToCopy = (int)this.length - begin;
			this.Length += items;
			if (itemsToCopy < 1)
			{
				return;
			}
			int bytesToCopy = itemsToCopy * UnsafeUtility.SizeOf<T>();
			byte* buffer = this.Buffer;
			byte* dest = buffer + end * UnsafeUtility.SizeOf<T>();
			byte* src = buffer + begin * UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemMove((void*)dest, (void*)src, (long)bytesToCopy);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006D36 File Offset: 0x00004F36
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006D42 File Offset: 0x00004F42
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006D5B File Offset: 0x00004F5B
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006D68 File Offset: 0x00004F68
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int copyFrom = math.max(this.Length - count, index + count);
				int sizeOf = UnsafeUtility.SizeOf<T>();
				void* dst = (void*)(this.Buffer + index * sizeOf);
				void* src = (void*)(this.Buffer + copyFrom * sizeOf);
				UnsafeUtility.MemCpy(dst, src, (long)((this.Length - copyFrom) * sizeOf));
				this.Length -= count;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006DC6 File Offset: 0x00004FC6
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int copyFrom = math.min(index + count, this.Length);
				int sizeOf = UnsafeUtility.SizeOf<T>();
				void* dst = (void*)(this.Buffer + index * sizeOf);
				void* src = (void*)(this.Buffer + copyFrom * sizeOf);
				UnsafeUtility.MemCpy(dst, src, (long)((this.Length - copyFrom) * sizeOf));
				this.Length -= count;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006E2C File Offset: 0x0000502C
		[ExcludeFromBurstCompatTesting("Returns managed array")]
		public unsafe T[] ToArray()
		{
			T[] array = new T[this.Length];
			byte* s = this.Buffer;
			fixed (T[] array2 = array)
			{
				T* d;
				if (array == null || array2.Length == 0)
				{
					d = null;
				}
				else
				{
					d = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)d, (void*)s, (long)this.LengthInBytes);
			}
			return array;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006E73 File Offset: 0x00005073
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x040000BE RID: 190
		[SerializeField]
		internal U data;
	}
}
