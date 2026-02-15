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
	// Token: 0x02000074 RID: 116
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 512)]
	public struct FixedString512Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000BB65 File Offset: 0x00009D65
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 509;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000BB6C File Offset: 0x00009D6C
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

		// Token: 0x06000483 RID: 1155 RVA: 0x0000BB7C File Offset: 0x00009D7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (FixedBytes510* ptr = &this.bytes)
			{
				return (byte*)ptr;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000BB94 File Offset: 0x00009D94
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0000BB9C File Offset: 0x00009D9C
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000BB65 File Offset: 0x00009D65
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 509;
			}
			set
			{
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 509)
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000BC36 File Offset: 0x00009E36
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x1700009C RID: 156
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

		// Token: 0x0600048C RID: 1164 RVA: 0x0000BC58 File Offset: 0x00009E58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BC62 File Offset: 0x00009E62
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000BC92 File Offset: 0x00009E92
		public FixedString512Bytes.Enumerator GetEnumerator()
		{
			return new FixedString512Bytes.Enumerator(this);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BC9F File Offset: 0x00009E9F
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int alen = (int)this.utf8LengthInBytes;
			int blen = other.Length;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in this.bytes);
			char* bptr = other;
			if (bptr != null)
			{
				bptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(aptr, alen, bptr, blen) == 0;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000BCF9 File Offset: 0x00009EF9
		public ref FixedList512Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList512Bytes<byte>>(UnsafeUtility.AddressOf<FixedString512Bytes>(ref this));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000BD06 File Offset: 0x00009F06
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString512Bytes(string source)
		{
			this = default(FixedString512Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000BD17 File Offset: 0x00009F17
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		internal CopyError Initialize(string source)
		{
			return (ref this).CopyFromTruncated(source);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BD20 File Offset: 0x00009F20
		public FixedString512Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BD32 File Offset: 0x00009F32
		internal FormatError Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			return (ref this).Append(rune, count);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BD44 File Offset: 0x00009F44
		internal unsafe FormatError Initialize(byte* srcBytes, int srcLength)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int len = 0;
			FormatError error = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(this.GetUnsafePtr(), ref len, 509, srcBytes, srcLength);
			if (error != FormatError.None)
			{
				return error;
			}
			this.Length = len;
			return FormatError.None;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BD88 File Offset: 0x00009F88
		public FixedString512Bytes(NativeText.ReadOnly other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(other.GetUnsafePtr(), other.Length);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public FixedString512Bytes(in UnsafeText other)
		{
			this = default(FixedString512Bytes);
			UnsafeText unsafeText = other;
			this.Initialize(unsafeText.GetUnsafePtr(), other.Length);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BDD7 File Offset: 0x00009FD7
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000BDE1 File Offset: 0x00009FE1
		public FixedString512Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		internal unsafe FormatError Initialize(in FixedString32Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000BE48 File Offset: 0x0000A048
		public static bool operator !=(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000BE54 File Offset: 0x0000A054
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000BE5E File Offset: 0x0000A05E
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000BE68 File Offset: 0x0000A068
		public FixedString512Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000BE79 File Offset: 0x0000A079
		internal unsafe FormatError Initialize(in FixedString64Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000BE94 File Offset: 0x0000A094
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		public static bool operator !=(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000BEE6 File Offset: 0x0000A0E6
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public FixedString512Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000BF01 File Offset: 0x0000A101
		internal unsafe FormatError Initialize(in FixedString128Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000BF58 File Offset: 0x0000A158
		public static bool operator !=(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000BF64 File Offset: 0x0000A164
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000BF6E File Offset: 0x0000A16E
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000BF78 File Offset: 0x0000A178
		public FixedString512Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000BF89 File Offset: 0x0000A189
		internal unsafe FormatError Initialize(in FixedString512Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public static bool operator !=(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000BFF6 File Offset: 0x0000A1F6
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C000 File Offset: 0x0000A200
		public FixedString512Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C011 File Offset: 0x0000A211
		internal unsafe FormatError Initialize(in FixedString4096Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C02C File Offset: 0x0000A22C
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000C068 File Offset: 0x0000A268
		public static bool operator !=(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000C074 File Offset: 0x0000A274
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000C07E File Offset: 0x0000A27E
		public static implicit operator FixedString4096Bytes(in FixedString512Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000C086 File Offset: 0x0000A286
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator FixedString512Bytes(string b)
		{
			return new FixedString512Bytes(b);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000C08E File Offset: 0x0000A28E
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString512Bytes>();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C096 File Offset: 0x0000A296
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString512Bytes>();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
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

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C13C File Offset: 0x0000A33C
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
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString512Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C190 File Offset: 0x0000A390
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString512Bytes of '{1}' Capacity.", length, 509));
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000C1DF File Offset: 0x0000A3DF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 509));
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000C209 File Offset: 0x0000A409
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString512Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000AC77 File Offset: 0x00008E77
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000287 RID: 647
		internal const ushort utf8MaxLengthInBytes = 509;

		// Token: 0x04000288 RID: 648
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000289 RID: 649
		[SerializeField]
		internal FixedBytes510 bytes;

		// Token: 0x02000075 RID: 117
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060004C2 RID: 1218 RVA: 0x0000C225 File Offset: 0x0000A425
			public Enumerator(FixedString512Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0000C241 File Offset: 0x0000A441
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

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000C281 File Offset: 0x0000A481
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000C296 File Offset: 0x0000A496
			public Unicode.Rune Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000C29E File Offset: 0x0000A49E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400028A RID: 650
			private FixedString512Bytes target;

			// Token: 0x0400028B RID: 651
			private int offset;

			// Token: 0x0400028C RID: 652
			private Unicode.Rune current;
		}
	}
}
