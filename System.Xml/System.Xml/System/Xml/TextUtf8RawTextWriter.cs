using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000043 RID: 67
	internal class TextUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000D072 File Offset: 0x0000B272
		public TextUtf8RawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void StartElementContent()
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000D07C File Offset: 0x0000B27C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000D085 File Offset: 0x0000B285
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D08E File Offset: 0x0000B28E
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D097 File Offset: 0x0000B297
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D097 File Offset: 0x0000B297
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D097 File Offset: 0x0000B297
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
