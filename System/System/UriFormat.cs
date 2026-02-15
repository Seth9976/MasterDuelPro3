using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FA RID: 250
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x0400041A RID: 1050
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x0400041B RID: 1051
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped. See Remarks.</summary>
		// Token: 0x0400041C RID: 1052
		SafeUnescaped
	}
}
