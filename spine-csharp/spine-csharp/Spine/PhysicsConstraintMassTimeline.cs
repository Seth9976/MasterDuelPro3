using System;

namespace Spine
{
	// Token: 0x0200002E RID: 46
	public class PhysicsConstraintMassTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000694A File Offset: 0x00004B4A
		public PhysicsConstraintMassTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintMass)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006957 File Offset: 0x00004B57
		protected override float Setup(PhysicsConstraint constraint)
		{
			return 1f / constraint.data.massInverse;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000696A File Offset: 0x00004B6A
		protected override float Get(PhysicsConstraint constraint)
		{
			return 1f / constraint.massInverse;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006978 File Offset: 0x00004B78
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.massInverse = 1f / value;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006987 File Offset: 0x00004B87
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.massGlobal;
		}
	}
}
