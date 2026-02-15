using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020E RID: 526
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00034568 File Offset: 0x00032768
		public Vector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00034574 File Offset: 0x00032774
		public override void Interp(Vector4 from, Vector4 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
			this.m_Value.w = from.w + (to.w - from.w) * t;
		}
	}
}
