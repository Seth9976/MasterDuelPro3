using System;

namespace System.Data
{
	// Token: 0x02000033 RID: 51
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000387 RID: 903 RVA: 0x000134C2 File Offset: 0x000116C2
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000134D2 File Offset: 0x000116D2
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this._table;
		}

		// Token: 0x04000115 RID: 277
		private readonly DataTable _table;
	}
}
