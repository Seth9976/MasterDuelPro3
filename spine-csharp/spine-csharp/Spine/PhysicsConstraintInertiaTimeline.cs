using System;

namespace Spine
{
	// Token: 0x0200002B RID: 43
	public class PhysicsConstraintInertiaTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000068B1 File Offset: 0x00004AB1
		public PhysicsConstraintInertiaTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintInertia)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000068BE File Offset: 0x00004ABE
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.inertia;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000068CB File Offset: 0x00004ACB
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.inertia;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000068D3 File Offset: 0x00004AD3
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.inertia = value;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000068DC File Offset: 0x00004ADC
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.inertiaGlobal;
		}
	}
}
