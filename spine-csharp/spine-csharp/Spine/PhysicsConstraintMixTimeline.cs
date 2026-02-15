using System;

namespace Spine
{
	// Token: 0x02000031 RID: 49
	public class PhysicsConstraintMixTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000069F5 File Offset: 0x00004BF5
		public PhysicsConstraintMixTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintMix)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006A02 File Offset: 0x00004C02
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.mix;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006A0F File Offset: 0x00004C0F
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.mix;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006A17 File Offset: 0x00004C17
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.mix = value;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006A20 File Offset: 0x00004C20
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.mixGlobal;
		}
	}
}
