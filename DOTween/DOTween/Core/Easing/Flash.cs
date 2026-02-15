using System;
using UnityEngine;

namespace DG.Tweening.Core.Easing
{
	// Token: 0x020000C5 RID: 197
	public static class Flash
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public static float Ease(float time, float duration, float overshootOrAmplitude, float period)
		{
			int num = Mathf.CeilToInt(time / duration * overshootOrAmplitude);
			float num2 = duration / overshootOrAmplitude;
			time -= num2 * (float)(num - 1);
			float num3 = (float)((num % 2 != 0) ? 1 : (-1));
			if (num3 < 0f)
			{
				time -= num2;
			}
			float num4 = time * num3 / num2;
			return Flash.WeightedEase(overshootOrAmplitude, period, num, num2, num3, num4);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014E00 File Offset: 0x00013000
		public static float EaseIn(float time, float duration, float overshootOrAmplitude, float period)
		{
			int num = Mathf.CeilToInt(time / duration * overshootOrAmplitude);
			float num2 = duration / overshootOrAmplitude;
			time -= num2 * (float)(num - 1);
			float num3 = (float)((num % 2 != 0) ? 1 : (-1));
			if (num3 < 0f)
			{
				time -= num2;
			}
			time *= num3;
			float num4 = (time /= num2) * time;
			return Flash.WeightedEase(overshootOrAmplitude, period, num, num2, num3, num4);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00014E58 File Offset: 0x00013058
		public static float EaseOut(float time, float duration, float overshootOrAmplitude, float period)
		{
			int num = Mathf.CeilToInt(time / duration * overshootOrAmplitude);
			float num2 = duration / overshootOrAmplitude;
			time -= num2 * (float)(num - 1);
			float num3 = (float)((num % 2 != 0) ? 1 : (-1));
			if (num3 < 0f)
			{
				time -= num2;
			}
			time *= num3;
			float num4 = -(time /= num2) * (time - 2f);
			return Flash.WeightedEase(overshootOrAmplitude, period, num, num2, num3, num4);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00014EB8 File Offset: 0x000130B8
		public static float EaseInOut(float time, float duration, float overshootOrAmplitude, float period)
		{
			int num = Mathf.CeilToInt(time / duration * overshootOrAmplitude);
			float num2 = duration / overshootOrAmplitude;
			time -= num2 * (float)(num - 1);
			float num3 = (float)((num % 2 != 0) ? 1 : (-1));
			if (num3 < 0f)
			{
				time -= num2;
			}
			time *= num3;
			float num4 = (((time /= num2 * 0.5f) < 1f) ? (0.5f * time * time) : (-0.5f * ((time -= 1f) * (time - 2f) - 1f)));
			return Flash.WeightedEase(overshootOrAmplitude, period, num, num2, num3, num4);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014F44 File Offset: 0x00013144
		private static float WeightedEase(float overshootOrAmplitude, float period, int stepIndex, float stepDuration, float dir, float res)
		{
			float num = 0f;
			float num2 = 0f;
			if (dir > 0f && (int)overshootOrAmplitude % 2 == 0)
			{
				stepIndex++;
			}
			else if (dir < 0f && (int)overshootOrAmplitude % 2 != 0)
			{
				stepIndex++;
			}
			if (period > 0f)
			{
				float num3 = (float)Math.Truncate((double)overshootOrAmplitude);
				num2 = overshootOrAmplitude - num3;
				if (num3 % 2f > 0f)
				{
					num2 = 1f - num2;
				}
				num2 = num2 * (float)stepIndex / overshootOrAmplitude;
				num = res * (overshootOrAmplitude - (float)stepIndex) / overshootOrAmplitude;
			}
			else if (period < 0f)
			{
				period = -period;
				num = res * (float)stepIndex / overshootOrAmplitude;
			}
			float num4 = num - res;
			res += num4 * period + num2;
			if (res > 1f)
			{
				res = 1f;
			}
			return res;
		}
	}
}
