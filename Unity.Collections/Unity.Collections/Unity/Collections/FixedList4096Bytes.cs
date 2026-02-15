using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Properties;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200005E RID: 94
	[DebuggerTypeProxy(typeof(FixedList4096BytesDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList4096Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009A0C File Offset: 0x00007C0C
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00009A28 File Offset: 0x00007C28
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (FixedBytes4096Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (FixedBytes4096Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009A48 File Offset: 0x00007C48
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (FixedBytes4096Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009A66 File Offset: 0x00007C66
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00009A6E File Offset: 0x00007C6E
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00009A78 File Offset: 0x00007C78
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00009A80 File Offset: 0x00007C80
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009A8B File Offset: 0x00007C8B
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009A99 File Offset: 0x00007C99
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00009AA7 File Offset: 0x00007CA7
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<FixedBytes4096Align8, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700007B RID: 123
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

		// Token: 0x0600035B RID: 859 RVA: 0x00009AD5 File Offset: 0x00007CD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009AE3 File Offset: 0x00007CE3
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009AF6 File Offset: 0x00007CF6
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009AFF File Offset: 0x00007CFF
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009B0C File Offset: 0x00007D0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009B38 File Offset: 0x00007D38
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009B78 File Offset: 0x00007D78
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

		// Token: 0x06000362 RID: 866 RVA: 0x00009BBE File Offset: 0x00007DBE
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009BC8 File Offset: 0x00007DC8
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

		// Token: 0x06000364 RID: 868 RVA: 0x00009C26 File Offset: 0x00007E26
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009C32 File Offset: 0x00007E32
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009C4B File Offset: 0x00007E4B
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009C58 File Offset: 0x00007E58
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

		// Token: 0x06000368 RID: 872 RVA: 0x00009CB6 File Offset: 0x00007EB6
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009CC0 File Offset: 0x00007EC0
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

		// Token: 0x0600036A RID: 874 RVA: 0x00009D1C File Offset: 0x00007F1C
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

		// Token: 0x0600036B RID: 875 RVA: 0x00009D63 File Offset: 0x00007F63
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009D8C File Offset: 0x00007F8C
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009DCC File Offset: 0x00007FCC
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public unsafe int CompareTo(FixedList32Bytes<T> other)
		{
			byte* buffer = this.buffer;
			byte* b = other.buffer;
			byte* aa = buffer + FixedList.PaddingBytes<T>();
			byte* bb = b + FixedList.PaddingBytes<T>();
			int mini = math.min(this.Length, other.Length);
			for (int i = 0; i < mini; i++)
			{
				int j = UnsafeUtility.MemCmp((void*)(aa + sizeof(T) * i), (void*)(bb + sizeof(T) * i), (long)sizeof(T));
				if (j != 0)
				{
					return j;
				}
			}
			return this.Length.CompareTo(other.Length);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009E66 File Offset: 0x00008066
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00009E72 File Offset: 0x00008072
		public FixedList4096Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009E83 File Offset: 0x00008083
		internal unsafe int Initialize(in FixedList32Bytes<T> other)
		{
			if (other.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			UnsafeUtility.MemCpy((void*)this.Buffer, (void*)other.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009EBA File Offset: 0x000080BA
		public static implicit operator FixedList4096Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(in other);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00009EC4 File Offset: 0x000080C4
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009F04 File Offset: 0x00008104
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009F10 File Offset: 0x00008110
		public unsafe int CompareTo(FixedList64Bytes<T> other)
		{
			byte* buffer = this.buffer;
			byte* b = other.buffer;
			byte* aa = buffer + FixedList.PaddingBytes<T>();
			byte* bb = b + FixedList.PaddingBytes<T>();
			int mini = math.min(this.Length, other.Length);
			for (int i = 0; i < mini; i++)
			{
				int j = UnsafeUtility.MemCmp((void*)(aa + sizeof(T) * i), (void*)(bb + sizeof(T) * i), (long)sizeof(T));
				if (j != 0)
				{
					return j;
				}
			}
			return this.Length.CompareTo(other.Length);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009F9E File Offset: 0x0000819E
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009FAA File Offset: 0x000081AA
		public FixedList4096Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009FBB File Offset: 0x000081BB
		internal unsafe int Initialize(in FixedList64Bytes<T> other)
		{
			if (other.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			UnsafeUtility.MemCpy((void*)this.Buffer, (void*)other.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00009FF2 File Offset: 0x000081F2
		public static implicit operator FixedList4096Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(in other);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00009FFC File Offset: 0x000081FC
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A03C File Offset: 0x0000823C
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A048 File Offset: 0x00008248
		public unsafe int CompareTo(FixedList128Bytes<T> other)
		{
			byte* buffer = this.buffer;
			byte* b = other.buffer;
			byte* aa = buffer + FixedList.PaddingBytes<T>();
			byte* bb = b + FixedList.PaddingBytes<T>();
			int mini = math.min(this.Length, other.Length);
			for (int i = 0; i < mini; i++)
			{
				int j = UnsafeUtility.MemCmp((void*)(aa + sizeof(T) * i), (void*)(bb + sizeof(T) * i), (long)sizeof(T));
				if (j != 0)
				{
					return j;
				}
			}
			return this.Length.CompareTo(other.Length);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A0D6 File Offset: 0x000082D6
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A0E2 File Offset: 0x000082E2
		public FixedList4096Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A0F3 File Offset: 0x000082F3
		internal unsafe int Initialize(in FixedList128Bytes<T> other)
		{
			if (other.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			UnsafeUtility.MemCpy((void*)this.Buffer, (void*)other.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A12A File Offset: 0x0000832A
		public static implicit operator FixedList4096Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(in other);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A134 File Offset: 0x00008334
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A174 File Offset: 0x00008374
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A180 File Offset: 0x00008380
		public unsafe int CompareTo(FixedList512Bytes<T> other)
		{
			byte* buffer = this.buffer;
			byte* b = other.buffer;
			byte* aa = buffer + FixedList.PaddingBytes<T>();
			byte* bb = b + FixedList.PaddingBytes<T>();
			int mini = math.min(this.Length, other.Length);
			for (int i = 0; i < mini; i++)
			{
				int j = UnsafeUtility.MemCmp((void*)(aa + sizeof(T) * i), (void*)(bb + sizeof(T) * i), (long)sizeof(T));
				if (j != 0)
				{
					return j;
				}
			}
			return this.Length.CompareTo(other.Length);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A20E File Offset: 0x0000840E
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A21A File Offset: 0x0000841A
		public FixedList4096Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList4096Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A22B File Offset: 0x0000842B
		internal unsafe int Initialize(in FixedList512Bytes<T> other)
		{
			if (other.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			UnsafeUtility.MemCpy((void*)this.Buffer, (void*)other.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000A262 File Offset: 0x00008462
		public static implicit operator FixedList4096Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList4096Bytes<T>(in other);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A26C File Offset: 0x0000846C
		public unsafe static bool operator ==(in FixedList4096Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList4096Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A2AC File Offset: 0x000084AC
		public static bool operator !=(in FixedList4096Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public unsafe int CompareTo(FixedList4096Bytes<T> other)
		{
			byte* buffer = this.buffer;
			byte* b = other.buffer;
			byte* aa = buffer + FixedList.PaddingBytes<T>();
			byte* bb = b + FixedList.PaddingBytes<T>();
			int mini = math.min(this.Length, other.Length);
			for (int i = 0; i < mini; i++)
			{
				int j = UnsafeUtility.MemCmp((void*)(aa + sizeof(T) * i), (void*)(bb + sizeof(T) * i), (long)sizeof(T));
				if (j != 0)
				{
					return j;
				}
			}
			return this.Length.CompareTo(other.Length);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000A346 File Offset: 0x00008546
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000A354 File Offset: 0x00008554
		[ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			if (obj is FixedList32Bytes<T>)
			{
				FixedList32Bytes<T> aFixedList32Bytes = (FixedList32Bytes<T>)obj;
				return this.Equals(aFixedList32Bytes);
			}
			if (obj is FixedList64Bytes<T>)
			{
				FixedList64Bytes<T> aFixedList64Bytes = (FixedList64Bytes<T>)obj;
				return this.Equals(aFixedList64Bytes);
			}
			if (obj is FixedList128Bytes<T>)
			{
				FixedList128Bytes<T> aFixedList128Bytes = (FixedList128Bytes<T>)obj;
				return this.Equals(aFixedList128Bytes);
			}
			if (obj is FixedList512Bytes<T>)
			{
				FixedList512Bytes<T> aFixedList512Bytes = (FixedList512Bytes<T>)obj;
				return this.Equals(aFixedList512Bytes);
			}
			if (obj is FixedList4096Bytes<T>)
			{
				FixedList4096Bytes<T> aFixedList4096Bytes = (FixedList4096Bytes<T>)obj;
				return this.Equals(aFixedList4096Bytes);
			}
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A3D7 File Offset: 0x000085D7
		public FixedList4096Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList4096Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000CF RID: 207
		[SerializeField]
		internal FixedBytes4096Align8 data;

		// Token: 0x0200005F RID: 95
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000390 RID: 912 RVA: 0x0000A3DF File Offset: 0x000085DF
			public Enumerator(ref FixedList4096Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x06000391 RID: 913 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000392 RID: 914 RVA: 0x0000A3F4 File Offset: 0x000085F4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000A417 File Offset: 0x00008617
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A420 File Offset: 0x00008620
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A433 File Offset: 0x00008633
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000D0 RID: 208
			private FixedList4096Bytes<T> m_List;

			// Token: 0x040000D1 RID: 209
			private int m_Index;
		}
	}
}
