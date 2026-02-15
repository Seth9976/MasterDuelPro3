using System;

namespace Spine
{
	// Token: 0x02000032 RID: 50
	public class PhysicsConstraintResetTimeline : Timeline
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00006A28 File Offset: 0x00004C28
		public PhysicsConstraintResetTimeline(int frameCount, int physicsConstraintIndex)
			: base(frameCount, PhysicsConstraintResetTimeline.propertyIds)
		{
			this.constraintIndex = physicsConstraintIndex;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006A3D File Offset: 0x00004C3D
		public int PhysicsConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006A45 File Offset: 0x00004C45
		public override int FrameCount
		{
			get
			{
				return this.frames.Length;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006A4F File Offset: 0x00004C4F
		public void SetFrame(int frame, float time)
		{
			this.frames[frame] = time;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006A5C File Offset: 0x00004C5C
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			PhysicsConstraint constraint = null;
			if (this.constraintIndex != -1)
			{
				constraint = skeleton.physicsConstraints.Items[this.constraintIndex];
				if (!constraint.active)
				{
					return;
				}
			}
			float[] frames = this.frames;
			if (lastTime > time)
			{
				this.Apply(skeleton, lastTime, 2.1474836E+09f, null, alpha, blend, direction);
				lastTime = -1f;
			}
			else if (lastTime >= frames[frames.Length - 1])
			{
				return;
			}
			if (time < frames[0])
			{
				return;
			}
			if (lastTime < frames[0] || time >= frames[Timeline.Search(frames, lastTime) + 1])
			{
				if (constraint != null)
				{
					constraint.Reset();
					return;
				}
				PhysicsConstraint[] constraints = skeleton.physicsConstraints.Items;
				int i = 0;
				int j = skeleton.physicsConstraints.Count;
				while (i < j)
				{
					constraint = constraints[i];
					if (constraint.active)
					{
						constraint.Reset();
					}
					i++;
				}
			}
		}

		// Token: 0x0400009C RID: 156
		private static readonly string[] propertyIds = new string[] { 27.ToString() };

		// Token: 0x0400009D RID: 157
		private readonly int constraintIndex;
	}
}
