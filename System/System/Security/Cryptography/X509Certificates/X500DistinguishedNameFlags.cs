using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies characteristics of the X.500 distinguished name.</summary>
	// Token: 0x020001B0 RID: 432
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		/// <summary>The distinguished name has no special characteristics.</summary>
		// Token: 0x040007B0 RID: 1968
		None = 0,
		/// <summary>The distinguished name is reversed.</summary>
		// Token: 0x040007B1 RID: 1969
		Reversed = 1,
		/// <summary>The distinguished name uses semicolons.</summary>
		// Token: 0x040007B2 RID: 1970
		UseSemicolons = 16,
		/// <summary>The distinguished name does not use the plus sign.</summary>
		// Token: 0x040007B3 RID: 1971
		DoNotUsePlusSign = 32,
		/// <summary>The distinguished name does not use quotation marks.</summary>
		// Token: 0x040007B4 RID: 1972
		DoNotUseQuotes = 64,
		/// <summary>The distinguished name uses commas.</summary>
		// Token: 0x040007B5 RID: 1973
		UseCommas = 128,
		/// <summary>The distinguished name uses the new line character.</summary>
		// Token: 0x040007B6 RID: 1974
		UseNewLines = 256,
		/// <summary>The distinguished name uses UTF8 encoding instead of Unicode character encoding.</summary>
		// Token: 0x040007B7 RID: 1975
		UseUTF8Encoding = 4096,
		/// <summary>The distinguished name uses T61 encoding.</summary>
		// Token: 0x040007B8 RID: 1976
		UseT61Encoding = 8192,
		/// <summary>Forces the distinguished name to encode specific X.500 keys as UTF-8 strings rather than printable Unicode strings. For more information and the list of X.500 keys affected, see the X500NameFlags enumeration.</summary>
		// Token: 0x040007B9 RID: 1977
		ForceUTF8Encoding = 16384
	}
}
