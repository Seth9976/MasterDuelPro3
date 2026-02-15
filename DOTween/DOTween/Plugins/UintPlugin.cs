using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007F RID: 127
	public class UintPlugin : ABSTweenPlugin<uint, uint, UintOptions>
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<uint, uint, UintOptions> t)
		{
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public override void SetFrom(TweenerCore<uint, uint, UintOptions> t, bool isRelative)
		{
			uint endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CF20 File Offset: 0x0000B120
		public override void SetFrom(TweenerCore<uint, uint, UintOptions> t, uint fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				uint num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009F4F File Offset: 0x0000814F
		public override uint ConvertToStartValue(TweenerCore<uint, uint, UintOptions> t, uint value)
		{
			return value;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000CF66 File Offset: 0x0000B166
		public override void SetRelativeEndValue(TweenerCore<uint, uint, UintOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public override void SetChangeValue(TweenerCore<uint, uint, UintOptions> t)
		{
			t.plugOptions.isNegativeChangeValue = t.endValue < t.startValue;
			t.changeValue = (t.plugOptions.isNegativeChangeValue ? (t.startValue - t.endValue) : (t.endValue - t.startValue));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public override float GetSpeedBasedDuration(UintOptions options, float unitsXSecond, uint changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public override void EvaluateAndApply(UintOptions options, Tween t, bool isRelative, DOGetter<uint> getter, DOSetter<uint> setter, float elapsed, uint startValue, uint changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			uint num;
			if (t.loopType == LoopType.Incremental)
			{
				num = (uint)((ulong)changeValue * (ulong)((long)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops)));
				if (options.isNegativeChangeValue)
				{
					startValue -= num;
				}
				else
				{
					startValue += num;
				}
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				num = (uint)((ulong)changeValue * (ulong)((long)((t.loopType == LoopType.Incremental) ? t.loops : 1)) * (ulong)((long)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops)));
				if (options.isNegativeChangeValue)
				{
					startValue -= num;
				}
				else
				{
					startValue += num;
				}
			}
			num = (uint)Math.Round((double)(changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
			if (options.isNegativeChangeValue)
			{
				setter(startValue - num);
				return;
			}
			setter(startValue + num);
		}
	}
}
