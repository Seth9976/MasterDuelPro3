using System;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200004C RID: 76
	internal class XmlAsyncCheckWriter : XmlWriter
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000DC33 File Offset: 0x0000BE33
		public XmlAsyncCheckWriter(XmlWriter writer)
		{
			this.coreWriter = writer;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000DC4D File Offset: 0x0000BE4D
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.coreWriter.Settings;
				if (xmlWriterSettings != null)
				{
					xmlWriterSettings = xmlWriterSettings.Clone();
				}
				else
				{
					xmlWriterSettings = new XmlWriterSettings();
				}
				xmlWriterSettings.Async = true;
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000DCA6 File Offset: 0x0000BEA6
		public override void WriteStartDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000DCB9 File Offset: 0x0000BEB9
		public override void WriteStartDocument(bool standalone)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument(standalone);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000DCCD File Offset: 0x0000BECD
		public override void WriteEndDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndDocument();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			this.coreWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000DD0E File Offset: 0x0000BF0E
		public override void WriteEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndElement();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DD21 File Offset: 0x0000BF21
		public override void WriteFullEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteFullEndElement();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DD4A File Offset: 0x0000BF4A
		public override void WriteEndAttribute()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndAttribute();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public override void WriteCData(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteCData(text);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000DD71 File Offset: 0x0000BF71
		public override void WriteComment(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteComment(text);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DD85 File Offset: 0x0000BF85
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		public override void WriteEntityRef(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteEntityRef(name);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DDAE File Offset: 0x0000BFAE
		public override void WriteCharEntity(char ch)
		{
			this.CheckAsync();
			this.coreWriter.WriteCharEntity(ch);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000DDC2 File Offset: 0x0000BFC2
		public override void WriteWhitespace(string ws)
		{
			this.CheckAsync();
			this.coreWriter.WriteWhitespace(ws);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		public override void WriteString(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteString(text);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.CheckAsync();
			this.coreWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DDFF File Offset: 0x0000BFFF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DE15 File Offset: 0x0000C015
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DE2B File Offset: 0x0000C02B
		public override void WriteRaw(string data)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(data);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000DE3F File Offset: 0x0000C03F
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000DE55 File Offset: 0x0000C055
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000DE6B File Offset: 0x0000C06B
		public override WriteState WriteState
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.WriteState;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000DE7E File Offset: 0x0000C07E
		public override void Close()
		{
			this.CheckAsync();
			this.coreWriter.Close();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000DE91 File Offset: 0x0000C091
		public override void Flush()
		{
			this.CheckAsync();
			this.coreWriter.Flush();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public override string LookupPrefix(string ns)
		{
			this.CheckAsync();
			return this.coreWriter.LookupPrefix(ns);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlSpace;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000DECB File Offset: 0x0000C0CB
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlLang;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000DEDE File Offset: 0x0000C0DE
		public override void WriteNmToken(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteNmToken(name);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000DEF2 File Offset: 0x0000C0F2
		public override void WriteName(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteName(name);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000DF06 File Offset: 0x0000C106
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000DF1B File Offset: 0x0000C11B
		public override void WriteValue(object value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public override void WriteValue(string value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000DF43 File Offset: 0x0000C143
		public override void WriteValue(bool value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000DF57 File Offset: 0x0000C157
		public override void WriteValue(DateTime value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DF6B File Offset: 0x0000C16B
		public override void WriteValue(double value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DF7F File Offset: 0x0000C17F
		public override void WriteValue(float value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DF93 File Offset: 0x0000C193
		public override void WriteValue(decimal value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DFA7 File Offset: 0x0000C1A7
		public override void WriteValue(int value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		public override void WriteValue(long value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteAttributes(reader, defattr);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(reader, defattr);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000DFF9 File Offset: 0x0000C1F9
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(navigator, defattr);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E00E File Offset: 0x0000C20E
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreWriter.Dispose();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E024 File Offset: 0x0000C224
		public override Task WriteStartDocumentAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartDocumentAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E04C File Offset: 0x0000C24C
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartAttributeAsync(prefix, localName, ns);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E078 File Offset: 0x0000C278
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEndAttributeAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public override Task WriteEntityRefAsync(string name)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEntityRefAsync(name);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public override Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCharEntityAsync(ch);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteWhitespaceAsync(ws);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E118 File Offset: 0x0000C318
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStringAsync(text);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E140 File Offset: 0x0000C340
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteSurrogateCharEntityAsync(lowChar, highChar);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E16C File Offset: 0x0000C36C
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCharsAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E198 File Offset: 0x0000C398
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteRawAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public override Task WriteRawAsync(string data)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteRawAsync(data);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteBase64Async(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x04000178 RID: 376
		private readonly XmlWriter coreWriter;

		// Token: 0x04000179 RID: 377
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
