using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000212 RID: 530
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture2DParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x00034656 File Offset: 0x00032856
		public Texture2DParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00034694 File Offset: 0x00032894
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
