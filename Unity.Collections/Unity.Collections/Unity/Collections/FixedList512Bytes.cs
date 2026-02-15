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
	// Token: 0x0200005A RID: 90
	[DebuggerTypeProxy(typeof(FixedList512BytesDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList512Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00008F50 File Offset: 0x00007150
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00008F6C File Offset: 0x0000716C
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (FixedBytes512Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (FixedBytes512Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00008F8C File Offset: 0x0000718C
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (FixedBytes512Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00008FAA File Offset: 0x000071AA
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00008FB2 File Offset: 0x000071B2
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00008FBC File Offset: 0x000071BC
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00008FC4 File Offset: 0x000071C4
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00008FCF File Offset: 0x000071CF
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00008FDD File Offset: 0x000071DD
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00008FEB File Offset: 0x000071EB
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<FixedBytes512Align8, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700006F RID: 111
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

		// Token: 0x0600030D RID: 781 RVA: 0x00009019 File Offset: 0x00007219
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009027 File Offset: 0x00007227
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000903A File Offset: 0x0000723A
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009043 File Offset: 0x00007243
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009050 File Offset: 0x00007250
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000907C File Offset: 0x0000727C
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000090BC File Offset: 0x000072BC
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

		// Token: 0x06000314 RID: 788 RVA: 0x00009102 File Offset: 0x00007302
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000910C File Offset: 0x0000730C
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

		// Token: 0x06000316 RID: 790 RVA: 0x0000916A File Offset: 0x0000736A
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009176 File Offset: 0x00007376
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000918F File Offset: 0x0000738F
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000919C File Offset: 0x0000739C
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

		// Token: 0x0600031A RID: 794 RVA: 0x000091FA File Offset: 0x000073FA
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009204 File Offset: 0x00007404
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

		// Token: 0x0600031C RID: 796 RVA: 0x00009260 File Offset: 0x00007460
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

		// Token: 0x0600031D RID: 797 RVA: 0x000092A7 File Offset: 0x000074A7
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000092D0 File Offset: 0x000074D0
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009310 File Offset: 0x00007510
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000931C File Offset: 0x0000751C
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

		// Token: 0x06000321 RID: 801 RVA: 0x000093AA File Offset: 0x000075AA
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000093B6 File Offset: 0x000075B6
		public FixedList512Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000093C7 File Offset: 0x000075C7
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

		// Token: 0x06000324 RID: 804 RVA: 0x000093FE File Offset: 0x000075FE
		public static implicit operator FixedList512Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList512Bytes<T>(in other);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009408 File Offset: 0x00007608
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00009448 File Offset: 0x00007648
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00009454 File Offset: 0x00007654
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

		// Token: 0x06000328 RID: 808 RVA: 0x000094E2 File Offset: 0x000076E2
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000094EE File Offset: 0x000076EE
		public FixedList512Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000094FF File Offset: 0x000076FF
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

		// Token: 0x0600032B RID: 811 RVA: 0x00009536 File Offset: 0x00007736
		public static implicit operator FixedList512Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList512Bytes<T>(in other);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009540 File Offset: 0x00007740
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009580 File Offset: 0x00007780
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000958C File Offset: 0x0000778C
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

		// Token: 0x0600032F RID: 815 RVA: 0x0000961A File Offset: 0x0000781A
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009626 File Offset: 0x00007826
		public FixedList512Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009637 File Offset: 0x00007837
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

		// Token: 0x06000332 RID: 818 RVA: 0x0000966E File Offset: 0x0000786E
		public static implicit operator FixedList512Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList512Bytes<T>(in other);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009678 File Offset: 0x00007878
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000096B8 File Offset: 0x000078B8
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000096C4 File Offset: 0x000078C4
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

		// Token: 0x06000336 RID: 822 RVA: 0x00009752 File Offset: 0x00007952
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009760 File Offset: 0x00007960
		public unsafe static bool operator ==(in FixedList512Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList512Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000097A0 File Offset: 0x000079A0
		public static bool operator !=(in FixedList512Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000097AC File Offset: 0x000079AC
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

		// Token: 0x0600033A RID: 826 RVA: 0x0000983A File Offset: 0x00007A3A
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009846 File Offset: 0x00007A46
		public FixedList512Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList512Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009857 File Offset: 0x00007A57
		internal unsafe int Initialize(in FixedList4096Bytes<T> other)
		{
			if (other.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			UnsafeUtility.MemCpy((void*)this.Buffer, (void*)other.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000988E File Offset: 0x00007A8E
		public static implicit operator FixedList512Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList512Bytes<T>(in other);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009898 File Offset: 0x00007A98
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

		// Token: 0x0600033F RID: 831 RVA: 0x0000991B File Offset: 0x00007B1B
		public FixedList512Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList512Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000CB RID: 203
		[SerializeField]
		internal FixedBytes512Align8 data;

		// Token: 0x0200005B RID: 91
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000342 RID: 834 RVA: 0x00009923 File Offset: 0x00007B23
			public Enumerator(ref FixedList512Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x06000343 RID: 835 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000344 RID: 836 RVA: 0x00009938 File Offset: 0x00007B38
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000995B File Offset: 0x00007B5B
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000346 RID: 838 RVA: 0x00009964 File Offset: 0x00007B64
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000347 RID: 839 RVA: 0x00009977 File Offset: 0x00007B77
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000CC RID: 204
			private FixedList512Bytes<T> m_List;

			// Token: 0x040000CD RID: 205
			private int m_Index;
		}
	}
}
