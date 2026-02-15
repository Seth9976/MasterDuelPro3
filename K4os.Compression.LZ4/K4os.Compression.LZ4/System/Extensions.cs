using System;

namespace System
{
	// Token: 0x02000002 RID: 2
	internal static class Extensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void Validate<T>(this T[] buffer, int offset, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "cannot be null");
			}
			if (offset < 0 || length < 0 || offset + length > buffer.Length)
			{
				throw new ArgumentException(string.Format("invalid offset/length combination: {0}/{1}", offset, length));
			}
		}
	}
}
