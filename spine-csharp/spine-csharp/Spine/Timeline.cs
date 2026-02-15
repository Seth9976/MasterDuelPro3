using System;

namespace Spine
{
	// Token: 0x0200000B RID: 11
	public abstract class Timeline
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002B14 File Offset: 0x00000D14
		public Timeline(int frameCount, params string[] propertyIds)
		{
			if (propertyIds == null)
			{
				throw new ArgumentNullException("propertyIds", "propertyIds cannot be null.");
			}
			this.propertyIds = propertyIds;
			this.frames = new float[frameCount * this.FrameEntries];
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B49 File Offset: 0x00000D49
		public string[] PropertyIds
		{
			get
			{
				return this.propertyIds;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002B51 File Offset: 0x00000D51
		public float[] Frames
		{
			get
			{
				return this.frames;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002B59 File Offset: 0x00000D59
		public virtual int FrameEntries
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002B5C File Offset: 0x00000D5C
		public virtual int FrameCount
		{
			get
			{
				return this.frames.Length / this.FrameEntries;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002B6D File Offset: 0x00000D6D
		public float Duration
		{
			get
			{
				return this.frames[this.frames.Length - this.FrameEntries];
			}
		}

		// Token: 0x0600002F RID: 47
		public abstract void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> events, float alpha, MixBlend blend, MixDirection direction);

		// Token: 0x06000030 RID: 48 RVA: 0x00002B88 File Offset: 0x00000D88
		internal static int Search(float[] frames, float time)
		{
			int i = frames.Length;
			for (int j = 1; j < i; j++)
			{
				if (frames[j] > time)
				{
					return j - 1;
				}
			}
			return i - 1;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BB4 File Offset: 0x00000DB4
		internal static int Search(float[] frames, float time, int step)
		{
			int i = frames.Length;
			for (int j = step; j < i; j += step)
			{
				if (frames[j] > time)
				{
					return j - step;
				}
			}
			return i - step;
		}

		// Token: 0x04000046 RID: 70
		private readonly string[] propertyIds;

		// Token: 0x04000047 RID: 71
		internal readonly float[] frames;
	}
}
