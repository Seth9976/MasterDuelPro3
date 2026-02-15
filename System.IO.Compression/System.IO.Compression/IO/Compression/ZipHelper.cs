using System;

namespace System.IO.Compression
{
	// Token: 0x0200002C RID: 44
	internal static class ZipHelper
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00007FB4 File Offset: 0x000061B4
		internal static bool RequiresUnicode(string test)
		{
			foreach (char c in test)
			{
				if (c > '~' || c < ' ')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007FEC File Offset: 0x000061EC
		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int i = bytesToRead;
			int num = 0;
			while (i > 0)
			{
				int num2 = stream.Read(buffer, num, i);
				if (num2 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
				num += num2;
				i -= num2;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008024 File Offset: 0x00006224
		internal static DateTime DosTimeToDateTime(uint dateTime)
		{
			int num = (int)(1980U + (dateTime >> 25));
			int num2 = (int)((dateTime >> 21) & 15U);
			int num3 = (int)((dateTime >> 16) & 31U);
			int num4 = (int)((dateTime >> 11) & 31U);
			int num5 = (int)((dateTime >> 5) & 63U);
			int num6 = (int)((dateTime & 31U) * 2U);
			DateTime dateTime2;
			try
			{
				dateTime2 = new DateTime(num, num2, num3, num4, num5, num6, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime2 = ZipHelper.s_invalidDateIndicator;
			}
			catch (ArgumentException)
			{
				dateTime2 = ZipHelper.s_invalidDateIndicator;
			}
			return dateTime2;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000080A8 File Offset: 0x000062A8
		internal static uint DateTimeToDosTime(DateTime dateTime)
		{
			return (uint)((((((((dateTime.Year - 1980) & 127) << 4) + dateTime.Month << 5) + dateTime.Day << 5) + dateTime.Hour << 6) + dateTime.Minute << 5) + dateTime.Second / 2);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000080FC File Offset: 0x000062FC
		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int num = 0;
			uint num2 = 0U;
			byte[] array = new byte[32];
			bool flag = false;
			bool flag2 = false;
			while (!flag2 && !flag)
			{
				flag = ZipHelper.SeekBackwardsAndRead(stream, array, out num);
				while (num >= 0 && !flag2)
				{
					num2 = (num2 << 8) | (uint)array[num];
					if (num2 == signatureToFind)
					{
						flag2 = true;
					}
					else
					{
						num--;
					}
				}
			}
			if (!flag2)
			{
				return false;
			}
			stream.Seek((long)num, SeekOrigin.Current);
			return true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008160 File Offset: 0x00006360
		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			int num3;
			for (long num = position - stream.Position; num != 0L; num -= (long)num3)
			{
				int num2 = ((num > 64L) ? 64 : ((int)num));
				num3 = stream.Read(new byte[64], 0, num2);
				if (num3 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000081AC File Offset: 0x000063AC
		private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
		{
			if (stream.Position >= (long)buffer.Length)
			{
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				ZipHelper.ReadBytes(stream, buffer, buffer.Length);
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				bufferPointer = buffer.Length - 1;
				return false;
			}
			int num = (int)stream.Position;
			stream.Seek(0L, SeekOrigin.Begin);
			ZipHelper.ReadBytes(stream, buffer, num);
			stream.Seek(0L, SeekOrigin.Begin);
			bufferPointer = num - 1;
			return true;
		}

		// Token: 0x04000123 RID: 291
		private static readonly DateTime s_invalidDateIndicator = new DateTime(1980, 1, 1, 0, 0, 0);
	}
}
