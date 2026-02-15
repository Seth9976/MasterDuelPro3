using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000058 RID: 88
	internal class Level2Map
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x04000166 RID: 358
		public byte Source;

		// Token: 0x04000167 RID: 359
		public byte Replace;
	}
}
