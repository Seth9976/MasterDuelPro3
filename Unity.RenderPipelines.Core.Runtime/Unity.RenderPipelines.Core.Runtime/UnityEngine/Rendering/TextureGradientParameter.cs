using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	public class TextureGradientParameter : VolumeParameter<TextureGradient>
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x00032A31 File Offset: 0x00030C31
		public TextureGradientParameter(TextureGradient value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00032A3B File Offset: 0x00030C3B
		public override void Release()
		{
			this.m_Value.Release();
		}
	}
}
