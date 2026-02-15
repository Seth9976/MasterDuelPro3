using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) selector element.</summary>
	// Token: 0x020002E5 RID: 741
	public class XmlSchemaXPath : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the attribute for the XPath expression.</summary>
		/// <returns>The string attribute value for the XPath expression.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000C045A File Offset: 0x000BE65A
		// (set) Token: 0x06002168 RID: 8552 RVA: 0x000C0462 File Offset: 0x000BE662
		[XmlAttribute("xpath")]
		[DefaultValue("")]
		public string XPath
		{
			get
			{
				return this.xpath;
			}
			set
			{
				this.xpath = value;
			}
		}

		// Token: 0x04000F99 RID: 3993
		private string xpath;
	}
}
