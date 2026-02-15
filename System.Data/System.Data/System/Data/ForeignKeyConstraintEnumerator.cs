using System;

namespace System.Data
{
	// Token: 0x02000031 RID: 49
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x06000382 RID: 898 RVA: 0x00013472 File Offset: 0x00011672
		public ForeignKeyConstraintEnumerator(DataSet dataSet)
			: base(dataSet)
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001347B File Offset: 0x0001167B
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00013486 File Offset: 0x00011686
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
