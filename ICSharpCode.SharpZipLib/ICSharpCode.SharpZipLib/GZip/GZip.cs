using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x0200008E RID: 142
	public static class GZip
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x00017D3C File Offset: 0x00015F3C
		public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream", "Input stream is null");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream", "Output stream is null");
			}
			try
			{
				using (GZipInputStream gzipInputStream = new GZipInputStream(inStream))
				{
					gzipInputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(gzipInputStream, outStream, new byte[4096]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					outStream.Dispose();
				}
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00017DC4 File Offset: 0x00015FC4
		public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int bufferSize = 512, int level = 6)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream", "Input stream is null");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream", "Output stream is null");
			}
			if (bufferSize < 512)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Deflate buffer size must be >= 512");
			}
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level", "Compression level must be 0-9");
			}
			try
			{
				using (GZipOutputStream gzipOutputStream = new GZipOutputStream(outStream, bufferSize))
				{
					gzipOutputStream.SetLevel(level);
					gzipOutputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(inStream, gzipOutputStream, new byte[bufferSize]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					inStream.Dispose();
				}
			}
		}
	}
}
