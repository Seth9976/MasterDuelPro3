using System;

namespace System
{
	/// <summary>Specifies the culture, case, and sort rules to be used by certain overloads of the <see cref="M:System.String.Compare(System.String,System.String)" /> and <see cref="M:System.String.Equals(System.Object)" /> methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014A RID: 330
	public enum StringComparison
	{
		/// <summary>Compare strings using culture-sensitive sort rules and the current culture.</summary>
		// Token: 0x0400048A RID: 1162
		CurrentCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x0400048B RID: 1163
		CurrentCultureIgnoreCase,
		/// <summary>Compare strings using culture-sensitive sort rules and the invariant culture.</summary>
		// Token: 0x0400048C RID: 1164
		InvariantCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x0400048D RID: 1165
		InvariantCultureIgnoreCase,
		/// <summary>Compare strings using ordinal sort rules.</summary>
		// Token: 0x0400048E RID: 1166
		Ordinal,
		/// <summary>Compare strings using ordinal sort rules and ignoring the case of the strings being compared.</summary>
		// Token: 0x0400048F RID: 1167
		OrdinalIgnoreCase
	}
}
