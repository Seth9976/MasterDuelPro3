using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000206 RID: 518
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003425F File Offset: 0x0003245F
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00034267 File Offset: 0x00032467
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

		// Token: 0x06000E44 RID: 3652 RVA: 0x000342A1 File Offset: 0x000324A1
		public FloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000342BC File Offset: 0x000324BC
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}

		// Token: 0x0400095F RID: 2399
		[NonSerialized]
		public float min;

		// Token: 0x04000960 RID: 2400
		[NonSerialized]
		public float max;
	}
}
