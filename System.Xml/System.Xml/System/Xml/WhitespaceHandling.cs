using System;

namespace System.Xml
{
	/// <summary>Specifies how white space is handled.</summary>
	// Token: 0x02000046 RID: 70
	public enum WhitespaceHandling
	{
		/// <summary>Return Whitespace and SignificantWhitespace nodes. This is the default.</summary>
		// Token: 0x0400016F RID: 367
		All,
		/// <summary>Return SignificantWhitespace nodes only.</summary>
		// Token: 0x04000170 RID: 368
		Significant,
		/// <summary>Return no Whitespace and no SignificantWhitespace nodes.</summary>
		// Token: 0x04000171 RID: 369
		None
	}
}
