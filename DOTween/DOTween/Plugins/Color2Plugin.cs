using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000074 RID: 116
	internal class Color2Plugin : ABSTweenPlugin<Color2, Color2, ColorOptions>
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Color2, Color2, ColorOptions> t)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A12C File Offset: 0x0000832C
		public override void SetFrom(TweenerCore<Color2, Color2, ColorOptions> t, bool isRelative)
		{
			Color2 endValue = t.endValue;
			t.endValue = t.getter();
			if (isRelative)
			{
				t.startValue = new Color2(t.endValue.ca + endValue.ca, t.endValue.cb + endValue.cb);
			}
			else
			{
				t.startValue = new Color2(endValue.ca, endValue.cb);
			}
			Color2 color = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				color = t.startValue;
			}
			else
			{
				color.ca.a = t.startValue.ca.a;
				color.cb.a = t.startValue.cb.a;
			}
			t.setter(color);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A208 File Offset: 0x00008408
		public override void SetFrom(TweenerCore<Color2, Color2, ColorOptions> t, Color2 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Color2 color = t.getter();
				t.endValue += color;
				fromValue += color;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				Color2 color2 = fromValue;
				if (t.plugOptions.alphaOnly)
				{
					color2 = t.getter();
					color2.ca.a = fromValue.ca.a;
					color2.cb.a = fromValue.cb.a;
				}
				t.setter(color2);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009F4F File Offset: 0x0000814F
		public override Color2 ConvertToStartValue(TweenerCore<Color2, Color2, ColorOptions> t, Color2 value)
		{
			return value;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A29F File Offset: 0x0000849F
		public override void SetRelativeEndValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public override void SetChangeValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A2D1 File Offset: 0x000084D1
		public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color2 changeValue)
		{
			return 1f / unitsXSecond;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A2DC File Offset: 0x000084DC
		public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color2> getter, DOSetter<Color2> setter, float elapsed, Color2 startValue, Color2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			if (!options.alphaOnly)
			{
				startValue.ca.r = startValue.ca.r + changeValue.ca.r * num;
				startValue.ca.g = startValue.ca.g + changeValue.ca.g * num;
				startValue.ca.b = startValue.ca.b + changeValue.ca.b * num;
				startValue.ca.a = startValue.ca.a + changeValue.ca.a * num;
				startValue.cb.r = startValue.cb.r + changeValue.cb.r * num;
				startValue.cb.g = startValue.cb.g + changeValue.cb.g * num;
				startValue.cb.b = startValue.cb.b + changeValue.cb.b * num;
				startValue.cb.a = startValue.cb.a + changeValue.cb.a * num;
				setter(startValue);
				return;
			}
			Color2 color = getter();
			color.ca.a = startValue.ca.a + changeValue.ca.a * num;
			color.cb.a = startValue.cb.a + changeValue.cb.a * num;
			setter(color);
		}
	}
}
