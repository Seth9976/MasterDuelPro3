using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200015D RID: 349
	public class ImportedAnimationKeyframedTrack
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000159C3 File Offset: 0x00013BC3
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000159CB File Offset: 0x00013BCB
		public string Path { get; set; }

		// Token: 0x0400096C RID: 2412
		public List<ImportedKeyframe<Vector3>> Scalings = new List<ImportedKeyframe<Vector3>>();

		// Token: 0x0400096D RID: 2413
		public List<ImportedKeyframe<Vector3>> Rotations = new List<ImportedKeyframe<Vector3>>();

		// Token: 0x0400096E RID: 2414
		public List<ImportedKeyframe<Vector3>> Translations = new List<ImportedKeyframe<Vector3>>();

		// Token: 0x0400096F RID: 2415
		public ImportedBlendShape BlendShape;
	}
}
