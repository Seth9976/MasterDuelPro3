using System;

namespace System.Xml.Schema
{
	// Token: 0x0200030C RID: 780
	internal class IdRefNode
	{
		// Token: 0x0600228A RID: 8842 RVA: 0x000C33A1 File Offset: 0x000C15A1
		internal IdRefNode(IdRefNode next, string id, int lineNo, int linePos)
		{
			this.Id = id;
			this.LineNo = lineNo;
			this.LinePos = linePos;
			this.Next = next;
		}

		// Token: 0x0400101C RID: 4124
		internal string Id;

		// Token: 0x0400101D RID: 4125
		internal int LineNo;

		// Token: 0x0400101E RID: 4126
		internal int LinePos;

		// Token: 0x0400101F RID: 4127
		internal IdRefNode Next;
	}
}
