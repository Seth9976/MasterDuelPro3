using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x02000011 RID: 17
	internal static class ZipHelper
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000031CC File Offset: 0x000013CC
		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int bytesLeftToRead = bytesToRead;
			int totalBytesRead = 0;
			while (bytesLeftToRead > 0)
			{
				int bytesRead = stream.Read(buffer, totalBytesRead, bytesLeftToRead);
				if (bytesRead == 0)
				{
					throw new IOException();
				}
				totalBytesRead += bytesRead;
				bytesLeftToRead -= bytesRead;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003200 File Offset: 0x00001400
		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int bufferPointer = 0;
			uint currentSignature = 0U;
			byte[] buffer = new byte[32];
			bool outOfBytes = false;
			bool signatureFound = false;
			while (!signatureFound && !outOfBytes)
			{
				outOfBytes = ZipHelper.SeekBackwardsAndRead(stream, buffer, out bufferPointer);
				while (bufferPointer >= 0 && !signatureFound)
				{
					currentSignature = (currentSignature << 8) | (uint)buffer[bufferPointer];
					if (currentSignature == signatureToFind)
					{
						signatureFound = true;
					}
					else
					{
						bufferPointer--;
					}
				}
			}
			if (!signatureFound)
			{
				return false;
			}
			stream.Seek((long)bufferPointer, SeekOrigin.Current);
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003264 File Offset: 0x00001464
		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			int numBytesActuallySkipped;
			for (long numBytesLeft = position - stream.Position; numBytesLeft != 0L; numBytesLeft -= (long)numBytesActuallySkipped)
			{
				int numBytesToSkip = ((numBytesLeft > 64L) ? 64 : ((int)numBytesLeft));
				numBytesActuallySkipped = stream.Read(new byte[64], 0, numBytesToSkip);
				if (numBytesActuallySkipped == 0)
				{
					throw new IOException();
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000032AC File Offset: 0x000014AC
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
			int bytesToRead = (int)stream.Position;
			stream.Seek(0L, SeekOrigin.Begin);
			ZipHelper.ReadBytes(stream, buffer, bytesToRead);
			stream.Seek(0L, SeekOrigin.Begin);
			bufferPointer = bytesToRead - 1;
			return true;
		}

		// Token: 0x04000054 RID: 84
		internal const uint Mask32Bit = 4294967295U;

		// Token: 0x04000055 RID: 85
		internal const ushort Mask16Bit = 65535;

		// Token: 0x04000056 RID: 86
		private const int BackwardsSeekingBufferSize = 32;
	}
}
