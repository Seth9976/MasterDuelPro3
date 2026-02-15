using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000211 RID: 529
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpTextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00034656 File Offset: 0x00032856
		public NoInterpTextureParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00034660 File Offset: 0x00032860
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
