using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class TextureCurveParameter : VolumeParameter<TextureCurve>
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00032645 File Offset: 0x00030845
		public TextureCurveParameter(TextureCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003264F File Offset: 0x0003084F
		public override void Release()
		{
			this.m_Value.Release();
		}
	}
}
