using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020C RID: 524
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x000344EB File Offset: 0x000326EB
		public Vector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000344F8 File Offset: 0x000326F8
		public override void Interp(Vector3 from, Vector3 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
		}
	}
}
