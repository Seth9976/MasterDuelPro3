using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000234 RID: 564
	internal class RestrictionFacets
	{
		// Token: 0x04000BAD RID: 2989
		internal int Length;

		// Token: 0x04000BAE RID: 2990
		internal int MinLength;

		// Token: 0x04000BAF RID: 2991
		internal int MaxLength;

		// Token: 0x04000BB0 RID: 2992
		internal ArrayList Patterns;

		// Token: 0x04000BB1 RID: 2993
		internal ArrayList Enumeration;

		// Token: 0x04000BB2 RID: 2994
		internal XmlSchemaWhiteSpace WhiteSpace;

		// Token: 0x04000BB3 RID: 2995
		internal object MaxInclusive;

		// Token: 0x04000BB4 RID: 2996
		internal object MaxExclusive;

		// Token: 0x04000BB5 RID: 2997
		internal object MinInclusive;

		// Token: 0x04000BB6 RID: 2998
		internal object MinExclusive;

		// Token: 0x04000BB7 RID: 2999
		internal int TotalDigits;

		// Token: 0x04000BB8 RID: 3000
		internal int FractionDigits;

		// Token: 0x04000BB9 RID: 3001
		internal RestrictionFlags Flags;

		// Token: 0x04000BBA RID: 3002
		internal RestrictionFlags FixedFlags;
	}
}
