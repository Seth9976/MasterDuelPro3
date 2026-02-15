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
	// Token: 0x02000077 RID: 119
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 4096)]
	public struct FixedString4096Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 4093;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
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

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (FixedBytes4094* ptr = &this.bytes)
			{
				return (byte*)ptr;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 4093;
			}
			set
			{
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 4093)
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000C37A File Offset: 0x0000A57A
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x170000A4 RID: 164
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

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000C39C File Offset: 0x0000A59C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public FixedString4096Bytes.Enumerator GetEnumerator()
		{
			return new FixedString4096Bytes.Enumerator(this);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000C3E3 File Offset: 0x0000A5E3
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int alen = (int)this.utf8LengthInBytes;
			int blen = other.Length;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in this.bytes);
			char* bptr = other;
			if (bptr != null)
			{
				bptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(aptr, alen, bptr, blen) == 0;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000C43D File Offset: 0x0000A63D
		public ref FixedList4096Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList4096Bytes<byte>>(UnsafeUtility.AddressOf<FixedString4096Bytes>(ref this));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000C44A File Offset: 0x0000A64A
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString4096Bytes(string source)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(source);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000C45B File Offset: 0x0000A65B
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		internal CopyError Initialize(string source)
		{
			return (ref this).CopyFromTruncated(source);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000C464 File Offset: 0x0000A664
		public FixedString4096Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000C476 File Offset: 0x0000A676
		internal FormatError Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			return (ref this).Append(rune, count);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000C488 File Offset: 0x0000A688
		internal unsafe FormatError Initialize(byte* srcBytes, int srcLength)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int len = 0;
			FormatError error = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(this.GetUnsafePtr(), ref len, 4093, srcBytes, srcLength);
			if (error != FormatError.None)
			{
				return error;
			}
			this.Length = len;
			return FormatError.None;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public FixedString4096Bytes(NativeText.ReadOnly other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(other.GetUnsafePtr(), other.Length);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		public FixedString4096Bytes(in UnsafeText other)
		{
			this = default(FixedString4096Bytes);
			UnsafeText unsafeText = other;
			this.Initialize(unsafeText.GetUnsafePtr(), other.Length);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C51B File Offset: 0x0000A71B
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000C525 File Offset: 0x0000A725
		public FixedString4096Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000C536 File Offset: 0x0000A736
		internal unsafe FormatError Initialize(in FixedString32Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000C550 File Offset: 0x0000A750
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000C58C File Offset: 0x0000A78C
		public static bool operator !=(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000C598 File Offset: 0x0000A798
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000C5A2 File Offset: 0x0000A7A2
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public FixedString4096Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000C5BD File Offset: 0x0000A7BD
		internal unsafe FormatError Initialize(in FixedString64Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000C614 File Offset: 0x0000A814
		public static bool operator !=(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C620 File Offset: 0x0000A820
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C62A File Offset: 0x0000A82A
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C634 File Offset: 0x0000A834
		public FixedString4096Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C645 File Offset: 0x0000A845
		internal unsafe FormatError Initialize(in FixedString128Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C660 File Offset: 0x0000A860
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000C69C File Offset: 0x0000A89C
		public static bool operator !=(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000C6B2 File Offset: 0x0000A8B2
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public FixedString4096Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C6CD File Offset: 0x0000A8CD
		internal unsafe FormatError Initialize(in FixedString512Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000C724 File Offset: 0x0000A924
		public static bool operator !=(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C730 File Offset: 0x0000A930
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C73A File Offset: 0x0000A93A
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C744 File Offset: 0x0000A944
		public FixedString4096Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C755 File Offset: 0x0000A955
		internal unsafe FormatError Initialize(in FixedString4096Bytes other)
		{
			return this.Initialize((byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes), (int)other.utf8LengthInBytes);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C770 File Offset: 0x0000A970
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			int alen = (int)a.utf8LengthInBytes;
			int blen = (int)b.utf8LengthInBytes;
			byte* aptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* bptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aptr, alen, bptr, blen);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public static bool operator !=(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator FixedString4096Bytes(string b)
		{
			return new FixedString4096Bytes(b);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString4096Bytes>();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString4096Bytes>();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C7DC File Offset: 0x0000A9DC
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

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C878 File Offset: 0x0000AA78
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
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString4096Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C8CC File Offset: 0x0000AACC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString4096Bytes of '{1}' Capacity.", length, 4093));
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000C91B File Offset: 0x0000AB1B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 4093));
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000C945 File Offset: 0x0000AB45
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString4096Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000AC77 File Offset: 0x00008E77
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400039A RID: 922
		internal const ushort utf8MaxLengthInBytes = 4093;

		// Token: 0x0400039B RID: 923
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400039C RID: 924
		[SerializeField]
		internal FixedBytes4094 bytes;

		// Token: 0x02000078 RID: 120
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x0000C961 File Offset: 0x0000AB61
			public Enumerator(FixedString4096Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000C97D File Offset: 0x0000AB7D
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

			// Token: 0x0600050B RID: 1291 RVA: 0x0000C9BD File Offset: 0x0000ABBD
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
			public Unicode.Rune Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.current;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000C9DA File Offset: 0x0000ABDA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400039D RID: 925
			private FixedString4096Bytes target;

			// Token: 0x0400039E RID: 926
			private int offset;

			// Token: 0x0400039F RID: 927
			private Unicode.Rune current;
		}
	}
}
