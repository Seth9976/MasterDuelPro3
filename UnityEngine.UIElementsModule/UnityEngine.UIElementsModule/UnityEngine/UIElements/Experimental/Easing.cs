using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005CD RID: 1485
	public static class Easing
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x000A67C0 File Offset: 0x000A49C0
		public static float Linear(float t)
		{
			return t;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000A67D4 File Offset: 0x000A49D4
		public static float InSine(float t)
		{
			return Mathf.Sin(1.5707964f * (t - 1f)) + 1f;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000A6800 File Offset: 0x000A4A00
		public static float OutSine(float t)
		{
			return Mathf.Sin(t * 1.5707964f);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000A6820 File Offset: 0x000A4A20
		public static float InOutSine(float t)
		{
			return (Mathf.Sin(3.1415927f * (t - 0.5f)) + 1f) * 0.5f;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000A6850 File Offset: 0x000A4A50
		public static float InQuad(float t)
		{
			return t * t;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000A6868 File Offset: 0x000A4A68
		public static float OutQuad(float t)
		{
			return t * (2f - t);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000A6884 File Offset: 0x000A4A84
		public static float InOutQuad(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = t * t * 0.5f;
			}
			else
			{
				num = -0.5f * ((t - 1f) * (t - 3f) - 1f);
			}
			return num;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000A68D4 File Offset: 0x000A4AD4
		public static float InCubic(float t)
		{
			return Easing.InPower(t, 3);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000A68F0 File Offset: 0x000A4AF0
		public static float OutCubic(float t)
		{
			return Easing.OutPower(t, 3);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000A690C File Offset: 0x000A4B0C
		public static float InOutCubic(float t)
		{
			return Easing.InOutPower(t, 3);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000A6928 File Offset: 0x000A4B28
		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, (float)power);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000A6944 File Offset: 0x000A4B44
		public static float OutPower(float t, int power)
		{
			int sign = ((power % 2 == 0) ? (-1) : 1);
			return (float)sign * (Mathf.Pow(t - 1f, (float)power) + (float)sign);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000A6978 File Offset: 0x000A4B78
		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = Easing.InPower(t, power) * 0.5f;
			}
			else
			{
				int sign = ((power % 2 == 0) ? (-1) : 1);
				num = (float)sign * 0.5f * (Mathf.Pow(t - 2f, (float)power) + (float)(sign * 2));
			}
			return num;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000A69D8 File Offset: 0x000A4BD8
		public static float InBounce(float t)
		{
			return 1f - Easing.OutBounce(1f - t);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000A69FC File Offset: 0x000A4BFC
		public static float OutBounce(float t)
		{
			bool flag = t < 0.36363637f;
			float num;
			if (flag)
			{
				num = 7.5625f * t * t;
			}
			else
			{
				bool flag2 = t < 0.72727275f;
				if (flag2)
				{
					float postFix;
					t = (postFix = t - 0.54545456f);
					num = 7.5625f * postFix * t + 0.75f;
				}
				else
				{
					bool flag3 = t < 0.90909094f;
					if (flag3)
					{
						float postFix2;
						t = (postFix2 = t - 0.8181818f);
						num = 7.5625f * postFix2 * t + 0.9375f;
					}
					else
					{
						float postFix3;
						t = (postFix3 = t - 0.95454544f);
						num = 7.5625f * postFix3 * t + 0.984375f;
					}
				}
			}
			return num;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000A6A9C File Offset: 0x000A4C9C
		public static float InOutBounce(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBounce(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		public static float InElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float p = 0.3f;
					float s = p / 4f;
					float power = Mathf.Pow(2f, 10f * (t -= 1f));
					num = -(power * Mathf.Sin((t - s) * 6.2831855f / p));
				}
			}
			return num;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000A6B6C File Offset: 0x000A4D6C
		public static float OutElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float p = 0.3f;
					float s = p / 4f;
					num = Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * 6.2831855f / p) + 1f;
				}
			}
			return num;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000A6BE0 File Offset: 0x000A4DE0
		public static float InOutElastic(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InElastic(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000A6C34 File Offset: 0x000A4E34
		public static float InBack(float t)
		{
			float s = 1.70158f;
			return t * t * ((s + 1f) * t - s);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000A6C5C File Offset: 0x000A4E5C
		public static float OutBack(float t)
		{
			return 1f - Easing.InBack(1f - t);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000A6C80 File Offset: 0x000A4E80
		public static float InOutBack(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBack(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000A6CD4 File Offset: 0x000A4ED4
		public static float InCirc(float t)
		{
			return -(Mathf.Sqrt(1f - t * t) - 1f);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000A6CFC File Offset: 0x000A4EFC
		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000A6D28 File Offset: 0x000A4F28
		public static float InOutCirc(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}
			else
			{
				t -= 2f;
				num = 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
			}
			return num;
		}
	}
}
