using System;

namespace System.Xml.Linq
{
	// Token: 0x02000004 RID: 4
	internal class LineInfoAnnotation
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
		public LineInfoAnnotation(int lineNumber, int linePosition)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x04000002 RID: 2
		internal int lineNumber;

		// Token: 0x04000003 RID: 3
		internal int linePosition;
	}
}
