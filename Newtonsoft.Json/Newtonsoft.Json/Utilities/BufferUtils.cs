using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CF RID: 207
	[NullableContext(2)]
	[Nullable(0)]
	internal static class BufferUtils
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x000209EF File Offset: 0x0001EBEF
		[NullableContext(1)]
		public static char[] RentBuffer([Nullable(2)] IArrayPool<char> bufferPool, int minSize)
		{
			if (bufferPool == null)
			{
				return new char[minSize];
			}
			return bufferPool.Rent(minSize);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00020A02 File Offset: 0x0001EC02
		public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer)
		{
			if (bufferPool != null)
			{
				bufferPool.Return(buffer);
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00020A0E File Offset: 0x0001EC0E
		[return: Nullable(1)]
		public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer)
		{
			if (bufferPool == null)
			{
				return new char[size];
			}
			if (buffer != null)
			{
				bufferPool.Return(buffer);
			}
			return bufferPool.Rent(size);
		}
	}
}
