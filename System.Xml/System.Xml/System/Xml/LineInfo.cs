using System;

namespace System.Xml
{
	// Token: 0x02000105 RID: 261
	internal struct LineInfo
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x00043405 File Offset: 0x00041605
		public LineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00043405 File Offset: 0x00041605
		public void Set(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x0400068B RID: 1675
		internal int lineNo;

		// Token: 0x0400068C RID: 1676
		internal int linePos;
	}
}
