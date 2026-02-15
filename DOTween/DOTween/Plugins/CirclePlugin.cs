using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000073 RID: 115
	public class CirclePlugin : ABSTweenPlugin<Vector2, Vector2, CircleOptions>
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Vector2, Vector2, CircleOptions> t)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public override void SetFrom(TweenerCore<Vector2, Vector2, CircleOptions> t, bool isRelative)
		{
			if (!t.plugOptions.initialized)
			{
				t.startValue = t.getter();
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			float endValueDegrees = t.plugOptions.endValueDegrees;
			t.plugOptions.endValueDegrees = t.plugOptions.startValueDegrees;
			t.plugOptions.startValueDegrees = (isRelative ? (t.plugOptions.endValueDegrees + endValueDegrees) : endValueDegrees);
			t.startValue = this.GetPositionOnCircle(t.plugOptions, t.plugOptions.startValueDegrees);
			t.setter(t.startValue);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009EA8 File Offset: 0x000080A8
		public override void SetFrom(TweenerCore<Vector2, Vector2, CircleOptions> t, Vector2 fromValue, bool setImmediately, bool isRelative)
		{
			if (!t.plugOptions.initialized)
			{
				t.startValue = t.getter();
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			float num = fromValue.x;
			if (isRelative)
			{
				float startValueDegrees = t.plugOptions.startValueDegrees;
				t.plugOptions.endValueDegrees = t.plugOptions.endValueDegrees + startValueDegrees;
				num += startValueDegrees;
			}
			t.plugOptions.startValueDegrees = num;
			t.startValue = this.GetPositionOnCircle(t.plugOptions, num);
			if (setImmediately)
			{
				t.setter(t.startValue);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00009F48 File Offset: 0x00008148
		public static ABSTweenPlugin<Vector2, Vector2, CircleOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<CirclePlugin, Vector2, Vector2, CircleOptions>();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009F4F File Offset: 0x0000814F
		public override Vector2 ConvertToStartValue(TweenerCore<Vector2, Vector2, CircleOptions> t, Vector2 value)
		{
			return value;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00009F52 File Offset: 0x00008152
		public override void SetRelativeEndValue(TweenerCore<Vector2, Vector2, CircleOptions> t)
		{
			if (!t.plugOptions.initialized)
			{
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			t.plugOptions.endValueDegrees = t.plugOptions.endValueDegrees + t.plugOptions.startValueDegrees;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009F94 File Offset: 0x00008194
		public override void SetChangeValue(TweenerCore<Vector2, Vector2, CircleOptions> t)
		{
			if (!t.plugOptions.initialized)
			{
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			t.changeValue = new Vector2(t.plugOptions.endValueDegrees - t.plugOptions.startValueDegrees, 0f);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009FEC File Offset: 0x000081EC
		public override float GetSpeedBasedDuration(CircleOptions options, float unitsXSecond, Vector2 changeValue)
		{
			return changeValue.x / unitsXSecond;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009FF8 File Offset: 0x000081F8
		public override void EvaluateAndApply(CircleOptions options, Tween t, bool isRelative, DOGetter<Vector2> getter, DOSetter<Vector2> setter, float elapsed, Vector2 startValue, Vector2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			float num = options.startValueDegrees;
			if (t.loopType == LoopType.Incremental)
			{
				num += changeValue.x * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				num += changeValue.x * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num2 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(this.GetPositionOnCircle(options, num + changeValue.x * num2));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A0D4 File Offset: 0x000082D4
		public Vector2 GetPositionOnCircle(CircleOptions options, float degrees)
		{
			Vector2 pointOnCircle = DOTweenUtils.GetPointOnCircle(options.center, options.radius, degrees);
			if (options.snapping)
			{
				pointOnCircle.x = Mathf.Round(pointOnCircle.x);
				pointOnCircle.y = Mathf.Round(pointOnCircle.y);
			}
			return pointOnCircle;
		}
	}
}
