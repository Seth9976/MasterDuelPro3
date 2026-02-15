using System;

namespace Mono.Xml
{
	// Token: 0x0200004E RID: 78
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x000038CB File Offset: 0x00001ACB
		public SmallXmlParserException(string msg, int line, int column)
			: base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x0400014F RID: 335
		private int line;

		// Token: 0x04000150 RID: 336
		private int column;
	}
}
