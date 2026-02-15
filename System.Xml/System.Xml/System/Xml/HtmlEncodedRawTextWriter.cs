using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000025 RID: 37
	internal class HtmlEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000A536 File Offset: 0x00008736
		public HtmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A547 File Offset: 0x00008747
		public HtmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A55C File Offset: 0x0000875C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			base.RawText("<!DOCTYPE ");
			if (name == "HTML")
			{
				base.RawText("HTML");
			}
			else
			{
				base.RawText("html");
			}
			int num;
			if (pubid != null)
			{
				base.RawText(" PUBLIC \"");
				base.RawText(pubid);
				if (sysid != null)
				{
					base.RawText("\" \"");
					base.RawText(sysid);
				}
				char[] bufChars = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars[num] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				char[] bufChars2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars2[num] = 34;
			}
			else
			{
				char[] bufChars3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars3[num] = 32;
			}
			if (subset != null)
			{
				char[] bufChars4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars4[num] = 91;
				base.RawText(subset);
				char[] bufChars5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars5[num] = 93;
			}
			char[] bufChars6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars6[num] = 62;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A69C File Offset: 0x0000889C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A730 File Offset: 0x00008930
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A774 File Offset: 0x00008974
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 60;
					char[] bufChars2 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars2[num] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars3[num] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A81C File Offset: 0x00008A1C
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 60;
					char[] bufChars2 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars2[num] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars3[num] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				int num;
				if (this.attrEndPos == this.bufPos)
				{
					char[] bufChars = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufChars[num] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlEncodedRawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
					if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
					{
						this.inAttributeValue = true;
						return;
					}
				}
				else
				{
					this.currentAttributeProperties = AttributeProperties.DEFAULT;
				}
				char[] bufChars2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars2[num] = 61;
				char[] bufChars3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				bufChars3[num] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public override void WriteEndAttribute()
		{
			if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
			{
				this.attrEndPos = this.bufPos;
			}
			else
			{
				if (this.endsWithAmpersand)
				{
					this.OutputRestAmps();
					this.endsWithAmpersand = false;
				}
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AA2C File Offset: 0x00008C2C
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			char[] bufChars = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			bufChars[num] = 60;
			char[] bufChars2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars2[num] = 63;
			base.RawText(target);
			char[] bufChars3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars3[num] = 32;
			base.WriteCommentOrPi(text, 63);
			char[] bufChars4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars4[num] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000AADC File Offset: 0x00008CDC
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + text.Length;
				if (this.inAttributeValue)
				{
					this.WriteHtmlAttributeTextBlock(ptr, ptr2);
				}
				else
				{
					this.WriteHtmlElementTextBlock(ptr, ptr2);
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					base.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					base.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlEncodedRawTextWriter.elementPropertySearch == null)
			{
				HtmlEncodedRawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlEncodedRawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AC10 File Offset: 0x00008E10
		protected void WriteMetaElement()
		{
			base.RawText("<META http-equiv=\"Content-Type\"");
			if (this.mediaType == null)
			{
				this.mediaType = "text/html";
			}
			base.RawText(" content=\"");
			base.RawText(this.mediaType);
			base.RawText("; charset=");
			base.RawText(this.encoding.WebName);
			base.RawText("\">");
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AC79 File Offset: 0x00008E79
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AC98 File Offset: 0x00008E98
		protected unsafe void WriteHtmlAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentAttributeProperties & (AttributeProperties)7U) != AttributeProperties.DEFAULT)
			{
				if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
				{
					return;
				}
				if ((this.currentAttributeProperties & (AttributeProperties)5U) != AttributeProperties.DEFAULT && !this.doNotEscapeUriAttributes)
				{
					this.WriteUriAttributeText(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
			else
			{
				if ((this.currentElementProperties & ElementProperties.HAS_NS) != ElementProperties.DEFAULT)
				{
					base.WriteAttributeTextBlock(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AD00 File Offset: 0x00008F00
		private unsafe void WriteHtmlAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0)
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_013B;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_0166;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_0166;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_0166;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_0166;
								}
								*(ptr2++) = c;
								goto IL_0166;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_013B;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_0166:
					pSrc++;
					continue;
					IL_013B:
					*(ptr2++) = c;
					goto IL_0166;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AE8C File Offset: 0x0000908C
		private unsafe void WriteUriAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_014F;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_01ED;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_01ED;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_01ED;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_01ED;
								}
								*(ptr2++) = c;
								goto IL_01ED;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_014F;
					}
					byte[] array2;
					byte* ptr4;
					if ((array2 = this.uriEscapingBuffer) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					byte* ptr5 = ptr4;
					byte* ptr6 = ptr5;
					XmlUtf8RawTextWriter.CharToUTF8(ref pSrc, pSrcEnd, ref ptr6);
					while (ptr5 < ptr6)
					{
						*(ptr2++) = '%';
						*(ptr2++) = "0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = "0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_01ED:
					pSrc++;
					continue;
					IL_014F:
					*(ptr2++) = c;
					goto IL_01ED;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000B0A0 File Offset: 0x000092A0
		private void OutputRestAmps()
		{
			char[] bufChars = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			bufChars[num] = 97;
			char[] bufChars2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars2[num] = 109;
			char[] bufChars3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars3[num] = 112;
			char[] bufChars4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			bufChars4[num] = 59;
		}

		// Token: 0x04000108 RID: 264
		protected ByteStack elementScope;

		// Token: 0x04000109 RID: 265
		protected ElementProperties currentElementProperties;

		// Token: 0x0400010A RID: 266
		private AttributeProperties currentAttributeProperties;

		// Token: 0x0400010B RID: 267
		private bool endsWithAmpersand;

		// Token: 0x0400010C RID: 268
		private byte[] uriEscapingBuffer;

		// Token: 0x0400010D RID: 269
		private string mediaType;

		// Token: 0x0400010E RID: 270
		private bool doNotEscapeUriAttributes;

		// Token: 0x0400010F RID: 271
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x04000110 RID: 272
		protected static TernaryTreeReadOnly attributePropertySearch;
	}
}
