using System;

namespace Spine
{
	// Token: 0x0200002A RID: 42
	public abstract class PhysicsConstraintTimeline : CurveTimeline1
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000067A4 File Offset: 0x000049A4
		public PhysicsConstraintTimeline(int frameCount, int bezierCount, int physicsConstraintIndex, Property property)
		{
			int num = (int)property;
			base..ctor(frameCount, bezierCount, num.ToString() + "|" + physicsConstraintIndex.ToString());
			this.constraintIndex = physicsConstraintIndex;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000067DB File Offset: 0x000049DB
		public int PhysicsConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000067E4 File Offset: 0x000049E4
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			PhysicsConstraint constraint;
			if (this.constraintIndex == -1)
			{
				float value = ((time >= this.frames[0]) ? base.GetCurveValue(time) : 0f);
				PhysicsConstraint[] constraints = skeleton.physicsConstraints.Items;
				int i = 0;
				int j = skeleton.physicsConstraints.Count;
				while (i < j)
				{
					constraint = constraints[i];
					if (constraint.active && this.Global(constraint.data))
					{
						this.Set(constraint, base.GetAbsoluteValue(time, alpha, blend, this.Get(constraint), this.Setup(constraint), value));
					}
					i++;
				}
				return;
			}
			constraint = skeleton.physicsConstraints.Items[this.constraintIndex];
			if (constraint.active)
			{
				this.Set(constraint, base.GetAbsoluteValue(time, alpha, blend, this.Get(constraint), this.Setup(constraint)));
			}
		}

		// Token: 0x060000B2 RID: 178
		protected abstract float Setup(PhysicsConstraint constraint);

		// Token: 0x060000B3 RID: 179
		protected abstract float Get(PhysicsConstraint constraint);

		// Token: 0x060000B4 RID: 180
		protected abstract void Set(PhysicsConstraint constraint, float value);

		// Token: 0x060000B5 RID: 181
		protected abstract bool Global(PhysicsConstraintData constraint);

		// Token: 0x0400009B RID: 155
		private readonly int constraintIndex;
	}
}
