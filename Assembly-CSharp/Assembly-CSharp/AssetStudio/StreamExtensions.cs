using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200014C RID: 332
	public static class StreamExtensions
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00014F04 File Offset: 0x00013104
		public static void CopyTo(this Stream source, Stream destination, long size)
		{
			byte[] buffer = new byte[81920];
			for (long left = size; left > 0L; left -= 81920L)
			{
				int toRead = ((81920L < left) ? 81920 : ((int)left));
				int read = source.Read(buffer, 0, toRead);
				destination.Write(buffer, 0, read);
				if (read != toRead)
				{
					return;
				}
			}
		}

		// Token: 0x04000921 RID: 2337
		private const int BufferSize = 81920;
	}
}
