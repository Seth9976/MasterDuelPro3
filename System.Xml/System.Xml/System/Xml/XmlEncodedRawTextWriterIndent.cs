using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000060 RID: 96
	internal class XmlEncodedRawTextWriterIndent : XmlEncodedRawTextWriter
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0001402E File Offset: 0x0001222E
		public XmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001403F File Offset: 0x0001223F
		public XmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00014050 File Offset: 0x00012250
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

		// Token: 0x060003AE RID: 942 RVA: 0x00014085 File Offset: 0x00012285
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000140B0 File Offset: 0x000122B0
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

		// Token: 0x060003B0 RID: 944 RVA: 0x00014101 File Offset: 0x00012301
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

		// Token: 0x060003B1 RID: 945 RVA: 0x00014135 File Offset: 0x00012335
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014140 File Offset: 0x00012340
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

		// Token: 0x060003B3 RID: 947 RVA: 0x000141A0 File Offset: 0x000123A0
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

		// Token: 0x060003B4 RID: 948 RVA: 0x000141FF File Offset: 0x000123FF
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00014218 File Offset: 0x00012418
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014228 File Offset: 0x00012428
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001424D File Offset: 0x0001244D
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014273 File Offset: 0x00012473
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014283 File Offset: 0x00012483
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014293 File Offset: 0x00012493
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000142A4 File Offset: 0x000124A4
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000142B4 File Offset: 0x000124B4
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000142C4 File Offset: 0x000124C4
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000142D6 File Offset: 0x000124D6
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000142E8 File Offset: 0x000124E8
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000142F8 File Offset: 0x000124F8
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001430C File Offset: 0x0001250C
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

		// Token: 0x060003C2 RID: 962 RVA: 0x000143A4 File Offset: 0x000125A4
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000143DC File Offset: 0x000125DC
		protected internal override async Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			base.CheckAsyncCall();
			if (this.newLineOnAttributes)
			{
				await this.WriteIndentAsync().ConfigureAwait(false);
			}
			await base.WriteStartAttributeAsync(prefix, localName, ns).ConfigureAwait(false);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00014437 File Offset: 0x00012637
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001444D File Offset: 0x0001264D
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00014463 File Offset: 0x00012663
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001447A File Offset: 0x0001267A
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00014490 File Offset: 0x00012690
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000144A6 File Offset: 0x000126A6
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000144BE File Offset: 0x000126BE
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000144D6 File Offset: 0x000126D6
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000144EC File Offset: 0x000126EC
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014504 File Offset: 0x00012704
		private async Task WriteIndentAsync()
		{
			base.CheckAsyncCall();
			await base.RawTextAsync(this.newLineChars).ConfigureAwait(false);
			for (int i = this.indentLevel; i > 0; i--)
			{
				await base.RawTextAsync(this.indentChars).ConfigureAwait(false);
			}
		}

		// Token: 0x0400021A RID: 538
		protected int indentLevel;

		// Token: 0x0400021B RID: 539
		protected bool newLineOnAttributes;

		// Token: 0x0400021C RID: 540
		protected string indentChars;

		// Token: 0x0400021D RID: 541
		protected bool mixedContent;

		// Token: 0x0400021E RID: 542
		private BitStack mixedContentStack;

		// Token: 0x0400021F RID: 543
		protected ConformanceLevel conformanceLevel;
	}
}
