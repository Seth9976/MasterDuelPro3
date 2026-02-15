using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000019 RID: 25
	internal class TEdge
	{
		// Token: 0x04000047 RID: 71
		internal IntPoint Bot;

		// Token: 0x04000048 RID: 72
		internal IntPoint Curr;

		// Token: 0x04000049 RID: 73
		internal IntPoint Top;

		// Token: 0x0400004A RID: 74
		internal IntPoint Delta;

		// Token: 0x0400004B RID: 75
		internal double Dx;

		// Token: 0x0400004C RID: 76
		internal PolyTypes PolyTyp;

		// Token: 0x0400004D RID: 77
		internal EdgeSides Side;

		// Token: 0x0400004E RID: 78
		internal int WindDelta;

		// Token: 0x0400004F RID: 79
		internal int WindCnt;

		// Token: 0x04000050 RID: 80
		internal int WindCnt2;

		// Token: 0x04000051 RID: 81
		internal int OutIdx;

		// Token: 0x04000052 RID: 82
		internal TEdge Next;

		// Token: 0x04000053 RID: 83
		internal TEdge Prev;

		// Token: 0x04000054 RID: 84
		internal TEdge NextInLML;

		// Token: 0x04000055 RID: 85
		internal TEdge NextInAEL;

		// Token: 0x04000056 RID: 86
		internal TEdge PrevInAEL;

		// Token: 0x04000057 RID: 87
		internal TEdge NextInSEL;

		// Token: 0x04000058 RID: 88
		internal TEdge PrevInSEL;
	}
}
