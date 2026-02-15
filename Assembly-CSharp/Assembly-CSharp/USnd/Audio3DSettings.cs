using System;
using UnityEngine;

namespace USnd
{
	// Token: 0x0200117C RID: 4476
	public class Audio3DSettings : ScriptableObject, ICloneable
	{
		// Token: 0x060084C3 RID: 33987 RVA: 0x0000216A File Offset: 0x0000036A
		public object Clone()
		{
			return null;
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x0000216D File Offset: 0x0000036D
		public void Copy(Audio3DSettings newParam)
		{
		}

		// Token: 0x0400C051 RID: 49233
		public string spatialName;

		// Token: 0x0400C052 RID: 49234
		public float spatialBlend;

		// Token: 0x0400C053 RID: 49235
		public float reverbZoneMix;

		// Token: 0x0400C054 RID: 49236
		public float dopplerLevel;

		// Token: 0x0400C055 RID: 49237
		public int spread;

		// Token: 0x0400C056 RID: 49238
		public AudioRolloffMode rolloffMode;

		// Token: 0x0400C057 RID: 49239
		public float minDistance;

		// Token: 0x0400C058 RID: 49240
		public float maxDistance;

		// Token: 0x0400C059 RID: 49241
		public AnimationCurve customRolloffCurve;

		// Token: 0x0400C05A RID: 49242
		public AnimationCurve spatialBlendCurve;

		// Token: 0x0400C05B RID: 49243
		public AnimationCurve reverbZoneMixCurve;

		// Token: 0x0400C05C RID: 49244
		public AnimationCurve spreadCurve;
	}
}
