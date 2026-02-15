using System;

namespace System
{
	/// <summary>Specifies whether applicable <see cref="Overload:System.String.Split" /> method overloads include or omit empty substrings from the return value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014B RID: 331
	[Flags]
	public enum StringSplitOptions
	{
		/// <summary>The return value includes array elements that contain an empty string</summary>
		// Token: 0x04000491 RID: 1169
		None = 0,
		/// <summary>The return value does not include array elements that contain an empty string</summary>
		// Token: 0x04000492 RID: 1170
		RemoveEmptyEntries = 1
	}
}
