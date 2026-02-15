using System;

namespace System.Xml
{
	// Token: 0x02000103 RID: 259
	internal class ReaderPositionInfo : PositionInfo
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x000433CF File Offset: 0x000415CF
		public ReaderPositionInfo(IXmlLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000433DE File Offset: 0x000415DE
		public override bool HasLineInfo()
		{
			return this.lineInfo.HasLineInfo();
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000433EB File Offset: 0x000415EB
		public override int LineNumber
		{
			get
			{
				return this.lineInfo.LineNumber;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000433F8 File Offset: 0x000415F8
		public override int LinePosition
		{
			get
			{
				return this.lineInfo.LinePosition;
			}
		}

		// Token: 0x0400068A RID: 1674
		private IXmlLineInfo lineInfo;
	}
}
