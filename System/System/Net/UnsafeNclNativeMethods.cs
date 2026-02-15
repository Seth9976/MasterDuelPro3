using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
	// Token: 0x020003F2 RID: 1010
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x020003F3 RID: 1011
		internal static class HttpApi
		{
			// Token: 0x0400100B RID: 4107
			private static string[] m_Strings = new string[]
			{
				"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
				"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
				"Accept-Ranges", "Age", "ETag", "Location", "Proxy-Authenticate", "Retry-After", "Server", "Set-Cookie", "Vary", "WWW-Authenticate"
			};

			// Token: 0x020003F4 RID: 1012
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x06001944 RID: 6468 RVA: 0x0006C02E File Offset: 0x0006A22E
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x0400100C RID: 4108
				private static string[] m_Strings = new string[]
				{
					"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
					"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
					"Accept", "Accept-Charset", "Accept-Encoding", "Accept-Language", "Authorization", "Cookie", "Expect", "From", "Host", "If-Match",
					"If-Modified-Since", "If-None-Match", "If-Range", "If-Unmodified-Since", "Max-Forwards", "Proxy-Authorization", "Referer", "Range", "Te", "Translate",
					"User-Agent"
				};
			}
		}

		// Token: 0x020003F5 RID: 1013
		internal static class SecureStringHelper
		{
			// Token: 0x06001946 RID: 6470 RVA: 0x0006C1BC File Offset: 0x0006A3BC
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string text;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					text = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
					}
				}
				return text;
			}

			// Token: 0x06001947 RID: 6471 RVA: 0x0006C218 File Offset: 0x0006A418
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString secureString;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					secureString = new SecureString(ptr, plainString.Length);
				}
				return secureString;
			}
		}
	}
}
