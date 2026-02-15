using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020001AC RID: 428
	internal class XmlCountingReader : XmlReader, IXmlTextParser, IXmlLineInfo
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x000634F7 File Offset: 0x000616F7
		internal XmlCountingReader(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.innerReader = xmlReader;
			this.advanceCount = 0;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0006351B File Offset: 0x0006171B
		internal int AdvanceCount
		{
			get
			{
				return this.advanceCount;
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00063523 File Offset: 0x00061723
		private void IncrementCount()
		{
			if (this.advanceCount == 2147483647)
			{
				this.advanceCount = 0;
				return;
			}
			this.advanceCount++;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00063548 File Offset: 0x00061748
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00063555 File Offset: 0x00061755
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00063562 File Offset: 0x00061762
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0006356F File Offset: 0x0006176F
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0006357C File Offset: 0x0006177C
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00063589 File Offset: 0x00061789
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00063596 File Offset: 0x00061796
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x000635A3 File Offset: 0x000617A3
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x000635B0 File Offset: 0x000617B0
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x000635BD File Offset: 0x000617BD
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x000635CA File Offset: 0x000617CA
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000635D7 File Offset: 0x000617D7
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000635E4 File Offset: 0x000617E4
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000635F1 File Offset: 0x000617F1
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000635FE File Offset: 0x000617FE
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0006360B File Offset: 0x0006180B
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00063618 File Offset: 0x00061818
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00063625 File Offset: 0x00061825
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x170004D8 RID: 1240
		public override string this[int i]
		{
			get
			{
				return this.innerReader[i];
			}
		}

		// Token: 0x170004D9 RID: 1241
		public override string this[string name]
		{
			get
			{
				return this.innerReader[name];
			}
		}

		// Token: 0x170004DA RID: 1242
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.innerReader[name, namespaceURI];
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0006365D File Offset: 0x0006185D
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0006366A File Offset: 0x0006186A
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00063677 File Offset: 0x00061877
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00063684 File Offset: 0x00061884
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00063691 File Offset: 0x00061891
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0006369E File Offset: 0x0006189E
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000636AB File Offset: 0x000618AB
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000636B8 File Offset: 0x000618B8
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000636C5 File Offset: 0x000618C5
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000636D3 File Offset: 0x000618D3
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000636E2 File Offset: 0x000618E2
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000636F0 File Offset: 0x000618F0
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000636FE File Offset: 0x000618FE
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0006370D File Offset: 0x0006190D
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0006371B File Offset: 0x0006191B
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00063728 File Offset: 0x00061928
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00063735 File Offset: 0x00061935
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00063742 File Offset: 0x00061942
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00063750 File Offset: 0x00061950
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0006375D File Offset: 0x0006195D
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0006376A File Offset: 0x0006196A
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00063777 File Offset: 0x00061977
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00063785 File Offset: 0x00061985
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00063794 File Offset: 0x00061994
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000637A1 File Offset: 0x000619A1
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000637AE File Offset: 0x000619AE
		public override bool Read()
		{
			this.IncrementCount();
			return this.innerReader.Read();
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000637C1 File Offset: 0x000619C1
		public override void Skip()
		{
			this.IncrementCount();
			this.innerReader.Skip();
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000637D4 File Offset: 0x000619D4
		public override string ReadInnerXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000637F5 File Offset: 0x000619F5
		public override string ReadOuterXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00063816 File Offset: 0x00061A16
		public override object ReadContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00063829 File Offset: 0x00061A29
		public override bool ReadContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0006383C File Offset: 0x00061A3C
		public override DateTime ReadContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0006384F File Offset: 0x00061A4F
		public override double ReadContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00063862 File Offset: 0x00061A62
		public override int ReadContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00063875 File Offset: 0x00061A75
		public override long ReadContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00063888 File Offset: 0x00061A88
		public override string ReadContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0006389B File Offset: 0x00061A9B
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000638B0 File Offset: 0x00061AB0
		public override object ReadElementContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000638C3 File Offset: 0x00061AC3
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000638D8 File Offset: 0x00061AD8
		public override bool ReadElementContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000638EB File Offset: 0x00061AEB
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00063900 File Offset: 0x00061B00
		public override DateTime ReadElementContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00063913 File Offset: 0x00061B13
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00063928 File Offset: 0x00061B28
		public override double ReadElementContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0006393B File Offset: 0x00061B3B
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00063950 File Offset: 0x00061B50
		public override int ReadElementContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00063963 File Offset: 0x00061B63
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00063978 File Offset: 0x00061B78
		public override long ReadElementContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0006398B File Offset: 0x00061B8B
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000639A0 File Offset: 0x00061BA0
		public override string ReadElementContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000639B3 File Offset: 0x00061BB3
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000639C8 File Offset: 0x00061BC8
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000639DD File Offset: 0x00061BDD
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000639F5 File Offset: 0x00061BF5
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00063A0B File Offset: 0x00061C0B
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00063A21 File Offset: 0x00061C21
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00063A37 File Offset: 0x00061C37
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00063A4D File Offset: 0x00061C4D
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00063A63 File Offset: 0x00061C63
		public override string ReadString()
		{
			this.IncrementCount();
			return this.innerReader.ReadString();
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00063A76 File Offset: 0x00061C76
		public override void ReadStartElement()
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement();
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00063A89 File Offset: 0x00061C89
		public override void ReadStartElement(string name)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00063A9D File Offset: 0x00061C9D
		public override void ReadStartElement(string localname, string ns)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00063AB2 File Offset: 0x00061CB2
		public override string ReadElementString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00063AC5 File Offset: 0x00061CC5
		public override string ReadElementString(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00063AD9 File Offset: 0x00061CD9
		public override string ReadElementString(string localname, string ns)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00063AEE File Offset: 0x00061CEE
		public override void ReadEndElement()
		{
			this.IncrementCount();
			this.innerReader.ReadEndElement();
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00063B01 File Offset: 0x00061D01
		public override bool ReadToFollowing(string name)
		{
			this.IncrementCount();
			return this.ReadToFollowing(name);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00063B10 File Offset: 0x00061D10
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00063B25 File Offset: 0x00061D25
		public override bool ReadToDescendant(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00063B39 File Offset: 0x00061D39
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00063B4E File Offset: 0x00061D4E
		public override bool ReadToNextSibling(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00063B62 File Offset: 0x00061D62
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00063B78 File Offset: 0x00061D78
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					IDisposable disposable = this.innerReader;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00063BB4 File Offset: 0x00061DB4
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x00063BF0 File Offset: 0x00061DF0
		bool IXmlTextParser.Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00063C2C File Offset: 0x00061E2C
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00063C68 File Offset: 0x00061E68
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00063CA4 File Offset: 0x00061EA4
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00063CC8 File Offset: 0x00061EC8
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00063CEC File Offset: 0x00061EEC
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x04000967 RID: 2407
		private XmlReader innerReader;

		// Token: 0x04000968 RID: 2408
		private int advanceCount;
	}
}
