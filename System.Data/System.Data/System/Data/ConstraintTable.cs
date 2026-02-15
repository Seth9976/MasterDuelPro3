using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000AA RID: 170
	internal sealed class ConstraintTable
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x0002A574 File Offset: 0x00028774
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x04000346 RID: 838
		public DataTable table;

		// Token: 0x04000347 RID: 839
		public XmlSchemaIdentityConstraint constraint;
	}
}
