using System;

namespace System.Security.Cryptography
{
	// Token: 0x020003C9 RID: 969
	internal static class CryptoConfigForwarder
	{
		// Token: 0x060020FE RID: 8446 RVA: 0x00089BBE File Offset: 0x00087DBE
		internal static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name);
		}
	}
}
