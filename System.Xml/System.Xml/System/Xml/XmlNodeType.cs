using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node.</summary>
	// Token: 0x0200012E RID: 302
	public enum XmlNodeType
	{
		/// <summary>This is returned by the <see cref="T:System.Xml.XmlReader" /> if a Read method has not been called.</summary>
		// Token: 0x04000765 RID: 1893
		None,
		/// <summary>An element (for example, &lt;item&gt; ).</summary>
		// Token: 0x04000766 RID: 1894
		Element,
		/// <summary>An attribute (for example, id='123' ).</summary>
		// Token: 0x04000767 RID: 1895
		Attribute,
		/// <summary>The text content of a node.</summary>
		// Token: 0x04000768 RID: 1896
		Text,
		/// <summary>A CDATA section (for example, &lt;![CDATA[my escaped text]]&gt; ).</summary>
		// Token: 0x04000769 RID: 1897
		CDATA,
		/// <summary>A reference to an entity (for example, &amp;num; ).</summary>
		// Token: 0x0400076A RID: 1898
		EntityReference,
		/// <summary>An entity declaration (for example, &lt;!ENTITY...&gt; ).</summary>
		// Token: 0x0400076B RID: 1899
		Entity,
		/// <summary>A processing instruction (for example, &lt;?pi test?&gt; ).</summary>
		// Token: 0x0400076C RID: 1900
		ProcessingInstruction,
		/// <summary>A comment (for example, &lt;!-- my comment --&gt; ).</summary>
		// Token: 0x0400076D RID: 1901
		Comment,
		/// <summary>A document object that, as the root of the document tree, provides access to the entire XML document.</summary>
		// Token: 0x0400076E RID: 1902
		Document,
		/// <summary>The document type declaration, indicated by the following tag (for example, &lt;!DOCTYPE...&gt; ).</summary>
		// Token: 0x0400076F RID: 1903
		DocumentType,
		/// <summary>A document fragment.</summary>
		// Token: 0x04000770 RID: 1904
		DocumentFragment,
		/// <summary>A notation in the document type declaration (for example, &lt;!NOTATION...&gt; ).</summary>
		// Token: 0x04000771 RID: 1905
		Notation,
		/// <summary>White space between markup.</summary>
		// Token: 0x04000772 RID: 1906
		Whitespace,
		/// <summary>White space between markup in a mixed content model or white space within the xml:space="preserve" scope.</summary>
		// Token: 0x04000773 RID: 1907
		SignificantWhitespace,
		/// <summary>An end element tag (for example, &lt;/item&gt; ).</summary>
		// Token: 0x04000774 RID: 1908
		EndElement,
		/// <summary>Returned when XmlReader gets to the end of the entity replacement as a result of a call to <see cref="M:System.Xml.XmlReader.ResolveEntity" />.</summary>
		// Token: 0x04000775 RID: 1909
		EndEntity,
		/// <summary>The XML declaration (for example, &lt;?xml version='1.0'?&gt; ).</summary>
		// Token: 0x04000776 RID: 1910
		XmlDeclaration
	}
}
