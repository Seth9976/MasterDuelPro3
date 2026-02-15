using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000077 RID: 119
	public class UlongPlugin : ABSTweenPlugin<ulong, ulong, NoOptions>
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<ulong, ulong, NoOptions> t)
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A860 File Offset: 0x00008A60
		public override void SetFrom(TweenerCore<ulong, ulong, NoOptions> t, bool isRelative)
		{
			ulong endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public override void SetFrom(TweenerCore<ulong, ulong, NoOptions> t, ulong fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				ulong num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009F4F File Offset: 0x0000814F
		public override ulong ConvertToStartValue(TweenerCore<ulong, ulong, NoOptions> t, ulong value)
		{
			return value;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A8F2 File Offset: 0x00008AF2
		public override void SetRelativeEndValue(TweenerCore<ulong, ulong, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A907 File Offset: 0x00008B07
		public override void SetChangeValue(TweenerCore<ulong, ulong, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A91C File Offset: 0x00008B1C
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, ulong changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A93C File Offset: 0x00008B3C
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<ulong> getter, DOSetter<ulong> setter, float elapsed, ulong startValue, ulong changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (ulong)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (ulong)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (ulong)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((ulong)(startValue + changeValue * (decimal)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
		}
	}
}
