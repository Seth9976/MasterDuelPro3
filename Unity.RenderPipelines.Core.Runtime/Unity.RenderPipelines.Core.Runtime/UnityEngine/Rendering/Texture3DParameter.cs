using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000213 RID: 531
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture3DParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x00034656 File Offset: 0x00032856
		public Texture3DParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000346C8 File Offset: 0x000328C8
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
