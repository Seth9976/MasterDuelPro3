using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000210 RID: 528
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class TextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x00034605 File Offset: 0x00032805
		public TextureParameter(Texture value, bool overrideState = false)
			: this(value, TextureDimension.Any, overrideState)
		{
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00034610 File Offset: 0x00032810
		public TextureParameter(Texture value, TextureDimension dimension, bool overrideState = false)
			: base(value, overrideState)
		{
			this.dimension = dimension;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00034624 File Offset: 0x00032824
		public override int GetHashCode()
		{
			int hash = base.GetHashCode();
			if (this.value != null)
			{
				hash = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return hash;
		}

		// Token: 0x04000969 RID: 2409
		public TextureDimension dimension;
	}
}
