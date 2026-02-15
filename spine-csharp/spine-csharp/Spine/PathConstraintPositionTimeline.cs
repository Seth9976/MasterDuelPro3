using System;

namespace Spine
{
	// Token: 0x02000027 RID: 39
	public class PathConstraintPositionTimeline : CurveTimeline1
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000063F0 File Offset: 0x000045F0
		public PathConstraintPositionTimeline(int frameCount, int bezierCount, int pathConstraintIndex)
			: base(frameCount, bezierCount, 17.ToString() + "|" + pathConstraintIndex.ToString())
		{
			this.constraintIndex = pathConstraintIndex;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006427 File Offset: 0x00004627
		public int PathConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006430 File Offset: 0x00004630
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			PathConstraint constraint = skeleton.pathConstraints.Items[this.constraintIndex];
			if (constraint.active)
			{
				constraint.position = base.GetAbsoluteValue(time, alpha, blend, constraint.position, constraint.data.position);
			}
		}

		// Token: 0x04000094 RID: 148
		private readonly int constraintIndex;
	}
}
