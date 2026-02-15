using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000075 RID: 117
	public class DoublePlugin : ABSTweenPlugin<double, double, NoOptions>
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<double, double, NoOptions> t)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A510 File Offset: 0x00008710
		public override void SetFrom(TweenerCore<double, double, NoOptions> t, bool isRelative)
		{
			double endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A55C File Offset: 0x0000875C
		public override void SetFrom(TweenerCore<double, double, NoOptions> t, double fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				double num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009F4F File Offset: 0x0000814F
		public override double ConvertToStartValue(TweenerCore<double, double, NoOptions> t, double value)
		{
			return value;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A5A2 File Offset: 0x000087A2
		public override void SetRelativeEndValue(TweenerCore<double, double, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A5B7 File Offset: 0x000087B7
		public override void SetChangeValue(TweenerCore<double, double, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A5CC File Offset: 0x000087CC
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, double changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A5EC File Offset: 0x000087EC
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<double> getter, DOSetter<double> setter, float elapsed, double startValue, double changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (double)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (double)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (double)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter(startValue + changeValue * (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
		}
	}
}
