using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200037B RID: 891
	public static class CryptographicOperations
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x0007BBCF File Offset: 0x00079DCF
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
