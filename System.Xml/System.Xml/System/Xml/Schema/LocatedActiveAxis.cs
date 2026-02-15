using System;

namespace System.Xml.Schema
{
	// Token: 0x02000214 RID: 532
	internal class LocatedActiveAxis : ActiveAxis
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00099B6E File Offset: 0x00097D6E
		internal int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00099B76 File Offset: 0x00097D76
		internal LocatedActiveAxis(Asttree astfield, KeySequence ks, int column)
			: base(astfield)
		{
			this.Ks = ks;
			this.column = column;
			this.isMatched = false;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00099B94 File Offset: 0x00097D94
		internal void Reactivate(KeySequence ks)
		{
			base.Reactivate();
			this.Ks = ks;
		}

		// Token: 0x04000B46 RID: 2886
		private int column;

		// Token: 0x04000B47 RID: 2887
		internal bool isMatched;

		// Token: 0x04000B48 RID: 2888
		internal KeySequence Ks;
	}
}
