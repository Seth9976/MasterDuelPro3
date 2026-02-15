using System;

namespace System.Xml
{
	// Token: 0x0200003C RID: 60
	internal enum ElementProperties : uint
	{
		// Token: 0x04000146 RID: 326
		DEFAULT,
		// Token: 0x04000147 RID: 327
		URI_PARENT,
		// Token: 0x04000148 RID: 328
		BOOL_PARENT,
		// Token: 0x04000149 RID: 329
		NAME_PARENT = 4U,
		// Token: 0x0400014A RID: 330
		EMPTY = 8U,
		// Token: 0x0400014B RID: 331
		NO_ENTITIES = 16U,
		// Token: 0x0400014C RID: 332
		HEAD = 32U,
		// Token: 0x0400014D RID: 333
		BLOCK_WS = 64U,
		// Token: 0x0400014E RID: 334
		HAS_NS = 128U
	}
}
