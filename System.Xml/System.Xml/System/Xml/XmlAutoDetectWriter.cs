using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200004D RID: 77
	internal class XmlAutoDetectWriter : XmlRawWriter
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000E216 File Offset: 0x0000C416
		private XmlAutoDetectWriter(XmlWriterSettings writerSettings)
		{
			this.writerSettings = writerSettings.Clone();
			this.writerSettings.ReadOnly = true;
			this.eventCache = new XmlEventCache(string.Empty, true);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E247 File Offset: 0x0000C447
		public XmlAutoDetectWriter(TextWriter textWriter, XmlWriterSettings writerSettings)
			: this(writerSettings)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E257 File Offset: 0x0000C457
		public XmlAutoDetectWriter(Stream strm, XmlWriterSettings writerSettings)
			: this(writerSettings)
		{
			this.strm = strm;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E267 File Offset: 0x0000C467
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writerSettings;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E26F File Offset: 0x0000C46F
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E288 File Offset: 0x0000C488
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.wrapped == null)
			{
				if (ns.Length == 0 && XmlAutoDetectWriter.IsHtmlTag(localName))
				{
					this.CreateWrappedWriter(XmlOutputMethod.Html);
				}
				else
				{
					this.CreateWrappedWriter(XmlOutputMethod.Xml);
				}
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E2D7 File Offset: 0x0000C4D7
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public override void WriteCData(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.eventCache.WriteCData(text);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E308 File Offset: 0x0000C508
		public override void WriteComment(string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteComment(text);
				return;
			}
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E32B File Offset: 0x0000C52B
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteProcessingInstruction(name, text);
				return;
			}
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E350 File Offset: 0x0000C550
		public override void WriteWhitespace(string ws)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteWhitespace(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E373 File Offset: 0x0000C573
		public override void WriteString(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteString(text);
				return;
			}
			this.eventCache.WriteString(text);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E397 File Offset: 0x0000C597
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
		public override void WriteRaw(string data)
		{
			if (this.TextBlockCreatesWriter(data))
			{
				this.wrapped.WriteRaw(data);
				return;
			}
			this.eventCache.WriteRaw(data);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E3DB File Offset: 0x0000C5DB
		public override void WriteEntityRef(string name)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public override void WriteCharEntity(char ch)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E405 File Offset: 0x0000C605
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E41B File Offset: 0x0000C61B
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E432 File Offset: 0x0000C632
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBinHex(buffer, index, count);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E449 File Offset: 0x0000C649
		public override void Close()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Close();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E45D File Offset: 0x0000C65D
		public override void Flush()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Flush();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E471 File Offset: 0x0000C671
		public override void WriteValue(object value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E486 File Offset: 0x0000C686
		public override void WriteValue(string value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E49B File Offset: 0x0000C69B
		public override void WriteValue(bool value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public override void WriteValue(DateTime value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		public override void WriteValue(double value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000E4DA File Offset: 0x0000C6DA
		public override void WriteValue(float value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000E4EF File Offset: 0x0000C6EF
		public override void WriteValue(decimal value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E504 File Offset: 0x0000C704
		public override void WriteValue(int value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E519 File Offset: 0x0000C719
		public override void WriteValue(long value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x17000087 RID: 135
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000E52E File Offset: 0x0000C72E
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			set
			{
				this.resolver = value;
				if (this.wrapped == null)
				{
					this.eventCache.NamespaceResolver = value;
					return;
				}
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E558 File Offset: 0x0000C758
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E56D File Offset: 0x0000C76D
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E582 File Offset: 0x0000C782
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E58F File Offset: 0x0000C78F
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E59F File Offset: 0x0000C79F
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E5C5 File Offset: 0x0000C7C5
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E5D2 File Offset: 0x0000C7D2
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E5E7 File Offset: 0x0000C7E7
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		private static bool IsHtmlTag(string tagName)
		{
			return tagName.Length == 4 && (tagName[0] == 'H' || tagName[0] == 'h') && (tagName[1] == 'T' || tagName[1] == 't') && (tagName[2] == 'M' || tagName[2] == 'm') && (tagName[3] == 'L' || tagName[3] == 'l');
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E66D File Offset: 0x0000C86D
		private void EnsureWrappedWriter(XmlOutputMethod outMethod)
		{
			if (this.wrapped == null)
			{
				this.CreateWrappedWriter(outMethod);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E680 File Offset: 0x0000C880
		private bool TextBlockCreatesWriter(string textBlock)
		{
			if (this.wrapped == null)
			{
				if (XmlCharType.Instance.IsOnlyWhitespace(textBlock))
				{
					return false;
				}
				this.CreateWrappedWriter(XmlOutputMethod.Xml);
			}
			return true;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		private void CreateWrappedWriter(XmlOutputMethod outMethod)
		{
			this.writerSettings.ReadOnly = false;
			this.writerSettings.OutputMethod = outMethod;
			if (outMethod == XmlOutputMethod.Html && this.writerSettings.IndentInternal == TriState.Unknown)
			{
				this.writerSettings.Indent = true;
			}
			this.writerSettings.ReadOnly = true;
			if (this.textWriter != null)
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.textWriter, this.writerSettings)).RawWriter;
			}
			else
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.strm, this.writerSettings)).RawWriter;
			}
			this.eventCache.EndEvents();
			this.eventCache.EventsToWriter(this.wrapped);
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x0400017A RID: 378
		private XmlRawWriter wrapped;

		// Token: 0x0400017B RID: 379
		private OnRemoveWriter onRemove;

		// Token: 0x0400017C RID: 380
		private XmlWriterSettings writerSettings;

		// Token: 0x0400017D RID: 381
		private XmlEventCache eventCache;

		// Token: 0x0400017E RID: 382
		private TextWriter textWriter;

		// Token: 0x0400017F RID: 383
		private Stream strm;
	}
}
