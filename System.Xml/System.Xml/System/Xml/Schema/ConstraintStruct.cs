using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000213 RID: 531
	internal sealed class ConstraintStruct
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00099B08 File Offset: 0x00097D08
		internal int TableDim
		{
			get
			{
				return this.tableDim;
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00099B10 File Offset: 0x00097D10
		internal ConstraintStruct(CompiledIdentityConstraint constraint)
		{
			this.constraint = constraint;
			this.tableDim = constraint.Fields.Length;
			this.axisFields = new ArrayList();
			this.axisSelector = new SelectorActiveAxis(constraint.Selector, this);
			if (this.constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref)
			{
				this.qualifiedTable = new Hashtable();
			}
		}

		// Token: 0x04000B40 RID: 2880
		internal CompiledIdentityConstraint constraint;

		// Token: 0x04000B41 RID: 2881
		internal SelectorActiveAxis axisSelector;

		// Token: 0x04000B42 RID: 2882
		internal ArrayList axisFields;

		// Token: 0x04000B43 RID: 2883
		internal Hashtable qualifiedTable;

		// Token: 0x04000B44 RID: 2884
		internal Hashtable keyrefTable;

		// Token: 0x04000B45 RID: 2885
		private int tableDim;
	}
}
