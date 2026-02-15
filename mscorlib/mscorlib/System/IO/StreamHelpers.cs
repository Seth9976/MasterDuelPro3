using System;

namespace System.IO
{
	// Token: 0x020007A2 RID: 1954
	internal static class StreamHelpers
	{
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000EDC38 File Offset: 0x000EBE38
		public static void ValidateCopyToArgs(Stream source, Stream destination, int bufferSize)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "Positive number required.");
			}
			bool canRead = source.CanRead;
			if (!canRead && !source.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
			bool canWrite = destination.CanWrite;
			if (!canWrite && !destination.CanRead)
			{
				throw new ObjectDisposedException("destination", "Cannot access a closed Stream.");
			}
			if (!canRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
			if (!canWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
		}
	}
}
