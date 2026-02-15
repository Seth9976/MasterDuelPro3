using System;

namespace System.Xml
{
	// Token: 0x020000C6 RID: 198
	internal class XsdCachingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00034F0B File Offset: 0x0003310B
		internal XsdCachingReader(XmlReader reader, IXmlLineInfo lineInfo, CachingEventHandler handlerMethod)
		{
			this.coreReader = reader;
			this.lineInfo = lineInfo;
			this.cacheHandler = handlerMethod;
			this.attributeEvents = new ValidatingReaderNodeData[8];
			this.contentEvents = new ValidatingReaderNodeData[4];
			this.Init();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00034F48 File Offset: 0x00033148
		private void Init()
		{
			this.coreReaderNameTable = this.coreReader.NameTable;
			this.cacheState = XsdCachingReader.CachingReaderState.Init;
			this.contentIndex = 0;
			this.currentAttrIndex = -1;
			this.currentContentIndex = -1;
			this.attributeCount = 0;
			this.cachedNode = null;
			this.readAhead = false;
			if (this.coreReader.NodeType == XmlNodeType.Element)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
				validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
				validatingReaderNodeData.SetLineInfo(this.lineInfo);
				this.RecordAttributes();
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00034FF9 File Offset: 0x000331F9
		internal void Reset(XmlReader reader)
		{
			this.coreReader = reader;
			this.Init();
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00035008 File Offset: 0x00033208
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.coreReader.Settings;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00035015 File Offset: 0x00033215
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cachedNode.NodeType;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00035022 File Offset: 0x00033222
		public override string Name
		{
			get
			{
				return this.cachedNode.GetAtomizedNameWPrefix(this.coreReaderNameTable);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00035035 File Offset: 0x00033235
		public override string LocalName
		{
			get
			{
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00035042 File Offset: 0x00033242
		public override string NamespaceURI
		{
			get
			{
				return this.cachedNode.Namespace;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0003504F File Offset: 0x0003324F
		public override string Prefix
		{
			get
			{
				return this.cachedNode.Prefix;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0003505C File Offset: 0x0003325C
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.cachedNode.NodeType);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0003506E File Offset: 0x0003326E
		public override string Value
		{
			get
			{
				if (!this.returnOriginalStringValues)
				{
					return this.cachedNode.RawValue;
				}
				return this.cachedNode.OriginalStringValue;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0003508F File Offset: 0x0003328F
		public override int Depth
		{
			get
			{
				return this.cachedNode.Depth;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0003509C File Offset: 0x0003329C
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000350A9 File Offset: 0x000332A9
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000350B6 File Offset: 0x000332B6
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000350C3 File Offset: 0x000332C3
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000350D0 File Offset: 0x000332D0
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000350D8 File Offset: 0x000332D8
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.attributeEvents[num].RawValue;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00035118 File Offset: 0x00033318
		public override string GetAttribute(string name, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, namespaceURI))
				{
					return validatingReaderNodeData.RawValue;
				}
			}
			return null;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00035189 File Offset: 0x00033389
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.attributeEvents[i].RawValue;
		}

		// Token: 0x170001EC RID: 492
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x170001ED RID: 493
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x170001EE RID: 494
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000351B0 File Offset: 0x000333B0
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num >= 0)
			{
				this.currentAttrIndex = num;
				this.cachedNode = this.attributeEvents[num];
				return true;
			}
			return false;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000351F8 File Offset: 0x000333F8
		public override bool MoveToAttribute(string name, string ns)
		{
			ns = ((ns == null) ? string.Empty : this.coreReaderNameTable.Get(ns));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					this.currentAttrIndex = i;
					this.cachedNode = this.attributeEvents[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00035279 File Offset: 0x00033479
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			this.cachedNode = this.attributeEvents[i];
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000352A8 File Offset: 0x000334A8
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.currentAttrIndex = 0;
			this.cachedNode = this.attributeEvents[0];
			return true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000352CC File Offset: 0x000334CC
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.attributeCount)
			{
				ValidatingReaderNodeData[] array = this.attributeEvents;
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				this.cachedNode = array[num];
				return true;
			}
			return false;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003530A File Offset: 0x0003350A
		public override bool MoveToElement()
		{
			if (this.cacheState != XsdCachingReader.CachingReaderState.Replay || this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
			return true;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003533C File Offset: 0x0003353C
		public override bool Read()
		{
			switch (this.cacheState)
			{
			case XsdCachingReader.CachingReaderState.Init:
				this.cacheState = XsdCachingReader.CachingReaderState.Record;
				break;
			case XsdCachingReader.CachingReaderState.Record:
				break;
			case XsdCachingReader.CachingReaderState.Replay:
				if (this.currentContentIndex >= this.contentIndex)
				{
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					this.cacheHandler(this);
					return (this.coreReader.NodeType == XmlNodeType.Element && !this.readAhead) || this.coreReader.Read();
				}
				this.cachedNode = this.contentEvents[this.currentContentIndex];
				if (this.currentContentIndex > 0)
				{
					this.ClearAttributesInfo();
				}
				this.currentContentIndex++;
				return true;
			default:
				return false;
			}
			ValidatingReaderNodeData validatingReaderNodeData = null;
			if (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Element:
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					return false;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.Value);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.Depth = this.coreReader.Depth;
					break;
				case XmlNodeType.EndElement:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					break;
				}
				this.cachedNode = validatingReaderNodeData;
				return true;
			}
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			return false;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000354FC File Offset: 0x000336FC
		internal ValidatingReaderNodeData RecordTextNode(string textValue, string originalStringValue, int depth, int lineNo, int linePos)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.Text);
			validatingReaderNodeData.SetItemData(textValue, originalStringValue);
			validatingReaderNodeData.SetLineInfo(lineNo, linePos);
			validatingReaderNodeData.Depth = depth;
			return validatingReaderNodeData;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00035520 File Offset: 0x00033720
		internal void SwitchTextNodeAndEndElement(string textValue, string originalStringValue)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.RecordTextNode(textValue, originalStringValue, this.coreReader.Depth + 1, 0, 0);
			int num = this.contentIndex - 2;
			ValidatingReaderNodeData validatingReaderNodeData2 = this.contentEvents[num];
			this.contentEvents[num] = validatingReaderNodeData;
			this.contentEvents[this.contentIndex - 1] = validatingReaderNodeData2;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00035570 File Offset: 0x00033770
		internal void RecordEndElementNode()
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.EndElement);
			validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
			validatingReaderNodeData.SetLineInfo(this.coreReader as IXmlLineInfo);
			if (this.coreReader.IsEmptyElement)
			{
				this.readAhead = true;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000355DB File Offset: 0x000337DB
		internal string ReadOriginalContentAsString()
		{
			this.returnOriginalStringValues = true;
			string text = base.InternalReadContentAsString();
			this.returnOriginalStringValues = false;
			return text;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000355F1 File Offset: 0x000337F1
		public override bool EOF
		{
			get
			{
				return this.cacheState == XsdCachingReader.CachingReaderState.ReaderClosed && this.coreReader.EOF;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00035609 File Offset: 0x00033809
		public override void Close()
		{
			this.coreReader.Close();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0003561D File Offset: 0x0003381D
		public override ReadState ReadState
		{
			get
			{
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0003562C File Offset: 0x0003382C
		public override void Skip()
		{
			XmlNodeType nodeType = this.cachedNode.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					this.Read();
					return;
				}
				this.MoveToElement();
			}
			if (this.coreReader.NodeType != XmlNodeType.EndElement && !this.readAhead)
			{
				int num = this.coreReader.Depth - 1;
				while (this.coreReader.Read() && this.coreReader.Depth > num)
				{
				}
			}
			this.coreReader.Read();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			this.cacheHandler(this);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000356BF File Offset: 0x000338BF
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000356C7 File Offset: 0x000338C7
		public override string LookupNamespace(string prefix)
		{
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000356D5 File Offset: 0x000338D5
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000356DC File Offset: 0x000338DC
		public override bool ReadAttributeValue()
		{
			if (this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
			return true;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00035713 File Offset: 0x00033913
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.cachedNode.LineNumber;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00035720 File Offset: 0x00033920
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.cachedNode.LinePosition;
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003572D File Offset: 0x0003392D
		internal void SetToReplayMode()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0003574B File Offset: 0x0003394B
		internal XmlReader GetCoreReader()
		{
			return this.coreReader;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00035753 File Offset: 0x00033953
		internal IXmlLineInfo GetLineInfo()
		{
			return this.lineInfo;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0003575B File Offset: 0x0003395B
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.currentAttrIndex = -1;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003576C File Offset: 0x0003396C
		private ValidatingReaderNodeData AddAttribute(int attIndex)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(XmlNodeType.Attribute);
				return validatingReaderNodeData;
			}
			if (attIndex >= this.attributeEvents.Length - 1)
			{
				ValidatingReaderNodeData[] array = new ValidatingReaderNodeData[this.attributeEvents.Length * 2];
				Array.Copy(this.attributeEvents, 0, array, 0, this.attributeEvents.Length);
				this.attributeEvents = array;
			}
			validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(XmlNodeType.Attribute);
				this.attributeEvents[attIndex] = validatingReaderNodeData;
			}
			return validatingReaderNodeData;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000357E8 File Offset: 0x000339E8
		private ValidatingReaderNodeData AddContent(XmlNodeType nodeType)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(nodeType);
				this.contentIndex++;
				return validatingReaderNodeData;
			}
			if (this.contentIndex >= this.contentEvents.Length - 1)
			{
				ValidatingReaderNodeData[] array = new ValidatingReaderNodeData[this.contentEvents.Length * 2];
				Array.Copy(this.contentEvents, 0, array, 0, this.contentEvents.Length);
				this.contentEvents = array;
			}
			validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(nodeType);
				this.contentEvents[this.contentIndex] = validatingReaderNodeData;
			}
			this.contentIndex++;
			return validatingReaderNodeData;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00035894 File Offset: 0x00033A94
		private void RecordAttributes()
		{
			this.attributeCount = this.coreReader.AttributeCount;
			if (this.coreReader.MoveToFirstAttribute())
			{
				int num = 0;
				do
				{
					ValidatingReaderNodeData validatingReaderNodeData = this.AddAttribute(num);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.RawValue = this.coreReader.Value;
					num++;
				}
				while (this.coreReader.MoveToNextAttribute());
				this.coreReader.MoveToElement();
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00035934 File Offset: 0x00033B34
		private int GetAttributeIndexWithoutPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && validatingReaderNodeData.Prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0003598C File Offset: 0x00033B8C
		private int GetAttributeIndexWithPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (Ref.Equal(this.attributeEvents[i].GetAtomizedNameWPrefix(this.coreReaderNameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000359DB File Offset: 0x00033BDB
		private ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x0400058D RID: 1421
		private XmlReader coreReader;

		// Token: 0x0400058E RID: 1422
		private XmlNameTable coreReaderNameTable;

		// Token: 0x0400058F RID: 1423
		private ValidatingReaderNodeData[] contentEvents;

		// Token: 0x04000590 RID: 1424
		private ValidatingReaderNodeData[] attributeEvents;

		// Token: 0x04000591 RID: 1425
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000592 RID: 1426
		private XsdCachingReader.CachingReaderState cacheState;

		// Token: 0x04000593 RID: 1427
		private int contentIndex;

		// Token: 0x04000594 RID: 1428
		private int attributeCount;

		// Token: 0x04000595 RID: 1429
		private bool returnOriginalStringValues;

		// Token: 0x04000596 RID: 1430
		private CachingEventHandler cacheHandler;

		// Token: 0x04000597 RID: 1431
		private int currentAttrIndex;

		// Token: 0x04000598 RID: 1432
		private int currentContentIndex;

		// Token: 0x04000599 RID: 1433
		private bool readAhead;

		// Token: 0x0400059A RID: 1434
		private IXmlLineInfo lineInfo;

		// Token: 0x0400059B RID: 1435
		private ValidatingReaderNodeData textNode;

		// Token: 0x020000C7 RID: 199
		private enum CachingReaderState
		{
			// Token: 0x0400059D RID: 1437
			None,
			// Token: 0x0400059E RID: 1438
			Init,
			// Token: 0x0400059F RID: 1439
			Record,
			// Token: 0x040005A0 RID: 1440
			Replay,
			// Token: 0x040005A1 RID: 1441
			ReaderClosed,
			// Token: 0x040005A2 RID: 1442
			Error
		}
	}
}
