using System;

namespace Spine
{
	// Token: 0x02000030 RID: 48
	public class PhysicsConstraintGravityTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000069C2 File Offset: 0x00004BC2
		public PhysicsConstraintGravityTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintGravity)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000069CF File Offset: 0x00004BCF
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.gravity;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000069DC File Offset: 0x00004BDC
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.gravity;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000069E4 File Offset: 0x00004BE4
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.gravity = value;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000069ED File Offset: 0x00004BED
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.gravityGlobal;
		}
	}
}
