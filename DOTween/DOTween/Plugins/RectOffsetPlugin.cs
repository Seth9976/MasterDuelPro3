using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007D RID: 125
	public class RectOffsetPlugin : ABSTweenPlugin<RectOffset, RectOffset, NoOptions>
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public override void Reset(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public override void SetFrom(TweenerCore<RectOffset, RectOffset, NoOptions> t, bool isRelative)
		{
			RectOffset endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.left += t.endValue.left;
				t.startValue.right += t.endValue.right;
				t.startValue.top += t.endValue.top;
				t.startValue.bottom += t.endValue.bottom;
			}
			t.setter(t.startValue);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C47C File Offset: 0x0000A67C
		public override void SetFrom(TweenerCore<RectOffset, RectOffset, NoOptions> t, RectOffset fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				RectOffset rectOffset = t.getter();
				t.endValue.left += rectOffset.left;
				t.endValue.right += rectOffset.right;
				t.endValue.top += rectOffset.top;
				t.endValue.bottom += rectOffset.bottom;
				fromValue.left += rectOffset.left;
				fromValue.right += rectOffset.right;
				fromValue.top += rectOffset.top;
				fromValue.bottom += rectOffset.bottom;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C55E File Offset: 0x0000A75E
		public override RectOffset ConvertToStartValue(TweenerCore<RectOffset, RectOffset, NoOptions> t, RectOffset value)
		{
			return new RectOffset(value.left, value.right, value.top, value.bottom);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C580 File Offset: 0x0000A780
		public override void SetRelativeEndValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.endValue.left += t.startValue.left;
			t.endValue.right += t.startValue.right;
			t.endValue.top += t.startValue.top;
			t.endValue.bottom += t.startValue.bottom;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C604 File Offset: 0x0000A804
		public override void SetChangeValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.changeValue = new RectOffset(t.endValue.left - t.startValue.left, t.endValue.right - t.startValue.right, t.endValue.top - t.startValue.top, t.endValue.bottom - t.startValue.bottom);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C678 File Offset: 0x0000A878
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, RectOffset changeValue)
		{
			float num = (float)changeValue.right;
			if (num < 0f)
			{
				num = -num;
			}
			float num2 = (float)changeValue.bottom;
			if (num2 < 0f)
			{
				num2 = -num2;
			}
			return (float)Math.Sqrt((double)(num * num + num2 * num2)) / unitsXSecond;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<RectOffset> getter, DOSetter<RectOffset> setter, float elapsed, RectOffset startValue, RectOffset changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			RectOffsetPlugin._r.left = startValue.left;
			RectOffsetPlugin._r.right = startValue.right;
			RectOffsetPlugin._r.top = startValue.top;
			RectOffsetPlugin._r.bottom = startValue.bottom;
			if (t.loopType == LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				RectOffsetPlugin._r.left += changeValue.left * num;
				RectOffsetPlugin._r.right += changeValue.right * num;
				RectOffsetPlugin._r.top += changeValue.top * num;
				RectOffsetPlugin._r.bottom += changeValue.bottom * num;
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				int num2 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				RectOffsetPlugin._r.left += changeValue.left * num2;
				RectOffsetPlugin._r.right += changeValue.right * num2;
				RectOffsetPlugin._r.top += changeValue.top * num2;
				RectOffsetPlugin._r.bottom += changeValue.bottom * num2;
			}
			float num3 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(new RectOffset((int)Math.Round((double)((float)RectOffsetPlugin._r.left + (float)changeValue.left * num3)), (int)Math.Round((double)((float)RectOffsetPlugin._r.right + (float)changeValue.right * num3)), (int)Math.Round((double)((float)RectOffsetPlugin._r.top + (float)changeValue.top * num3)), (int)Math.Round((double)((float)RectOffsetPlugin._r.bottom + (float)changeValue.bottom * num3))));
		}

		// Token: 0x0400015F RID: 351
		private static RectOffset _r = new RectOffset();
	}
}
