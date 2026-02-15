using System;

namespace YGOSharp
{
	// Token: 0x020001BD RID: 445
	public class PlayerEventArgs : EventArgs
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0002508D File Offset: 0x0002328D
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00025095 File Offset: 0x00023295
		public Player Player { get; private set; }

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002509E File Offset: 0x0002329E
		public PlayerEventArgs(Player player)
		{
			this.Player = player;
		}
	}
}
