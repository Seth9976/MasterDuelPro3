using System;

namespace System.Xml
{
	// Token: 0x020000FC RID: 252
	internal class XmlUnspecifiedAttribute : XmlAttribute
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x00043074 File Offset: 0x00041274
		protected internal XmlUnspecifiedAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00043081 File Offset: 0x00041281
		public override bool Specified
		{
			get
			{
				return this.fSpecified;
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0004308C File Offset: 0x0004128C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = (XmlUnspecifiedAttribute)ownerDocument.CreateDefaultAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlUnspecifiedAttribute.CopyChildren(ownerDocument, this, true);
			xmlUnspecifiedAttribute.fSpecified = true;
			return xmlUnspecifiedAttribute;
		}

		// Token: 0x17000342 RID: 834
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000430CD File Offset: 0x000412CD
		public override string InnerText
		{
			set
			{
				base.InnerText = value;
				this.fSpecified = true;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000430DD File Offset: 0x000412DD
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode = base.InsertBefore(newChild, refChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000430EE File Offset: 0x000412EE
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode = base.InsertAfter(newChild, refChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000430FF File Offset: 0x000412FF
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode xmlNode = base.RemoveChild(oldChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0004310F File Offset: 0x0004130F
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode xmlNode = base.AppendChild(newChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0004311F File Offset: 0x0004131F
		public override void WriteTo(XmlWriter w)
		{
			if (this.fSpecified)
			{
				base.WriteTo(w);
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00043130 File Offset: 0x00041330
		internal void SetSpecified(bool f)
		{
			this.fSpecified = f;
		}

		// Token: 0x04000684 RID: 1668
		private bool fSpecified;
	}
}
