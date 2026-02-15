using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element with ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). This class is used within complex types that reference a group defined at the schema level.</summary>
	// Token: 0x020002E3 RID: 739
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		/// <summary>Gets or sets the name of a group defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of a group defined in this schema.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000C0389 File Offset: 0x000BE589
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x000C0391 File Offset: 0x000BE591
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes, which holds the post-compilation value of the Particle property.</summary>
		/// <returns>The post-compilation value of the Particle property, which is one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000C03AA File Offset: 0x000BE5AA
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000C03B2 File Offset: 0x000BE5B2
		internal void SetParticle(XmlSchemaGroupBase value)
		{
			this.particle = value;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000C03BB File Offset: 0x000BE5BB
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000C03C3 File Offset: 0x000BE5C3
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.refined;
			}
			set
			{
				this.refined = value;
			}
		}

		// Token: 0x04000F91 RID: 3985
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04000F92 RID: 3986
		private XmlSchemaGroupBase particle;

		// Token: 0x04000F93 RID: 3987
		private XmlSchemaGroup refined;
	}
}
