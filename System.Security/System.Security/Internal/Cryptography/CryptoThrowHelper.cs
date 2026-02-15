using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x02000009 RID: 9
	internal static class CryptoThrowHelper
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002334 File Offset: 0x00000534
		public static CryptographicException ToCryptographicException(this int hr)
		{
			string message = global::Interop.Kernel32.GetMessage(hr);
			return new CryptoThrowHelper.WindowsCryptographicException(hr, message);
		}

		// Token: 0x0200000A RID: 10
		private sealed class WindowsCryptographicException : CryptographicException
		{
			// Token: 0x0600000E RID: 14 RVA: 0x0000234F File Offset: 0x0000054F
			public WindowsCryptographicException(int hr, string message)
				: base(message)
			{
				base.HResult = hr;
			}
		}
	}
}
