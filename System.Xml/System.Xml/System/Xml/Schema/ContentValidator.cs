using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000229 RID: 553
	internal class ContentValidator
	{
		// Token: 0x06001ACE RID: 6862 RVA: 0x0009ADEC File Offset: 0x00098FEC
		public ContentValidator(XmlSchemaContentType contentType)
		{
			this.contentType = contentType;
			this.isEmptiable = true;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0009AE02 File Offset: 0x00099002
		protected ContentValidator(XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
		{
			this.contentType = contentType;
			this.isOpen = isOpen;
			this.isEmptiable = isEmptiable;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0009AE1F File Offset: 0x0009901F
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0009AE27 File Offset: 0x00099027
		public bool PreserveWhitespace
		{
			get
			{
				return this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Mixed;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x0009AE3C File Offset: 0x0009903C
		public virtual bool IsEmptiable
		{
			get
			{
				return this.isEmptiable;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0009AE44 File Offset: 0x00099044
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x0009AE5F File Offset: 0x0009905F
		public bool IsOpen
		{
			get
			{
				return this.contentType != XmlSchemaContentType.TextOnly && this.contentType != XmlSchemaContentType.Empty && this.isOpen;
			}
			set
			{
				this.isOpen = value;
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void InitValidation(ValidationState context)
		{
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009AE68 File Offset: 0x00099068
		public virtual object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			if (this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Empty)
			{
				context.NeedValidateChildren = false;
			}
			errorCode = -1;
			return null;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public virtual bool CompleteValidation(ValidationState context)
		{
			return true;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			return null;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0009AE86 File Offset: 0x00099086
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles)
		{
			ContentValidator.AddParticleToExpected(p, schemaSet, particles, false);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0009AE94 File Offset: 0x00099094
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles, bool global)
		{
			if (!particles.Contains(p))
			{
				particles.Add(p);
			}
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null && (global || !xmlSchemaElement.RefName.IsEmpty))
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)schemaSet.SubstitutionGroups[xmlSchemaElement.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i];
						if (!xmlSchemaElement.QualifiedName.Equals(xmlSchemaElement2.QualifiedName) && !particles.Contains(xmlSchemaElement2))
						{
							particles.Add(xmlSchemaElement2);
						}
					}
				}
			}
		}

		// Token: 0x04000B74 RID: 2932
		private XmlSchemaContentType contentType;

		// Token: 0x04000B75 RID: 2933
		private bool isOpen;

		// Token: 0x04000B76 RID: 2934
		private bool isEmptiable;

		// Token: 0x04000B77 RID: 2935
		public static readonly ContentValidator Empty = new ContentValidator(XmlSchemaContentType.Empty);

		// Token: 0x04000B78 RID: 2936
		public static readonly ContentValidator TextOnly = new ContentValidator(XmlSchemaContentType.TextOnly, false, false);

		// Token: 0x04000B79 RID: 2937
		public static readonly ContentValidator Mixed = new ContentValidator(XmlSchemaContentType.Mixed);

		// Token: 0x04000B7A RID: 2938
		public static readonly ContentValidator Any = new ContentValidator(XmlSchemaContentType.Mixed, true, true);
	}
}
