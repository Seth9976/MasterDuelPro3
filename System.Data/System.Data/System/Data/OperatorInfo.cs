using System;

namespace System.Data
{
	// Token: 0x0200006D RID: 109
	internal sealed class OperatorInfo
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0001F71E File Offset: 0x0001D91E
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this._type = type;
			this._op = op;
			this._priority = pri;
		}

		// Token: 0x0400026B RID: 619
		internal Nodes _type;

		// Token: 0x0400026C RID: 620
		internal int _op;

		// Token: 0x0400026D RID: 621
		internal int _priority;
	}
}
