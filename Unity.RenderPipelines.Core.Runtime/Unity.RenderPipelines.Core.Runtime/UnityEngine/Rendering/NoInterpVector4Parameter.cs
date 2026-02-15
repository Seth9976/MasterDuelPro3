using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020F RID: 527
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x06000E56 RID: 3670 RVA: 0x00034568 File Offset: 0x00032768
		public NoInterpVector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
