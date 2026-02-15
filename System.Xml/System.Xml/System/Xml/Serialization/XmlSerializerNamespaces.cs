using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Contains the XML namespaces and prefixes that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> uses to generate qualified names in an XML-document instance.</summary>
	// Token: 0x020001E3 RID: 483
	public class XmlSerializerNamespaces
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		// Token: 0x06001905 RID: 6405 RVA: 0x00002127 File Offset: 0x00000327
		public XmlSerializerNamespaces()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class, using the specified instance of XmlSerializerNamespaces containing the collection of prefix and namespace pairs.</summary>
		/// <param name="namespaces">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" />containing the namespace and prefix pairs. </param>
		// Token: 0x06001906 RID: 6406 RVA: 0x0009603E File Offset: 0x0009423E
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
		{
			this.namespaces = (Hashtable)namespaces.Namespaces.Clone();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		/// <param name="namespaces">An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects. </param>
		// Token: 0x06001907 RID: 6407 RVA: 0x0009605C File Offset: 0x0009425C
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.Add(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
		}

		/// <summary>Adds a prefix and namespace pair to an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <param name="prefix">The prefix associated with an XML namespace. </param>
		/// <param name="ns">An XML namespace. </param>
		// Token: 0x06001908 RID: 6408 RVA: 0x00096093 File Offset: 0x00094293
		public void Add(string prefix, string ns)
		{
			if (prefix != null && prefix.Length > 0)
			{
				XmlConvert.VerifyNCName(prefix);
			}
			if (ns != null && ns.Length > 0)
			{
				XmlConvert.ToUri(ns);
			}
			this.AddInternal(prefix, ns);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000960C3 File Offset: 0x000942C3
		internal void AddInternal(string prefix, string ns)
		{
			this.Namespaces[prefix] = ns;
		}

		/// <summary>Gets the array of prefix and namespace pairs in an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects that are used as qualified names in an XML document.</returns>
		// Token: 0x0600190A RID: 6410 RVA: 0x000960D2 File Offset: 0x000942D2
		public XmlQualifiedName[] ToArray()
		{
			if (this.NamespaceList == null)
			{
				return new XmlQualifiedName[0];
			}
			return (XmlQualifiedName[])this.NamespaceList.ToArray(typeof(XmlQualifiedName));
		}

		/// <summary>Gets the number of prefix and namespace pairs in the collection.</summary>
		/// <returns>The number of prefix and namespace pairs in the collection.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000960FD File Offset: 0x000942FD
		public int Count
		{
			get
			{
				return this.Namespaces.Count;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0009610C File Offset: 0x0009430C
		internal ArrayList NamespaceList
		{
			get
			{
				if (this.namespaces == null || this.namespaces.Count == 0)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.Namespaces.Keys)
				{
					string text = (string)obj;
					arrayList.Add(new XmlQualifiedName(text, (string)this.Namespaces[text]));
				}
				return arrayList;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x000961A0 File Offset: 0x000943A0
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x000961BB File Offset: 0x000943BB
		internal Hashtable Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000961C4 File Offset: 0x000943C4
		internal string LookupPrefix(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return null;
			}
			if (this.namespaces == null || this.namespaces.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.namespaces.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(text) && (string)this.namespaces[text] == ns)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04000A9E RID: 2718
		private Hashtable namespaces;
	}
}
