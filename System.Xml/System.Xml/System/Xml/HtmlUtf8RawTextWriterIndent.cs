using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000029 RID: 41
	internal class HtmlUtf8RawTextWriterIndent : HtmlUtf8RawTextWriter
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0000BF21 File Offset: 0x0000A121
		public HtmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BF32 File Offset: 0x0000A132
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			base.WriteDocType(name, pubid, sysid, subset);
			this.endBlockPos = this.bufPos;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlUtf8RawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				if (this.endBlockPos == this.bufPos && (this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				byte[] bufBytes = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufBytes[num] = 60;
			}
			else
			{
				this.currentElementProperties = (ElementProperties)192U;
				if (this.endBlockPos == this.bufPos)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				byte[] bufBytes2 = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				bufBytes2[num] = 60;
				if (prefix.Length != 0)
				{
					base.RawText(prefix);
					byte[] bufBytes3 = this.bufBytes;
					num = this.bufPos;
					this.bufPos = num + 1;
					bufBytes3[num] = 58;
				}
			}
			base.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C050 File Offset: 0x0000A250
		internal override void StartElementContent()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteIndent();
				base.WriteMetaElement();
				this.endBlockPos = this.bufPos;
				return;
			}
			if ((this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			bool flag = (this.currentElementProperties & ElementProperties.BLOCK_WS) > ElementProperties.DEFAULT;
			if (flag && this.endBlockPos == this.bufPos && this.contentPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteEndElement(prefix, localName, ns);
			this.contentPos = 0;
			if (flag)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C12C File Offset: 0x0000A32C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				base.RawText(this.newLineChars);
				this.indentLevel++;
				this.WriteIndent();
				this.indentLevel--;
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C178 File Offset: 0x0000A378
		protected override void FlushBuffer()
		{
			this.endBlockPos = ((this.endBlockPos == this.bufPos) ? 1 : 0);
			base.FlushBuffer();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C198 File Offset: 0x0000A398
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x04000120 RID: 288
		private int indentLevel;

		// Token: 0x04000121 RID: 289
		private int endBlockPos;

		// Token: 0x04000122 RID: 290
		private string indentChars;

		// Token: 0x04000123 RID: 291
		private bool newLineOnAttributes;
	}
}
