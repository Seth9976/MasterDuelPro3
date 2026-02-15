using System;

namespace UnityEngine
{
	// Token: 0x020001C0 RID: 448
	[Flags]
	public enum HideFlags
	{
		// Token: 0x0400068E RID: 1678
		None = 0,
		// Token: 0x0400068F RID: 1679
		HideInHierarchy = 1,
		// Token: 0x04000690 RID: 1680
		HideInInspector = 2,
		// Token: 0x04000691 RID: 1681
		DontSaveInEditor = 4,
		// Token: 0x04000692 RID: 1682
		NotEditable = 8,
		// Token: 0x04000693 RID: 1683
		DontSaveInBuild = 16,
		// Token: 0x04000694 RID: 1684
		DontUnloadUnusedAsset = 32,
		// Token: 0x04000695 RID: 1685
		DontSave = 52,
		// Token: 0x04000696 RID: 1686
		HideAndDontSave = 61
	}
}
