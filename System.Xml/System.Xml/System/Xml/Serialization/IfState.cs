using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200014A RID: 330
	internal class IfState
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00050A4B File Offset: 0x0004EC4B
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00050A53 File Offset: 0x0004EC53
		internal Label EndIf
		{
			get
			{
				return this.endIf;
			}
			set
			{
				this.endIf = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00050A5C File Offset: 0x0004EC5C
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x00050A64 File Offset: 0x0004EC64
		internal Label ElseBegin
		{
			get
			{
				return this.elseBegin;
			}
			set
			{
				this.elseBegin = value;
			}
		}

		// Token: 0x040007F7 RID: 2039
		private Label elseBegin;

		// Token: 0x040007F8 RID: 2040
		private Label endIf;
	}
}
