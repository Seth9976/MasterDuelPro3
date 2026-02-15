using System;

namespace DG.Tweening.Core.Easing
{
	// Token: 0x020000C1 RID: 193
	public static class Bounce
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00013924 File Offset: 0x00011B24
		public static float EaseIn(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			return 1f - Bounce.EaseOut(duration - time, duration, -1f, -1f);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013940 File Offset: 0x00011B40
		public static float EaseOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			if ((time /= duration) < 0.36363637f)
			{
				return 7.5625f * time * time;
			}
			if (time < 0.72727275f)
			{
				return 7.5625f * (time -= 0.54545456f) * time + 0.75f;
			}
			if (time < 0.90909094f)
			{
				return 7.5625f * (time -= 0.8181818f) * time + 0.9375f;
			}
			return 7.5625f * (time -= 0.95454544f) * time + 0.984375f;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000139C0 File Offset: 0x00011BC0
		public static float EaseInOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			if (time < duration * 0.5f)
			{
				return Bounce.EaseIn(time * 2f, duration, -1f, -1f) * 0.5f;
			}
			return Bounce.EaseOut(time * 2f - duration, duration, -1f, -1f) * 0.5f + 0.5f;
		}
	}
}
