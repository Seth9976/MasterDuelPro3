using System;

namespace System.Data
{
	// Token: 0x02000032 RID: 50
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00013493 File Offset: 0x00011693
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000134A3 File Offset: 0x000116A3
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this._table;
		}

		// Token: 0x04000114 RID: 276
		private readonly DataTable _table;
	}
}
