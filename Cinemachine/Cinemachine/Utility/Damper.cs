using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000EC RID: 236
	public static class Damper
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x000222F2 File Offset: 0x000204F2
		private static float DecayConstant(float time, float residual)
		{
			return Mathf.Log(1f / residual) / time;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00022302 File Offset: 0x00020502
		private static float DecayedRemainder(float initial, float decayConstant, float deltaTime)
		{
			return initial / Mathf.Exp(decayConstant * deltaTime);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00022310 File Offset: 0x00020510
		public static float Damp(float initial, float dampTime, float deltaTime)
		{
			if (dampTime < 0.0001f || Mathf.Abs(initial) < 0.0001f)
			{
				return initial;
			}
			if (deltaTime < 0.0001f)
			{
				return 0f;
			}
			float i = 4.6051702f / dampTime;
			return initial * (1f - Mathf.Exp(-i * deltaTime));
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002235C File Offset: 0x0002055C
		public static Vector3 Damp(Vector3 initial, Vector3 dampTime, float deltaTime)
		{
			for (int i = 0; i < 3; i++)
			{
				initial[i] = Damper.Damp(initial[i], dampTime[i], deltaTime);
			}
			return initial;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00022394 File Offset: 0x00020594
		public static Vector3 Damp(Vector3 initial, float dampTime, float deltaTime)
		{
			for (int i = 0; i < 3; i++)
			{
				initial[i] = Damper.Damp(initial[i], dampTime, deltaTime);
			}
			return initial;
		}

		// Token: 0x040004B1 RID: 1201
		private const float Epsilon = 0.0001f;

		// Token: 0x040004B2 RID: 1202
		public const float kNegligibleResidual = 0.01f;

		// Token: 0x040004B3 RID: 1203
		private const float kLogNegligibleResidual = -4.6051702f;
	}
}
