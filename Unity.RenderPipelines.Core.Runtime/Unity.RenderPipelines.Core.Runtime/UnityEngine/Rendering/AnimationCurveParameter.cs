using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200021A RID: 538
	[Serializable]
	public class AnimationCurveParameter : VolumeParameter<AnimationCurve>
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x00034964 File Offset: 0x00032B64
		public AnimationCurveParameter(AnimationCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003496E File Offset: 0x00032B6E
		public override void Interp(AnimationCurve lhsCurve, AnimationCurve rhsCurve, float t)
		{
			this.m_Value = lhsCurve;
			KeyframeUtility.InterpAnimationCurve(ref this.m_Value, rhsCurve, t);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00034984 File Offset: 0x00032B84
		public override void SetValue(VolumeParameter parameter)
		{
			this.m_Value.CopyFrom(((AnimationCurveParameter)parameter).m_Value);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003499C File Offset: 0x00032B9C
		public override object Clone()
		{
			return new AnimationCurveParameter(new AnimationCurve(base.GetValue<AnimationCurve>().keys), this.overrideState);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000349BC File Offset: 0x00032BBC
		public override int GetHashCode()
		{
			return this.overrideState.GetHashCode() * 23 + this.value.GetHashCode();
		}
	}
}
