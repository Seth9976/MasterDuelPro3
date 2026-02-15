using System;

namespace YgomSystem.Utility
{
	// Token: 0x0200053C RID: 1340
	public static class RuntimeEnvironment
	{
		// Token: 0x06002ADB RID: 10971 RVA: 0x000029CC File Offset: 0x00000BCC
		public static RuntimeEnvironment.ServerType GetDefaultServerType()
		{
			return RuntimeEnvironment.ServerType.Product;
		}

		// Token: 0x040029DF RID: 10719
		public static RuntimeEnvironment.ServerType server;

		// Token: 0x040029E0 RID: 10720
		private static readonly string[] langs;

		// Token: 0x0200053D RID: 1341
		public enum ServerType
		{
			// Token: 0x040029E2 RID: 10722
			Product,
			// Token: 0x040029E3 RID: 10723
			Staging,
			// Token: 0x040029E4 RID: 10724
			QC1,
			// Token: 0x040029E5 RID: 10725
			Development,
			// Token: 0x040029E6 RID: 10726
			KonamiLan,
			// Token: 0x040029E7 RID: 10727
			Mock,
			// Token: 0x040029E8 RID: 10728
			Apple,
			// Token: 0x040029E9 RID: 10729
			Event,
			// Token: 0x040029EA RID: 10730
			QC2,
			// Token: 0x040029EB RID: 10731
			Mock1,
			// Token: 0x040029EC RID: 10732
			Mock2,
			// Token: 0x040029ED RID: 10733
			Mock3,
			// Token: 0x040029EE RID: 10734
			Dummy = 9999
		}
	}
}
