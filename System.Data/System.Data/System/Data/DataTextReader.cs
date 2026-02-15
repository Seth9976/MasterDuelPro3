using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000BB RID: 187
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x00037577 File Offset: 0x00035777
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003757F File Offset: 0x0003577F
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0003758E File Offset: 0x0003578E
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0003759B File Offset: 0x0003579B
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000375A8 File Offset: 0x000357A8
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000375B5 File Offset: 0x000357B5
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000375C2 File Offset: 0x000357C2
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x000375CF File Offset: 0x000357CF
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000375DC File Offset: 0x000357DC
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x000375E9 File Offset: 0x000357E9
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000375F6 File Offset: 0x000357F6
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00037603 File Offset: 0x00035803
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00037610 File Offset: 0x00035810
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0003761D File Offset: 0x0003581D
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0003762A File Offset: 0x0003582A
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00037637 File Offset: 0x00035837
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00037644 File Offset: 0x00035844
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00037651 File Offset: 0x00035851
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003765E File Offset: 0x0003585E
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0003766C File Offset: 0x0003586C
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003767B File Offset: 0x0003587B
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00037689 File Offset: 0x00035889
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00037697 File Offset: 0x00035897
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000376A6 File Offset: 0x000358A6
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000376B4 File Offset: 0x000358B4
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000376C1 File Offset: 0x000358C1
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000376CE File Offset: 0x000358CE
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000376DB File Offset: 0x000358DB
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000376E8 File Offset: 0x000358E8
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000376F5 File Offset: 0x000358F5
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00037702 File Offset: 0x00035902
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0003770F File Offset: 0x0003590F
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003771C File Offset: 0x0003591C
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00037729 File Offset: 0x00035929
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00037736 File Offset: 0x00035936
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00037744 File Offset: 0x00035944
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00037751 File Offset: 0x00035951
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0003775E File Offset: 0x0003595E
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003776B File Offset: 0x0003596B
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0003777B File Offset: 0x0003597B
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003778B File Offset: 0x0003598B
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0003779B File Offset: 0x0003599B
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000377AB File Offset: 0x000359AB
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000377B8 File Offset: 0x000359B8
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x040003BA RID: 954
		private XmlReader _xmlreader;
	}
}
