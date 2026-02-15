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
	// Token: 0x0200006B RID: 107
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	public struct FixedString32Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000A5C6 File Offset: 0x000087C6
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A5CA File Offset: 0x000087CA
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

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A5D8 File Offset: 0x000087D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (FixedBytes30* ptr = &this.bytes)
			{
				return (byte*)ptr;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000A5F0 File Offset: 0x000087F0
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000A5F8 File Offset: 0x000087F8
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A5C6 File Offset: 0x000087C6
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 29;
			}
			set
			{
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A614 File Offset: 0x00008814
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 29)
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000A68F File Offset: 0x0000888F
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x17000084 RID: 132
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

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A6B1 File Offset: 0x000088B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A6BB File Offset: 0x000088BB
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A6EA File Offset: 0x000088EA
		public FixedString32Bytes.Enumerator GetEnumerator()
		{
			return new FixedString32Bytes.Enumerator(this);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000A6F7 File Offset: 0x000088F7
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A70C File Offset: 0x0000890C
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int alen = (int)this.utf8LengthInBytes;
			int blen = other.Length;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in this.bytes);
			char* bptr = other;
			if (bptr != null)
			{
				bptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(aptr, alen, bptr, blen) == 0;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A751 File Offset: 0x00008951
		public ref FixedList32Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList32Bytes<byte>>(UnsafeUtility.AddressOf<FixedString32Bytes>(ref this));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000A75E File Offset: 0x0000895E
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString32Bytes(string source)
		{
			this = default(FixedString32Bytes);
			this.Initialize(source);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000A76F File Offset: 0x0000896F
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		internal CopyError Initialize(string source)
		{
			return (ref this).CopyFromTruncated(source);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000A778 File Offset: 0x00008978
		public FixedString32Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString32Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A78A File Offset: 0x0000898A
		internal FormatError Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString32Bytes);
			return (ref this).Append(rune, count);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000A79C File Offset: 0x0000899C
		internal unsafe FormatError Initialize(byte* srcBytes, int srcLength)
		{
			this.bytes = default(FixedBytes30);
			this.utf8LengthInBytes = 0;
			int len = 0;
			FormatError error = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(this.GetUnsafePtr(), ref len, 29, srcBytes, srcLength);
			if (error != FormatError.None)
			{
				return error;
			}
			this.Length = len;
			return FormatError.None;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A7DD File Offset: 0x000089DD
		public FixedString32Bytes(NativeText.ReadOnly other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(other.GetUnsafePtr(), other.Length);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A7FC File Offset: 0x000089FC
		public FixedString32Bytes(in UnsafeText other)
		{
			this = default(FixedString32Bytes);
			UnsafeText unsafeText = other;
			this.Initialize(unsafeText.GetUnsafePtr(), other.Length);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000A82B File Offset: 0x00008A2B
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A835 File Offset: 0x00008A35
		public FixedString32Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000A846 File Offset: 0x00008A46
		internal unsafe FormatError Initialize(in FixedString32Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000A860 File Offset: 0x00008A60
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString32Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000A89C File Offset: 0x00008A9C
		public static bool operator !=(in FixedString32Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000A8B2 File Offset: 0x00008AB2
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000A8BC File Offset: 0x00008ABC
		public FixedString32Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000A8CD File Offset: 0x00008ACD
		internal unsafe FormatError Initialize(in FixedString64Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString64Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A924 File Offset: 0x00008B24
		public static bool operator !=(in FixedString32Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000A930 File Offset: 0x00008B30
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000A93A File Offset: 0x00008B3A
		public static implicit operator FixedString64Bytes(in FixedString32Bytes fs)
		{
			return new FixedString64Bytes(in fs);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A942 File Offset: 0x00008B42
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A94C File Offset: 0x00008B4C
		public FixedString32Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000A95D File Offset: 0x00008B5D
		internal unsafe FormatError Initialize(in FixedString128Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000A978 File Offset: 0x00008B78
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString128Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public static bool operator !=(in FixedString32Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000A9CA File Offset: 0x00008BCA
		public static implicit operator FixedString128Bytes(in FixedString32Bytes fs)
		{
			return new FixedString128Bytes(in fs);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public FixedString32Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000A9ED File Offset: 0x00008BED
		internal unsafe FormatError Initialize(in FixedString512Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000AA08 File Offset: 0x00008C08
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString512Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000AA44 File Offset: 0x00008C44
		public static bool operator !=(in FixedString32Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000AA50 File Offset: 0x00008C50
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000AA5A File Offset: 0x00008C5A
		public static implicit operator FixedString512Bytes(in FixedString32Bytes fs)
		{
			return new FixedString512Bytes(in fs);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000AA62 File Offset: 0x00008C62
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public FixedString32Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString32Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000AA7D File Offset: 0x00008C7D
		internal unsafe FormatError Initialize(in FixedString4096Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000AA98 File Offset: 0x00008C98
		public unsafe static bool operator ==(in FixedString32Bytes a, in FixedString4096Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public static bool operator !=(in FixedString32Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000AAEA File Offset: 0x00008CEA
		public static implicit operator FixedString4096Bytes(in FixedString32Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000AAF2 File Offset: 0x00008CF2
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator FixedString32Bytes(string b)
		{
			return new FixedString32Bytes(b);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000AAFA File Offset: 0x00008CFA
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString32Bytes>();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000AB02 File Offset: 0x00008D02
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString32Bytes>();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000AB0C File Offset: 0x00008D0C
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

		// Token: 0x060003E5 RID: 997 RVA: 0x0000ABA8 File Offset: 0x00008DA8
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
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString32Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000ABF9 File Offset: 0x00008DF9
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 29)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString32Bytes of '{1}' Capacity.", length, 29));
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000AC37 File Offset: 0x00008E37
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 29)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 29));
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000AC5B File Offset: 0x00008E5B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString32Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000AC77 File Offset: 0x00008E77
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000222 RID: 546
		internal const ushort utf8MaxLengthInBytes = 29;

		// Token: 0x04000223 RID: 547
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		internal FixedBytes30 bytes;

		// Token: 0x0200006C RID: 108
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060003EA RID: 1002 RVA: 0x0000AC87 File Offset: 0x00008E87
			public Enumerator(FixedString32Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x0000ACA3 File Offset: 0x00008EA3
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

			// Token: 0x060003ED RID: 1005 RVA: 0x0000ACE3 File Offset: 0x00008EE3
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000ACF8 File Offset: 0x00008EF8
			public Unicode.Rune Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000AD00 File Offset: 0x00008F00
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000225 RID: 549
			private FixedString32Bytes target;

			// Token: 0x04000226 RID: 550
			private int offset;

			// Token: 0x04000227 RID: 551
			private Unicode.Rune current;
		}
	}
}
