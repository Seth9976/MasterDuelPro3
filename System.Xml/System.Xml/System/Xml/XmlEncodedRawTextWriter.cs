using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200004E RID: 78
	internal class XmlEncodedRawTextWriter : XmlRawWriter
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000E780 File Offset: 0x0000C980
		protected XmlEncodedRawTextWriter(XmlWriterSettings settings)
		{
			this.useAsync = settings.Async;
			this.newLineHandling = settings.NewLineHandling;
			this.omitXmlDeclaration = settings.OmitXmlDeclaration;
			this.newLineChars = settings.NewLineChars;
			this.checkCharacters = settings.CheckCharacters;
			this.closeOutput = settings.CloseOutput;
			this.standalone = settings.Standalone;
			this.outputMethod = settings.OutputMethod;
			this.mergeCDataSections = settings.MergeCDataSections;
			if (this.checkCharacters && this.newLineHandling == NewLineHandling.Replace)
			{
				this.ValidateContentChars(this.newLineChars, "NewLineChars", false);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E848 File Offset: 0x0000CA48
		public XmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings)
			: this(settings)
		{
			this.writer = writer;
			this.encoding = writer.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public XmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings)
			: this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			this.bufBytes = new byte[this.bufChars.Length];
			this.bufBytesUsed = 0;
			this.trackTextContent = true;
			this.inTextContent = false;
			this.lastMarkPos = 0;
			this.textContentMarks = new int[64];
			this.textContentMarks[0] = 1;
			this.charEntityFallback = new CharEntityEncoderFallback();
			this.encoding = (Encoding)settings.Encoding.Clone();
			this.encoding.EncoderFallback = this.charEntityFallback;
			this.encoder = this.encoding.GetEncoder();
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					this.stream.Write(preamble, 0, preamble.Length);
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		public override XmlWriterSettings Settings
		{
			get
			{
				return new XmlWriterSettings
				{
					Encoding = this.encoding,
					OmitXmlDeclaration = this.omitXmlDeclaration,
					NewLineHandling = this.newLineHandling,
					NewLineChars = this.newLineChars,
					CloseOutput = this.closeOutput,
					ConformanceLevel = ConformanceLevel.Auto,
					CheckCharacters = this.checkCharacters,
					AutoXmlDeclaration = this.autoXmlDeclaration,
					Standalone = this.standalone,
					OutputMethod = this.outputMethod,
					ReadOnly = true
				};
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000EA64 File Offset: 0x0000CC64
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					this.ChangeTextContentMark(false);
				}
				this.RawText("<?xml version=\"");
				this.RawText("1.0");
				if (this.encoding != null)
				{
					this.RawText("\" encoding=\"");
					this.RawText(this.encoding.WebName);
				}
				if (standalone != XmlStandalone.Omit)
				{
					this.RawText("\" standalone=\"");
					this.RawText((standalone == XmlStandalone.Yes) ? "yes" : "no");
				}
				this.RawText("\"?>");
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000EB07 File Offset: 0x0000CD07
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.RawText("<!DOCTYPE ");
			this.RawText(name);
			int num;
			if (pubid != null)
			{
				this.RawText(" PUBLIC \"");
				this.RawText(pubid);
				this.RawText("\" \"");
				if (sysid != null)
				{
					this.RawText(sysid);
				}
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000ECCC File Offset: 0x0000CECC
		internal override void StartElementContent()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000ED00 File Offset: 0x0000CF00
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.contentPos != this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					char[] array3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000EE14 File Offset: 0x0000D014
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000EF78 File Offset: 0x0000D178
		public override void WriteEndAttribute()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F084 File Offset: 0x0000D284
		internal override void WriteEndNamespaceDeclaration()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public override void WriteCData(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				char[] array6 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				char[] array7 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				char[] array8 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				char[] array9 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			char[] array10 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			char[] array11 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			char[] array12 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F27C File Offset: 0x0000D47C
		public override void WriteComment(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F360 File Offset: 0x0000D560
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F420 File Offset: 0x0000D620
		public override void WriteEntityRef(string name)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(text);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F588 File Offset: 0x0000D788
		public unsafe override void WriteWhitespace(string ws)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (string text = ws)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + ws.Length;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr, ptr2);
				}
				else
				{
					this.WriteElementTextBlock(ptr, ptr2);
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
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
					this.WriteAttributeTextBlock(ptr, ptr2);
				}
				else
				{
					this.WriteElementTextBlock(ptr, ptr2);
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F640 File Offset: 0x0000D840
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			char[] array = this.bufChars;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			char[] array2 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			char[] array3 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			char[] array4 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					this.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F750 File Offset: 0x0000D950
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F79C File Offset: 0x0000D99C
		public unsafe override void WriteRaw(string data)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.WriteRawWithCharChecking(ptr, ptr + data.Length);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public override void Close()
		{
			try
			{
				this.FlushBuffer();
				this.FlushEncoder();
			}
			finally
			{
				this.writeToNull = true;
				if (this.stream != null)
				{
					try
					{
						this.stream.Flush();
						goto IL_007D;
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.stream.Close();
							}
						}
						finally
						{
							this.stream = null;
						}
					}
				}
				if (this.writer != null)
				{
					try
					{
						this.writer.Flush();
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.writer.Close();
							}
						}
						finally
						{
							this.writer = null;
						}
					}
				}
				IL_007D:;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F8BC File Offset: 0x0000DABC
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
				return;
			}
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					if (this.stream != null)
					{
						if (this.trackTextContent)
						{
							this.charEntityFallback.Reset(this.textContentMarks, this.lastMarkPos);
							if ((this.lastMarkPos & 1) != 0)
							{
								this.textContentMarks[1] = 1;
								this.lastMarkPos = 1;
							}
							else
							{
								this.lastMarkPos = 0;
							}
						}
						this.EncodeChars(1, this.bufPos, true);
					}
					else
					{
						this.writer.Write(this.bufChars, 1, this.bufPos - 1);
					}
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufChars[0] = this.bufChars[this.bufPos - 1];
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000FA04 File Offset: 0x0000DC04
		private void EncodeChars(int startOffset, int endOffset, bool writeAllToStream)
		{
			while (startOffset < endOffset)
			{
				if (this.charEntityFallback != null)
				{
					this.charEntityFallback.StartOffset = startOffset;
				}
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, startOffset, endOffset - startOffset, this.bufBytes, this.bufBytesUsed, this.bufBytes.Length - this.bufBytesUsed, false, out num, out num2, out flag);
				startOffset += num;
				this.bufBytesUsed += num2;
				if (this.bufBytesUsed >= this.bufBytes.Length - 16)
				{
					this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
					this.bufBytesUsed = 0;
				}
			}
			if (writeAllToStream && this.bufBytesUsed > 0)
			{
				this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
				this.bufBytesUsed = 0;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		private void FlushEncoder()
		{
			if (this.stream != null)
			{
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, 1, 0, this.bufBytes, 0, this.bufBytes.Length, true, out num, out num2, out flag);
				if (num2 != 0)
				{
					this.stream.Write(this.bufBytes, 0, num2);
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 >= ptr3)
				{
					this.bufPos = (int)((long)(ptr2 - ptr));
					this.FlushBuffer();
					ptr2 = ptr + 1;
				}
				else
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_01D3;
							}
							ptr2 = XmlEncodedRawTextWriter.TabEntity(ptr2);
							goto IL_01D3;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_01D3;
							}
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_01D3;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_01D3;
							}
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_01D3;
						default:
							if (num == 34)
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_01D3;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_01D3;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (char)num;
							ptr2++;
							goto IL_01D3;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_01D3;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_01D3;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
					continue;
					IL_01D3:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000FD28 File Offset: 0x0000DF28
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_010F;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_01D6;
							}
							*ptr2 = (char)num;
							ptr2++;
							goto IL_01D6;
						case 11:
						case 12:
							break;
						case 13:
							switch (this.newLineHandling)
							{
							case NewLineHandling.Replace:
								if (pSrc[1] == '\n')
								{
									pSrc++;
								}
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_01D6;
							case NewLineHandling.Entitize:
								ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_01D6;
							case NewLineHandling.None:
								*ptr2 = (char)num;
								ptr2++;
								goto IL_01D6;
							default:
								goto IL_01D6;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_010F;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_01D6;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_010F;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_01D6;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_01D6;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
					continue;
					IL_01D6:
					pSrc++;
					continue;
					IL_010F:
					*ptr2 = (char)num;
					ptr2++;
					goto IL_01D6;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FF38 File Offset: 0x0000E138
		protected unsafe void RawText(string s)
		{
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.RawText(ptr, ptr + s.Length);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000FF6C File Offset: 0x0000E16C
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr2 + (long)(pSrcEnd - ptr3) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) < 55296)
				{
					ptr3++;
					*ptr2 = (char)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					break;
				}
				if (ptr2 >= ptr4)
				{
					this.bufPos = (int)((long)(ptr2 - ptr));
					this.FlushBuffer();
					ptr2 = ptr + 1;
				}
				else if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					*ptr2 = (char)num;
					ptr2++;
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010078 File Offset: 0x0000E278
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr2 = pSrcBegin;
			char* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - ptr2) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					break;
				}
				if (ptr3 < ptr4)
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_00DF;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_0186;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_0186;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								if (ptr2[1] == '\n')
								{
									ptr2++;
								}
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_0186;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_0186;
						default:
							if (num == 38)
							{
								goto IL_00DF;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_00DF;
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
						ptr2 += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr3 = this.InvalidXmlChar(num, ptr3, false);
						ptr2++;
						continue;
					}
					*ptr3 = (char)num;
					ptr3++;
					ptr2++;
					continue;
					IL_0186:
					ptr2++;
					continue;
					IL_00DF:
					*ptr3 = (char)num;
					ptr3++;
					goto IL_0186;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010224 File Offset: 0x0000E424
		protected unsafe void WriteCommentOrPi(string text, int stopChar)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				char* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar)
					{
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
					}
					if (ptr3 >= ptr4)
					{
						break;
					}
					if (ptr5 < ptr6)
					{
						if (num <= 45)
						{
							switch (num)
							{
							case 9:
								goto IL_0226;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_0296;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_0296;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									if (ptr3[1] == '\n')
									{
										ptr3++;
									}
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_0296;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_0296;
							default:
								if (num == 38)
								{
									goto IL_0226;
								}
								if (num == 45)
								{
									*ptr5 = '-';
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = ' ';
										ptr5++;
										goto IL_0296;
									}
									goto IL_0296;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_0226;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = ']';
									ptr5++;
									goto IL_0296;
								}
							}
							else
							{
								*ptr5 = '?';
								ptr5++;
								if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
								{
									*ptr5 = ' ';
									ptr5++;
									goto IL_0296;
								}
								goto IL_0296;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
						continue;
						IL_0296:
						ptr3++;
						continue;
						IL_0226:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_0296;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000104E4 File Offset: 0x0000E6E4
		protected unsafe void WriteCDataSection(string text)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				char* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93)
					{
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
					}
					if (ptr3 >= ptr4)
					{
						break;
					}
					if (ptr5 < ptr6)
					{
						if (num <= 39)
						{
							switch (num)
							{
							case 9:
								goto IL_0210;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_0280;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_0280;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									if (ptr3[1] == '\n')
									{
										ptr3++;
									}
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_0280;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_0280;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_0210;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_0210;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == ']')
								{
									ptr5 = XmlEncodedRawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlEncodedRawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = '>';
								ptr5++;
								goto IL_0280;
							}
							if (num == 93)
							{
								if (ptr5[-1] == ']')
								{
									this.hadDoubleBracket = true;
								}
								else
								{
									this.hadDoubleBracket = false;
								}
								*ptr5 = ']';
								ptr5++;
								goto IL_0280;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
						continue;
						IL_0280:
						ptr3++;
						continue;
						IL_0210:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_0280;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010790 File Offset: 0x0000E990
		private unsafe static char* EncodeSurrogate(char* pSrc, char* pSrcEnd, char* pDst)
		{
			int num = (int)(*pSrc);
			if (num > 56319)
			{
				throw XmlConvert.CreateInvalidHighSurrogateCharException((char)num);
			}
			if (pSrc + 1 >= pSrcEnd)
			{
				throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			}
			int num2 = (int)pSrc[1];
			if (num2 >= 56320 && (LocalAppContextSwitches.DontThrowOnInvalidSurrogatePairs || num2 <= 57343))
			{
				*pDst = (char)num;
				pDst[1] = (char)num2;
				pDst += 2;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000107FF File Offset: 0x0000E9FF
		private unsafe char* InvalidXmlChar(int ch, char* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlEncodedRawTextWriter.CharEntity(pDst, (char)ch);
			}
			*pDst = (char)ch;
			pDst++;
			return pDst;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010828 File Offset: 0x0000EA28
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref char* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			*pDst = (short)((ushort)num);
			pDst += 2;
			pSrc += 2;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010888 File Offset: 0x0000EA88
		protected void ChangeTextContentMark(bool value)
		{
			this.inTextContent = value;
			if (this.lastMarkPos + 1 == this.textContentMarks.Length)
			{
				this.GrowTextContentMarks();
			}
			int[] array = this.textContentMarks;
			int num = this.lastMarkPos + 1;
			this.lastMarkPos = num;
			array[num] = this.bufPos;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000108D4 File Offset: 0x0000EAD4
		private void GrowTextContentMarks()
		{
			int[] array = new int[this.textContentMarks.Length * 2];
			Array.Copy(this.textContentMarks, array, this.textContentMarks.Length);
			this.textContentMarks = array;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001090C File Offset: 0x0000EB0C
		protected unsafe char* WriteNewLine(char* pDst)
		{
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
			this.bufPos = (int)((long)(pDst - ptr));
			this.RawText(this.newLineChars);
			return ptr + this.bufPos;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001095A File Offset: 0x0000EB5A
		protected unsafe static char* LtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'l';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001097E File Offset: 0x0000EB7E
		protected unsafe static char* GtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'g';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000109A2 File Offset: 0x0000EBA2
		protected unsafe static char* AmpEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'a';
			pDst[2] = 'm';
			pDst[3] = 'p';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000109CF File Offset: 0x0000EBCF
		protected unsafe static char* QuoteEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'q';
			pDst[2] = 'u';
			pDst[3] = 'o';
			pDst[4] = 't';
			pDst[5] = ';';
			return pDst + 6;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00010A05 File Offset: 0x0000EC05
		protected unsafe static char* TabEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = '9';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010A32 File Offset: 0x0000EC32
		protected unsafe static char* LineFeedEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'A';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00010A5F File Offset: 0x0000EC5F
		protected unsafe static char* CarriageReturnEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'D';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00010A8C File Offset: 0x0000EC8C
		private unsafe static char* CharEntity(char* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = *(ptr2++)) != '\0')
				{
				}
			}
			pDst[-1] = ';';
			return pDst;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		protected unsafe static char* RawStartCData(char* pDst)
		{
			*pDst = '<';
			pDst[1] = '!';
			pDst[2] = '[';
			pDst[3] = 'C';
			pDst[4] = 'D';
			pDst[5] = 'A';
			pDst[6] = 'T';
			pDst[7] = 'A';
			pDst[8] = '[';
			return pDst + 9;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010B55 File Offset: 0x0000ED55
		protected unsafe static char* RawEndCData(char* pDst)
		{
			*pDst = ']';
			pDst[1] = ']';
			pDst[2] = '>';
			return pDst + 3;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010B70 File Offset: 0x0000ED70
		protected void ValidateContentChars(string chars, string propertyName, bool allowOnlyWhitespace)
		{
			if (!allowOnlyWhitespace)
			{
				for (int i = 0; i < chars.Length; i++)
				{
					if (!this.xmlCharType.IsTextChar(chars[i]))
					{
						char c = chars[i];
						if (c <= '&')
						{
							switch (c)
							{
							case '\t':
							case '\n':
							case '\r':
								goto IL_011C;
							case '\v':
							case '\f':
								goto IL_00A2;
							default:
								if (c != '&')
								{
									goto IL_00A2;
								}
								break;
							}
						}
						else if (c != '<' && c != ']')
						{
							goto IL_00A2;
						}
						string text = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] array = XmlException.BuildCharExceptionArgs(chars, i);
						string text2 = Res.GetString(text, array);
						goto IL_012D;
						IL_00A2:
						if (XmlCharType.IsHighSurrogate((int)chars[i]))
						{
							if (i + 1 < chars.Length && XmlCharType.IsLowSurrogate((int)chars[i + 1]))
							{
								i++;
								goto IL_011C;
							}
							text2 = Res.GetString("The surrogate pair is invalid. Missing a low surrogate character.");
						}
						else
						{
							if (!XmlCharType.IsLowSurrogate((int)chars[i]))
							{
								goto IL_011C;
							}
							text2 = Res.GetString("Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).", new object[] { ((uint)chars[i]).ToString("X", CultureInfo.InvariantCulture) });
						}
						IL_012D:
						string text3 = "XmlWriterSettings.{0} can contain only valid XML text content characters when XmlWriterSettings.CheckCharacters is true. {1}";
						array = new string[] { propertyName, text2 };
						throw new ArgumentException(Res.GetString(text3, array));
					}
					IL_011C:;
				}
				return;
			}
			if (!this.xmlCharType.IsOnlyWhitespace(chars))
			{
				throw new ArgumentException(Res.GetString("XmlWriterSettings.{0} can contain only valid XML white space characters when XmlWriterSettings.CheckCharacters and XmlWriterSettings.NewLineOnAttributes are true.", new object[] { propertyName }));
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00010CCA File Offset: 0x0000EECA
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Set XmlWriterSettings.Async to true if you want to use Async Methods."));
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		internal override async Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					this.ChangeTextContentMark(false);
				}
				await this.RawTextAsync("<?xml version=\"").ConfigureAwait(false);
				await this.RawTextAsync("1.0").ConfigureAwait(false);
				if (this.encoding != null)
				{
					await this.RawTextAsync("\" encoding=\"").ConfigureAwait(false);
					await this.RawTextAsync(this.encoding.WebName).ConfigureAwait(false);
				}
				if (standalone != XmlStandalone.Omit)
				{
					await this.RawTextAsync("\" standalone=\"").ConfigureAwait(false);
					await this.RawTextAsync((standalone == XmlStandalone.Yes) ? "yes" : "no").ConfigureAwait(false);
				}
				await this.RawTextAsync("\"?>").ConfigureAwait(false);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010D30 File Offset: 0x0000EF30
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName);
			}
			else
			{
				task = this.RawTextAsync(localName);
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		private void WriteStartAttribute_SetInAttribute()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 61;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00010E08 File Offset: 0x0000F008
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00010E64 File Offset: 0x0000F064
		internal override async Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			this.CheckAsyncCall();
			await this.WriteStartNamespaceDeclarationAsync(prefix).ConfigureAwait(false);
			await this.WriteStringAsync(namespaceName).ConfigureAwait(false);
			await this.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		internal override async Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (prefix.Length == 0)
			{
				await this.RawTextAsync(" xmlns=\"").ConfigureAwait(false);
			}
			else
			{
				await this.RawTextAsync(" xmlns:").ConfigureAwait(false);
				await this.RawTextAsync(prefix).ConfigureAwait(false);
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00010F04 File Offset: 0x0000F104
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010F60 File Offset: 0x0000F160
		public override async Task WriteEntityRefAsync(string name)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			await this.RawTextAsync(name).ConfigureAwait(false);
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				await this.FlushBufferAsync().ConfigureAwait(false);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010FAC File Offset: 0x0000F1AC
		public override async Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsyncCall();
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			await this.RawTextAsync(text).ConfigureAwait(false);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				await this.FlushBufferAsync().ConfigureAwait(false);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010FF7 File Offset: 0x0000F1F7
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001102D File Offset: 0x0000F22D
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011064 File Offset: 0x0000F264
		public override async Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			char[] array = this.bufChars;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			char[] array2 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			char[] array3 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			await this.RawTextAsync(num.ToString("X", NumberFormatInfo.InvariantInfo)).ConfigureAwait(false);
			char[] array4 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000110B7 File Offset: 0x0000F2B7
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public override async Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			await this.WriteRawWithCharCheckingAsync(buffer, index, count).ConfigureAwait(false);
			this.textPos = this.bufPos;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011150 File Offset: 0x0000F350
		public override async Task WriteRawAsync(string data)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			await this.WriteRawWithCharCheckingAsync(data).ConfigureAwait(false);
			this.textPos = this.bufPos;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001119C File Offset: 0x0000F39C
		protected virtual async Task FlushBufferAsync()
		{
			try
			{
				if (!this.writeToNull)
				{
					if (this.stream != null)
					{
						if (this.trackTextContent)
						{
							this.charEntityFallback.Reset(this.textContentMarks, this.lastMarkPos);
							if ((this.lastMarkPos & 1) != 0)
							{
								this.textContentMarks[1] = 1;
								this.lastMarkPos = 1;
							}
							else
							{
								this.lastMarkPos = 0;
							}
						}
						await this.EncodeCharsAsync(1, this.bufPos, true).ConfigureAwait(false);
					}
					else
					{
						await this.writer.WriteAsync(this.bufChars, 1, this.bufPos - 1).ConfigureAwait(false);
					}
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufChars[0] = this.bufChars[this.bufPos - 1];
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000111E0 File Offset: 0x0000F3E0
		private async Task EncodeCharsAsync(int startOffset, int endOffset, bool writeAllToStream)
		{
			while (startOffset < endOffset)
			{
				if (this.charEntityFallback != null)
				{
					this.charEntityFallback.StartOffset = startOffset;
				}
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, startOffset, endOffset - startOffset, this.bufBytes, this.bufBytesUsed, this.bufBytes.Length - this.bufBytesUsed, false, out num, out num2, out flag);
				startOffset += num;
				this.bufBytesUsed += num2;
				if (this.bufBytesUsed >= this.bufBytes.Length - 16)
				{
					await this.stream.WriteAsync(this.bufBytes, 0, this.bufBytesUsed).ConfigureAwait(false);
					this.bufBytesUsed = 0;
				}
			}
			if (writeAllToStream && this.bufBytesUsed > 0)
			{
				await this.stream.WriteAsync(this.bufBytes, 0, this.bufBytesUsed).ConfigureAwait(false);
				this.bufBytesUsed = 0;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001123C File Offset: 0x0000F43C
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_01EE;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_01E4;
						}
						ptr3 = XmlEncodedRawTextWriter.TabEntity(ptr3);
						goto IL_01E4;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_01E4;
						}
						ptr3 = XmlEncodedRawTextWriter.LineFeedEntity(ptr3);
						goto IL_01E4;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_01E4;
						}
						ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_01E4;
					default:
						if (num == 34)
						{
							ptr3 = XmlEncodedRawTextWriter.QuoteEntity(ptr3);
							goto IL_01E4;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_01E4;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (char)num;
						ptr3++;
						goto IL_01E4;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_01E4;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_01E4;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_01E4:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_01EE:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011448 File Offset: 0x0000F648
		protected unsafe int WriteAttributeTextBlockNoFlush(char[] chars, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + count;
				return this.WriteAttributeTextBlockNoFlush(ptr2, ptr3);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011474 File Offset: 0x0000F674
		protected unsafe int WriteAttributeTextBlockNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* ptr3 = ptr2 + count;
			return this.WriteAttributeTextBlockNoFlush(ptr2, ptr3);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000114AC File Offset: 0x0000F6AC
		protected async Task WriteAttributeTextBlockAsync(char[] chars, int index, int count)
		{
			int writeLen = 0;
			int curIndex = index;
			int leftCount = count;
			do
			{
				writeLen = this.WriteAttributeTextBlockNoFlush(chars, curIndex, leftCount);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011508 File Offset: 0x0000F708
		protected Task WriteAttributeTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.WriteAttributeTextBlockNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._WriteAttributeTextBlockAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011548 File Offset: 0x0000F748
		private async Task _WriteAttributeTextBlockAsync(string text, int curIndex, int leftCount)
		{
			await this.FlushBufferAsync().ConfigureAwait(false);
			int writeLen;
			do
			{
				writeLen = this.WriteAttributeTextBlockNoFlush(text, curIndex, leftCount);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000115A4 File Offset: 0x0000F7A4
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_020F;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_011A;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_0205;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_0176;
						case NewLineHandling.Entitize:
							ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_0205;
						case NewLineHandling.None:
							*ptr3 = (char)num;
							ptr3++;
							goto IL_0205;
						default:
							goto IL_0205;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_011A;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_0205;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_011A;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_0205;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_0205;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_0205:
				pSrc++;
				continue;
				IL_011A:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_0205;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_0176:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_020F:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000117E4 File Offset: 0x0000F9E4
		protected unsafe int WriteElementTextBlockNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + count;
				return this.WriteElementTextBlockNoFlush(ptr2, ptr3, out needWriteNewLine);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011820 File Offset: 0x0000FA20
		protected unsafe int WriteElementTextBlockNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* ptr3 = ptr2 + count;
			return this.WriteElementTextBlockNoFlush(ptr2, ptr3, out needWriteNewLine);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011868 File Offset: 0x0000FA68
		protected async Task WriteElementTextBlockAsync(char[] chars, int index, int count)
		{
			int writeLen = 0;
			int curIndex = index;
			int leftCount = count;
			bool needWriteNewLine = false;
			do
			{
				writeLen = this.WriteElementTextBlockNoFlush(chars, curIndex, leftCount, out needWriteNewLine);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (needWriteNewLine)
				{
					await this.RawTextAsync(this.newLineChars).ConfigureAwait(false);
					curIndex++;
					leftCount--;
				}
				else if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0 || needWriteNewLine);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000118C4 File Offset: 0x0000FAC4
		protected Task WriteElementTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			bool flag = false;
			int num3 = this.WriteElementTextBlockNoFlush(text, num, num2, out flag);
			num += num3;
			num2 -= num3;
			if (flag)
			{
				return this._WriteElementTextBlockAsync(true, text, num, num2);
			}
			if (num3 >= 0)
			{
				return this._WriteElementTextBlockAsync(false, text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011914 File Offset: 0x0000FB14
		private async Task _WriteElementTextBlockAsync(bool newLine, string text, int curIndex, int leftCount)
		{
			int writeLen = 0;
			bool needWriteNewLine = false;
			if (newLine)
			{
				await this.RawTextAsync(this.newLineChars).ConfigureAwait(false);
				curIndex++;
				leftCount--;
			}
			else
			{
				await this.FlushBufferAsync().ConfigureAwait(false);
			}
			do
			{
				writeLen = this.WriteElementTextBlockNoFlush(text, curIndex, leftCount, out needWriteNewLine);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (needWriteNewLine)
				{
					await this.RawTextAsync(this.newLineChars).ConfigureAwait(false);
					curIndex++;
					leftCount--;
				}
				else if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0 || needWriteNewLine);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011978 File Offset: 0x0000FB78
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr2 + (long)(pSrcEnd - ptr3) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) < 55296)
				{
					ptr3++;
					*ptr2 = (char)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					goto IL_00F9;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					*ptr2 = (char)num;
					ptr2++;
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			return (int)((long)(ptr3 - pSrcBegin));
			IL_00F9:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011A90 File Offset: 0x0000FC90
		protected unsafe int RawTextNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* ptr3 = ptr2 + count;
			return this.RawTextNoFlush(ptr2, ptr3);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		protected Task RawTextAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.RawTextNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._RawTextAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011B08 File Offset: 0x0000FD08
		private async Task _RawTextAsync(string text, int curIndex, int leftCount)
		{
			await this.FlushBufferAsync().ConfigureAwait(false);
			int writeLen = 0;
			do
			{
				writeLen = this.RawTextNoFlush(text, curIndex, leftCount);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011B64 File Offset: 0x0000FD64
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
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
			char* ptr2 = pSrcBegin;
			char* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - ptr2) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					goto IL_01CC;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_00EB;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_12;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_01C3;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_10;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_01C3;
					default:
						if (num == 38)
						{
							goto IL_00EB;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_00EB;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
					ptr2 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, false);
					ptr2++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				ptr2++;
				continue;
				IL_01C3:
				ptr2++;
				continue;
				IL_00EB:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_01C3;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_10:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_12:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_01CC:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011D50 File Offset: 0x0000FF50
		protected unsafe int WriteRawWithCharCheckingNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + count;
				return this.WriteRawWithCharCheckingNoFlush(ptr2, ptr3, out needWriteNewLine);
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011D84 File Offset: 0x0000FF84
		protected unsafe int WriteRawWithCharCheckingNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* ptr3 = ptr2 + count;
			return this.WriteRawWithCharCheckingNoFlush(ptr2, ptr3, out needWriteNewLine);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		protected async Task WriteRawWithCharCheckingAsync(char[] chars, int index, int count)
		{
			int writeLen = 0;
			int curIndex = index;
			int leftCount = count;
			bool needWriteNewLine = false;
			do
			{
				writeLen = this.WriteRawWithCharCheckingNoFlush(chars, curIndex, leftCount, out needWriteNewLine);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (needWriteNewLine)
				{
					await this.RawTextAsync(this.newLineChars).ConfigureAwait(false);
					curIndex++;
					leftCount--;
				}
				else if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0 || needWriteNewLine);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011E20 File Offset: 0x00010020
		protected async Task WriteRawWithCharCheckingAsync(string text)
		{
			int writeLen = 0;
			int curIndex = 0;
			int leftCount = text.Length;
			bool needWriteNewLine = false;
			do
			{
				writeLen = this.WriteRawWithCharCheckingNoFlush(text, curIndex, leftCount, out needWriteNewLine);
				curIndex += writeLen;
				leftCount -= writeLen;
				if (needWriteNewLine)
				{
					await this.RawTextAsync(this.newLineChars).ConfigureAwait(false);
					curIndex++;
					leftCount--;
				}
				else if (writeLen >= 0)
				{
					await this.FlushBufferAsync().ConfigureAwait(false);
				}
			}
			while (writeLen >= 0 || needWriteNewLine);
		}

		// Token: 0x04000180 RID: 384
		private readonly bool useAsync;

		// Token: 0x04000181 RID: 385
		protected byte[] bufBytes;

		// Token: 0x04000182 RID: 386
		protected Stream stream;

		// Token: 0x04000183 RID: 387
		protected Encoding encoding;

		// Token: 0x04000184 RID: 388
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000185 RID: 389
		protected int bufPos = 1;

		// Token: 0x04000186 RID: 390
		protected int textPos = 1;

		// Token: 0x04000187 RID: 391
		protected int contentPos;

		// Token: 0x04000188 RID: 392
		protected int cdataPos;

		// Token: 0x04000189 RID: 393
		protected int attrEndPos;

		// Token: 0x0400018A RID: 394
		protected int bufLen = 6144;

		// Token: 0x0400018B RID: 395
		protected bool writeToNull;

		// Token: 0x0400018C RID: 396
		protected bool hadDoubleBracket;

		// Token: 0x0400018D RID: 397
		protected bool inAttributeValue;

		// Token: 0x0400018E RID: 398
		protected int bufBytesUsed;

		// Token: 0x0400018F RID: 399
		protected char[] bufChars;

		// Token: 0x04000190 RID: 400
		protected Encoder encoder;

		// Token: 0x04000191 RID: 401
		protected TextWriter writer;

		// Token: 0x04000192 RID: 402
		protected bool trackTextContent;

		// Token: 0x04000193 RID: 403
		protected bool inTextContent;

		// Token: 0x04000194 RID: 404
		private int lastMarkPos;

		// Token: 0x04000195 RID: 405
		private int[] textContentMarks;

		// Token: 0x04000196 RID: 406
		private CharEntityEncoderFallback charEntityFallback;

		// Token: 0x04000197 RID: 407
		protected NewLineHandling newLineHandling;

		// Token: 0x04000198 RID: 408
		protected bool closeOutput;

		// Token: 0x04000199 RID: 409
		protected bool omitXmlDeclaration;

		// Token: 0x0400019A RID: 410
		protected string newLineChars;

		// Token: 0x0400019B RID: 411
		protected bool checkCharacters;

		// Token: 0x0400019C RID: 412
		protected XmlStandalone standalone;

		// Token: 0x0400019D RID: 413
		protected XmlOutputMethod outputMethod;

		// Token: 0x0400019E RID: 414
		protected bool autoXmlDeclaration;

		// Token: 0x0400019F RID: 415
		protected bool mergeCDataSections;
	}
}
