using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200000C RID: 12
	public static class DOVirtual
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003070 File Offset: 0x00001270
		public static Tweener Float(float from, float to, float duration, TweenCallback<float> onVirtualUpdate)
		{
			return DOTween.To(() => from, delegate(float x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030C4 File Offset: 0x000012C4
		public static Tweener Int(int from, int to, float duration, TweenCallback<int> onVirtualUpdate)
		{
			return DOTween.To(() => from, delegate(int x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003118 File Offset: 0x00001318
		public static Tweener Vector3(Vector3 from, Vector3 to, float duration, TweenCallback<Vector3> onVirtualUpdate)
		{
			return DOTween.To(() => from, delegate(Vector3 x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000316C File Offset: 0x0000136C
		public static Tweener Color(Color from, Color to, float duration, TweenCallback<Color> onVirtualUpdate)
		{
			return DOTween.To(() => from, delegate(Color x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031BD File Offset: 0x000013BD
		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031DC File Offset: 0x000013DC
		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType, float overshoot)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, overshoot, DOTween.defaultEasePeriod);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031F8 File Offset: 0x000013F8
		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType, float amplitude, float period)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, amplitude, period);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003211 File Offset: 0x00001411
		public static float EasedValue(float from, float to, float lifetimePercentage, AnimationCurve easeCurve)
		{
			return from + (to - from) * EaseManager.Evaluate(Ease.INTERNAL_Custom, new EaseFunction(new EaseCurve(easeCurve).Evaluate), lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003241 File Offset: 0x00001441
		public static Vector3 EasedValue(Vector3 from, Vector3 to, float lifetimePercentage, Ease easeType)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000326C File Offset: 0x0000146C
		public static Vector3 EasedValue(Vector3 from, Vector3 to, float lifetimePercentage, Ease easeType, float overshoot)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, overshoot, DOTween.defaultEasePeriod);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003294 File Offset: 0x00001494
		public static Vector3 EasedValue(Vector3 from, Vector3 to, float lifetimePercentage, Ease easeType, float amplitude, float period)
		{
			return from + (to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, amplitude, period);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032B9 File Offset: 0x000014B9
		public static Vector3 EasedValue(Vector3 from, Vector3 to, float lifetimePercentage, AnimationCurve easeCurve)
		{
			return from + (to - from) * EaseManager.Evaluate(Ease.INTERNAL_Custom, new EaseFunction(new EaseCurve(easeCurve).Evaluate), lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000032F5 File Offset: 0x000014F5
		public static Tween DelayedCall(float delay, TweenCallback callback, bool ignoreTimeScale = true)
		{
			return DOTween.Sequence().AppendInterval(delay).OnStepComplete(callback)
				.SetUpdate(UpdateType.Normal, ignoreTimeScale)
				.SetAutoKill(true);
		}
	}
}
