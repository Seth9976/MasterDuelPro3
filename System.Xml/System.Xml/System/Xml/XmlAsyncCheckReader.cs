using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000047 RID: 71
	internal class XmlAsyncCheckReader : XmlReader
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000D299 File Offset: 0x0000B499
		internal XmlReader CoreReader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public static XmlAsyncCheckReader CreateAsyncCheckWrapper(XmlReader reader)
		{
			if (reader is IXmlLineInfo)
			{
				if (!(reader is IXmlNamespaceResolver))
				{
					return new XmlAsyncCheckReaderWithLineInfo(reader);
				}
				if (reader is IXmlSchemaInfo)
				{
					return new XmlAsyncCheckReaderWithLineInfoNSSchema(reader);
				}
				return new XmlAsyncCheckReaderWithLineInfoNS(reader);
			}
			else
			{
				if (reader is IXmlNamespaceResolver)
				{
					return new XmlAsyncCheckReaderWithNS(reader);
				}
				return new XmlAsyncCheckReader(reader);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		public XmlAsyncCheckReader(XmlReader reader)
		{
			this.coreReader = reader;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D30D File Offset: 0x0000B50D
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000D32C File Offset: 0x0000B52C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Async = true;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D366 File Offset: 0x0000B566
		public override XmlNodeType NodeType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000D379 File Offset: 0x0000B579
		public override string Name
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Name;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000D38C File Offset: 0x0000B58C
		public override string LocalName
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000D39F File Offset: 0x0000B59F
		public override string NamespaceURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		public override string Prefix
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
		public override bool HasValue
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		public override string Value
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		public override int Depth
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000D3FE File Offset: 0x0000B5FE
		public override string BaseURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000D411 File Offset: 0x0000B611
		public override bool IsEmptyElement
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000D424 File Offset: 0x0000B624
		public override bool IsDefault
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000D437 File Offset: 0x0000B637
		public override char QuoteChar
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000D44A File Offset: 0x0000B64A
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000D45D File Offset: 0x0000B65D
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000D470 File Offset: 0x0000B670
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.SchemaInfo;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000D483 File Offset: 0x0000B683
		public override Type ValueType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ValueType;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D496 File Offset: 0x0000B696
		public override object ReadContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsObject();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D4A9 File Offset: 0x0000B6A9
		public override bool ReadContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBoolean();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public override DateTime ReadContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTime();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D4CF File Offset: 0x0000B6CF
		public override double ReadContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDouble();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D4E2 File Offset: 0x0000B6E2
		public override float ReadContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsFloat();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
		public override decimal ReadContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDecimal();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D508 File Offset: 0x0000B708
		public override int ReadContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsInt();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D51B File Offset: 0x0000B71B
		public override long ReadContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsLong();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D52E File Offset: 0x0000B72E
		public override string ReadContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsString();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D541 File Offset: 0x0000B741
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D556 File Offset: 0x0000B756
		public override object ReadElementContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D569 File Offset: 0x0000B769
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D57E File Offset: 0x0000B77E
		public override bool ReadElementContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D591 File Offset: 0x0000B791
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		public override DateTime ReadElementContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D5CE File Offset: 0x0000B7CE
		public override double ReadElementContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D5E1 File Offset: 0x0000B7E1
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D5F6 File Offset: 0x0000B7F6
		public override float ReadElementContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D609 File Offset: 0x0000B809
		public override decimal ReadElementContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D61C File Offset: 0x0000B81C
		public override int ReadElementContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D62F File Offset: 0x0000B82F
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D644 File Offset: 0x0000B844
		public override long ReadElementContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D657 File Offset: 0x0000B857
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D66C File Offset: 0x0000B86C
		public override string ReadElementContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D67F File Offset: 0x0000B87F
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D694 File Offset: 0x0000B894
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D6A9 File Offset: 0x0000B8A9
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
		public override int AttributeCount
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public override string GetAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		public override string GetAttribute(string name, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		public override string GetAttribute(int i)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x1700006D RID: 109
		public override string this[int i]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[i];
			}
		}

		// Token: 0x1700006E RID: 110
		public override string this[string name]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name];
			}
		}

		// Token: 0x1700006F RID: 111
		public override string this[string name, string namespaceURI]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name, namespaceURI];
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D74E File Offset: 0x0000B94E
		public override bool MoveToAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D762 File Offset: 0x0000B962
		public override bool MoveToAttribute(string name, string ns)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D777 File Offset: 0x0000B977
		public override void MoveToAttribute(int i)
		{
			this.CheckAsync();
			this.coreReader.MoveToAttribute(i);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D78B File Offset: 0x0000B98B
		public override bool MoveToFirstAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToFirstAttribute();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D79E File Offset: 0x0000B99E
		public override bool MoveToNextAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToNextAttribute();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D7B1 File Offset: 0x0000B9B1
		public override bool MoveToElement()
		{
			this.CheckAsync();
			return this.coreReader.MoveToElement();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public override bool ReadAttributeValue()
		{
			this.CheckAsync();
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D7D7 File Offset: 0x0000B9D7
		public override bool Read()
		{
			this.CheckAsync();
			return this.coreReader.Read();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000D7EA File Offset: 0x0000B9EA
		public override bool EOF
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000D7FD File Offset: 0x0000B9FD
		public override void Close()
		{
			this.CheckAsync();
			this.coreReader.Close();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000D810 File Offset: 0x0000BA10
		public override ReadState ReadState
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000D823 File Offset: 0x0000BA23
		public override void Skip()
		{
			this.CheckAsync();
			this.coreReader.Skip();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000D836 File Offset: 0x0000BA36
		public override XmlNameTable NameTable
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000D849 File Offset: 0x0000BA49
		public override string LookupNamespace(string prefix)
		{
			this.CheckAsync();
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000D85D File Offset: 0x0000BA5D
		public override bool CanResolveEntity
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanResolveEntity;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000D870 File Offset: 0x0000BA70
		public override void ResolveEntity()
		{
			this.CheckAsync();
			this.coreReader.ResolveEntity();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000D883 File Offset: 0x0000BA83
		public override bool CanReadBinaryContent
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadBinaryContent;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000D896 File Offset: 0x0000BA96
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D8C2 File Offset: 0x0000BAC2
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		public override bool CanReadValueChunk
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadValueChunk;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D901 File Offset: 0x0000BB01
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D917 File Offset: 0x0000BB17
		public override string ReadString()
		{
			this.CheckAsync();
			return this.coreReader.ReadString();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D92A File Offset: 0x0000BB2A
		public override XmlNodeType MoveToContent()
		{
			this.CheckAsync();
			return this.coreReader.MoveToContent();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D93D File Offset: 0x0000BB3D
		public override void ReadStartElement()
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D950 File Offset: 0x0000BB50
		public override void ReadStartElement(string name)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(name);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D964 File Offset: 0x0000BB64
		public override void ReadStartElement(string localname, string ns)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(localname, ns);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D979 File Offset: 0x0000BB79
		public override string ReadElementString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public override string ReadElementString(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(name);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		public override string ReadElementString(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(localname, ns);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D9B5 File Offset: 0x0000BBB5
		public override void ReadEndElement()
		{
			this.CheckAsync();
			this.coreReader.ReadEndElement();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public override bool IsStartElement()
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D9DB File Offset: 0x0000BBDB
		public override bool IsStartElement(string name)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(name);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		public override bool IsStartElement(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(localname, ns);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public override bool ReadToFollowing(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(name);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000DA2D File Offset: 0x0000BC2D
		public override bool ReadToDescendant(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(name);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000DA41 File Offset: 0x0000BC41
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DA56 File Offset: 0x0000BC56
		public override bool ReadToNextSibling(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(name);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000DA6A File Offset: 0x0000BC6A
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DA7F File Offset: 0x0000BC7F
		public override string ReadInnerXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadInnerXml();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DA92 File Offset: 0x0000BC92
		public override string ReadOuterXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadOuterXml();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DAA5 File Offset: 0x0000BCA5
		public override XmlReader ReadSubtree()
		{
			this.CheckAsync();
			return XmlAsyncCheckReader.CreateAsyncCheckWrapper(this.coreReader.ReadSubtree());
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000DABD File Offset: 0x0000BCBD
		public override bool HasAttributes
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasAttributes;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreReader.Dispose();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceManager;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000DAF6 File Offset: 0x0000BCF6
		internal override IDtdInfo DtdInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.DtdInfo;
			}
		}

		// Token: 0x04000172 RID: 370
		private readonly XmlReader coreReader;

		// Token: 0x04000173 RID: 371
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
