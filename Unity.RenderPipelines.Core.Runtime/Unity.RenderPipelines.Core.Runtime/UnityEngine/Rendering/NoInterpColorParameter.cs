using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000209 RID: 521
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x00034441 File Offset: 0x00032641
		public NoInterpColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00034459 File Offset: 0x00032659
		public NoInterpColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x04000966 RID: 2406
		public bool hdr;

		// Token: 0x04000967 RID: 2407
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x04000968 RID: 2408
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
