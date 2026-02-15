using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000067 RID: 103
	internal abstract class XmlRawWriter : XmlWriter
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteStartDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteStartDocument(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteEndDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteFullEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000152B5 File Offset: 0x000134B5
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			this.base64Encoder.Encode(buffer, index, count);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override string LookupPrefix(string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override WriteState WriteState
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override string XmlLang
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteNmToken(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteName(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014D86 File Offset: 0x00012F86
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000152D9 File Offset: 0x000134D9
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(new char[] { ch }));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000152F0 File Offset: 0x000134F0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(new string(new char[] { lowChar, highChar }));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014D86 File Offset: 0x00012F86
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000E397 File Offset: 0x0000C597
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000E397 File Offset: 0x0000C597
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014D86 File Offset: 0x00012F86
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001530B File Offset: 0x0001350B
		public override void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00014D86 File Offset: 0x00012F86
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x170000A0 RID: 160
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00015332 File Offset: 0x00013532
		internal virtual IXmlNamespaceResolver NamespaceResolver
		{
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000432 RID: 1074
		internal abstract void StartElementContent();

		// Token: 0x06000433 RID: 1075 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void OnRootElement(ConformanceLevel conformanceLevel)
		{
		}

		// Token: 0x06000434 RID: 1076
		internal abstract void WriteEndElement(string prefix, string localName, string ns);

		// Token: 0x06000435 RID: 1077 RVA: 0x0001533B File Offset: 0x0001353B
		internal virtual void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00015346 File Offset: 0x00013546
		internal virtual void WriteQualifiedName(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				this.WriteString(prefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x06000437 RID: 1079
		internal abstract void WriteNamespaceDeclaration(string prefix, string ns);

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal virtual bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00004C6A File Offset: 0x00002E6A
		internal virtual void WriteStartNamespaceDeclaration(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00004C6A File Offset: 0x00002E6A
		internal virtual void WriteEndNamespaceDeclaration()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00015369 File Offset: 0x00013569
		internal virtual void WriteEndBase64()
		{
			this.base64Encoder.Flush();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00015376 File Offset: 0x00013576
		internal virtual void Close(WriteState currentState)
		{
			this.Close();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000AB38 File Offset: 0x00008D38
		public override Task WriteStartDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001537E File Offset: 0x0001357E
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			return this.base64Encoder.EncodeAsync(buffer, index, count);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000153A2 File Offset: 0x000135A2
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.WriteStringAsync(new string(new char[] { ch }));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000153B9 File Offset: 0x000135B9
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.WriteStringAsync(new string(new char[] { lowChar, highChar }));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000153D4 File Offset: 0x000135D4
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.WriteStringAsync(ws);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000153DD File Offset: 0x000135DD
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000153DD File Offset: 0x000135DD
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000153D4 File Offset: 0x000135D4
		public override Task WriteRawAsync(string data)
		{
			return this.WriteStringAsync(data);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000153ED File Offset: 0x000135ED
		internal virtual Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00002CE0 File Offset: 0x00000EE0
		internal virtual Task WriteNamespaceDeclarationAsync(string prefix, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00004C6A File Offset: 0x00002E6A
		internal virtual Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00004C6A File Offset: 0x00002E6A
		internal virtual Task WriteEndNamespaceDeclarationAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000153F4 File Offset: 0x000135F4
		internal virtual Task WriteEndBase64Async()
		{
			return this.base64Encoder.FlushAsync();
		}

		// Token: 0x0400025C RID: 604
		protected XmlRawWriterBase64Encoder base64Encoder;

		// Token: 0x0400025D RID: 605
		protected IXmlNamespaceResolver resolver;
	}
}
