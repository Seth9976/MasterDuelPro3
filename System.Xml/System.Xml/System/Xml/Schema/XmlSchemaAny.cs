using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) any element.</summary>
	// Token: 0x020002B8 RID: 696
	public class XmlSchemaAny : XmlSchemaParticle
	{
		/// <summary>Gets or sets the namespaces containing the elements that can be used.</summary>
		/// <returns>Namespaces for elements that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x000BE1E0 File Offset: 0x000BC3E0
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x000BE1E8 File Offset: 0x000BC3E8
		[XmlAttribute("namespace")]
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the elements specified by the any element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x000BE1F1 File Offset: 0x000BC3F1
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x000BE1F9 File Offset: 0x000BC3F9
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

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000BE202 File Offset: 0x000BC402
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x000BE20A File Offset: 0x000BC40A
		[XmlIgnore]
		internal string ResolvedNamespace
		{
			get
			{
				if (this.ns == null || this.ns.Length == 0)
				{
					return "##any";
				}
				return this.ns;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000BE22D File Offset: 0x000BC42D
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000BE240 File Offset: 0x000BC440
		internal override string NameString
		{
			get
			{
				switch (this.namespaceList.Type)
				{
				case NamespaceList.ListType.Any:
					return "##any:*";
				case NamespaceList.ListType.Other:
					return "##other:*";
				case NamespaceList.ListType.Set:
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 1;
					foreach (object obj in this.namespaceList.Enumerate)
					{
						string text = (string)obj;
						stringBuilder.Append(text + ":*");
						if (num < this.namespaceList.Enumerate.Count)
						{
							stringBuilder.Append(" ");
						}
						num++;
					}
					return stringBuilder.ToString();
				}
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000BE314 File Offset: 0x000BC514
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000BE33C File Offset: 0x000BC53C
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000BE364 File Offset: 0x000BC564
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x04000EFA RID: 3834
		private string ns;

		// Token: 0x04000EFB RID: 3835
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04000EFC RID: 3836
		private NamespaceList namespaceList;
	}
}
