using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the notation element from XML Schema as specified by the World Wide Web Consortium (W3C). An XML Schema notation declaration is a reconstruction of XML 1.0 NOTATION declarations. The purpose of notations is to describe the format of non-XML data within an XML document.</summary>
	// Token: 0x020002EC RID: 748
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the notation.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000C0655 File Offset: 0x000BE855
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x000C065D File Offset: 0x000BE85D
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier. The value must be a valid Uniform Resource Identifier (URI).</returns>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000C0666 File Offset: 0x000BE866
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x000C066E File Offset: 0x000BE86E
		[XmlAttribute("public")]
		public string Public
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier. The value must be a valid URI.</returns>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000C0677 File Offset: 0x000BE877
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x000C067F File Offset: 0x000BE87F
		[XmlAttribute("system")]
		public string System
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000C0688 File Offset: 0x000BE888
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x000C0690 File Offset: 0x000BE890
		[XmlIgnore]
		internal XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
			set
			{
				this.qname = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000C0699 File Offset: 0x000BE899
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x000C06A1 File Offset: 0x000BE8A1
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x04000FA6 RID: 4006
		private string name;

		// Token: 0x04000FA7 RID: 4007
		private string publicId;

		// Token: 0x04000FA8 RID: 4008
		private string systemId;

		// Token: 0x04000FA9 RID: 4009
		private XmlQualifiedName qname = XmlQualifiedName.Empty;
	}
}
