using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000147 RID: 327
	internal class ArgBuilder
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x000509E9 File Offset: 0x0004EBE9
		internal ArgBuilder(string name, int index, Type argType)
		{
			this.Name = name;
			this.Index = index;
			this.ArgType = argType;
		}

		// Token: 0x040007E9 RID: 2025
		internal string Name;

		// Token: 0x040007EA RID: 2026
		internal int Index;

		// Token: 0x040007EB RID: 2027
		internal Type ArgType;
	}
}
