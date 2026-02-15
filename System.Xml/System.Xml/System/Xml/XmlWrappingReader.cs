using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000BF RID: 191
	internal class XmlWrappingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0003408A File Offset: 0x0003228A
		internal XmlWrappingReader(XmlReader baseReader)
		{
			this.reader = baseReader;
			this.readerAsIXmlLineInfo = baseReader as IXmlLineInfo;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x000340A5 File Offset: 0x000322A5
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.reader.Settings;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000340B2 File Offset: 0x000322B2
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000340BF File Offset: 0x000322BF
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000340CC File Offset: 0x000322CC
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000340D9 File Offset: 0x000322D9
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000340E6 File Offset: 0x000322E6
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000340F3 File Offset: 0x000322F3
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00034100 File Offset: 0x00032300
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0003410D File Offset: 0x0003230D
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000172AE File Offset: 0x000154AE
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000172BB File Offset: 0x000154BB
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0003411A File Offset: 0x0003231A
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00034127 File Offset: 0x00032327
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00034134 File Offset: 0x00032334
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00034141 File Offset: 0x00032341
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0003414E File Offset: 0x0003234E
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0003415B File Offset: 0x0003235B
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00034168 File Offset: 0x00032368
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00034175 File Offset: 0x00032375
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00017301 File Offset: 0x00015501
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00034182 File Offset: 0x00032382
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0003418F File Offset: 0x0003238F
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0003419C File Offset: 0x0003239C
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000341A9 File Offset: 0x000323A9
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000341B7 File Offset: 0x000323B7
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000341C6 File Offset: 0x000323C6
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000341D4 File Offset: 0x000323D4
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000341E2 File Offset: 0x000323E2
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000341F1 File Offset: 0x000323F1
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000341FF File Offset: 0x000323FF
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003420C File Offset: 0x0003240C
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00034219 File Offset: 0x00032419
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00034226 File Offset: 0x00032426
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00034233 File Offset: 0x00032433
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00034240 File Offset: 0x00032440
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0003424D File Offset: 0x0003244D
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0003425B File Offset: 0x0003245B
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00034268 File Offset: 0x00032468
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00034275 File Offset: 0x00032475
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo != null && this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0003428C File Offset: 0x0003248C
		public virtual int LineNumber
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000342A3 File Offset: 0x000324A3
		public virtual int LinePosition
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000342BA File Offset: 0x000324BA
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.reader.DtdInfo;
			}
		}

		// Token: 0x0400055E RID: 1374
		protected XmlReader reader;

		// Token: 0x0400055F RID: 1375
		protected IXmlLineInfo readerAsIXmlLineInfo;
	}
}
