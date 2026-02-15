using System;

namespace Spine
{
	// Token: 0x0200000F RID: 15
	public abstract class CurveTimeline1 : CurveTimeline
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002E64 File Offset: 0x00001064
		public CurveTimeline1(int frameCount, int bezierCount, string propertyId)
			: base(frameCount, bezierCount, new string[] { propertyId })
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E78 File Offset: 0x00001078
		public override int FrameEntries
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E7B File Offset: 0x0000107B
		public void SetFrame(int frame, float time, float value)
		{
			frame <<= 1;
			this.frames[frame] = time;
			this.frames[frame + 1] = value;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E98 File Offset: 0x00001098
		public float GetCurveValue(float time)
		{
			float[] frames = this.frames;
			int i = frames.Length - 2;
			for (int ii = 2; ii <= i; ii += 2)
			{
				if (frames[ii] > time)
				{
					i = ii - 2;
					break;
				}
			}
			int curveType = (int)this.curves[i >> 1];
			if (curveType == 0)
			{
				float before = frames[i];
				float value = frames[i + 1];
				return value + (time - before) / (frames[i + 2] - before) * (frames[i + 2 + 1] - value);
			}
			if (curveType != 1)
			{
				return base.GetBezierValue(time, i, 1, curveType - 2);
			}
			return frames[i + 1];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F1C File Offset: 0x0000111C
		public float GetRelativeValue(float time, float alpha, MixBlend blend, float current, float setup)
		{
			if (time < this.frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					return setup;
				}
				if (blend != MixBlend.First)
				{
					return current;
				}
				return current + (setup - current) * alpha;
			}
			else
			{
				float value = this.GetCurveValue(time);
				if (blend != MixBlend.Setup)
				{
					if (blend - MixBlend.First <= 1)
					{
						value += setup - current;
					}
					return current + value * alpha;
				}
				return setup + value * alpha;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F78 File Offset: 0x00001178
		public float GetAbsoluteValue(float time, float alpha, MixBlend blend, float current, float setup)
		{
			if (time < this.frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					return setup;
				}
				if (blend != MixBlend.First)
				{
					return current;
				}
				return current + (setup - current) * alpha;
			}
			else
			{
				float value = this.GetCurveValue(time);
				if (blend == MixBlend.Setup)
				{
					return setup + (value - setup) * alpha;
				}
				return current + (value - current) * alpha;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FC8 File Offset: 0x000011C8
		public float GetAbsoluteValue(float time, float alpha, MixBlend blend, float current, float setup, float value)
		{
			if (time < this.frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					return setup;
				}
				if (blend != MixBlend.First)
				{
					return current;
				}
				return current + (setup - current) * alpha;
			}
			else
			{
				if (blend == MixBlend.Setup)
				{
					return setup + (value - setup) * alpha;
				}
				return current + (value - current) * alpha;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003008 File Offset: 0x00001208
		public float GetScaleValue(float time, float alpha, MixBlend blend, MixDirection direction, float current, float setup)
		{
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					return setup;
				}
				if (blend != MixBlend.First)
				{
					return current;
				}
				return current + (setup - current) * alpha;
			}
			else
			{
				float value = this.GetCurveValue(time) * setup;
				if (alpha != 1f)
				{
					if (direction == MixDirection.Out)
					{
						if (blend == MixBlend.Setup)
						{
							return setup + (Math.Abs(value) * (float)Math.Sign(setup) - setup) * alpha;
						}
						if (blend - MixBlend.First <= 1)
						{
							return current + (Math.Abs(value) * (float)Math.Sign(current) - current) * alpha;
						}
					}
					else
					{
						if (blend == MixBlend.Setup)
						{
							float s = Math.Abs(setup) * (float)Math.Sign(value);
							return s + (value - s) * alpha;
						}
						if (blend - MixBlend.First <= 1)
						{
							float s = Math.Abs(current) * (float)Math.Sign(value);
							return s + (value - s) * alpha;
						}
					}
					return current + (value - setup) * alpha;
				}
				if (blend == MixBlend.Add)
				{
					return current + value - setup;
				}
				return value;
			}
		}

		// Token: 0x0400004D RID: 77
		public const int ENTRIES = 2;

		// Token: 0x0400004E RID: 78
		internal const int VALUE = 1;
	}
}
