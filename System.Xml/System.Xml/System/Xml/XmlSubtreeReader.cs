using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200006B RID: 107
	internal sealed class XmlSubtreeReader : XmlWrappingReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x000170F8 File Offset: 0x000152F8
		internal XmlSubtreeReader(XmlReader reader)
			: base(reader)
		{
			this.initialDepth = reader.Depth;
			this.state = XmlSubtreeReader.State.Initial;
			this.nsManager = new XmlNamespaceManager(reader.NameTable);
			this.xmlns = reader.NameTable.Add("xmlns");
			this.xmlnsUri = reader.NameTable.Add("http://www.w3.org/2000/xmlns/");
			this.tmpNode = new XmlSubtreeReader.NodeData();
			this.tmpNode.Set(XmlNodeType.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
			this.SetCurrentNode(this.tmpNode);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000171A6 File Offset: 0x000153A6
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NodeType;
				}
				return this.curNode.type;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000171C7 File Offset: 0x000153C7
		public override string Name
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Name;
				}
				return this.curNode.name;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000171E8 File Offset: 0x000153E8
		public override string LocalName
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.LocalName;
				}
				return this.curNode.localName;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00017209 File Offset: 0x00015409
		public override string NamespaceURI
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NamespaceURI;
				}
				return this.curNode.namespaceUri;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001722A File Offset: 0x0001542A
		public override string Prefix
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Prefix;
				}
				return this.curNode.prefix;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001724B File Offset: 0x0001544B
		public override string Value
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Value;
				}
				return this.curNode.value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001726C File Offset: 0x0001546C
		public override int Depth
		{
			get
			{
				int num = this.reader.Depth - this.initialDepth;
				if (this.curNsAttr != -1)
				{
					if (this.curNode.type == XmlNodeType.Text)
					{
						num += 2;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000172AE File Offset: 0x000154AE
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000172BB File Offset: 0x000154BB
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000172C8 File Offset: 0x000154C8
		public override bool EOF
		{
			get
			{
				return this.state == XmlSubtreeReader.State.EndOfFile || this.state == XmlSubtreeReader.State.Closed;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000172DE File Offset: 0x000154DE
		public override ReadState ReadState
		{
			get
			{
				if (this.reader.ReadState == ReadState.Error)
				{
					return ReadState.Error;
				}
				if (this.state <= XmlSubtreeReader.State.Closed)
				{
					return (ReadState)this.state;
				}
				return ReadState.Interactive;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00017301 File Offset: 0x00015501
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001730E File Offset: 0x0001550E
		public override int AttributeCount
		{
			get
			{
				if (!this.InAttributeActiveState)
				{
					return 0;
				}
				return this.reader.AttributeCount + this.nsAttrCount;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001732C File Offset: 0x0001552C
		public override string GetAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001738C File Offset: 0x0001558C
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name, namespaceURI);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && namespaceURI == this.xmlnsUri)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000173FC File Offset: 0x000155FC
		public override string GetAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				return this.reader.GetAttribute(i);
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				return this.nsAttributes[i - attributeCount].value;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017460 File Offset: 0x00015660
		public override bool MoveToAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000174C8 File Offset: 0x000156C8
		public override bool MoveToAttribute(string name, string ns)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name, ns))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && ns == this.xmlnsUri)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001753C File Offset: 0x0001573C
		public override void MoveToAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				this.reader.MoveToAttribute(i);
				this.curNsAttr = -1;
				this.useCurNode = false;
				return;
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				this.MoveToNsAttribute(i - attributeCount);
				return;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000175A6 File Offset: 0x000157A6
		public override bool MoveToFirstAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToFirstAttribute())
			{
				this.useCurNode = false;
				return true;
			}
			if (this.nsAttrCount > 0)
			{
				this.MoveToNsAttribute(0);
				return true;
			}
			return false;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000175DC File Offset: 0x000157DC
		public override bool MoveToNextAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1 && this.reader.MoveToNextAttribute())
			{
				return true;
			}
			if (this.curNsAttr + 1 < this.nsAttrCount)
			{
				this.MoveToNsAttribute(this.curNsAttr + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001762C File Offset: 0x0001582C
		public override bool MoveToElement()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			this.useCurNode = false;
			if (this.curNsAttr >= 0)
			{
				this.curNsAttr = -1;
				return true;
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001765C File Offset: 0x0001585C
		public override bool ReadAttributeValue()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.curNode.type == XmlNodeType.Text)
			{
				return false;
			}
			this.tmpNode.type = XmlNodeType.Text;
			this.tmpNode.value = this.curNode.value;
			this.SetCurrentNode(this.tmpNode);
			return true;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000176C8 File Offset: 0x000158C8
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				return true;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return false;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_00E5;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_00E5;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				return this.FinishReadElementContentAsBinary() && this.Read();
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				return this.FinishReadContentAsBinary() && this.Read();
			default:
				return false;
			}
			IL_0054:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth && (this.reader.NodeType == XmlNodeType.EndElement || (this.reader.NodeType == XmlNodeType.Element && this.reader.IsEmptyElement)))
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			if (this.reader.Read())
			{
				this.ProcessNamespaces();
				return true;
			}
			this.SetEmptyNode();
			return false;
			IL_00E5:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_0054;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000177F0 File Offset: 0x000159F0
		public override void Close()
		{
			if (this.state == XmlSubtreeReader.State.Closed)
			{
				return;
			}
			try
			{
				if (this.state != XmlSubtreeReader.State.EndOfFile)
				{
					this.reader.MoveToElement();
					if (this.reader.Depth == this.initialDepth && this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
					{
						this.reader.Read();
					}
					while (this.reader.Depth > this.initialDepth && this.reader.Read())
					{
					}
				}
			}
			catch
			{
			}
			finally
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Closed;
				this.SetEmptyNode();
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000178B4 File Offset: 0x00015AB4
		public override void Skip()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.Read();
				return;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
				return;
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_011A;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_011A;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				if (this.FinishReadElementContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				if (this.FinishReadContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			default:
				return;
			}
			IL_0042:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth)
			{
				if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement && this.reader.Read())
				{
					while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.Depth > this.initialDepth)
					{
						this.reader.Skip();
					}
				}
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
			{
				this.nsManager.PopScope();
			}
			this.reader.Skip();
			this.ProcessNamespaces();
			return;
			IL_011A:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_0042;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017A10 File Offset: 0x00015C10
		public override object ReadContentAsObject()
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAsObject");
				object obj = this.reader.ReadContentAsObject();
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017A58 File Offset: 0x00015C58
		public override bool ReadContentAsBoolean()
		{
			bool flag2;
			try
			{
				this.InitReadContentAsType("ReadContentAsBoolean");
				bool flag = this.reader.ReadContentAsBoolean();
				this.FinishReadContentAsType();
				flag2 = flag;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return flag2;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public override DateTime ReadContentAsDateTime()
		{
			DateTime dateTime2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDateTime");
				DateTime dateTime = this.reader.ReadContentAsDateTime();
				this.FinishReadContentAsType();
				dateTime2 = dateTime;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return dateTime2;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public override double ReadContentAsDouble()
		{
			double num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDouble");
				double num = this.reader.ReadContentAsDouble();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017B30 File Offset: 0x00015D30
		public override float ReadContentAsFloat()
		{
			float num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsFloat");
				float num = this.reader.ReadContentAsFloat();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017B78 File Offset: 0x00015D78
		public override decimal ReadContentAsDecimal()
		{
			decimal num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsDecimal");
				decimal num = this.reader.ReadContentAsDecimal();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017BC0 File Offset: 0x00015DC0
		public override int ReadContentAsInt()
		{
			int num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsInt");
				int num = this.reader.ReadContentAsInt();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017C08 File Offset: 0x00015E08
		public override long ReadContentAsLong()
		{
			long num2;
			try
			{
				this.InitReadContentAsType("ReadContentAsLong");
				long num = this.reader.ReadContentAsLong();
				this.FinishReadContentAsType();
				num2 = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return num2;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017C50 File Offset: 0x00015E50
		public override string ReadContentAsString()
		{
			string text2;
			try
			{
				this.InitReadContentAsType("ReadContentAsString");
				string text = this.reader.ReadContentAsString();
				this.FinishReadContentAsType();
				text2 = text;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return text2;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017C98 File Offset: 0x00015E98
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			object obj2;
			try
			{
				this.InitReadContentAsType("ReadContentAs");
				object obj = this.reader.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadContentAsType();
				obj2 = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return obj2;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00017CE4 File Offset: 0x00015EE4
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.reader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017CF4 File Offset: 0x00015EF4
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBase64;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is Base64Decoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new Base64Decoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBase64(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBase64:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017E90 File Offset: 0x00016090
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBase64))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017F84 File Offset: 0x00016184
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBinHex;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is BinHexDecoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new BinHexDecoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBinHex(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00018120 File Offset: 0x00016320
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBinHex))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00018212 File Offset: 0x00016412
		public override bool CanReadValueChunk
		{
			get
			{
				return this.reader.CanReadValueChunk;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00018220 File Offset: 0x00016420
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (this.curNsAttr != -1 && this.reader.CanReadValueChunk)
				{
					this.CheckBuffer(buffer, index, count);
					int num = this.curNode.value.Length - this.nsIncReadOffset;
					if (num > count)
					{
						num = count;
					}
					if (num > 0)
					{
						this.curNode.value.CopyTo(this.nsIncReadOffset, buffer, index, num);
					}
					this.nsIncReadOffset += num;
					return num;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			default:
				return 0;
			}
			return this.reader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000182F7 File Offset: 0x000164F7
		public override string LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this).LookupNamespace(prefix);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00018300 File Offset: 0x00016500
		protected override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00018308 File Offset: 0x00016508
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LineNumber;
					}
				}
				return 0;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00018334 File Offset: 0x00016534
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LinePosition;
					}
				}
				return 0;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00018360 File Offset: 0x00016560
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.reader is IXmlLineInfo;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00018370 File Offset: 0x00016570
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (!this.InNamespaceActiveState)
			{
				return new Dictionary<string, string>();
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001838C File Offset: 0x0001658C
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000183A4 File Offset: 0x000165A4
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000183BC File Offset: 0x000165BC
		private void ProcessNamespaces()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.EndElement)
				{
					return;
				}
				this.state = XmlSubtreeReader.State.PopNamespaceScope;
			}
			else
			{
				this.nsManager.PushScope();
				string text = this.reader.Prefix;
				string text2 = this.reader.NamespaceURI;
				if (this.nsManager.LookupNamespace(text) != text2)
				{
					this.AddNamespace(text, text2);
				}
				if (this.reader.MoveToFirstAttribute())
				{
					do
					{
						text = this.reader.Prefix;
						text2 = this.reader.NamespaceURI;
						if (Ref.Equal(text2, this.xmlnsUri))
						{
							if (text.Length == 0)
							{
								this.nsManager.AddNamespace(string.Empty, this.reader.Value);
								this.RemoveNamespace(string.Empty, this.xmlns);
							}
							else
							{
								text = this.reader.LocalName;
								this.nsManager.AddNamespace(text, this.reader.Value);
								this.RemoveNamespace(this.xmlns, text);
							}
						}
						else if (text.Length != 0 && this.nsManager.LookupNamespace(text) != text2)
						{
							this.AddNamespace(text, text2);
						}
					}
					while (this.reader.MoveToNextAttribute());
					this.reader.MoveToElement();
				}
				if (this.reader.IsEmptyElement)
				{
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
					return;
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001851C File Offset: 0x0001671C
		private void AddNamespace(string prefix, string ns)
		{
			this.nsManager.AddNamespace(prefix, ns);
			int num = this.nsAttrCount;
			this.nsAttrCount = num + 1;
			int num2 = num;
			if (this.nsAttributes == null)
			{
				this.nsAttributes = new XmlSubtreeReader.NodeData[this.InitialNamespaceAttributeCount];
			}
			if (num2 == this.nsAttributes.Length)
			{
				XmlSubtreeReader.NodeData[] array = new XmlSubtreeReader.NodeData[this.nsAttributes.Length * 2];
				Array.Copy(this.nsAttributes, 0, array, 0, num2);
				this.nsAttributes = array;
			}
			if (this.nsAttributes[num2] == null)
			{
				this.nsAttributes[num2] = new XmlSubtreeReader.NodeData();
			}
			if (prefix.Length == 0)
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, this.xmlns, string.Empty, this.xmlns, this.xmlnsUri, ns);
			}
			else
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, prefix, this.xmlns, this.reader.NameTable.Add(this.xmlns + ":" + prefix), this.xmlnsUri, ns);
			}
			this.state = XmlSubtreeReader.State.ClearNsAttributes;
			this.curNsAttr = -1;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018624 File Offset: 0x00016824
		private void RemoveNamespace(string prefix, string localName)
		{
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (Ref.Equal(prefix, this.nsAttributes[i].prefix) && Ref.Equal(localName, this.nsAttributes[i].localName))
				{
					if (i < this.nsAttrCount - 1)
					{
						XmlSubtreeReader.NodeData nodeData = this.nsAttributes[i];
						this.nsAttributes[i] = this.nsAttributes[this.nsAttrCount - 1];
						this.nsAttributes[this.nsAttrCount - 1] = nodeData;
					}
					this.nsAttrCount--;
					return;
				}
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000186B9 File Offset: 0x000168B9
		private void MoveToNsAttribute(int index)
		{
			this.reader.MoveToElement();
			this.curNsAttr = index;
			this.nsIncReadOffset = 0;
			this.SetCurrentNode(this.nsAttributes[index]);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000186E4 File Offset: 0x000168E4
		private bool InitReadElementContentAsBinary(XmlSubtreeReader.State binaryState)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
			}
			bool isEmptyElement = this.IsEmptyElement;
			if (!this.Read() || isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			if (nodeType != XmlNodeType.EndElement)
			{
				this.state = binaryState;
				return true;
			}
			this.ProcessNamespaces();
			this.Read();
			return false;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001877C File Offset: 0x0001697C
		private bool FinishReadElementContentAsBinary()
		{
			byte[] array = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadElementContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(array, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(array, 0, 256) > 0)
				{
				}
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return this.Read();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018838 File Offset: 0x00016A38
		private bool FinishReadContentAsBinary()
		{
			byte[] array = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(array, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(array, 0, 256) > 0)
				{
				}
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return true;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000188B6 File Offset: 0x00016AB6
		private bool InAttributeActiveState
		{
			get
			{
				return (98 & (1 << (int)this.state)) != 0;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000188C9 File Offset: 0x00016AC9
		private bool InNamespaceActiveState
		{
			get
			{
				return (2018 & (1 << (int)this.state)) != 0;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000188DF File Offset: 0x00016ADF
		private void SetEmptyNode()
		{
			this.tmpNode.type = XmlNodeType.None;
			this.tmpNode.value = string.Empty;
			this.curNode = this.tmpNode;
			this.useCurNode = true;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018910 File Offset: 0x00016B10
		private void SetCurrentNode(XmlSubtreeReader.NodeData node)
		{
			this.curNode = node;
			this.useCurNode = true;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018920 File Offset: 0x00016B20
		private void InitReadContentAsType(string methodName)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				throw new InvalidOperationException(Res.GetString("The XmlReader is closed or in error state."));
			case XmlSubtreeReader.State.Interactive:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				return;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			default:
				throw base.CreateReadContentAsException(methodName);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018994 File Offset: 0x00016B94
		private void FinishReadContentAsType()
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return;
					}
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
				}
				return;
			}
			this.ProcessNamespaces();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000189C4 File Offset: 0x00016BC4
		private void CheckBuffer(Array buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x0400027A RID: 634
		private int initialDepth;

		// Token: 0x0400027B RID: 635
		private XmlSubtreeReader.State state;

		// Token: 0x0400027C RID: 636
		private XmlNamespaceManager nsManager;

		// Token: 0x0400027D RID: 637
		private XmlSubtreeReader.NodeData[] nsAttributes;

		// Token: 0x0400027E RID: 638
		private int nsAttrCount;

		// Token: 0x0400027F RID: 639
		private int curNsAttr = -1;

		// Token: 0x04000280 RID: 640
		private string xmlns;

		// Token: 0x04000281 RID: 641
		private string xmlnsUri;

		// Token: 0x04000282 RID: 642
		private int nsIncReadOffset;

		// Token: 0x04000283 RID: 643
		private IncrementalReadDecoder binDecoder;

		// Token: 0x04000284 RID: 644
		private bool useCurNode;

		// Token: 0x04000285 RID: 645
		private XmlSubtreeReader.NodeData curNode;

		// Token: 0x04000286 RID: 646
		private XmlSubtreeReader.NodeData tmpNode;

		// Token: 0x04000287 RID: 647
		internal int InitialNamespaceAttributeCount = 4;

		// Token: 0x0200006C RID: 108
		private class NodeData
		{
			// Token: 0x0600053E RID: 1342 RVA: 0x00002127 File Offset: 0x00000327
			internal NodeData()
			{
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00018A13 File Offset: 0x00016C13
			internal void Set(XmlNodeType nodeType, string localName, string prefix, string name, string namespaceUri, string value)
			{
				this.type = nodeType;
				this.localName = localName;
				this.prefix = prefix;
				this.name = name;
				this.namespaceUri = namespaceUri;
				this.value = value;
			}

			// Token: 0x04000288 RID: 648
			internal XmlNodeType type;

			// Token: 0x04000289 RID: 649
			internal string localName;

			// Token: 0x0400028A RID: 650
			internal string prefix;

			// Token: 0x0400028B RID: 651
			internal string name;

			// Token: 0x0400028C RID: 652
			internal string namespaceUri;

			// Token: 0x0400028D RID: 653
			internal string value;
		}

		// Token: 0x0200006D RID: 109
		private enum State
		{
			// Token: 0x0400028F RID: 655
			Initial,
			// Token: 0x04000290 RID: 656
			Interactive,
			// Token: 0x04000291 RID: 657
			Error,
			// Token: 0x04000292 RID: 658
			EndOfFile,
			// Token: 0x04000293 RID: 659
			Closed,
			// Token: 0x04000294 RID: 660
			PopNamespaceScope,
			// Token: 0x04000295 RID: 661
			ClearNsAttributes,
			// Token: 0x04000296 RID: 662
			ReadElementContentAsBase64,
			// Token: 0x04000297 RID: 663
			ReadElementContentAsBinHex,
			// Token: 0x04000298 RID: 664
			ReadContentAsBase64,
			// Token: 0x04000299 RID: 665
			ReadContentAsBinHex
		}
	}
}
