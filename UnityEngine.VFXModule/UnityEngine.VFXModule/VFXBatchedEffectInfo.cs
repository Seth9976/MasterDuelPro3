using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000007 RID: 7
	[RequiredByNativeCode]
	public struct VFXBatchedEffectInfo
	{
		// Token: 0x0400000D RID: 13
		public VisualEffectAsset vfxAsset;

		// Token: 0x0400000E RID: 14
		public uint activeBatchCount;

		// Token: 0x0400000F RID: 15
		public uint inactiveBatchCount;

		// Token: 0x04000010 RID: 16
		public uint activeInstanceCount;

		// Token: 0x04000011 RID: 17
		public uint unbatchedInstanceCount;

		// Token: 0x04000012 RID: 18
		public uint totalInstanceCapacity;

		// Token: 0x04000013 RID: 19
		public uint maxInstancePerBatchCapacity;

		// Token: 0x04000014 RID: 20
		public ulong totalGPUSizeInBytes;

		// Token: 0x04000015 RID: 21
		public ulong totalCPUSizeInBytes;
	}
}
