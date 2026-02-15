using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000ED RID: 237
	public class HeadingTracker
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x000223C8 File Offset: 0x000205C8
		public HeadingTracker(int filterSize)
		{
			this.mHistory = new HeadingTracker.Item[filterSize];
			float historyHalfLife = (float)filterSize / 5f;
			HeadingTracker.mDecayExponent = -Mathf.Log(2f) / historyHalfLife;
			this.ClearHistory();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00022413 File Offset: 0x00020613
		public int FilterSize
		{
			get
			{
				return this.mHistory.Length;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00022420 File Offset: 0x00020620
		private void ClearHistory()
		{
			this.mTop = (this.mBottom = (this.mCount = 0));
			this.mWeightSum = 0f;
			this.mHeadingSum = Vector3.zero;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002245C File Offset: 0x0002065C
		private static float Decay(float time)
		{
			return Mathf.Exp(time * HeadingTracker.mDecayExponent);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002246C File Offset: 0x0002066C
		public void Add(Vector3 velocity)
		{
			if (this.FilterSize == 0)
			{
				this.mLastGoodHeading = velocity;
				return;
			}
			float weight = velocity.magnitude;
			if (weight > 0.0001f)
			{
				HeadingTracker.Item item = default(HeadingTracker.Item);
				item.velocity = velocity;
				item.weight = weight;
				item.time = CinemachineCore.CurrentTime;
				if (this.mCount == this.FilterSize)
				{
					this.PopBottom();
				}
				this.mCount++;
				this.mHistory[this.mTop] = item;
				int num = this.mTop + 1;
				this.mTop = num;
				if (num == this.FilterSize)
				{
					this.mTop = 0;
				}
				this.mWeightSum *= HeadingTracker.Decay(item.time - this.mWeightTime);
				this.mWeightTime = item.time;
				this.mWeightSum += weight;
				this.mHeadingSum += item.velocity;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00022564 File Offset: 0x00020764
		private void PopBottom()
		{
			if (this.mCount > 0)
			{
				float currentTime = CinemachineCore.CurrentTime;
				HeadingTracker.Item item = this.mHistory[this.mBottom];
				int num = this.mBottom + 1;
				this.mBottom = num;
				if (num == this.FilterSize)
				{
					this.mBottom = 0;
				}
				this.mCount--;
				float decay = HeadingTracker.Decay(currentTime - item.time);
				this.mWeightSum -= item.weight * decay;
				this.mHeadingSum -= item.velocity * decay;
				if (this.mWeightSum <= 0.0001f || this.mCount == 0)
				{
					this.ClearHistory();
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002261C File Offset: 0x0002081C
		public void DecayHistory()
		{
			float time = CinemachineCore.CurrentTime;
			float decay = HeadingTracker.Decay(time - this.mWeightTime);
			this.mWeightSum *= decay;
			this.mWeightTime = time;
			if (this.mWeightSum < 0.0001f)
			{
				this.ClearHistory();
				return;
			}
			this.mHeadingSum *= decay;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00022678 File Offset: 0x00020878
		public Vector3 GetReliableHeading()
		{
			if (this.mWeightSum > 0.0001f && (this.mCount == this.mHistory.Length || this.mLastGoodHeading.AlmostZero()))
			{
				Vector3 h = this.mHeadingSum / this.mWeightSum;
				if (!h.AlmostZero())
				{
					this.mLastGoodHeading = h.normalized;
				}
			}
			return this.mLastGoodHeading;
		}

		// Token: 0x040004B4 RID: 1204
		private HeadingTracker.Item[] mHistory;

		// Token: 0x040004B5 RID: 1205
		private int mTop;

		// Token: 0x040004B6 RID: 1206
		private int mBottom;

		// Token: 0x040004B7 RID: 1207
		private int mCount;

		// Token: 0x040004B8 RID: 1208
		private Vector3 mHeadingSum;

		// Token: 0x040004B9 RID: 1209
		private float mWeightSum;

		// Token: 0x040004BA RID: 1210
		private float mWeightTime;

		// Token: 0x040004BB RID: 1211
		private Vector3 mLastGoodHeading = Vector3.zero;

		// Token: 0x040004BC RID: 1212
		private static float mDecayExponent;

		// Token: 0x020000EE RID: 238
		private struct Item
		{
			// Token: 0x040004BD RID: 1213
			public Vector3 velocity;

			// Token: 0x040004BE RID: 1214
			public float weight;

			// Token: 0x040004BF RID: 1215
			public float time;
		}
	}
}
