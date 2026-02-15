using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000098 RID: 152
	internal class XmlUtf8RawTextWriterIndent : XmlUtf8RawTextWriter
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0002C0BE File Offset: 0x0002A2BE
		public XmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0002C0CF File Offset: 0x0002A2CF
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = base.Settings;
				settings.ReadOnly = false;
				settings.Indent = true;
				settings.IndentChars = this.indentChars;
				settings.NewLineOnAttributes = this.newLineOnAttributes;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002C104 File Offset: 0x0002A304
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002C130 File Offset: 0x0002A330
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002C181 File Offset: 0x0002A381
		internal override void StartElementContent()
		{
			if (this.indentLevel == 1 && this.conformanceLevel == ConformanceLevel.Document)
			{
				this.mixedContent = false;
			}
			else
			{
				this.mixedContent = this.mixedContentStack.PeekBit();
			}
			base.StartElementContent();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002C1B5 File Offset: 0x0002A3B5
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002C220 File Offset: 0x0002A420
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002C27F File Offset: 0x0002A47F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002C298 File Offset: 0x0002A498
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002C2CD File Offset: 0x0002A4CD
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002C2F3 File Offset: 0x0002A4F3
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002C303 File Offset: 0x0002A503
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002C313 File Offset: 0x0002A513
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002C324 File Offset: 0x0002A524
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002C334 File Offset: 0x0002A534
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002C344 File Offset: 0x0002A544
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002C356 File Offset: 0x0002A556
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002C368 File Offset: 0x0002A568
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002C378 File Offset: 0x0002A578
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002C38C File Offset: 0x0002A58C
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
			this.mixedContentStack = new BitStack();
			if (this.checkCharacters)
			{
				if (this.newLineOnAttributes)
				{
					base.ValidateContentChars(this.indentChars, "IndentChars", true);
					base.ValidateContentChars(this.newLineChars, "NewLineChars", true);
					return;
				}
				base.ValidateContentChars(this.indentChars, "IndentChars", false);
				if (this.newLineHandling != NewLineHandling.Replace)
				{
					base.ValidateContentChars(this.newLineChars, "NewLineChars", false);
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002C424 File Offset: 0x0002A624
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002C45C File Offset: 0x0002A65C
		protected internal override async Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			if (this.newLineOnAttributes)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteStartAttributeAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002C4B7 File Offset: 0x0002A6B7
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002C4CD File Offset: 0x0002A6CD
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002C4E3 File Offset: 0x0002A6E3
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002C4FA File Offset: 0x0002A6FA
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002C510 File Offset: 0x0002A710
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002C526 File Offset: 0x0002A726
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002C53E File Offset: 0x0002A73E
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002C556 File Offset: 0x0002A756
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002C56C File Offset: 0x0002A76C
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002C584 File Offset: 0x0002A784
		private async Task WriteIndentAsync()
		{
			base.CheckAsyncCall();
			await base.RawTextAsync(this.newLineChars).ConfigureAwait(false);
			for (int i = this.indentLevel; i > 0; i--)
			{
				await base.RawTextAsync(this.indentChars).ConfigureAwait(false);
			}
		}

		// Token: 0x04000444 RID: 1092
		protected int indentLevel;

		// Token: 0x04000445 RID: 1093
		protected bool newLineOnAttributes;

		// Token: 0x04000446 RID: 1094
		protected string indentChars;

		// Token: 0x04000447 RID: 1095
		protected bool mixedContent;

		// Token: 0x04000448 RID: 1096
		private BitStack mixedContentStack;

		// Token: 0x04000449 RID: 1097
		protected ConformanceLevel conformanceLevel;
	}
}
