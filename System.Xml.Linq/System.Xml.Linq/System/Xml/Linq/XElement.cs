using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML element.</summary>
	// Token: 0x0200000F RID: 15
	[XmlSchemaProvider(null, IsAny = true)]
	public class XElement : XContainer, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name. </summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		// Token: 0x06000061 RID: 97 RVA: 0x0000398B File Offset: 0x00001B8B
		public XElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from another <see cref="T:System.Xml.Linq.XElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XElement" /> object to copy from.</param>
		// Token: 0x06000062 RID: 98 RVA: 0x000039B0 File Offset: 0x00001BB0
		public XElement(XElement other)
			: base(other)
		{
			this.name = other.name;
			XAttribute next = other.lastAttr;
			if (next != null)
			{
				do
				{
					next = next.next;
					this.AppendAttributeSkipNotify(new XAttribute(next));
				}
				while (next != other.lastAttr);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from an <see cref="T:System.Xml.Linq.XStreamingElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement" /> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement" />.</param>
		// Token: 0x06000063 RID: 99 RVA: 0x000039F6 File Offset: 0x00001BF6
		public XElement(XStreamingElement other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			base.AddContentSkipNotify(other.content);
		}

		/// <summary>Gets a value indicating whether this element as at least one attribute.</summary>
		/// <returns>true if this element has at least one attribute; otherwise false.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A24 File Offset: 0x00001C24
		public bool HasAttributes
		{
			get
			{
				return this.lastAttr != null;
			}
		}

		/// <summary>Gets a value indicating whether this element contains no content.</summary>
		/// <returns>true if this element contains no content; otherwise false.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003A2F File Offset: 0x00001C2F
		public bool IsEmpty
		{
			get
			{
				return this.content == null;
			}
		}

		/// <summary>Gets or sets the name of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this element.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003A3A File Offset: 0x00001C3A
		public XName Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XElement" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Element" />.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003A42 File Offset: 0x00001C42
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		/// <summary>Gets or sets the concatenated text contents of this element.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains all of the text content of this element. If there are multiple text nodes, they will be concatenated.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003A48 File Offset: 0x00001C48
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003A89 File Offset: 0x00001C89
		public string Value
		{
			get
			{
				if (this.content == null)
				{
					return string.Empty;
				}
				string text = this.content as string;
				if (text != null)
				{
					return text;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				this.AppendText(stringBuilder);
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.RemoveNodes();
				base.Add(value);
			}
		}

		/// <summary>Returns the <see cref="T:System.Xml.Linq.XAttribute" /> of this <see cref="T:System.Xml.Linq.XElement" /> that has the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that has the specified <see cref="T:System.Xml.Linq.XName" />; null if there is no attribute with the specified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the <see cref="T:System.Xml.Linq.XAttribute" /> to get.</param>
		// Token: 0x0600006A RID: 106 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public XAttribute Attribute(XName name)
		{
			XAttribute next = this.lastAttr;
			if (next != null)
			{
				for (;;)
				{
					next = next.next;
					if (next.name == name)
					{
						break;
					}
					if (next == this.lastAttr)
					{
						goto IL_002A;
					}
				}
				return next;
			}
			IL_002A:
			return null;
		}

		/// <summary>Returns a collection of attributes of this element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> of attributes of this element.</returns>
		// Token: 0x0600006B RID: 107 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public IEnumerable<XAttribute> Attributes()
		{
			return this.GetAttributes(null);
		}

		/// <summary>Gets the prefix associated with a namespace for this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix.</returns>
		/// <param name="ns">An <see cref="T:System.Xml.Linq.XNamespace" /> to look up.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600006C RID: 108 RVA: 0x00003AEC File Offset: 0x00001CEC
		public string GetPrefixOfNamespace(XNamespace ns)
		{
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			string namespaceName = ns.NamespaceName;
			bool flag = false;
			XElement xelement = this;
			XAttribute next;
			for (;;)
			{
				next = xelement.lastAttr;
				if (next != null)
				{
					bool flag2 = false;
					do
					{
						next = next.next;
						if (next.IsNamespaceDeclaration)
						{
							if (next.Value == namespaceName && next.Name.NamespaceName.Length != 0 && (!flag || this.GetNamespaceOfPrefixInScope(next.Name.LocalName, xelement) == null))
							{
								goto IL_0072;
							}
							flag2 = true;
						}
					}
					while (next != xelement.lastAttr);
					flag = flag || flag2;
				}
				xelement = xelement.parent as XElement;
				if (xelement == null)
				{
					goto Block_8;
				}
			}
			IL_0072:
			return next.Name.LocalName;
			Block_8:
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				if (!flag || this.GetNamespaceOfPrefixInScope("xml", null) == null)
				{
					return "xml";
				}
			}
			else if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		/// <summary>Write this element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600006D RID: 109 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new ElementWriter(writer).WriteElement(this);
		}

		/// <summary>Gets an XML schema definition that describes the XML representation of this object.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x0600006E RID: 110 RVA: 0x00003BF2 File Offset: 0x00001DF2
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>Generates an object from its XML representation.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which the object is deserialized.</param>
		// Token: 0x0600006F RID: 111 RVA: 0x00003BF8 File Offset: 0x00001DF8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.parent != null || this.annotations != null || this.content != null || this.lastAttr != null)
			{
				throw new InvalidOperationException("This instance cannot be deserialized.");
			}
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw new InvalidOperationException(global::SR.Format("The XmlReader must be on a node of type {0} instead of a node of type {1}.", XmlNodeType.Element, reader.NodeType));
			}
			this.ReadElementFrom(reader, LoadOptions.None);
		}

		/// <summary>Converts an object into its XML representation.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to which this object is serialized.</param>
		// Token: 0x06000070 RID: 112 RVA: 0x00003C70 File Offset: 0x00001E70
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C79 File Offset: 0x00001E79
		internal override void AddAttribute(XAttribute a)
		{
			if (this.Attribute(a.Name) != null)
			{
				throw new InvalidOperationException("Duplicate attribute.");
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			this.AppendAttribute(a);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			if (this.Attribute(a.Name) != null)
			{
				throw new InvalidOperationException("Duplicate attribute.");
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			this.AppendAttributeSkipNotify(a);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CDD File Offset: 0x00001EDD
		internal void AppendAttribute(XAttribute a)
		{
			bool flag = base.NotifyChanging(a, XObjectChangeEventArgs.Add);
			if (a.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			this.AppendAttributeSkipNotify(a);
			if (flag)
			{
				base.NotifyChanged(a, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D14 File Offset: 0x00001F14
		internal void AppendAttributeSkipNotify(XAttribute a)
		{
			a.parent = this;
			if (this.lastAttr == null)
			{
				a.next = a;
			}
			else
			{
				a.next = this.lastAttr.next;
				this.lastAttr.next = a;
			}
			this.lastAttr = a;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D52 File Offset: 0x00001F52
		internal override XNode CloneNode()
		{
			return new XElement(this);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003D5A File Offset: 0x00001F5A
		private IEnumerable<XAttribute> GetAttributes(XName name)
		{
			XAttribute a = this.lastAttr;
			if (a != null)
			{
				do
				{
					a = a.next;
					if (name == null || a.name == name)
					{
						yield return a;
					}
				}
				while (a.parent == this && a != this.lastAttr);
			}
			yield break;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003D74 File Offset: 0x00001F74
		private string GetNamespaceOfPrefixInScope(string prefix, XElement outOfScope)
		{
			for (XElement xelement = this; xelement != outOfScope; xelement = xelement.parent as XElement)
			{
				XAttribute next = xelement.lastAttr;
				if (next != null)
				{
					for (;;)
					{
						next = next.next;
						if (next.IsNamespaceDeclaration && next.Name.LocalName == prefix)
						{
							break;
						}
						if (next == xelement.lastAttr)
						{
							goto IL_0040;
						}
					}
					return next.Value;
				}
				IL_0040:;
			}
			return null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003DD2 File Offset: 0x00001FD2
		private void ReadElementFrom(XmlReader r, LoadOptions o)
		{
			this.ReadElementFromImpl(r, o);
			if (!r.IsEmptyElement)
			{
				r.Read();
				base.ReadContentFrom(r, o);
			}
			r.Read();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003DFC File Offset: 0x00001FFC
		private void ReadElementFromImpl(XmlReader r, LoadOptions o)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			this.name = XNamespace.Get(r.NamespaceURI).GetName(r.LocalName);
			if ((o & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = r.BaseURI;
				if (!string.IsNullOrEmpty(baseURI))
				{
					base.SetBaseUri(baseURI);
				}
			}
			IXmlLineInfo xmlLineInfo = null;
			if ((o & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				xmlLineInfo = r as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					base.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
			}
			if (r.MoveToFirstAttribute())
			{
				do
				{
					XAttribute xattribute = new XAttribute(XNamespace.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
					if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
					{
						xattribute.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
					}
					this.AppendAttributeSkipNotify(xattribute);
				}
				while (r.MoveToNextAttribute());
				r.MoveToElement();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EF0 File Offset: 0x000020F0
		internal void SetEndElementLineInfo(int lineNumber, int linePosition)
		{
			base.AddAnnotation(new LineInfoEndElementAnnotation(lineNumber, linePosition));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003EFF File Offset: 0x000020FF
		internal override void ValidateNode(XNode node, XNode previous)
		{
			if (node is XDocument)
			{
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.Document));
			}
			if (node is XDocumentType)
			{
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.DocumentType));
			}
		}

		// Token: 0x0400001B RID: 27
		internal XName name;

		// Token: 0x0400001C RID: 28
		internal XAttribute lastAttr;
	}
}
