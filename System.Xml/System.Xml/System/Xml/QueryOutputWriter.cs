using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000039 RID: 57
	internal class QueryOutputWriter : XmlRawWriter
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public QueryOutputWriter(XmlRawWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				if (this.systemId != null)
				{
					this.outputDocType = true;
					this.checkWellFormedDoc = true;
				}
				if (settings.AutoXmlDeclaration && settings.Standalone == XmlStandalone.Yes)
				{
					this.checkWellFormedDoc = true;
				}
				if (settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, int>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName xmlQualifiedName in settings.CDataSectionElements)
					{
						this.lookupCDataElems[xmlQualifiedName] = 0;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x1700004B RID: 75
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000C314 File Offset: 0x0000A514
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			set
			{
				this.resolver = value;
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C329 File Offset: 0x0000A529
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C337 File Offset: 0x0000A537
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000C345 File Offset: 0x0000A545
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.wrapped.Settings;
				settings.ReadOnly = false;
				settings.DocTypeSystem = this.systemId;
				settings.DocTypePublic = this.publicId;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C378 File Offset: 0x0000A578
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C39C File Offset: 0x0000A59C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.checkWellFormedDoc)
			{
				if (this.depth == 0 && this.hasDocElem)
				{
					throw new XmlException("Document cannot have multiple document elements.", string.Empty);
				}
				this.depth++;
				this.hasDocElem = true;
			}
			if (this.outputDocType)
			{
				this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C469 File Offset: 0x0000A669
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C4A9 File Offset: 0x0000A6A9
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C50D File Offset: 0x0000A70D
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C521 File Offset: 0x0000A721
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000C530 File Offset: 0x0000A730
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C53D File Offset: 0x0000A73D
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C54B File Offset: 0x0000A74B
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C558 File Offset: 0x0000A758
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C566 File Offset: 0x0000A766
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C57A File Offset: 0x0000A77A
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C58F File Offset: 0x0000A78F
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C5F5 File Offset: 0x0000A7F5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C631 File Offset: 0x0000A831
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C645 File Offset: 0x0000A845
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C659 File Offset: 0x0000A859
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C66E File Offset: 0x0000A86E
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C6DD File Offset: 0x0000A8DD
		public override void Close()
		{
			this.wrapped.Close();
			if (this.checkWellFormedDoc && !this.hasDocElem)
			{
				throw new XmlException("Document does not have a root element.", string.Empty);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C70A File Offset: 0x0000A90A
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C717 File Offset: 0x0000A917
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C738 File Offset: 0x0000A938
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x0400012B RID: 299
		private XmlRawWriter wrapped;

		// Token: 0x0400012C RID: 300
		private bool inCDataSection;

		// Token: 0x0400012D RID: 301
		private Dictionary<XmlQualifiedName, int> lookupCDataElems;

		// Token: 0x0400012E RID: 302
		private BitStack bitsCData;

		// Token: 0x0400012F RID: 303
		private XmlQualifiedName qnameCData;

		// Token: 0x04000130 RID: 304
		private bool outputDocType;

		// Token: 0x04000131 RID: 305
		private bool checkWellFormedDoc;

		// Token: 0x04000132 RID: 306
		private bool hasDocElem;

		// Token: 0x04000133 RID: 307
		private bool inAttr;

		// Token: 0x04000134 RID: 308
		private string systemId;

		// Token: 0x04000135 RID: 309
		private string publicId;

		// Token: 0x04000136 RID: 310
		private int depth;
	}
}
