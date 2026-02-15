using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000076 RID: 118
	public class LongPlugin : ABSTweenPlugin<long, long, NoOptions>
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<long, long, NoOptions> t)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public override void SetFrom(TweenerCore<long, long, NoOptions> t, bool isRelative)
		{
			long endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A700 File Offset: 0x00008900
		public override void SetFrom(TweenerCore<long, long, NoOptions> t, long fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				long num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009F4F File Offset: 0x0000814F
		public override long ConvertToStartValue(TweenerCore<long, long, NoOptions> t, long value)
		{
			return value;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A746 File Offset: 0x00008946
		public override void SetRelativeEndValue(TweenerCore<long, long, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A75B File Offset: 0x0000895B
		public override void SetChangeValue(TweenerCore<long, long, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A770 File Offset: 0x00008970
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, long changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A790 File Offset: 0x00008990
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<long> getter, DOSetter<long> setter, float elapsed, long startValue, long changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (long)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (long)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (long)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((long)Math.Round((double)((float)startValue + (float)changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))));
		}
	}
}
