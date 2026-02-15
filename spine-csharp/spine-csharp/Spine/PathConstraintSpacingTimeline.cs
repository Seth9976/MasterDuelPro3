using System;

namespace Spine
{
	// Token: 0x02000028 RID: 40
	public class PathConstraintSpacingTimeline : CurveTimeline1
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000647C File Offset: 0x0000467C
		public PathConstraintSpacingTimeline(int frameCount, int bezierCount, int pathConstraintIndex)
			: base(frameCount, bezierCount, 18.ToString() + "|" + pathConstraintIndex.ToString())
		{
			this.constraintIndex = pathConstraintIndex;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000064B3 File Offset: 0x000046B3
		public int PathConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000064BC File Offset: 0x000046BC
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> events, float alpha, MixBlend blend, MixDirection direction)
		{
			PathConstraint constraint = skeleton.pathConstraints.Items[this.constraintIndex];
			if (constraint.active)
			{
				constraint.spacing = base.GetAbsoluteValue(time, alpha, blend, constraint.spacing, constraint.data.spacing);
			}
		}

		// Token: 0x04000095 RID: 149
		private readonly int constraintIndex;
	}
}
