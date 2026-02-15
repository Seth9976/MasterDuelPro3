using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x020000C7 RID: 199
	public static class BZip2
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001BA04 File Offset: 0x00019C04
		public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			try
			{
				using (BZip2InputStream bzip2InputStream = new BZip2InputStream(inStream))
				{
					bzip2InputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(bzip2InputStream, outStream, new byte[4096]);
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

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001BA80 File Offset: 0x00019C80
		public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			try
			{
				using (BZip2OutputStream bzip2OutputStream = new BZip2OutputStream(outStream, level))
				{
					bzip2OutputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(inStream, bzip2OutputStream, new byte[4096]);
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
