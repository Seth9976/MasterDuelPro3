using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Class for the identity constraints: key, keyref, and unique elements.</summary>
	// Token: 0x020002E4 RID: 740
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the identity constraint.</summary>
		/// <returns>The name of the identity constraint.</returns>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000C03DF File Offset: 0x000BE5DF
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x000C03E7 File Offset: 0x000BE5E7
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

		/// <summary>Gets or sets the XPath expression selector element.</summary>
		/// <returns>The XPath expression selector element.</returns>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x000C03F0 File Offset: 0x000BE5F0
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x000C03F8 File Offset: 0x000BE5F8
		[XmlElement("selector", typeof(XmlSchemaXPath))]
		public XmlSchemaXPath Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		/// <summary>Gets the collection of fields that apply as children for the XML Path Language (XPath) expression selector.</summary>
		/// <returns>The collection of fields.</returns>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x000C0401 File Offset: 0x000BE601
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		/// <summary>Gets the qualified name of the identity constraint, which holds the post-compilation value of the QualifiedName property.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000C0409 File Offset: 0x000BE609
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000C0411 File Offset: 0x000BE611
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000C041A File Offset: 0x000BE61A
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x000C0422 File Offset: 0x000BE622
		[XmlIgnore]
		internal CompiledIdentityConstraint CompiledConstraint
		{
			get
			{
				return this.compiledConstraint;
			}
			set
			{
				this.compiledConstraint = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000C042B File Offset: 0x000BE62B
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x000C0433 File Offset: 0x000BE633
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

		// Token: 0x04000F94 RID: 3988
		private string name;

		// Token: 0x04000F95 RID: 3989
		private XmlSchemaXPath selector;

		// Token: 0x04000F96 RID: 3990
		private XmlSchemaObjectCollection fields = new XmlSchemaObjectCollection();

		// Token: 0x04000F97 RID: 3991
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04000F98 RID: 3992
		private CompiledIdentityConstraint compiledConstraint;
	}
}
