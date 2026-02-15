using System;

namespace AssetStudio
{
	// Token: 0x0200017A RID: 378
	public enum SerializedFileFormatVersion
	{
		// Token: 0x040009EC RID: 2540
		Unsupported = 1,
		// Token: 0x040009ED RID: 2541
		Unknown_2,
		// Token: 0x040009EE RID: 2542
		Unknown_3,
		// Token: 0x040009EF RID: 2543
		Unknown_5 = 5,
		// Token: 0x040009F0 RID: 2544
		Unknown_6,
		// Token: 0x040009F1 RID: 2545
		Unknown_7,
		// Token: 0x040009F2 RID: 2546
		Unknown_8,
		// Token: 0x040009F3 RID: 2547
		Unknown_9,
		// Token: 0x040009F4 RID: 2548
		Unknown_10,
		// Token: 0x040009F5 RID: 2549
		HasScriptTypeIndex,
		// Token: 0x040009F6 RID: 2550
		Unknown_12,
		// Token: 0x040009F7 RID: 2551
		HasTypeTreeHashes,
		// Token: 0x040009F8 RID: 2552
		Unknown_14,
		// Token: 0x040009F9 RID: 2553
		SupportsStrippedObject,
		// Token: 0x040009FA RID: 2554
		RefactoredClassId,
		// Token: 0x040009FB RID: 2555
		RefactorTypeData,
		// Token: 0x040009FC RID: 2556
		RefactorShareableTypeTreeData,
		// Token: 0x040009FD RID: 2557
		TypeTreeNodeWithTypeFlags,
		// Token: 0x040009FE RID: 2558
		SupportsRefObject,
		// Token: 0x040009FF RID: 2559
		StoresTypeDependencies,
		// Token: 0x04000A00 RID: 2560
		LargeFilesSupport
	}
}
