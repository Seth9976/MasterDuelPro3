using System;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000009 RID: 9
	[Flags]
	public enum TileFlags
	{
		// Token: 0x0400001B RID: 27
		None = 0,
		// Token: 0x0400001C RID: 28
		LockColor = 1,
		// Token: 0x0400001D RID: 29
		LockTransform = 2,
		// Token: 0x0400001E RID: 30
		InstantiateGameObjectRuntimeOnly = 4,
		// Token: 0x0400001F RID: 31
		KeepGameObjectRuntimeOnly = 8,
		// Token: 0x04000020 RID: 32
		LockAll = 3
	}
}
