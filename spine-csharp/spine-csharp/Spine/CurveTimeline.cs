using System;

namespace Spine
{
	// Token: 0x0200000E RID: 14
	public abstract class CurveTimeline : Timeline
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002BDE File Offset: 0x00000DDE
		public CurveTimeline(int frameCount, int bezierCount, params string[] propertyIds)
			: base(frameCount, propertyIds)
		{
			this.curves = new float[frameCount + bezierCount * 18];
			this.curves[frameCount - 1] = 1f;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C08 File Offset: 0x00000E08
		public void SetLinear(int frame)
		{
			this.curves[frame] = 0f;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C17 File Offset: 0x00000E17
		public void SetStepped(int frame)
		{
			this.curves[frame] = 1f;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C26 File Offset: 0x00000E26
		public float GetCurveType(int frame)
		{
			return (float)((int)this.curves[frame]);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C34 File Offset: 0x00000E34
		public void Shrink(int bezierCount)
		{
			int size = this.FrameCount + bezierCount * 18;
			if (this.curves.Length > size)
			{
				float[] newCurves = new float[size];
				Array.Copy(this.curves, 0, newCurves, 0, size);
				this.curves = newCurves;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C78 File Offset: 0x00000E78
		public void SetBezier(int bezier, int frame, int value, float time1, float value1, float cx1, float cy1, float cx2, float cy2, float time2, float value2)
		{
			float[] curves = this.curves;
			int i = this.FrameCount + bezier * 18;
			if (value == 0)
			{
				curves[frame] = (float)(2 + i);
			}
			float tmpx = (time1 - cx1 * 2f + cx2) * 0.03f;
			float tmpy = (value1 - cy1 * 2f + cy2) * 0.03f;
			float dddx = ((cx1 - cx2) * 3f - time1 + time2) * 0.006f;
			float dddy = ((cy1 - cy2) * 3f - value1 + value2) * 0.006f;
			float ddx = tmpx * 2f + dddx;
			float ddy = tmpy * 2f + dddy;
			float dx = (cx1 - time1) * 0.3f + tmpx + dddx * 0.16666667f;
			float dy = (cy1 - value1) * 0.3f + tmpy + dddy * 0.16666667f;
			float x = time1 + dx;
			float y = value1 + dy;
			int j = i + 18;
			while (i < j)
			{
				curves[i] = x;
				curves[i + 1] = y;
				dx += ddx;
				dy += ddy;
				ddx += dddx;
				ddy += dddy;
				x += dx;
				y += dy;
				i += 2;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D9C File Offset: 0x00000F9C
		public float GetBezierValue(float time, int frameIndex, int valueOffset, int i)
		{
			float[] curves = this.curves;
			if (curves[i] > time)
			{
				float x = this.frames[frameIndex];
				float y = this.frames[frameIndex + valueOffset];
				return y + (time - x) / (curves[i] - x) * (curves[i + 1] - y);
			}
			int j = i + 18;
			for (i += 2; i < j; i += 2)
			{
				if (curves[i] >= time)
				{
					float x2 = curves[i - 2];
					float y2 = curves[i - 1];
					return y2 + (time - x2) / (curves[i] - x2) * (curves[i + 1] - y2);
				}
			}
			frameIndex += this.FrameEntries;
			float x3 = curves[j - 2];
			float y3 = curves[j - 1];
			return y3 + (time - x3) / (this.frames[frameIndex] - x3) * (this.frames[frameIndex + valueOffset] - y3);
		}

		// Token: 0x04000048 RID: 72
		public const int LINEAR = 0;

		// Token: 0x04000049 RID: 73
		public const int STEPPED = 1;

		// Token: 0x0400004A RID: 74
		public const int BEZIER = 2;

		// Token: 0x0400004B RID: 75
		public const int BEZIER_SIZE = 18;

		// Token: 0x0400004C RID: 76
		internal float[] curves;
	}
}
