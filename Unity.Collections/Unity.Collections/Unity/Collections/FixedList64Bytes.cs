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
	// Token: 0x02000052 RID: 82
	[DebuggerTypeProxy(typeof(FixedList64BytesDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList64Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000079D8 File Offset: 0x00005BD8
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000079F4 File Offset: 0x00005BF4
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (FixedBytes64Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (FixedBytes64Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00007A14 File Offset: 0x00005C14
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (FixedBytes64Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00007A32 File Offset: 0x00005C32
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00007A3A File Offset: 0x00005C3A
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00007A44 File Offset: 0x00005C44
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00007A4C File Offset: 0x00005C4C
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00007A57 File Offset: 0x00005C57
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00007A65 File Offset: 0x00005C65
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00007A73 File Offset: 0x00005C73
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<FixedBytes64Align8, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000057 RID: 87
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

		// Token: 0x06000271 RID: 625 RVA: 0x00007AA1 File Offset: 0x00005CA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007AAF File Offset: 0x00005CAF
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007AC2 File Offset: 0x00005CC2
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007ACB File Offset: 0x00005CCB
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00007AD8 File Offset: 0x00005CD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007B04 File Offset: 0x00005D04
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00007B44 File Offset: 0x00005D44
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

		// Token: 0x06000278 RID: 632 RVA: 0x00007B8A File Offset: 0x00005D8A
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007B94 File Offset: 0x00005D94
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

		// Token: 0x0600027A RID: 634 RVA: 0x00007BF2 File Offset: 0x00005DF2
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007BFE File Offset: 0x00005DFE
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007C17 File Offset: 0x00005E17
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007C24 File Offset: 0x00005E24
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

		// Token: 0x0600027E RID: 638 RVA: 0x00007C82 File Offset: 0x00005E82
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007C8C File Offset: 0x00005E8C
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

		// Token: 0x06000280 RID: 640 RVA: 0x00007CE8 File Offset: 0x00005EE8
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

		// Token: 0x06000281 RID: 641 RVA: 0x00007D2F File Offset: 0x00005F2F
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007D58 File Offset: 0x00005F58
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007D98 File Offset: 0x00005F98
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007DA4 File Offset: 0x00005FA4
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

		// Token: 0x06000285 RID: 645 RVA: 0x00007E32 File Offset: 0x00006032
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007E3E File Offset: 0x0000603E
		public FixedList64Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007E4F File Offset: 0x0000604F
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

		// Token: 0x06000288 RID: 648 RVA: 0x00007E86 File Offset: 0x00006086
		public static implicit operator FixedList64Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList64Bytes<T>(in other);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007E90 File Offset: 0x00006090
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007ED0 File Offset: 0x000060D0
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007EDC File Offset: 0x000060DC
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

		// Token: 0x0600028C RID: 652 RVA: 0x00007F6A File Offset: 0x0000616A
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007F78 File Offset: 0x00006178
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007FB8 File Offset: 0x000061B8
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007FC4 File Offset: 0x000061C4
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

		// Token: 0x06000290 RID: 656 RVA: 0x00008052 File Offset: 0x00006252
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000805E File Offset: 0x0000625E
		public FixedList64Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000806F File Offset: 0x0000626F
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

		// Token: 0x06000293 RID: 659 RVA: 0x000080A6 File Offset: 0x000062A6
		public static implicit operator FixedList64Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList64Bytes<T>(in other);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000080B0 File Offset: 0x000062B0
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000080F0 File Offset: 0x000062F0
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000080FC File Offset: 0x000062FC
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

		// Token: 0x06000297 RID: 663 RVA: 0x0000818A File Offset: 0x0000638A
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008196 File Offset: 0x00006396
		public FixedList64Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000081A7 File Offset: 0x000063A7
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

		// Token: 0x0600029A RID: 666 RVA: 0x000081DE File Offset: 0x000063DE
		public static implicit operator FixedList64Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList64Bytes<T>(in other);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000081E8 File Offset: 0x000063E8
		public unsafe static bool operator ==(in FixedList64Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList64Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008228 File Offset: 0x00006428
		public static bool operator !=(in FixedList64Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008234 File Offset: 0x00006434
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

		// Token: 0x0600029E RID: 670 RVA: 0x000082C2 File Offset: 0x000064C2
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000082CE File Offset: 0x000064CE
		public FixedList64Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList64Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000082DF File Offset: 0x000064DF
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

		// Token: 0x060002A1 RID: 673 RVA: 0x00008316 File Offset: 0x00006516
		public static implicit operator FixedList64Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList64Bytes<T>(in other);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00008320 File Offset: 0x00006520
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

		// Token: 0x060002A3 RID: 675 RVA: 0x000083A3 File Offset: 0x000065A3
		public FixedList64Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList64Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		internal FixedBytes64Align8 data;

		// Token: 0x02000053 RID: 83
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060002A6 RID: 678 RVA: 0x000083AB File Offset: 0x000065AB
			public Enumerator(ref FixedList64Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x000083C0 File Offset: 0x000065C0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x000083E3 File Offset: 0x000065E3
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060002AA RID: 682 RVA: 0x000083EC File Offset: 0x000065EC
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060002AB RID: 683 RVA: 0x000083FF File Offset: 0x000065FF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000C4 RID: 196
			private FixedList64Bytes<T> m_List;

			// Token: 0x040000C5 RID: 197
			private int m_Index;
		}
	}
}
