using System;

namespace YGOSharp
{
	// Token: 0x020001AD RID: 429
	public abstract class AddonBase
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001F9EE File Offset: 0x0001DBEE
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001F9F6 File Offset: 0x0001DBF6
		public Game Game { get; private set; }

		// Token: 0x06000660 RID: 1632 RVA: 0x0001F9FF File Offset: 0x0001DBFF
		protected AddonBase(Game game)
		{
			this.Game = game;
		}
	}
}
