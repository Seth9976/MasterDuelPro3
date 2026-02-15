using System;
using UnityEngine;

namespace DG.Tweening.Core.Easing
{
	// Token: 0x020000C4 RID: 196
	public class EaseCurve
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x00014D60 File Offset: 0x00012F60
		public EaseCurve(AnimationCurve animCurve)
		{
			this._animCurve = animCurve;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014D70 File Offset: 0x00012F70
		public float Evaluate(float time, float duration, float unusedOvershoot, float unusedPeriod)
		{
			float time2 = this._animCurve[this._animCurve.length - 1].time;
			float num = time / duration;
			return this._animCurve.Evaluate(num * time2);
		}

		// Token: 0x040002AB RID: 683
		private readonly AnimationCurve _animCurve;
	}
}
