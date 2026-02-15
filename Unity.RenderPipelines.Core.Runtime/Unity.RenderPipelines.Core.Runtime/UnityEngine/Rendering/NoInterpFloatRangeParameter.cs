using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000207 RID: 519
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003425F File Offset: 0x0003245F
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0003430B File Offset: 0x0003250B
		public override Vector2 value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value.x = Mathf.Max(value.x, this.min);
				this.m_Value.y = Mathf.Min(value.y, this.max);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00034345 File Offset: 0x00032545
		public NoInterpFloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000961 RID: 2401
		[NonSerialized]
		public float min;

		// Token: 0x04000962 RID: 2402
		[NonSerialized]
		public float max;
	}
}
