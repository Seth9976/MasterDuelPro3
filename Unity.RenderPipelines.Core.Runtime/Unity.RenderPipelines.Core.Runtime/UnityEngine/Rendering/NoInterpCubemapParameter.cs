using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000217 RID: 535
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpCubemapParameter : VolumeParameter<Cubemap>
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x0003479E File Offset: 0x0003299E
		public NoInterpCubemapParameter(Cubemap value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000347A8 File Offset: 0x000329A8
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
