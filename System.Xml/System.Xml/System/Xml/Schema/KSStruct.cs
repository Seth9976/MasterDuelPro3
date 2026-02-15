using System;

namespace System.Xml.Schema
{
	// Token: 0x02000216 RID: 534
	internal class KSStruct
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x00099D47 File Offset: 0x00097F47
		public KSStruct(KeySequence ks, int dim)
		{
			this.ks = ks;
			this.fields = new LocatedActiveAxis[dim];
		}

		// Token: 0x04000B4C RID: 2892
		public int depth;

		// Token: 0x04000B4D RID: 2893
		public KeySequence ks;

		// Token: 0x04000B4E RID: 2894
		public LocatedActiveAxis[] fields;
	}
}
