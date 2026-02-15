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
	// Token: 0x02000071 RID: 113
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 128)]
	public struct FixedString128Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B43D File Offset: 0x0000963D
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 125;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000B441 File Offset: 0x00009641
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

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B450 File Offset: 0x00009650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (FixedBytes126* ptr = &this.bytes)
			{
				return (byte*)ptr;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000B468 File Offset: 0x00009668
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000B470 File Offset: 0x00009670
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000B43D File Offset: 0x0000963D
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 125;
			}
			set
			{
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B48C File Offset: 0x0000968C
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 125)
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B507 File Offset: 0x00009707
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x17000094 RID: 148
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

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B529 File Offset: 0x00009729
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B533 File Offset: 0x00009733
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B53C File Offset: 0x0000973C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B562 File Offset: 0x00009762
		public FixedString128Bytes.Enumerator GetEnumerator()
		{
			return new FixedString128Bytes.Enumerator(this);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B56F File Offset: 0x0000976F
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B584 File Offset: 0x00009784
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int alen = (int)this.utf8LengthInBytes;
			int blen = other.Length;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in this.bytes);
			char* bptr = other;
			if (bptr != null)
			{
				bptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(aptr, alen, bptr, blen) == 0;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000B5C9 File Offset: 0x000097C9
		public ref FixedList128Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList128Bytes<byte>>(UnsafeUtility.AddressOf<FixedString128Bytes>(ref this));
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000B5D6 File Offset: 0x000097D6
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString128Bytes(string source)
		{
			this = default(FixedString128Bytes);
			this.Initialize(source);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000B5E7 File Offset: 0x000097E7
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		internal CopyError Initialize(string source)
		{
			return (ref this).CopyFromTruncated(source);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public FixedString128Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000B602 File Offset: 0x00009802
		internal FormatError Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			return (ref this).Append(rune, count);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000B614 File Offset: 0x00009814
		internal unsafe FormatError Initialize(byte* srcBytes, int srcLength)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int len = 0;
			FormatError error = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(this.GetUnsafePtr(), ref len, 125, srcBytes, srcLength);
			if (error != FormatError.None)
			{
				return error;
			}
			this.Length = len;
			return FormatError.None;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000B655 File Offset: 0x00009855
		public FixedString128Bytes(NativeText.ReadOnly other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(other.GetUnsafePtr(), other.Length);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000B674 File Offset: 0x00009874
		public FixedString128Bytes(in UnsafeText other)
		{
			this = default(FixedString128Bytes);
			UnsafeText unsafeText = other;
			this.Initialize(unsafeText.GetUnsafePtr(), other.Length);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000B6A3 File Offset: 0x000098A3
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000B6AD File Offset: 0x000098AD
		public FixedString128Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000B6BE File Offset: 0x000098BE
		internal unsafe FormatError Initialize(in FixedString32Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000B714 File Offset: 0x00009914
		public static bool operator !=(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000B720 File Offset: 0x00009920
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000B72A File Offset: 0x0000992A
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000B734 File Offset: 0x00009934
		public FixedString128Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000B745 File Offset: 0x00009945
		internal unsafe FormatError Initialize(in FixedString64Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000B760 File Offset: 0x00009960
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000B79C File Offset: 0x0000999C
		public static bool operator !=(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000B7B2 File Offset: 0x000099B2
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000B7BC File Offset: 0x000099BC
		public FixedString128Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000B7CD File Offset: 0x000099CD
		internal unsafe FormatError Initialize(in FixedString128Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000B824 File Offset: 0x00009A24
		public static bool operator !=(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B830 File Offset: 0x00009A30
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B83A File Offset: 0x00009A3A
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000B844 File Offset: 0x00009A44
		public FixedString128Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000B855 File Offset: 0x00009A55
		internal unsafe FormatError Initialize(in FixedString512Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B870 File Offset: 0x00009A70
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public static bool operator !=(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000B8C2 File Offset: 0x00009AC2
		public static implicit operator FixedString512Bytes(in FixedString128Bytes fs)
		{
			return new FixedString512Bytes(in fs);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000B8CA File Offset: 0x00009ACA
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public FixedString128Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		internal unsafe FormatError Initialize(in FixedString4096Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B900 File Offset: 0x00009B00
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B93C File Offset: 0x00009B3C
		public static bool operator !=(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B948 File Offset: 0x00009B48
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000B952 File Offset: 0x00009B52
		public static implicit operator FixedString4096Bytes(in FixedString128Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B95A File Offset: 0x00009B5A
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator FixedString128Bytes(string b)
		{
			return new FixedString128Bytes(b);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B962 File Offset: 0x00009B62
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString128Bytes>();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B96A File Offset: 0x00009B6A
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString128Bytes>();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B974 File Offset: 0x00009B74
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

		// Token: 0x06000476 RID: 1142 RVA: 0x0000BA10 File Offset: 0x00009C10
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
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString128Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000BA61 File Offset: 0x00009C61
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString128Bytes of '{1}' Capacity.", length, 125));
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000BA9F File Offset: 0x00009C9F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 125));
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString128Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000AC77 File Offset: 0x00008E77
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000254 RID: 596
		internal const ushort utf8MaxLengthInBytes = 125;

		// Token: 0x04000255 RID: 597
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000256 RID: 598
		[SerializeField]
		internal FixedBytes126 bytes;

		// Token: 0x02000072 RID: 114
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600047B RID: 1147 RVA: 0x0000BADF File Offset: 0x00009CDF
			public Enumerator(FixedString128Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x0000BAFB File Offset: 0x00009CFB
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

			// Token: 0x0600047E RID: 1150 RVA: 0x0000BB3B File Offset: 0x00009D3B
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000BB50 File Offset: 0x00009D50
			public Unicode.Rune Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000BB58 File Offset: 0x00009D58
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000257 RID: 599
			private FixedString128Bytes target;

			// Token: 0x04000258 RID: 600
			private int offset;

			// Token: 0x04000259 RID: 601
			private Unicode.Rune current;
		}
	}
}
