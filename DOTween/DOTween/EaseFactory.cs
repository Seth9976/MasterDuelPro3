using System;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000012 RID: 18
	public class EaseFactory
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000033A8 File Offset: 0x000015A8
		public static EaseFunction StopMotion(int motionFps, Ease? ease = null)
		{
			EaseFunction easeFunction = EaseManager.ToEaseFunction((ease == null) ? DOTween.defaultEaseType : ease.Value);
			return EaseFactory.StopMotion(motionFps, easeFunction);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033D9 File Offset: 0x000015D9
		public static EaseFunction StopMotion(int motionFps, AnimationCurve animCurve)
		{
			return EaseFactory.StopMotion(motionFps, new EaseFunction(new EaseCurve(animCurve).Evaluate));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000033F2 File Offset: 0x000015F2
		public static EaseFunction StopMotion(int motionFps, EaseFunction customEase)
		{
			float motionDelay = 1f / (float)motionFps;
			return delegate(float time, float duration, float overshootOrAmplitude, float period)
			{
				float num = ((time < duration) ? (time - time % motionDelay) : time);
				return customEase(num, duration, overshootOrAmplitude, period);
			};
		}
	}
}
