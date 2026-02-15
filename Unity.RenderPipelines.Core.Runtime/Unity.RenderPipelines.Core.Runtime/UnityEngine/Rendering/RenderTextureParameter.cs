using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000214 RID: 532
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class RenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x000346FA File Offset: 0x000328FA
		public RenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00034704 File Offset: 0x00032904
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
