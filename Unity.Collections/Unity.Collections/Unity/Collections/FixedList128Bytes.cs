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
	// Token: 0x02000056 RID: 86
	[DebuggerTypeProxy(typeof(FixedList128BytesDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList128Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00008494 File Offset: 0x00006694
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000084B0 File Offset: 0x000066B0
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (FixedBytes128Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (FixedBytes128Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000084D0 File Offset: 0x000066D0
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (FixedBytes128Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000084EE File Offset: 0x000066EE
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000084F6 File Offset: 0x000066F6
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008500 File Offset: 0x00006700
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008508 File Offset: 0x00006708
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008513 File Offset: 0x00006713
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008521 File Offset: 0x00006721
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000852F File Offset: 0x0000672F
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<FixedBytes128Align8, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000063 RID: 99
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

		// Token: 0x060002BF RID: 703 RVA: 0x0000855D File Offset: 0x0000675D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000856B File Offset: 0x0000676B
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000857E File Offset: 0x0000677E
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008587 File Offset: 0x00006787
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008594 File Offset: 0x00006794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000085C0 File Offset: 0x000067C0
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008600 File Offset: 0x00006800
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

		// Token: 0x060002C6 RID: 710 RVA: 0x00008646 File Offset: 0x00006846
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008650 File Offset: 0x00006850
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

		// Token: 0x060002C8 RID: 712 RVA: 0x000086AE File Offset: 0x000068AE
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000086BA File Offset: 0x000068BA
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000086D3 File Offset: 0x000068D3
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000086E0 File Offset: 0x000068E0
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

		// Token: 0x060002CC RID: 716 RVA: 0x0000873E File Offset: 0x0000693E
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008748 File Offset: 0x00006948
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

		// Token: 0x060002CE RID: 718 RVA: 0x000087A4 File Offset: 0x000069A4
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

		// Token: 0x060002CF RID: 719 RVA: 0x000087EB File Offset: 0x000069EB
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008814 File Offset: 0x00006A14
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008854 File Offset: 0x00006A54
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008860 File Offset: 0x00006A60
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

		// Token: 0x060002D3 RID: 723 RVA: 0x000088EE File Offset: 0x00006AEE
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000088FA File Offset: 0x00006AFA
		public FixedList128Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000890B File Offset: 0x00006B0B
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

		// Token: 0x060002D6 RID: 726 RVA: 0x00008942 File Offset: 0x00006B42
		public static implicit operator FixedList128Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000894C File Offset: 0x00006B4C
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000898C File Offset: 0x00006B8C
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008998 File Offset: 0x00006B98
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

		// Token: 0x060002DA RID: 730 RVA: 0x00008A26 File Offset: 0x00006C26
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008A32 File Offset: 0x00006C32
		public FixedList128Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008A43 File Offset: 0x00006C43
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

		// Token: 0x060002DD RID: 733 RVA: 0x00008A7A File Offset: 0x00006C7A
		public static implicit operator FixedList128Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008A84 File Offset: 0x00006C84
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008AD0 File Offset: 0x00006CD0
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

		// Token: 0x060002E1 RID: 737 RVA: 0x00008B5E File Offset: 0x00006D5E
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008B6C File Offset: 0x00006D6C
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008BAC File Offset: 0x00006DAC
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008BB8 File Offset: 0x00006DB8
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

		// Token: 0x060002E5 RID: 741 RVA: 0x00008C46 File Offset: 0x00006E46
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008C52 File Offset: 0x00006E52
		public FixedList128Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008C63 File Offset: 0x00006E63
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

		// Token: 0x060002E8 RID: 744 RVA: 0x00008C9A File Offset: 0x00006E9A
		public static implicit operator FixedList128Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList128Bytes<T> fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008CF0 File Offset: 0x00006EF0
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

		// Token: 0x060002EC RID: 748 RVA: 0x00008D7E File Offset: 0x00006F7E
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008D8A File Offset: 0x00006F8A
		public FixedList128Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008D9B File Offset: 0x00006F9B
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

		// Token: 0x060002EF RID: 751 RVA: 0x00008DD2 File Offset: 0x00006FD2
		public static implicit operator FixedList128Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008DDC File Offset: 0x00006FDC
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

		// Token: 0x060002F1 RID: 753 RVA: 0x00008E5F File Offset: 0x0000705F
		public FixedList128Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList128Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		internal FixedBytes128Align8 data;

		// Token: 0x02000057 RID: 87
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060002F4 RID: 756 RVA: 0x00008E67 File Offset: 0x00007067
			public Enumerator(ref FixedList128Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x00008E7C File Offset: 0x0000707C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x00008E9F File Offset: 0x0000709F
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x00008EA8 File Offset: 0x000070A8
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008EBB File Offset: 0x000070BB
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000C8 RID: 200
			private FixedList128Bytes<T> m_List;

			// Token: 0x040000C9 RID: 201
			private int m_Index;
		}
	}
}
