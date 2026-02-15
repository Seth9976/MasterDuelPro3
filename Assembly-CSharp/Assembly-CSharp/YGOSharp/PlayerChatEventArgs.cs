using System;

namespace YGOSharp
{
	// Token: 0x020001BC RID: 444
	public class PlayerChatEventArgs : PlayerEventArgs
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0002506C File Offset: 0x0002326C
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00025074 File Offset: 0x00023274
		public string Message { get; private set; }

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002507D File Offset: 0x0002327D
		public PlayerChatEventArgs(Player player, string message)
			: base(player)
		{
			this.Message = message;
		}
	}
}
