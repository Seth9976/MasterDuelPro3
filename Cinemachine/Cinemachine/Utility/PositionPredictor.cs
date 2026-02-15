using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000EB RID: 235
	public class PositionPredictor
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00022200 File Offset: 0x00020400
		public bool IsEmpty()
		{
			return !this.m_HavePos;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002220B File Offset: 0x0002040B
		public void ApplyTransformDelta(Vector3 positionDelta)
		{
			this.m_Pos += positionDelta;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0002221F File Offset: 0x0002041F
		public void Reset()
		{
			this.m_HavePos = false;
			this.m_SmoothDampVelocity = Vector3.zero;
			this.m_Velocity = Vector3.zero;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00022240 File Offset: 0x00020440
		public void AddPosition(Vector3 pos, float deltaTime, float lookaheadTime)
		{
			if (deltaTime < 0f)
			{
				this.Reset();
			}
			if (this.m_HavePos && deltaTime > 0.0001f)
			{
				Vector3 vel = (pos - this.m_Pos) / deltaTime;
				bool slowing = vel.sqrMagnitude < this.m_Velocity.sqrMagnitude;
				this.m_Velocity = Vector3.SmoothDamp(this.m_Velocity, vel, ref this.m_SmoothDampVelocity, this.Smoothing / (float)(slowing ? 30 : 10), float.PositiveInfinity, deltaTime);
			}
			this.m_Pos = pos;
			this.m_HavePos = true;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000222D0 File Offset: 0x000204D0
		public Vector3 PredictPositionDelta(float lookaheadTime)
		{
			return this.m_Velocity * lookaheadTime;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000222DE File Offset: 0x000204DE
		public Vector3 PredictPosition(float lookaheadTime)
		{
			return this.m_Pos + this.PredictPositionDelta(lookaheadTime);
		}

		// Token: 0x040004AC RID: 1196
		private Vector3 m_Velocity;

		// Token: 0x040004AD RID: 1197
		private Vector3 m_SmoothDampVelocity;

		// Token: 0x040004AE RID: 1198
		private Vector3 m_Pos;

		// Token: 0x040004AF RID: 1199
		private bool m_HavePos;

		// Token: 0x040004B0 RID: 1200
		public float Smoothing;
	}
}
