using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the XPath node types that can be returned from the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x02000141 RID: 321
	public enum XPathNodeType
	{
		/// <summary>The root node of the XML document or node tree.</summary>
		// Token: 0x040007A8 RID: 1960
		Root,
		/// <summary>An element, such as &lt;element&gt;.</summary>
		// Token: 0x040007A9 RID: 1961
		Element,
		/// <summary>An attribute, such as id='123'.</summary>
		// Token: 0x040007AA RID: 1962
		Attribute,
		/// <summary>A namespace, such as xmlns="namespace".</summary>
		// Token: 0x040007AB RID: 1963
		Namespace,
		/// <summary>The text content of a node. Equivalent to the Document Object Model (DOM) Text and CDATA node types. Contains at least one character.</summary>
		// Token: 0x040007AC RID: 1964
		Text,
		/// <summary>A node with white space characters and xml:space set to preserve.</summary>
		// Token: 0x040007AD RID: 1965
		SignificantWhitespace,
		/// <summary>A node with only white space characters and no significant white space. White space characters are #x20, #x9, #xD, or #xA.</summary>
		// Token: 0x040007AE RID: 1966
		Whitespace,
		/// <summary>A processing instruction, such as &lt;?pi test?&gt;. This does not include XML declarations, which are not visible to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class. </summary>
		// Token: 0x040007AF RID: 1967
		ProcessingInstruction,
		/// <summary>A comment, such as &lt;!-- my comment --&gt;</summary>
		// Token: 0x040007B0 RID: 1968
		Comment,
		/// <summary>Any of the <see cref="T:System.Xml.XPath.XPathNodeType" /> node types.</summary>
		// Token: 0x040007B1 RID: 1969
		All
	}
}
