using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines groups at the schema level that are referenced from the complex types. It groups a set of element declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x020002E1 RID: 737
	public class XmlSchemaGroup : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the schema group.</summary>
		/// <returns>The name of the schema group.</returns>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000C02AA File Offset: 0x000BE4AA
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x000C02B2 File Offset: 0x000BE4B2
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x000C02BB File Offset: 0x000BE4BB
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x000C02C3 File Offset: 0x000BE4C3
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the qualified name of the schema group.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> object representing the qualified name of the schema group.</returns>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x000C02CC File Offset: 0x000BE4CC
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000C02D4 File Offset: 0x000BE4D4
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x000C02DC File Offset: 0x000BE4DC
		[XmlIgnore]
		internal XmlSchemaParticle CanonicalParticle
		{
			get
			{
				return this.canonicalParticle;
			}
			set
			{
				this.canonicalParticle = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000C02E5 File Offset: 0x000BE4E5
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x000C02ED File Offset: 0x000BE4ED
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000C02F6 File Offset: 0x000BE4F6
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x000C02FE File Offset: 0x000BE4FE
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x000C0307 File Offset: 0x000BE507
		// (set) Token: 0x0600214C RID: 8524 RVA: 0x000C030F File Offset: 0x000BE50F
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

		// Token: 0x0600214D RID: 8525 RVA: 0x000C0318 File Offset: 0x000BE518
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000C0321 File Offset: 0x000BE521
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000C032C File Offset: 0x000BE52C
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasParticleRef(this.particle, parentSchema))
			{
				xmlSchemaGroup.particle = XmlSchemaComplexType.CloneParticle(this.particle, parentSchema) as XmlSchemaGroupBase;
			}
			xmlSchemaGroup.canonicalParticle = XmlSchemaParticle.Empty;
			return xmlSchemaGroup;
		}

		// Token: 0x04000F8B RID: 3979
		private string name;

		// Token: 0x04000F8C RID: 3980
		private XmlSchemaGroupBase particle;

		// Token: 0x04000F8D RID: 3981
		private XmlSchemaParticle canonicalParticle;

		// Token: 0x04000F8E RID: 3982
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04000F8F RID: 3983
		private XmlSchemaGroup redefined;

		// Token: 0x04000F90 RID: 3984
		private int selfReferenceCount;
	}
}
