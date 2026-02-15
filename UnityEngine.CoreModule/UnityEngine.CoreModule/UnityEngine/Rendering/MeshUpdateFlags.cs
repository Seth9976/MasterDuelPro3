using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000326 RID: 806
	[Flags]
	public enum MeshUpdateFlags
	{
		// Token: 0x04000869 RID: 2153
		Default = 0,
		// Token: 0x0400086A RID: 2154
		DontValidateIndices = 1,
		// Token: 0x0400086B RID: 2155
		DontResetBoneBounds = 2,
		// Token: 0x0400086C RID: 2156
		DontNotifyMeshUsers = 4,
		// Token: 0x0400086D RID: 2157
		DontRecalculateBounds = 8
	}
}
