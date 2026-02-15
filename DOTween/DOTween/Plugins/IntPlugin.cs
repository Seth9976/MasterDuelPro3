using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007B RID: 123
	public class IntPlugin : ABSTweenPlugin<int, int, NoOptions>
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<int, int, NoOptions> t)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public override void SetFrom(TweenerCore<int, int, NoOptions> t, bool isRelative)
		{
			int endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public override void SetFrom(TweenerCore<int, int, NoOptions> t, int fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				int num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009F4F File Offset: 0x0000814F
		public override int ConvertToStartValue(TweenerCore<int, int, NoOptions> t, int value)
		{
			return value;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BB52 File Offset: 0x00009D52
		public override void SetRelativeEndValue(TweenerCore<int, int, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BB67 File Offset: 0x00009D67
		public override void SetChangeValue(TweenerCore<int, int, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BB7C File Offset: 0x00009D7C
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, int changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<int> getter, DOSetter<int> setter, float elapsed, int startValue, int changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((int)Math.Round((double)((float)startValue + (float)changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))));
		}
	}
}
