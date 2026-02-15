using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A1 RID: 161
	internal static class ByteOrderStreamExtensions
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x000198CC File Offset: 0x00017ACC
		internal static byte[] SwappedBytes(ushort value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8)
			};
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000198CC File Offset: 0x00017ACC
		internal static byte[] SwappedBytes(short value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8)
			};
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000198E0 File Offset: 0x00017AE0
		internal static byte[] SwappedBytes(uint value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8),
				(byte)(value >> 16),
				(byte)(value >> 24)
			};
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00019904 File Offset: 0x00017B04
		internal static byte[] SwappedBytes(int value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8),
				(byte)(value >> 16),
				(byte)(value >> 24)
			};
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00019928 File Offset: 0x00017B28
		internal static byte[] SwappedBytes(long value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8),
				(byte)(value >> 16),
				(byte)(value >> 24),
				(byte)(value >> 32),
				(byte)(value >> 40),
				(byte)(value >> 48),
				(byte)(value >> 56)
			};
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00019978 File Offset: 0x00017B78
		internal static byte[] SwappedBytes(ulong value)
		{
			return new byte[]
			{
				(byte)value,
				(byte)(value >> 8),
				(byte)(value >> 16),
				(byte)(value >> 24),
				(byte)(value >> 32),
				(byte)(value >> 40),
				(byte)(value >> 48),
				(byte)(value >> 56)
			};
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000199C7 File Offset: 0x00017BC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static long SwappedS64(byte[] bytes)
		{
			return (long)((ulong)bytes[0] | ((ulong)bytes[1] << 8) | ((ulong)bytes[2] << 16) | ((ulong)bytes[3] << 24) | ((ulong)bytes[4] << 32) | ((ulong)bytes[5] << 40) | ((ulong)bytes[6] << 48) | ((ulong)bytes[7] << 56));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000199C7 File Offset: 0x00017BC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ulong SwappedU64(byte[] bytes)
		{
			return (ulong)bytes[0] | ((ulong)bytes[1] << 8) | ((ulong)bytes[2] << 16) | ((ulong)bytes[3] << 24) | ((ulong)bytes[4] << 32) | ((ulong)bytes[5] << 40) | ((ulong)bytes[6] << 48) | ((ulong)bytes[7] << 56);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00019A04 File Offset: 0x00017C04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int SwappedS32(byte[] bytes)
		{
			return (int)bytes[0] | ((int)bytes[1] << 8) | ((int)bytes[2] << 16) | ((int)bytes[3] << 24);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00019A1D File Offset: 0x00017C1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint SwappedU32(byte[] bytes)
		{
			return (uint)ByteOrderStreamExtensions.SwappedS32(bytes);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019A25 File Offset: 0x00017C25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static short SwappedS16(byte[] bytes)
		{
			return (short)((int)bytes[0] | ((int)bytes[1] << 8));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00019A31 File Offset: 0x00017C31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ushort SwappedU16(byte[] bytes)
		{
			return (ushort)ByteOrderStreamExtensions.SwappedS16(bytes);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019A3C File Offset: 0x00017C3C
		internal static byte[] ReadBytes(this Stream stream, int count)
		{
			byte[] array = new byte[count];
			int num;
			for (int i = count; i > 0; i -= num)
			{
				num = stream.Read(array, count - i, i);
				if (num < 1)
				{
					throw new EndOfStreamException();
				}
			}
			return array;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019A73 File Offset: 0x00017C73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadLEShort(this Stream stream)
		{
			return (int)ByteOrderStreamExtensions.SwappedS16(stream.ReadBytes(2));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00019A81 File Offset: 0x00017C81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadLEInt(this Stream stream)
		{
			return ByteOrderStreamExtensions.SwappedS32(stream.ReadBytes(4));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00019A8F File Offset: 0x00017C8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadLELong(this Stream stream)
		{
			return ByteOrderStreamExtensions.SwappedS64(stream.ReadBytes(8));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00019A9D File Offset: 0x00017C9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLEShort(this Stream stream, int value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 2);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00019AB0 File Offset: 0x00017CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLEShortAsync(this Stream stream, int value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 2, ct).ConfigureAwait(false);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00019B03 File Offset: 0x00017D03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLEUshort(this Stream stream, ushort value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 2);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00019B14 File Offset: 0x00017D14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLEUshortAsync(this Stream stream, ushort value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 2, ct).ConfigureAwait(false);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00019B67 File Offset: 0x00017D67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLEInt(this Stream stream, int value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 4);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00019B78 File Offset: 0x00017D78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLEIntAsync(this Stream stream, int value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 4, ct).ConfigureAwait(false);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019BCB File Offset: 0x00017DCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLEUint(this Stream stream, uint value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 4);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00019BDC File Offset: 0x00017DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLEUintAsync(this Stream stream, uint value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 4, ct).ConfigureAwait(false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00019C2F File Offset: 0x00017E2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLELong(this Stream stream, long value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 8);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00019C40 File Offset: 0x00017E40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLELongAsync(this Stream stream, long value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 8, ct).ConfigureAwait(false);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019C93 File Offset: 0x00017E93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLEUlong(this Stream stream, ulong value)
		{
			stream.Write(ByteOrderStreamExtensions.SwappedBytes(value), 0, 8);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00019CA4 File Offset: 0x00017EA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task WriteLEUlongAsync(this Stream stream, ulong value, CancellationToken ct)
		{
			await stream.WriteAsync(ByteOrderStreamExtensions.SwappedBytes(value), 0, 8, ct).ConfigureAwait(false);
		}
	}
}
