using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000215 RID: 533
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpRenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x000346FA File Offset: 0x000328FA
		public NoInterpRenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00034738 File Offset: 0x00032938
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
