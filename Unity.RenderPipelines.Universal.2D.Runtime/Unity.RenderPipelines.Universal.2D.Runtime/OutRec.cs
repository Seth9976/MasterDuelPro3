using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200001F RID: 31
	internal class OutRec
	{
		// Token: 0x04000065 RID: 101
		internal int Idx;

		// Token: 0x04000066 RID: 102
		internal bool IsHole;

		// Token: 0x04000067 RID: 103
		internal bool IsOpen;

		// Token: 0x04000068 RID: 104
		internal OutRec FirstLeft;

		// Token: 0x04000069 RID: 105
		internal OutPt Pts;

		// Token: 0x0400006A RID: 106
		internal OutPt BottomPt;

		// Token: 0x0400006B RID: 107
		internal PolyNode PolyNode;
	}
}
