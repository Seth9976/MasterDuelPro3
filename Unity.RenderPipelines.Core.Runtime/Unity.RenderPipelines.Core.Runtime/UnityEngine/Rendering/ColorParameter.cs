using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000208 RID: 520
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x0003435E File Offset: 0x0003255E
		public ColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00034376 File Offset: 0x00032576
		public ColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000343B0 File Offset: 0x000325B0
		public override void Interp(Color from, Color to, float t)
		{
			this.m_Value.r = from.r + (to.r - from.r) * t;
			this.m_Value.g = from.g + (to.g - from.g) * t;
			this.m_Value.b = from.b + (to.b - from.b) * t;
			this.m_Value.a = from.a + (to.a - from.a) * t;
		}

		// Token: 0x04000963 RID: 2403
		[NonSerialized]
		public bool hdr;

		// Token: 0x04000964 RID: 2404
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x04000965 RID: 2405
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
