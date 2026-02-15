using System;

namespace System
{
	/// <summary>Specifies whether relevant <see cref="Overload:System.Convert.ToBase64CharArray" /> and <see cref="Overload:System.Convert.ToBase64String" /> methods insert line breaks in their output. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000D1 RID: 209
	[Flags]
	public enum Base64FormattingOptions
	{
		/// <summary>Does not insert line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x040002EC RID: 748
		None = 0,
		/// <summary>Inserts line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x040002ED RID: 749
		InsertLineBreaks = 1
	}
}
