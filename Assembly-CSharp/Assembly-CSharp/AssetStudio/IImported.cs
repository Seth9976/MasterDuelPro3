using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000150 RID: 336
	public interface IImported
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060003CB RID: 971
		ImportedFrame RootFrame { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060003CC RID: 972
		List<ImportedMesh> MeshList { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060003CD RID: 973
		List<ImportedMaterial> MaterialList { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003CE RID: 974
		List<ImportedTexture> TextureList { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003CF RID: 975
		List<ImportedKeyframedAnimation> AnimationList { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060003D0 RID: 976
		List<ImportedMorph> MorphList { get; }
	}
}
