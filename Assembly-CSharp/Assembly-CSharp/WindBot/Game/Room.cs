using System;

namespace WindBot.Game
{
	// Token: 0x02000204 RID: 516
	public class Room
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00030B1C File Offset: 0x0002ED1C
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00030B24 File Offset: 0x0002ED24
		public bool IsHost { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00030B2D File Offset: 0x0002ED2D
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00030B35 File Offset: 0x0002ED35
		public string[] Names { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00030B3E File Offset: 0x0002ED3E
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00030B46 File Offset: 0x0002ED46
		public bool[] IsReady { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00030B4F File Offset: 0x0002ED4F
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00030B57 File Offset: 0x0002ED57
		public int Position { get; set; }

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00030B60 File Offset: 0x0002ED60
		public Room()
		{
			this.Names = new string[8];
			this.IsReady = new bool[8];
			this.Position = -1;
		}
	}
}
