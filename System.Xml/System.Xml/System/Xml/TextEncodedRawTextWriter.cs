using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000042 RID: 66
	internal class TextEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000D01F File Offset: 0x0000B21F
		public TextEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D029 File Offset: 0x0000B229
		public TextEncodedRawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void StartElementContent()
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000D033 File Offset: 0x0000B233
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000D03C File Offset: 0x0000B23C
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D045 File Offset: 0x0000B245
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteComment(string text)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000D04E File Offset: 0x0000B24E
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000D04E File Offset: 0x0000B24E
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D05F File Offset: 0x0000B25F
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D05F File Offset: 0x0000B25F
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D04E File Offset: 0x0000B24E
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
