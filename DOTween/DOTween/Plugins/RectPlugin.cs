using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007E RID: 126
	public class RectPlugin : ABSTweenPlugin<Rect, Rect, RectOptions>
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Rect, Rect, RectOptions> t)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C910 File Offset: 0x0000AB10
		public override void SetFrom(TweenerCore<Rect, Rect, RectOptions> t, bool isRelative)
		{
			Rect endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.x = t.startValue.x + t.endValue.x;
				t.startValue.y = t.startValue.y + t.endValue.y;
				t.startValue.width = t.startValue.width + t.endValue.width;
				t.startValue.height = t.startValue.height + t.endValue.height;
			}
			Rect startValue = t.startValue;
			if (t.plugOptions.snapping)
			{
				startValue.x = (float)Math.Round((double)startValue.x);
				startValue.y = (float)Math.Round((double)startValue.y);
				startValue.width = (float)Math.Round((double)startValue.width);
				startValue.height = (float)Math.Round((double)startValue.height);
			}
			t.setter(startValue);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public override void SetFrom(TweenerCore<Rect, Rect, RectOptions> t, Rect fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Rect rect = t.getter();
				t.endValue.x = t.endValue.x + rect.x;
				t.endValue.y = t.endValue.y + rect.y;
				t.endValue.width = t.endValue.width + rect.width;
				t.endValue.height = t.endValue.height + rect.height;
				fromValue.x += rect.x;
				fromValue.y += rect.y;
				fromValue.width += rect.width;
				fromValue.height += rect.height;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				Rect rect2 = fromValue;
				if (t.plugOptions.snapping)
				{
					rect2.x = (float)Math.Round((double)rect2.x);
					rect2.y = (float)Math.Round((double)rect2.y);
					rect2.width = (float)Math.Round((double)rect2.width);
					rect2.height = (float)Math.Round((double)rect2.height);
				}
				t.setter(rect2);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009F4F File Offset: 0x0000814F
		public override Rect ConvertToStartValue(TweenerCore<Rect, Rect, RectOptions> t, Rect value)
		{
			return value;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public override void SetRelativeEndValue(TweenerCore<Rect, Rect, RectOptions> t)
		{
			t.endValue.x = t.endValue.x + t.startValue.x;
			t.endValue.y = t.endValue.y + t.startValue.y;
			t.endValue.width = t.endValue.width + t.startValue.width;
			t.endValue.height = t.endValue.height + t.startValue.height;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public override void SetChangeValue(TweenerCore<Rect, Rect, RectOptions> t)
		{
			t.changeValue = new Rect(t.endValue.x - t.startValue.x, t.endValue.y - t.startValue.y, t.endValue.width - t.startValue.width, t.endValue.height - t.startValue.height);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public override float GetSpeedBasedDuration(RectOptions options, float unitsXSecond, Rect changeValue)
		{
			float width = changeValue.width;
			float height = changeValue.height;
			return (float)Math.Sqrt((double)(width * width + height * height)) / unitsXSecond;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public override void EvaluateAndApply(RectOptions options, Tween t, bool isRelative, DOGetter<Rect> getter, DOSetter<Rect> setter, float elapsed, Rect startValue, Rect changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				startValue.x += changeValue.x * (float)num;
				startValue.y += changeValue.y * (float)num;
				startValue.width += changeValue.width * (float)num;
				startValue.height += changeValue.height * (float)num;
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				int num2 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				startValue.x += changeValue.x * (float)num2;
				startValue.y += changeValue.y * (float)num2;
				startValue.width += changeValue.width * (float)num2;
				startValue.height += changeValue.height * (float)num2;
			}
			float num3 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			startValue.x += changeValue.x * num3;
			startValue.y += changeValue.y * num3;
			startValue.width += changeValue.width * num3;
			startValue.height += changeValue.height * num3;
			if (options.snapping)
			{
				startValue.x = (float)Math.Round((double)startValue.x);
				startValue.y = (float)Math.Round((double)startValue.y);
				startValue.width = (float)Math.Round((double)startValue.width);
				startValue.height = (float)Math.Round((double)startValue.height);
			}
			setter(startValue);
		}
	}
}
