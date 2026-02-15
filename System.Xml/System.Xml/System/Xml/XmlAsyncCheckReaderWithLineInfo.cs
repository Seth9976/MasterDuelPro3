using System;

namespace System.Xml
{
	// Token: 0x02000049 RID: 73
	internal class XmlAsyncCheckReaderWithLineInfo : XmlAsyncCheckReader, IXmlLineInfo
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public XmlAsyncCheckReaderWithLineInfo(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlLineInfo = (IXmlLineInfo)reader;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000DB5D File Offset: 0x0000BD5D
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		public virtual int LineNumber
		{
			get
			{
				return this.readerAsIXmlLineInfo.LineNumber;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000DB77 File Offset: 0x0000BD77
		public virtual int LinePosition
		{
			get
			{
				return this.readerAsIXmlLineInfo.LinePosition;
			}
		}

		// Token: 0x04000175 RID: 373
		private readonly IXmlLineInfo readerAsIXmlLineInfo;
	}
}
