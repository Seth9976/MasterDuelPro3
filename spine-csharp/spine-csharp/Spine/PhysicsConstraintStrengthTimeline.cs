using System;

namespace Spine
{
	// Token: 0x0200002C RID: 44
	public class PhysicsConstraintStrengthTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000068E4 File Offset: 0x00004AE4
		public PhysicsConstraintStrengthTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintStrength)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000068F1 File Offset: 0x00004AF1
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.strength;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000068FE File Offset: 0x00004AFE
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.strength;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006906 File Offset: 0x00004B06
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.strength = value;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000690F File Offset: 0x00004B0F
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.strengthGlobal;
		}
	}
}
