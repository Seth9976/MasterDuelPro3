using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200020A RID: 522
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00034490 File Offset: 0x00032690
		public Vector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003449C File Offset: 0x0003269C
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}
	}
}
