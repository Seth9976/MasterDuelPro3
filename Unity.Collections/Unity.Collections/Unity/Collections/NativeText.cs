using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Properties;

namespace Unity.Collections
{
	// Token: 0x020000D2 RID: 210
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[GenerateTestsForBurstCompatibility]
	public struct NativeText : INativeList<byte>, IIndexable<byte>, INativeDisposable, IDisposable, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x0001C58A File Offset: 0x0001A78A
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public NativeText(string source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001C59C File Offset: 0x0001A79C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe NativeText(string source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(source.Length * 2, allocator);
			this.Length = source.Length * 2;
			fixed (string text = source)
			{
				char* sourceptr = text;
				if (sourceptr != null)
				{
					sourceptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int actualBytes;
				if (UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out actualBytes, this.Capacity, sourceptr, source.Length) != CopyError.None)
				{
					this.m_Data->Dispose();
					this.m_Data = UnsafeText.Alloc(allocator);
					*this.m_Data = default(UnsafeText);
				}
				this.Length = actualBytes;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001C61E File Offset: 0x0001A81E
		public NativeText(int capacity, Allocator allocator)
		{
			this = new NativeText(capacity, allocator);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001C62D File Offset: 0x0001A82D
		public unsafe NativeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = UnsafeText.Alloc(allocator);
			*this.m_Data = new UnsafeText(capacity, allocator);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001C64D File Offset: 0x0001A84D
		public NativeText(Allocator allocator)
		{
			this = new NativeText(allocator);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001C65B File Offset: 0x0001A85B
		public NativeText(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(512, allocator);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001C66C File Offset: 0x0001A86C
		public unsafe NativeText(in FixedString32Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* sbytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)sbytes, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001C6B6 File Offset: 0x0001A8B6
		public NativeText(in FixedString32Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public unsafe NativeText(in FixedString64Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* sbytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)sbytes, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001C712 File Offset: 0x0001A912
		public NativeText(in FixedString64Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001C724 File Offset: 0x0001A924
		public unsafe NativeText(in FixedString128Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* sbytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)sbytes, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001C76E File Offset: 0x0001A96E
		public NativeText(in FixedString128Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001C780 File Offset: 0x0001A980
		public unsafe NativeText(in FixedString512Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* sbytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)sbytes, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001C7CA File Offset: 0x0001A9CA
		public NativeText(in FixedString512Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001C7DC File Offset: 0x0001A9DC
		public unsafe NativeText(in FixedString4096Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* sbytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)sbytes, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001C826 File Offset: 0x0001AA26
		public NativeText(in FixedString4096Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0001C835 File Offset: 0x0001AA35
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x0001C842 File Offset: 0x0001AA42
		public unsafe int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Data->Length;
			}
			set
			{
				this.m_Data->Length = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001C850 File Offset: 0x0001AA50
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0001C85D File Offset: 0x0001AA5D
		public unsafe int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Data->Capacity;
			}
			set
			{
				this.m_Data->Capacity = value;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001C86B File Offset: 0x0001AA6B
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			this.Length = newLength;
			return true;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001C875 File Offset: 0x0001AA75
		public unsafe readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_Data->IsEmpty;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data != null;
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001C89B File Offset: 0x0001AA9B
		public unsafe byte* GetUnsafePtr()
		{
			return this.m_Data->GetUnsafePtr();
		}

		// Token: 0x17000115 RID: 277
		public unsafe byte this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return *this.m_Data->ElementAt(index);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				*this.m_Data->ElementAt(index) = value;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return this.m_Data->ElementAt(index);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001C8D5 File Offset: 0x0001AAD5
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001C906 File Offset: 0x0001AB06
		public unsafe int CompareTo(NativeText other)
		{
			return (ref this).CompareTo(in *other.m_Data);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001C914 File Offset: 0x0001AB14
		public unsafe bool Equals(NativeText other)
		{
			return (ref this).Equals(in *other.m_Data);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001C922 File Offset: 0x0001AB22
		public int CompareTo(NativeText.ReadOnly other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001C92C File Offset: 0x0001AB2C
		public unsafe bool Equals(NativeText.ReadOnly other)
		{
			return (ref this).Equals(in *other.m_Data);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001C93A File Offset: 0x0001AB3A
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeText.Free(this.m_Data);
			this.m_Data = null;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001C958 File Offset: 0x0001AB58
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeTextDisposeJob
			{
				Data = new NativeTextDispose
				{
					m_TextData = this.m_Data
				}
			}.Schedule(inputDeps);
			this.m_Data = null;
			return jobHandle;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001C9A3 File Offset: 0x0001ABA3
		[CreateProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001C9B1 File Offset: 0x0001ABB1
		public NativeText.Enumerator GetEnumerator()
		{
			return new NativeText.Enumerator(this);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001C9BE File Offset: 0x0001ABBE
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public bool Equals(string other)
		{
			return this.ToString().Equals(other);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001C9E6 File Offset: 0x0001ABE6
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		public unsafe static bool operator ==(in NativeText a, in FixedString32Bytes b)
		{
			NativeText aref = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int alen = aref.Length;
			int blen = (int)b.utf8LengthInBytes;
			byte* unsafePtr = aref.GetUnsafePtr();
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001CA33 File Offset: 0x0001AC33
		public static bool operator !=(in NativeText a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001CA3F File Offset: 0x0001AC3F
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001CA49 File Offset: 0x0001AC49
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001CA54 File Offset: 0x0001AC54
		public unsafe static bool operator ==(in NativeText a, in FixedString64Bytes b)
		{
			NativeText aref = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int alen = aref.Length;
			int blen = (int)b.utf8LengthInBytes;
			byte* unsafePtr = aref.GetUnsafePtr();
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001CA97 File Offset: 0x0001AC97
		public static bool operator !=(in NativeText a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001CAA3 File Offset: 0x0001ACA3
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001CAAD File Offset: 0x0001ACAD
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		public unsafe static bool operator ==(in NativeText a, in FixedString128Bytes b)
		{
			NativeText aref = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int alen = aref.Length;
			int blen = (int)b.utf8LengthInBytes;
			byte* unsafePtr = aref.GetUnsafePtr();
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001CAFB File Offset: 0x0001ACFB
		public static bool operator !=(in NativeText a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001CB07 File Offset: 0x0001AD07
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001CB11 File Offset: 0x0001AD11
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		public unsafe static bool operator ==(in NativeText a, in FixedString512Bytes b)
		{
			NativeText aref = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int alen = aref.Length;
			int blen = (int)b.utf8LengthInBytes;
			byte* unsafePtr = aref.GetUnsafePtr();
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001CB5F File Offset: 0x0001AD5F
		public static bool operator !=(in NativeText a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001CB6B File Offset: 0x0001AD6B
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001CB75 File Offset: 0x0001AD75
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001CB80 File Offset: 0x0001AD80
		public unsafe static bool operator ==(in NativeText a, in FixedString4096Bytes b)
		{
			NativeText aref = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int alen = aref.Length;
			int blen = (int)b.utf8LengthInBytes;
			byte* unsafePtr = aref.GetUnsafePtr();
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001CBC3 File Offset: 0x0001ADC3
		public static bool operator !=(in NativeText a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001CBCF File Offset: 0x0001ADCF
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001CBD9 File Offset: 0x0001ADD9
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			if (this.m_Data == null)
			{
				return "";
			}
			return (ref this).ConvertToString<NativeText>();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001CBF1 File Offset: 0x0001ADF1
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<NativeText>();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		[ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			string aString = other as string;
			if (aString != null)
			{
				return this.Equals(aString);
			}
			if (other is NativeText)
			{
				NativeText aNativeText = (NativeText)other;
				return this.Equals(aNativeText);
			}
			if (other is NativeText.ReadOnly)
			{
				NativeText.ReadOnly aReadOnly = (NativeText.ReadOnly)other;
				return this.Equals(aReadOnly);
			}
			if (other is FixedString32Bytes)
			{
				FixedString32Bytes a32 = (FixedString32Bytes)other;
				return this.Equals(a32);
			}
			if (other is FixedString64Bytes)
			{
				FixedString64Bytes a33 = (FixedString64Bytes)other;
				return this.Equals(a33);
			}
			if (other is FixedString128Bytes)
			{
				FixedString128Bytes a34 = (FixedString128Bytes)other;
				return this.Equals(a34);
			}
			if (other is FixedString512Bytes)
			{
				FixedString512Bytes a35 = (FixedString512Bytes)other;
				return this.Equals(a35);
			}
			if (other is FixedString4096Bytes)
			{
				FixedString4096Bytes a36 = (FixedString4096Bytes)other;
				return this.Equals(a36);
			}
			return false;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001CCCA File Offset: 0x0001AECA
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void CheckNull(void* dataPtr)
		{
			if (dataPtr == null)
			{
				throw new InvalidOperationException("NativeText has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckWriteAndBumpSecondaryVersion()
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in NativeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001CD2D File Offset: 0x0001AF2D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("NativeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001CD45 File Offset: 0x0001AF45
		public NativeText.ReadOnly AsReadOnly()
		{
			return new NativeText.ReadOnly(this.m_Data);
		}

		// Token: 0x0400040C RID: 1036
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeText* m_Data;

		// Token: 0x020000D3 RID: 211
		public struct Enumerator : IEnumerator<Unicode.Rune>, IEnumerator, IDisposable
		{
			// Token: 0x060009AD RID: 2477 RVA: 0x0001CD52 File Offset: 0x0001AF52
			public Enumerator(NativeText source)
			{
				this.target = source.AsReadOnly();
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0001CD74 File Offset: 0x0001AF74
			public Enumerator(NativeText.ReadOnly source)
			{
				this.target = source;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x0001CD90 File Offset: 0x0001AF90
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0001CDE5 File Offset: 0x0001AFE5
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001CDF2 File Offset: 0x0001AFF2
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x0400040D RID: 1037
			private NativeText.ReadOnly target;

			// Token: 0x0400040E RID: 1038
			private int offset;

			// Token: 0x0400040F RID: 1039
			private Unicode.Rune current;
		}

		// Token: 0x020000D4 RID: 212
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
		{
			// Token: 0x060009B4 RID: 2484 RVA: 0x0001CDFA File Offset: 0x0001AFFA
			internal unsafe ReadOnly(UnsafeText* text)
			{
				this.m_Data = text;
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001CE03 File Offset: 0x0001B003
			// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00002C47 File Offset: 0x00000E47
			public unsafe int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return this.m_Data->Capacity;
				}
				set
				{
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001CE10 File Offset: 0x0001B010
			// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00002C47 File Offset: 0x00000E47
			public unsafe bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return this.m_Data == null || this.m_Data->IsEmpty;
				}
				set
				{
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001CE29 File Offset: 0x0001B029
			// (set) Token: 0x060009BA RID: 2490 RVA: 0x00002C47 File Offset: 0x00000E47
			public unsafe int Length
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return this.m_Data->Length;
				}
				set
				{
				}
			}

			// Token: 0x1700011C RID: 284
			public unsafe byte this[int index]
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return *this.m_Data->ElementAt(index);
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
				}
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Clear()
			{
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x0001CE45 File Offset: 0x0001B045
			public ref byte ElementAt(int index)
			{
				throw new NotSupportedException("Trying to retrieve non-readonly ref to NativeText.ReadOnly data. This is not permitted.");
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x0001CE51 File Offset: 0x0001B051
			public unsafe byte* GetUnsafePtr()
			{
				return this.m_Data->GetUnsafePtr();
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x0000348F File Offset: 0x0000168F
			public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
			{
				return false;
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0001CE5E File Offset: 0x0001B05E
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal unsafe static void CheckNull(void* dataPtr)
			{
				if (dataPtr == null)
				{
					throw new InvalidOperationException("NativeText.ReadOnly has yet to be created or has been destroyed!");
				}
			}

			// Token: 0x060009C2 RID: 2498 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly void CheckRead()
			{
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x0001CE70 File Offset: 0x0001B070
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void ErrorWrite()
			{
				throw new NotSupportedException("Trying to write to a NativeText.ReadOnly. Write operations are not permitted and are ignored.");
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x0001CE7C File Offset: 0x0001B07C
			[ExcludeFromBurstCompatTesting("Takes managed string")]
			public unsafe int CompareTo(string other)
			{
				return this.m_Data->ToString().CompareTo(other);
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x0001CE95 File Offset: 0x0001B095
			[ExcludeFromBurstCompatTesting("Takes managed string")]
			public unsafe bool Equals(string other)
			{
				return this.m_Data->ToString().Equals(other);
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x0001CEAE File Offset: 0x0001B0AE
			public unsafe int CompareTo(NativeText.ReadOnly other)
			{
				return (ref *this.m_Data).CompareTo(in *other.m_Data);
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x0001CEC1 File Offset: 0x0001B0C1
			public unsafe bool Equals(NativeText.ReadOnly other)
			{
				return (ref *this.m_Data).Equals(in *other.m_Data);
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0001CED4 File Offset: 0x0001B0D4
			public unsafe int CompareTo(NativeText other)
			{
				return (ref this).CompareTo(in *other.m_Data);
			}

			// Token: 0x060009C9 RID: 2505 RVA: 0x0001CEE2 File Offset: 0x0001B0E2
			public unsafe bool Equals(NativeText other)
			{
				return (ref this).Equals(in *other.m_Data);
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
			public int CompareTo(FixedString32Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060009CB RID: 2507 RVA: 0x0001CEFC File Offset: 0x0001B0FC
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				UnsafeText aref = *a.m_Data;
				int alen = aref.Length;
				int blen = (int)b.utf8LengthInBytes;
				byte* unsafePtr = aref.GetUnsafePtr();
				byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x0001CF3F File Offset: 0x0001B13F
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0001CF4B File Offset: 0x0001B14B
			public bool Equals(FixedString32Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x0001CF55 File Offset: 0x0001B155
			public int CompareTo(FixedString64Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0001CF60 File Offset: 0x0001B160
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				UnsafeText aref = *a.m_Data;
				int alen = aref.Length;
				int blen = (int)b.utf8LengthInBytes;
				byte* unsafePtr = aref.GetUnsafePtr();
				byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x0001CFA3 File Offset: 0x0001B1A3
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x0001CFAF File Offset: 0x0001B1AF
			public bool Equals(FixedString64Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x0001CFB9 File Offset: 0x0001B1B9
			public int CompareTo(FixedString128Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				UnsafeText aref = *a.m_Data;
				int alen = aref.Length;
				int blen = (int)b.utf8LengthInBytes;
				byte* unsafePtr = aref.GetUnsafePtr();
				byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x0001D007 File Offset: 0x0001B207
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060009D5 RID: 2517 RVA: 0x0001D013 File Offset: 0x0001B213
			public bool Equals(FixedString128Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x0001D01D File Offset: 0x0001B21D
			public int CompareTo(FixedString512Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0001D028 File Offset: 0x0001B228
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				UnsafeText aref = *a.m_Data;
				int alen = aref.Length;
				int blen = (int)b.utf8LengthInBytes;
				byte* unsafePtr = aref.GetUnsafePtr();
				byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x0001D06B File Offset: 0x0001B26B
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x0001D077 File Offset: 0x0001B277
			public bool Equals(FixedString512Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x0001D081 File Offset: 0x0001B281
			public int CompareTo(FixedString4096Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x0001D08C File Offset: 0x0001B28C
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				UnsafeText aref = *a.m_Data;
				int alen = aref.Length;
				int blen = (int)b.utf8LengthInBytes;
				byte* unsafePtr = aref.GetUnsafePtr();
				byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, alen, bptr, blen);
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0001D0CF File Offset: 0x0001B2CF
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x0001D0DB File Offset: 0x0001B2DB
			public bool Equals(FixedString4096Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0001D0E5 File Offset: 0x0001B2E5
			[ExcludeFromBurstCompatTesting("Returns managed string")]
			public override string ToString()
			{
				if (this.m_Data == null)
				{
					return "";
				}
				return (ref this).ConvertToString<NativeText.ReadOnly>();
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x0001D0FD File Offset: 0x0001B2FD
			public override int GetHashCode()
			{
				return (ref this).ComputeHashCode<NativeText.ReadOnly>();
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0001D108 File Offset: 0x0001B308
			[ExcludeFromBurstCompatTesting("Takes managed object")]
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				string aString = other as string;
				if (aString != null)
				{
					return this.Equals(aString);
				}
				if (other is NativeText)
				{
					NativeText aNativeText = (NativeText)other;
					return this.Equals(aNativeText);
				}
				if (other is NativeText.ReadOnly)
				{
					NativeText.ReadOnly aReadOnly = (NativeText.ReadOnly)other;
					return this.Equals(aReadOnly);
				}
				if (other is FixedString32Bytes)
				{
					FixedString32Bytes a32 = (FixedString32Bytes)other;
					return this.Equals(a32);
				}
				if (other is FixedString64Bytes)
				{
					FixedString64Bytes a33 = (FixedString64Bytes)other;
					return this.Equals(a33);
				}
				if (other is FixedString128Bytes)
				{
					FixedString128Bytes a34 = (FixedString128Bytes)other;
					return this.Equals(a34);
				}
				if (other is FixedString512Bytes)
				{
					FixedString512Bytes a35 = (FixedString512Bytes)other;
					return this.Equals(a35);
				}
				if (other is FixedString4096Bytes)
				{
					FixedString4096Bytes a36 = (FixedString4096Bytes)other;
					return this.Equals(a36);
				}
				return false;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001D1D6 File Offset: 0x0001B3D6
			[CreateProperty]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[ExcludeFromBurstCompatTesting("Returns managed string")]
			public string Value
			{
				get
				{
					return this.ToString();
				}
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
			public NativeText.Enumerator GetEnumerator()
			{
				return new NativeText.Enumerator(this);
			}

			// Token: 0x04000410 RID: 1040
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeText* m_Data;
		}
	}
}
