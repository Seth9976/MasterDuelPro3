using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007A RID: 122
	public class ColorPlugin : ABSTweenPlugin<Color, Color, ColorOptions>
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Color, Color, ColorOptions> t)
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B844 File Offset: 0x00009A44
		public override void SetFrom(TweenerCore<Color, Color, ColorOptions> t, bool isRelative)
		{
			Color endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			Color color = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				color = t.startValue;
			}
			else
			{
				color.a = t.startValue.a;
			}
			t.setter(color);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public override void SetFrom(TweenerCore<Color, Color, ColorOptions> t, Color fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Color color = t.getter();
				t.endValue += color;
				fromValue += color;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				Color color2 = fromValue;
				if (t.plugOptions.alphaOnly)
				{
					color2 = t.getter();
					color2.a = fromValue.a;
				}
				t.setter(color2);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009F4F File Offset: 0x0000814F
		public override Color ConvertToStartValue(TweenerCore<Color, Color, ColorOptions> t, Color value)
		{
			return value;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B932 File Offset: 0x00009B32
		public override void SetRelativeEndValue(TweenerCore<Color, Color, ColorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B94B File Offset: 0x00009B4B
		public override void SetChangeValue(TweenerCore<Color, Color, ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A2D1 File Offset: 0x000084D1
		public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color changeValue)
		{
			return 1f / unitsXSecond;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B964 File Offset: 0x00009B64
		public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color> getter, DOSetter<Color> setter, float elapsed, Color startValue, Color changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
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
				startValue.r += changeValue.r * num;
				startValue.g += changeValue.g * num;
				startValue.b += changeValue.b * num;
				startValue.a += changeValue.a * num;
				setter(startValue);
				return;
			}
			Color color = getter();
			color.a = startValue.a + changeValue.a * num;
			setter(color);
		}
	}
}
