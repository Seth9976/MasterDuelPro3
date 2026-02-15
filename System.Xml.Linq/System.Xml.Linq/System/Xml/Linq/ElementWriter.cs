using System;

namespace System.Xml.Linq
{
	// Token: 0x02000016 RID: 22
	internal struct ElementWriter
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004547 File Offset: 0x00002747
		public ElementWriter(XmlWriter writer)
		{
			this._writer = writer;
			this._resolver = default(NamespaceResolver);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000455C File Offset: 0x0000275C
		public void WriteElement(XElement e)
		{
			this.PushAncestors(e);
			XElement xelement = e;
			XNode xnode = e;
			for (;;)
			{
				e = xnode as XElement;
				if (e != null)
				{
					this.WriteStartElement(e);
					if (e.content == null)
					{
						this.WriteEndElement();
					}
					else
					{
						string text = e.content as string;
						if (text == null)
						{
							xnode = ((XNode)e.content).next;
							continue;
						}
						this._writer.WriteString(text);
						this.WriteFullEndElement();
					}
				}
				else
				{
					xnode.WriteTo(this._writer);
				}
				while (xnode != xelement && xnode == xnode.parent.content)
				{
					xnode = xnode.parent;
					this.WriteFullEndElement();
				}
				if (xnode == xelement)
				{
					break;
				}
				xnode = xnode.next;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000460C File Offset: 0x0000280C
		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = this._resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004660 File Offset: 0x00002860
		private void PushAncestors(XElement e)
		{
			for (;;)
			{
				e = e.parent as XElement;
				if (e == null)
				{
					break;
				}
				XAttribute xattribute = e.lastAttr;
				if (xattribute != null)
				{
					do
					{
						xattribute = xattribute.next;
						if (xattribute.IsNamespaceDeclaration)
						{
							this._resolver.AddFirst((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
						}
					}
					while (xattribute != e.lastAttr);
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000046DC File Offset: 0x000028DC
		private void PushElement(XElement e)
		{
			this._resolver.PushScope();
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					if (xattribute.IsNamespaceDeclaration)
					{
						this._resolver.Add((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
					}
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004750 File Offset: 0x00002950
		private void WriteEndElement()
		{
			this._writer.WriteEndElement();
			this._resolver.PopScope();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004768 File Offset: 0x00002968
		private void WriteFullEndElement()
		{
			this._writer.WriteFullEndElement();
			this._resolver.PopScope();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004780 File Offset: 0x00002980
		private void WriteStartElement(XElement e)
		{
			this.PushElement(e);
			XNamespace xnamespace = e.Name.Namespace;
			this._writer.WriteStartElement(this.GetPrefixOfNamespace(xnamespace, true), e.Name.LocalName, xnamespace.NamespaceName);
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					xnamespace = xattribute.Name.Namespace;
					string localName = xattribute.Name.LocalName;
					string namespaceName = xnamespace.NamespaceName;
					this._writer.WriteAttributeString(this.GetPrefixOfNamespace(xnamespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xattribute.Value);
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x0400002E RID: 46
		private XmlWriter _writer;

		// Token: 0x0400002F RID: 47
		private NamespaceResolver _resolver;
	}
}
