using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) anyAttribute element.</summary>
	// Token: 0x020002B9 RID: 697
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the namespaces containing the attributes that can be used.</summary>
		/// <returns>Namespaces for attributes that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x17000789 RID: 1929
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x000BE37F File Offset: 0x000BC57F
		[XmlAttribute("namespace")]
		public string Namespace
		{
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the attributes specified by the anyAttribute element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000BE388 File Offset: 0x000BC588
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x000BE390 File Offset: 0x000BC590
		[DefaultValue(XmlSchemaContentProcessing.None)]
		[XmlAttribute("processContents")]
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processContents;
			}
			set
			{
				this.processContents = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000BE399 File Offset: 0x000BC599
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x000BE3A1 File Offset: 0x000BC5A1
		[XmlIgnore]
		internal XmlSchemaContentProcessing ProcessContentsCorrect
		{
			get
			{
				if (this.processContents != XmlSchemaContentProcessing.None)
				{
					return this.processContents;
				}
				return XmlSchemaContentProcessing.Strict;
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000BE3B3 File Offset: 0x000BC5B3
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000BE3DB File Offset: 0x000BC5DB
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000BE403 File Offset: 0x000BC603
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000BE416 File Offset: 0x000BC616
		internal static bool IsSubset(XmlSchemaAnyAttribute sub, XmlSchemaAnyAttribute super)
		{
			return NamespaceList.IsSubset(sub.NamespaceList, super.NamespaceList);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000BE42C File Offset: 0x000BC62C
		internal static XmlSchemaAnyAttribute Intersection(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Intersection(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					ProcessContents = o1.ProcessContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000BE478 File Offset: 0x000BC678
		internal static XmlSchemaAnyAttribute Union(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Union(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					processContents = o1.processContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x04000EFD RID: 3837
		private string ns;

		// Token: 0x04000EFE RID: 3838
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04000EFF RID: 3839
		private NamespaceList namespaceList;
	}
}
