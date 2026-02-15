using System;

namespace Spine
{
	// Token: 0x0200002D RID: 45
	public class PhysicsConstraintDampingTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00006917 File Offset: 0x00004B17
		public PhysicsConstraintDampingTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintDamping)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006924 File Offset: 0x00004B24
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.damping;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006931 File Offset: 0x00004B31
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.damping;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006939 File Offset: 0x00004B39
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.damping = value;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006942 File Offset: 0x00004B42
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.dampingGlobal;
		}
	}
}
