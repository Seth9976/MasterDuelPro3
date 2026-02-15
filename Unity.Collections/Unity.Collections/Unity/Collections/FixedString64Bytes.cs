using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Properties;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200006E RID: 110
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 64)]
	public struct FixedString64Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000AD0D File Offset: 0x00008F0D
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000AD11 File Offset: 0x00008F11
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

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000AD20 File Offset: 0x00008F20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (FixedBytes62* ptr = &this.bytes)
			{
				return (byte*)ptr;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000AD38 File Offset: 0x00008F38
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000AD40 File Offset: 0x00008F40
		public unsafe int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return (int)this.utf8LengthInBytes;
			}
			set
			{
				this.utf8LengthInBytes = (ushort)value;
				this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000AD0D File Offset: 0x00008F0D
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 61;
			}
			set
			{
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 61)
			{
				return false;
			}
			if (newLength == (int)this.utf8LengthInBytes)
			{
				return true;
			}
			if (clearOptions == NativeArrayOptions.ClearMemory)
			{
				if (newLength > (int)this.utf8LengthInBytes)
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + this.utf8LengthInBytes), (long)(newLength - (int)this.utf8LengthInBytes));
				}
				else
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + newLength), (long)((int)this.utf8LengthInBytes - newLength));
				}
			}
			this.utf8LengthInBytes = (ushort)newLength;
			this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			return true;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000ADD7 File Offset: 0x00008FD7
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x1700008C RID: 140
		public unsafe byte this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.GetUnsafePtr()[index];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.GetUnsafePtr()[index] = value;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000ADF9 File Offset: 0x00008FF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000AE03 File Offset: 0x00009003
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000AE0C File Offset: 0x0000900C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000AE32 File Offset: 0x00009032
		public FixedString64Bytes.Enumerator GetEnumerator()
		{
			return new FixedString64Bytes.Enumerator(this);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000AE3F File Offset: 0x0000903F
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000AE54 File Offset: 0x00009054
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int alen = (int)this.utf8LengthInBytes;
			int blen = other.Length;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in this.bytes);
			char* bptr = other;
			if (bptr != null)
			{
				bptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(aptr, alen, bptr, blen) == 0;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000AE99 File Offset: 0x00009099
		public ref FixedList64Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList64Bytes<byte>>(UnsafeUtility.AddressOf<FixedString64Bytes>(ref this));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000AEA6 File Offset: 0x000090A6
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString64Bytes(string source)
		{
			this = default(FixedString64Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000AEB7 File Offset: 0x000090B7
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		internal CopyError Initialize(string source)
		{
			return (ref this).CopyFromTruncated(source);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public FixedString64Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString64Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000AED2 File Offset: 0x000090D2
		internal FormatError Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString64Bytes);
			return (ref this).Append(rune, count);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000AEE4 File Offset: 0x000090E4
		internal unsafe FormatError Initialize(byte* srcBytes, int srcLength)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int len = 0;
			FormatError error = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(this.GetUnsafePtr(), ref len, 61, srcBytes, srcLength);
			if (error != FormatError.None)
			{
				return error;
			}
			this.Length = len;
			return FormatError.None;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000AF25 File Offset: 0x00009125
		public FixedString64Bytes(NativeText.ReadOnly other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(other.GetUnsafePtr(), other.Length);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000AF44 File Offset: 0x00009144
		public FixedString64Bytes(in UnsafeText other)
		{
			this = default(FixedString64Bytes);
			UnsafeText unsafeText = other;
			this.Initialize(unsafeText.GetUnsafePtr(), other.Length);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000AF73 File Offset: 0x00009173
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000AF7D File Offset: 0x0000917D
		public FixedString64Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000AF8E File Offset: 0x0000918E
		internal unsafe FormatError Initialize(in FixedString32Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString32Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public static bool operator !=(in FixedString64Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000AFFA File Offset: 0x000091FA
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B004 File Offset: 0x00009204
		public FixedString64Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B015 File Offset: 0x00009215
		internal unsafe FormatError Initialize(in FixedString64Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000B030 File Offset: 0x00009230
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString64Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000B06C File Offset: 0x0000926C
		public static bool operator !=(in FixedString64Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000B078 File Offset: 0x00009278
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B082 File Offset: 0x00009282
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B08C File Offset: 0x0000928C
		public FixedString64Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000B09D File Offset: 0x0000929D
		internal unsafe FormatError Initialize(in FixedString128Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString128Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public static bool operator !=(in FixedString64Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000B100 File Offset: 0x00009300
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000B10A File Offset: 0x0000930A
		public static implicit operator FixedString128Bytes(in FixedString64Bytes fs)
		{
			return new FixedString128Bytes(in fs);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000B112 File Offset: 0x00009312
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000B11C File Offset: 0x0000931C
		public FixedString64Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000B12D File Offset: 0x0000932D
		internal unsafe FormatError Initialize(in FixedString512Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000B148 File Offset: 0x00009348
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString512Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000B184 File Offset: 0x00009384
		public static bool operator !=(in FixedString64Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000B190 File Offset: 0x00009390
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000B19A File Offset: 0x0000939A
		public static implicit operator FixedString512Bytes(in FixedString64Bytes fs)
		{
			return new FixedString512Bytes(in fs);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000B1A2 File Offset: 0x000093A2
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000B1AC File Offset: 0x000093AC
		public FixedString64Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000B1BD File Offset: 0x000093BD
		internal unsafe FormatError Initialize(in FixedString4096Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString4096Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000B214 File Offset: 0x00009414
		public static bool operator !=(in FixedString64Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000B220 File Offset: 0x00009420
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000B22A File Offset: 0x0000942A
		public static implicit operator FixedString4096Bytes(in FixedString64Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000B232 File Offset: 0x00009432
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator FixedString64Bytes(string b)
		{
			return new FixedString64Bytes(b);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000B23A File Offset: 0x0000943A
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString64Bytes>();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000B242 File Offset: 0x00009442
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString64Bytes>();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000B24C File Offset: 0x0000944C
		[ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			string aString = obj as string;
			if (aString != null)
			{
				return this.Equals(aString);
			}
			if (obj is FixedString32Bytes)
			{
				FixedString32Bytes aFixedString32Bytes = (FixedString32Bytes)obj;
				return this.Equals(aFixedString32Bytes);
			}
			if (obj is FixedString64Bytes)
			{
				FixedString64Bytes aFixedString64Bytes = (FixedString64Bytes)obj;
				return this.Equals(aFixedString64Bytes);
			}
			if (obj is FixedString128Bytes)
			{
				FixedString128Bytes aFixedString128Bytes = (FixedString128Bytes)obj;
				return this.Equals(aFixedString128Bytes);
			}
			if (obj is FixedString512Bytes)
			{
				FixedString512Bytes aFixedString512Bytes = (FixedString512Bytes)obj;
				return this.Equals(aFixedString512Bytes);
			}
			if (obj is FixedString4096Bytes)
			{
				FixedString4096Bytes aFixedString4096Bytes = (FixedString4096Bytes)obj;
				return this.Equals(aFixedString4096Bytes);
			}
			return false;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000B2E8 File Offset: 0x000094E8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString64Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000B339 File Offset: 0x00009539
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 61)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString64Bytes of '{1}' Capacity.", length, 61));
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000B377 File Offset: 0x00009577
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 61)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 61));
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000B39B File Offset: 0x0000959B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString64Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AC77 File Offset: 0x00008E77
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000239 RID: 569
		internal const ushort utf8MaxLengthInBytes = 61;

		// Token: 0x0400023A RID: 570
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400023B RID: 571
		[SerializeField]
		internal FixedBytes62 bytes;

		// Token: 0x0200006F RID: 111
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06000433 RID: 1075 RVA: 0x0000B3B7 File Offset: 0x000095B7
			public Enumerator(FixedString64Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0000B3D3 File Offset: 0x000095D3
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

			// Token: 0x06000436 RID: 1078 RVA: 0x0000B413 File Offset: 0x00009613
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000B428 File Offset: 0x00009628
			public Unicode.Rune Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000B430 File Offset: 0x00009630
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400023C RID: 572
			private FixedString64Bytes target;

			// Token: 0x0400023D RID: 573
			private int offset;

			// Token: 0x0400023E RID: 574
			private Unicode.Rune current;
		}
	}
}
