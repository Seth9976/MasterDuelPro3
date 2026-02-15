using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000030 RID: 48
	internal class ConstraintEnumerator
	{
		// Token: 0x0600037D RID: 893 RVA: 0x000133AD File Offset: 0x000115AD
		public ConstraintEnumerator(DataSet dataSet)
		{
			this._tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this._currentObject = null;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000133D4 File Offset: 0x000115D4
		public bool GetNext()
		{
			this._currentObject = null;
			while (this._tables != null)
			{
				if (this._constraints == null)
				{
					if (!this._tables.MoveNext())
					{
						this._tables = null;
						return false;
					}
					this._constraints = ((DataTable)this._tables.Current).Constraints.GetEnumerator();
				}
				if (!this._constraints.MoveNext())
				{
					this._constraints = null;
				}
				else
				{
					Constraint constraint = (Constraint)this._constraints.Current;
					if (this.IsValidCandidate(constraint))
					{
						this._currentObject = constraint;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001346A File Offset: 0x0001166A
		public Constraint GetConstraint()
		{
			return this._currentObject;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000593A File Offset: 0x00003B3A
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001346A File Offset: 0x0001166A
		protected Constraint CurrentObject
		{
			get
			{
				return this._currentObject;
			}
		}

		// Token: 0x04000111 RID: 273
		private IEnumerator _tables;

		// Token: 0x04000112 RID: 274
		private IEnumerator _constraints;

		// Token: 0x04000113 RID: 275
		private Constraint _currentObject;
	}
}
