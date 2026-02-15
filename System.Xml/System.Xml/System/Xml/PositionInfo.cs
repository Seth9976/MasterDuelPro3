using System;

namespace System.Xml
{
	// Token: 0x02000102 RID: 258
	internal class PositionInfo : IXmlLineInfo
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool HasLineInfo()
		{
			return false;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual int LineNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual int LinePosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000433AC File Offset: 0x000415AC
		public static PositionInfo GetPositionInfo(object o)
		{
			IXmlLineInfo xmlLineInfo = o as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return new ReaderPositionInfo(xmlLineInfo);
			}
			return new PositionInfo();
		}
	}
}
