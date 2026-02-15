using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000017 RID: 23
	internal interface ICurvesOwner
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A8 RID: 168
		AnimationClip curves { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169
		bool hasCurves { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AA RID: 170
		double duration { get; }

		// Token: 0x060000AB RID: 171
		void CreateCurves(string curvesClipName);

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AC RID: 172
		string defaultCurvesName { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AD RID: 173
		Object asset { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AE RID: 174
		Object assetOwner { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175
		TrackAsset targetTrack { get; }
	}
}
