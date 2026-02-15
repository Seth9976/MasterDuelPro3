using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000216 RID: 534
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class CubemapParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000E64 RID: 3684 RVA: 0x00034656 File Offset: 0x00032856
		public CubemapParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003476C File Offset: 0x0003296C
		public override int GetHashCode()
		{
			int hash = base.GetHashCode();
			if (this.value != null)
			{
				hash = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return hash;
		}
	}
}
