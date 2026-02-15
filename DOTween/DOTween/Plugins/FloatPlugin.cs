using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000084 RID: 132
	public class FloatPlugin : ABSTweenPlugin<float, float, FloatOptions>
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<float, float, FloatOptions> t)
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E404 File Offset: 0x0000C604
		public override void SetFrom(TweenerCore<float, float, FloatOptions> t, bool isRelative)
		{
			float endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter((!t.plugOptions.snapping) ? t.startValue : ((float)Math.Round((double)t.startValue)));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E46C File Offset: 0x0000C66C
		public override void SetFrom(TweenerCore<float, float, FloatOptions> t, float fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				float num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter((!t.plugOptions.snapping) ? fromValue : ((float)Math.Round((double)fromValue)));
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009F4F File Offset: 0x0000814F
		public override float ConvertToStartValue(TweenerCore<float, float, FloatOptions> t, float value)
		{
			return value;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E4C9 File Offset: 0x0000C6C9
		public override void SetRelativeEndValue(TweenerCore<float, float, FloatOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public override void SetChangeValue(TweenerCore<float, float, FloatOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public override float GetSpeedBasedDuration(FloatOptions options, float unitsXSecond, float changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000E514 File Offset: 0x0000C714
		public override void EvaluateAndApply(FloatOptions options, Tween t, bool isRelative, DOGetter<float> getter, DOSetter<float> setter, float elapsed, float startValue, float changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((!options.snapping) ? (startValue + changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)) : ((float)Math.Round((double)(startValue + changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)))));
		}
	}
}
