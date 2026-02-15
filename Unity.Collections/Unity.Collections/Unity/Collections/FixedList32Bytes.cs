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
	// Token: 0x0200004E RID: 78
	[DebuggerTypeProxy(typeof(FixedList32BytesDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList32Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006F14 File Offset: 0x00005114
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006F30 File Offset: 0x00005130
		internal unsafe ushort length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (FixedBytes32Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return *(ushort*)ptr;
				}
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (FixedBytes32Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006F50 File Offset: 0x00005150
		internal unsafe readonly byte* buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (FixedBytes32Align8* ptr2 = &this.data)
				{
					void* ptr = (void*)ptr2;
					return (byte*)ptr + UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006F6E File Offset: 0x0000516E
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00006F76 File Offset: 0x00005176
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006F80 File Offset: 0x00005180
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006F88 File Offset: 0x00005188
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006F93 File Offset: 0x00005193
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006FA1 File Offset: 0x000051A1
		internal unsafe readonly byte* Buffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.buffer + FixedList.PaddingBytes<T>();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006FAF File Offset: 0x000051AF
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return FixedList.Capacity<FixedBytes32Align8, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700004B RID: 75
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

		// Token: 0x06000223 RID: 547 RVA: 0x00006FDD File Offset: 0x000051DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006FEB File Offset: 0x000051EB
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006FFE File Offset: 0x000051FE
		public void Add(in T item)
		{
			this.AddNoResize(in item);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007007 File Offset: 0x00005207
		public unsafe void AddRange(void* ptr, int length)
		{
			this.AddRangeNoResize(ptr, length);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007014 File Offset: 0x00005214
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = item;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007040 File Offset: 0x00005240
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int idx = this.Length;
			this.Length += length;
			UnsafeUtility.MemCpy((void*)(this.Buffer + (IntPtr)idx * (IntPtr)sizeof(T)), ptr, (long)(UnsafeUtility.SizeOf<T>() * length));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007080 File Offset: 0x00005280
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

		// Token: 0x0600022A RID: 554 RVA: 0x000070C6 File Offset: 0x000052C6
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000070D0 File Offset: 0x000052D0
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

		// Token: 0x0600022C RID: 556 RVA: 0x0000712E File Offset: 0x0000532E
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000713A File Offset: 0x0000533A
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007153 File Offset: 0x00005353
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007160 File Offset: 0x00005360
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

		// Token: 0x06000230 RID: 560 RVA: 0x000071BE File Offset: 0x000053BE
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000071C8 File Offset: 0x000053C8
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

		// Token: 0x06000232 RID: 562 RVA: 0x00007224 File Offset: 0x00005424
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

		// Token: 0x06000233 RID: 563 RVA: 0x0000726B File Offset: 0x0000546B
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007294 File Offset: 0x00005494
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000072D4 File Offset: 0x000054D4
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000072E0 File Offset: 0x000054E0
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

		// Token: 0x06000237 RID: 567 RVA: 0x0000736E File Offset: 0x0000556E
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000737C File Offset: 0x0000557C
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000073BC File Offset: 0x000055BC
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000073C8 File Offset: 0x000055C8
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

		// Token: 0x0600023B RID: 571 RVA: 0x00007456 File Offset: 0x00005656
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007462 File Offset: 0x00005662
		public FixedList32Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007473 File Offset: 0x00005673
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

		// Token: 0x0600023E RID: 574 RVA: 0x000074AA File Offset: 0x000056AA
		public static implicit operator FixedList32Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList32Bytes<T>(in other);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000074B4 File Offset: 0x000056B4
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000074F4 File Offset: 0x000056F4
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007500 File Offset: 0x00005700
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

		// Token: 0x06000242 RID: 578 RVA: 0x0000758E File Offset: 0x0000578E
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000759A File Offset: 0x0000579A
		public FixedList32Bytes(in FixedList128Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000075AB File Offset: 0x000057AB
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

		// Token: 0x06000245 RID: 581 RVA: 0x000075E2 File Offset: 0x000057E2
		public static implicit operator FixedList32Bytes<T>(in FixedList128Bytes<T> other)
		{
			return new FixedList32Bytes<T>(in other);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000075EC File Offset: 0x000057EC
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000762C File Offset: 0x0000582C
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007638 File Offset: 0x00005838
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

		// Token: 0x06000249 RID: 585 RVA: 0x000076C6 File Offset: 0x000058C6
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000076D2 File Offset: 0x000058D2
		public FixedList32Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000076E3 File Offset: 0x000058E3
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

		// Token: 0x0600024C RID: 588 RVA: 0x0000771A File Offset: 0x0000591A
		public static implicit operator FixedList32Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList32Bytes<T>(in other);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007724 File Offset: 0x00005924
		public unsafe static bool operator ==(in FixedList32Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			void* buffer = (void*)a.Buffer;
			void* buffer2 = (void*)b.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = a;
			return UnsafeUtility.MemCmp(buffer, buffer2, (long)fixedList32Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007764 File Offset: 0x00005964
		public static bool operator !=(in FixedList32Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007770 File Offset: 0x00005970
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

		// Token: 0x06000250 RID: 592 RVA: 0x000077FE File Offset: 0x000059FE
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000780A File Offset: 0x00005A0A
		public FixedList32Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList32Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000781B File Offset: 0x00005A1B
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

		// Token: 0x06000253 RID: 595 RVA: 0x00007852 File Offset: 0x00005A52
		public static implicit operator FixedList32Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList32Bytes<T>(in other);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000785C File Offset: 0x00005A5C
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

		// Token: 0x06000255 RID: 597 RVA: 0x000078DF File Offset: 0x00005ADF
		public FixedList32Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList32Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000BF RID: 191
		[SerializeField]
		internal FixedBytes32Align8 data;

		// Token: 0x0200004F RID: 79
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000258 RID: 600 RVA: 0x000078EE File Offset: 0x00005AEE
			public Enumerator(ref FixedList32Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x06000259 RID: 601 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x0600025A RID: 602 RVA: 0x00007903 File Offset: 0x00005B03
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x0600025B RID: 603 RVA: 0x00007926 File Offset: 0x00005B26
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600025C RID: 604 RVA: 0x0000792F File Offset: 0x00005B2F
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600025D RID: 605 RVA: 0x00007942 File Offset: 0x00005B42
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000C0 RID: 192
			private FixedList32Bytes<T> m_List;

			// Token: 0x040000C1 RID: 193
			private int m_Index;
		}
	}
}
