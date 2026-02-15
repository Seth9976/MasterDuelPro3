using System;

namespace WindBot
{
	// Token: 0x020001EC RID: 492
	public class WindBotInfo
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000286DE File Offset: 0x000268DE
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000286E6 File Offset: 0x000268E6
		public string Name { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000286EF File Offset: 0x000268EF
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000286F7 File Offset: 0x000268F7
		public string Deck { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00028700 File Offset: 0x00026900
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00028708 File Offset: 0x00026908
		public string DeckFile { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00028711 File Offset: 0x00026911
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00028719 File Offset: 0x00026919
		public string Dialog { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00028722 File Offset: 0x00026922
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0002872A File Offset: 0x0002692A
		public string Host { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00028733 File Offset: 0x00026933
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0002873B File Offset: 0x0002693B
		public int Port { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00028744 File Offset: 0x00026944
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0002874C File Offset: 0x0002694C
		public string HostInfo { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00028755 File Offset: 0x00026955
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0002875D File Offset: 0x0002695D
		public int Version { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00028766 File Offset: 0x00026966
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0002876E File Offset: 0x0002696E
		public int Hand { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00028777 File Offset: 0x00026977
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0002877F File Offset: 0x0002697F
		public bool Debug { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00028788 File Offset: 0x00026988
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00028790 File Offset: 0x00026990
		public bool Chat { get; set; }

		// Token: 0x060008BC RID: 2236 RVA: 0x0002879C File Offset: 0x0002699C
		public WindBotInfo()
		{
			this.Name = "WindBot";
			this.Deck = null;
			this.DeckFile = null;
			this.Dialog = "default";
			this.Host = "127.0.0.1";
			this.Port = 7911;
			this.HostInfo = "";
			this.Version = 4962;
			this.Hand = 0;
			this.Debug = false;
			this.Chat = true;
		}
	}
}
