using System;

namespace System.Xml.XPath
{
	/// <summary>Specifies the return type of the XPath expression.</summary>
	// Token: 0x02000139 RID: 313
	public enum XPathResultType
	{
		/// <summary>A numeric value.</summary>
		// Token: 0x04000794 RID: 1940
		Number,
		/// <summary>A <see cref="T:System.String" /> value.</summary>
		// Token: 0x04000795 RID: 1941
		String,
		/// <summary>A <see cref="T:System.Boolean" />true or false value.</summary>
		// Token: 0x04000796 RID: 1942
		Boolean,
		/// <summary>A node collection.</summary>
		// Token: 0x04000797 RID: 1943
		NodeSet,
		/// <summary>A tree fragment.</summary>
		// Token: 0x04000798 RID: 1944
		Navigator = 1,
		/// <summary>Any of the XPath node types.</summary>
		// Token: 0x04000799 RID: 1945
		Any = 5,
		/// <summary>The expression does not evaluate to the correct XPath type.</summary>
		// Token: 0x0400079A RID: 1946
		Error
	}
}
