using System;

namespace Spine
{
	// Token: 0x0200002F RID: 47
	public class PhysicsConstraintWindTimeline : PhysicsConstraintTimeline
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000698F File Offset: 0x00004B8F
		public PhysicsConstraintWindTimeline(int frameCount, int bezierCount, int physicsConstraintIndex)
			: base(frameCount, bezierCount, physicsConstraintIndex, Property.PhysicsConstraintWind)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000699C File Offset: 0x00004B9C
		protected override float Setup(PhysicsConstraint constraint)
		{
			return constraint.data.wind;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000069A9 File Offset: 0x00004BA9
		protected override float Get(PhysicsConstraint constraint)
		{
			return constraint.wind;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000069B1 File Offset: 0x00004BB1
		protected override void Set(PhysicsConstraint constraint, float value)
		{
			constraint.wind = value;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000069BA File Offset: 0x00004BBA
		protected override bool Global(PhysicsConstraintData constraint)
		{
			return constraint.windGlobal;
		}
	}
}
