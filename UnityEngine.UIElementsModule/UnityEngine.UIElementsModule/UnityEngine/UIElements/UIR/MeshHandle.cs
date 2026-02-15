using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000508 RID: 1288
	internal class MeshHandle : LinkedPoolItem<MeshHandle>
	{
		// Token: 0x04001072 RID: 4210
		internal Alloc allocVerts;

		// Token: 0x04001073 RID: 4211
		internal Alloc allocIndices;

		// Token: 0x04001074 RID: 4212
		internal uint triangleCount;

		// Token: 0x04001075 RID: 4213
		internal Page allocPage;

		// Token: 0x04001076 RID: 4214
		internal uint allocTime;

		// Token: 0x04001077 RID: 4215
		internal uint updateAllocID;
	}
}
