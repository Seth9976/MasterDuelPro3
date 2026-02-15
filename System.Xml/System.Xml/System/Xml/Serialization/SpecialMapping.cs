using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000170 RID: 368
	internal class SpecialMapping : TypeMapping
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00054E59 File Offset: 0x00053059
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00054E61 File Offset: 0x00053061
		internal bool NamedAny
		{
			get
			{
				return this.namedAny;
			}
			set
			{
				this.namedAny = value;
			}
		}

		// Token: 0x04000873 RID: 2163
		private bool namedAny;
	}
}
