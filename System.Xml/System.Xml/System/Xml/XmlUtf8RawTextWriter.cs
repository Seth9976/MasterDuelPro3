using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000087 RID: 135
	internal class XmlUtf8RawTextWriter : XmlRawWriter
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0002716C File Offset: 0x0002536C
		protected XmlUtf8RawTextWriter(XmlWriterSettings settings)
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

		// Token: 0x06000748 RID: 1864 RVA: 0x00027234 File Offset: 0x00025434
		public XmlUtf8RawTextWriter(Stream stream, XmlWriterSettings settings)
			: this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufBytes = new byte[this.bufLen + 32];
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					Buffer.BlockCopy(preamble, 0, this.bufBytes, 1, preamble.Length);
					this.bufPos += preamble.Length;
					this.textPos += preamble.Length;
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000272F0 File Offset: 0x000254F0
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

		// Token: 0x0600074A RID: 1866 RVA: 0x0002737C File Offset: 0x0002557C
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
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

		// Token: 0x0600074B RID: 1867 RVA: 0x00027402 File Offset: 0x00025602
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00027420 File Offset: 0x00025620
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
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
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0002752C File Offset: 0x0002572C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00027594 File Offset: 0x00025794
		internal override void StartElementContent()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000275C8 File Offset: 0x000257C8
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			int num;
			if (this.contentPos != this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					byte[] array3 = this.bufBytes;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000276C4 File Offset: 0x000258C4
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00027754 File Offset: 0x00025954
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000277F8 File Offset: 0x000259F8
		public override void WriteEndAttribute()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00027834 File Offset: 0x00025A34
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000278A4 File Offset: 0x00025AA4
		internal override void WriteEndNamespaceDeclaration()
		{
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000278E0 File Offset: 0x00025AE0
		public override void WriteCData(string text)
		{
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				byte[] array6 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				byte[] array7 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				byte[] array8 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				byte[] array9 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			byte[] array10 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			byte[] array11 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			byte[] array12 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00027A70 File Offset: 0x00025C70
		public override void WriteComment(string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00027B3C File Offset: 0x00025D3C
		public override void WriteProcessingInstruction(string name, string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00027BE4 File Offset: 0x00025DE4
		public override void WriteEntityRef(string name)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00027C4C File Offset: 0x00025E4C
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			byte[] array = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(text);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00027D1C File Offset: 0x00025F1C
		public unsafe override void WriteWhitespace(string ws)
		{
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

		// Token: 0x0600075D RID: 1885 RVA: 0x00027D64 File Offset: 0x00025F64
		public unsafe override void WriteString(string text)
		{
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

		// Token: 0x0600075E RID: 1886 RVA: 0x00027DAC File Offset: 0x00025FAC
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			byte[] array = this.bufBytes;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			byte[] array2 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			byte[] array3 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			byte[] array4 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00027E4C File Offset: 0x0002604C
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
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

		// Token: 0x06000760 RID: 1888 RVA: 0x00027E8C File Offset: 0x0002608C
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00027EC0 File Offset: 0x000260C0
		public unsafe override void WriteRaw(string data)
		{
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

		// Token: 0x06000762 RID: 1890 RVA: 0x00027F00 File Offset: 0x00026100
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
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00027F80 File Offset: 0x00026180
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00027FA4 File Offset: 0x000261A4
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					this.stream.Write(this.bufBytes, 1, this.bufPos - 1);
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufBytes[0] = this.bufBytes[this.bufPos - 1];
				if (XmlUtf8RawTextWriter.IsSurrogateByte(this.bufBytes[0]))
				{
					this.bufBytes[1] = this.bufBytes[this.bufPos];
					this.bufBytes[2] = this.bufBytes[this.bufPos + 1];
					this.bufBytes[3] = this.bufBytes[this.bufPos + 2];
				}
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000A558 File Offset: 0x00008758
		private void FlushEncoder()
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000280B0 File Offset: 0x000262B0
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
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
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_01CC;
							}
							ptr2 = XmlUtf8RawTextWriter.TabEntity(ptr2);
							goto IL_01CC;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_01CC;
							}
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_01CC;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_01CC;
							}
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_01CC;
						default:
							if (num == 34)
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_01CC;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_01CC;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_01CC;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_01CC;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_01CC;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_01CC:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000282A4 File Offset: 0x000264A4
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
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
							goto IL_0108;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_01CF;
							}
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_01CF;
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
								goto IL_01CF;
							case NewLineHandling.Entitize:
								ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_01CF;
							case NewLineHandling.None:
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_01CF;
							default:
								goto IL_01CF;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_0108;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_01CF;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_0108;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_01CF;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_01CF;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_01CF:
					pSrc++;
					continue;
					IL_0108:
					*ptr2 = (byte)num;
					ptr2++;
					goto IL_01CF;
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

		// Token: 0x06000768 RID: 1896 RVA: 0x000284AC File Offset: 0x000266AC
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

		// Token: 0x06000769 RID: 1897 RVA: 0x000284E0 File Offset: 0x000266E0
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
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
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000285D8 File Offset: 0x000267D8
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
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
							goto IL_00D9;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_0180;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_0180;
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
								goto IL_0180;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_0180;
						default:
							if (num == 38)
							{
								goto IL_00D9;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_00D9;
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
						ptr2 += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr3 = this.InvalidXmlChar(num, ptr3, false);
						ptr2++;
						continue;
					}
					ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
					ptr2++;
					continue;
					IL_0180:
					ptr2++;
					continue;
					IL_00D9:
					*ptr3 = (byte)num;
					ptr3++;
					goto IL_0180;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00028780 File Offset: 0x00026980
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
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar && num <= 127)
					{
						*ptr5 = (byte)num;
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
								goto IL_0220;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_028F;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_028F;
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
									goto IL_028F;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_028F;
							default:
								if (num == 38)
								{
									goto IL_0220;
								}
								if (num == 45)
								{
									*ptr5 = 45;
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = 32;
										ptr5++;
										goto IL_028F;
									}
									goto IL_028F;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_0220;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = 93;
									ptr5++;
									goto IL_028F;
								}
							}
							else
							{
								*ptr5 = 63;
								ptr5++;
								if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
								{
									*ptr5 = 32;
									ptr5++;
									goto IL_028F;
								}
								goto IL_028F;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_028F:
						ptr3++;
						continue;
						IL_0220:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_028F;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00028A3C File Offset: 0x00026C3C
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
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93 && num <= 127)
					{
						*ptr5 = (byte)num;
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
								goto IL_0204;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_0273;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_0273;
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
									goto IL_0273;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_0273;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_0204;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_0204;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == 93)
								{
									ptr5 = XmlUtf8RawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlUtf8RawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = 62;
								ptr5++;
								goto IL_0273;
							}
							if (num == 93)
							{
								if (ptr5[-1] == 93)
								{
									this.hadDoubleBracket = true;
								}
								else
								{
									this.hadDoubleBracket = false;
								}
								*ptr5 = 93;
								ptr5++;
								goto IL_0273;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_0273:
						ptr3++;
						continue;
						IL_0204:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_0273;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00028CD9 File Offset: 0x00026ED9
		private static bool IsSurrogateByte(byte b)
		{
			return (b & 248) == 240;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00028CEC File Offset: 0x00026EEC
		private unsafe static byte* EncodeSurrogate(char* pSrc, char* pSrcEnd, byte* pDst)
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
				num = XmlCharType.CombineSurrogateChar(num2, num);
				*pDst = (byte)(240 | (num >> 18));
				pDst[1] = (byte)(128 | ((num >> 12) & 63));
				pDst[2] = (byte)(128 | ((num >> 6) & 63));
				pDst[3] = (byte)(128 | (num & 63));
				pDst += 4;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00028D98 File Offset: 0x00026F98
		private unsafe byte* InvalidXmlChar(int ch, byte* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlUtf8RawTextWriter.CharEntity(pDst, (char)ch);
			}
			if (ch < 128)
			{
				*pDst = (byte)ch;
				pDst++;
			}
			else
			{
				pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(ch, pDst);
			}
			return pDst;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00028DD4 File Offset: 0x00026FD4
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00028E34 File Offset: 0x00027034
		internal unsafe static byte* EncodeMultibyteUTF8(int ch, byte* pDst)
		{
			if (ch < 2048)
			{
				*pDst = (byte)(-64 | (ch >> 6));
			}
			else
			{
				*pDst = (byte)(-32 | (ch >> 12));
				pDst++;
				*pDst = (byte)(-128 | ((ch >> 6) & 63));
			}
			pDst++;
			*pDst = (byte)(128 | (ch & 63));
			return pDst + 1;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00028E84 File Offset: 0x00027084
		internal unsafe static void CharToUTF8(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (num <= 127)
			{
				*pDst = (byte)num;
				pDst++;
				pSrc += 2;
				return;
			}
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00028EDC File Offset: 0x000270DC
		protected unsafe byte* WriteNewLine(byte* pDst)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
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

		// Token: 0x06000774 RID: 1908 RVA: 0x00028F27 File Offset: 0x00027127
		protected unsafe static byte* LtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 108;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00028F42 File Offset: 0x00027142
		protected unsafe static byte* GtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 103;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00028F5D File Offset: 0x0002715D
		protected unsafe static byte* AmpEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 97;
			pDst[2] = 109;
			pDst[3] = 112;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00028F7E File Offset: 0x0002717E
		protected unsafe static byte* QuoteEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 113;
			pDst[2] = 117;
			pDst[3] = 111;
			pDst[4] = 116;
			pDst[5] = 59;
			return pDst + 6;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00028FA5 File Offset: 0x000271A5
		protected unsafe static byte* TabEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 57;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00028FC6 File Offset: 0x000271C6
		protected unsafe static byte* LineFeedEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 65;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00028FE7 File Offset: 0x000271E7
		protected unsafe static byte* CarriageReturnEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 68;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00029008 File Offset: 0x00027208
		private unsafe static byte* CharEntity(byte* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = (byte)(*(ptr2++))) != 0)
				{
				}
			}
			pDst[-1] = 59;
			return pDst;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002906C File Offset: 0x0002726C
		protected unsafe static byte* RawStartCData(byte* pDst)
		{
			*pDst = 60;
			pDst[1] = 33;
			pDst[2] = 91;
			pDst[3] = 67;
			pDst[4] = 68;
			pDst[5] = 65;
			pDst[6] = 84;
			pDst[7] = 65;
			pDst[8] = 91;
			return pDst + 9;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000290A6 File Offset: 0x000272A6
		protected unsafe static byte* RawEndCData(byte* pDst)
		{
			*pDst = 93;
			pDst[1] = 93;
			pDst[2] = 62;
			return pDst + 3;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000290BC File Offset: 0x000272BC
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

		// Token: 0x0600077F RID: 1919 RVA: 0x00029216 File Offset: 0x00027416
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Set XmlWriterSettings.Async to true if you want to use Async Methods."));
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00029230 File Offset: 0x00027430
		internal override async Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
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

		// Token: 0x06000781 RID: 1921 RVA: 0x0002927C File Offset: 0x0002747C
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName + "=\"");
			}
			else
			{
				task = this.RawTextAsync(localName + "=\"");
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000D07C File Offset: 0x0000B27C
		private void WriteStartAttribute_SetInAttribute()
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00029304 File Offset: 0x00027504
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002934C File Offset: 0x0002754C
		internal override async Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			this.CheckAsyncCall();
			await this.WriteStartNamespaceDeclarationAsync(prefix).ConfigureAwait(false);
			await this.WriteStringAsync(namespaceName).ConfigureAwait(false);
			await this.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000293A0 File Offset: 0x000275A0
		internal override async Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			this.CheckAsyncCall();
			if (prefix.Length == 0)
			{
				await this.RawTextAsync(" xmlns=\"").ConfigureAwait(false);
			}
			else
			{
				await this.RawTextAsync(" xmlns:").ConfigureAwait(false);
				await this.RawTextAsync(prefix).ConfigureAwait(false);
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000293EC File Offset: 0x000275EC
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00029434 File Offset: 0x00027634
		public override async Task WriteEntityRefAsync(string name)
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			await this.RawTextAsync(name).ConfigureAwait(false);
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				await this.FlushBufferAsync().ConfigureAwait(false);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00029480 File Offset: 0x00027680
		public override async Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsyncCall();
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			byte[] array = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			await this.RawTextAsync(text).ConfigureAwait(false);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				await this.FlushBufferAsync().ConfigureAwait(false);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000294CB File Offset: 0x000276CB
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000294CB File Offset: 0x000276CB
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000294EC File Offset: 0x000276EC
		public override async Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsyncCall();
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			byte[] array = this.bufBytes;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			byte[] array2 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			byte[] array3 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			await this.RawTextAsync(num.ToString("X", NumberFormatInfo.InvariantInfo)).ConfigureAwait(false);
			byte[] array4 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002953F File Offset: 0x0002773F
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00029564 File Offset: 0x00027764
		public override async Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			await this.WriteRawWithCharCheckingAsync(buffer, index, count).ConfigureAwait(false);
			this.textPos = this.bufPos;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000295C0 File Offset: 0x000277C0
		public override async Task WriteRawAsync(string data)
		{
			this.CheckAsyncCall();
			await this.WriteRawWithCharCheckingAsync(data).ConfigureAwait(false);
			this.textPos = this.bufPos;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0002960C File Offset: 0x0002780C
		protected virtual async Task FlushBufferAsync()
		{
			try
			{
				if (!this.writeToNull)
				{
					await this.stream.WriteAsync(this.bufBytes, 1, this.bufPos - 1).ConfigureAwait(false);
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufBytes[0] = this.bufBytes[this.bufPos - 1];
				if (XmlUtf8RawTextWriter.IsSurrogateByte(this.bufBytes[0]))
				{
					this.bufBytes[1] = this.bufBytes[this.bufPos];
					this.bufBytes[2] = this.bufBytes[this.bufPos + 1];
					this.bufBytes[3] = this.bufBytes[this.bufPos + 2];
				}
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00029650 File Offset: 0x00027850
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_01E8;
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
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_01DE;
						}
						ptr3 = XmlUtf8RawTextWriter.TabEntity(ptr3);
						goto IL_01DE;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_01DE;
						}
						ptr3 = XmlUtf8RawTextWriter.LineFeedEntity(ptr3);
						goto IL_01DE;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_01DE;
						}
						ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_01DE;
					default:
						if (num == 34)
						{
							ptr3 = XmlUtf8RawTextWriter.QuoteEntity(ptr3);
							goto IL_01DE;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_01DE;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_01DE;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_01DE;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_01DE;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_01DE:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_01E8:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00029858 File Offset: 0x00027A58
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

		// Token: 0x06000792 RID: 1938 RVA: 0x00029884 File Offset: 0x00027A84
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

		// Token: 0x06000793 RID: 1939 RVA: 0x000298BC File Offset: 0x00027ABC
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

		// Token: 0x06000794 RID: 1940 RVA: 0x00029918 File Offset: 0x00027B18
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

		// Token: 0x06000795 RID: 1941 RVA: 0x00029958 File Offset: 0x00027B58
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

		// Token: 0x06000796 RID: 1942 RVA: 0x000299B4 File Offset: 0x00027BB4
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_0209;
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
						goto IL_0114;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_14;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_01FF;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_0170;
						case NewLineHandling.Entitize:
							ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_01FF;
						case NewLineHandling.None:
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_01FF;
						default:
							goto IL_01FF;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_0114;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_01FF;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_0114;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_01FF;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_01FF;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_01FF:
				pSrc++;
				continue;
				IL_0114:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_01FF;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_14:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_0170:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_0209:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00029BF0 File Offset: 0x00027DF0
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

		// Token: 0x06000798 RID: 1944 RVA: 0x00029C2C File Offset: 0x00027E2C
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

		// Token: 0x06000799 RID: 1945 RVA: 0x00029C74 File Offset: 0x00027E74
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

		// Token: 0x0600079A RID: 1946 RVA: 0x00029CD0 File Offset: 0x00027ED0
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

		// Token: 0x0600079B RID: 1947 RVA: 0x00029D20 File Offset: 0x00027F20
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

		// Token: 0x0600079C RID: 1948 RVA: 0x00029D84 File Offset: 0x00027F84
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					goto IL_00E7;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			return (int)((long)(ptr3 - pSrcBegin));
			IL_00E7:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00029E88 File Offset: 0x00028088
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

		// Token: 0x0600079E RID: 1950 RVA: 0x00029EC0 File Offset: 0x000280C0
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

		// Token: 0x0600079F RID: 1951 RVA: 0x00029F00 File Offset: 0x00028100
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x00029F5C File Offset: 0x0002815C
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					goto IL_01C5;
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
						goto IL_00E5;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_01BC;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_11;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_01BC;
					default:
						if (num == 38)
						{
							goto IL_00E5;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_00E5;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
					ptr2 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, false);
					ptr2++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				ptr2++;
				continue;
				IL_01BC:
				ptr2++;
				continue;
				IL_00E5:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_01BC;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_11:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_01C5:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002A140 File Offset: 0x00028340
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

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002A174 File Offset: 0x00028374
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

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002A1B4 File Offset: 0x000283B4
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

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002A210 File Offset: 0x00028410
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

		// Token: 0x040003BA RID: 954
		private readonly bool useAsync;

		// Token: 0x040003BB RID: 955
		protected byte[] bufBytes;

		// Token: 0x040003BC RID: 956
		protected Stream stream;

		// Token: 0x040003BD RID: 957
		protected Encoding encoding;

		// Token: 0x040003BE RID: 958
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040003BF RID: 959
		protected int bufPos = 1;

		// Token: 0x040003C0 RID: 960
		protected int textPos = 1;

		// Token: 0x040003C1 RID: 961
		protected int contentPos;

		// Token: 0x040003C2 RID: 962
		protected int cdataPos;

		// Token: 0x040003C3 RID: 963
		protected int attrEndPos;

		// Token: 0x040003C4 RID: 964
		protected int bufLen = 6144;

		// Token: 0x040003C5 RID: 965
		protected bool writeToNull;

		// Token: 0x040003C6 RID: 966
		protected bool hadDoubleBracket;

		// Token: 0x040003C7 RID: 967
		protected bool inAttributeValue;

		// Token: 0x040003C8 RID: 968
		protected NewLineHandling newLineHandling;

		// Token: 0x040003C9 RID: 969
		protected bool closeOutput;

		// Token: 0x040003CA RID: 970
		protected bool omitXmlDeclaration;

		// Token: 0x040003CB RID: 971
		protected string newLineChars;

		// Token: 0x040003CC RID: 972
		protected bool checkCharacters;

		// Token: 0x040003CD RID: 973
		protected XmlStandalone standalone;

		// Token: 0x040003CE RID: 974
		protected XmlOutputMethod outputMethod;

		// Token: 0x040003CF RID: 975
		protected bool autoXmlDeclaration;

		// Token: 0x040003D0 RID: 976
		protected bool mergeCDataSections;
	}
}
