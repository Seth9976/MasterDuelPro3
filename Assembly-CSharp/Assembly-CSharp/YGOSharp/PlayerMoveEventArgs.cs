using System;

namespace YGOSharp
{
	// Token: 0x020001BE RID: 446
	public class PlayerMoveEventArgs : PlayerEventArgs
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000250AD File Offset: 0x000232AD
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x000250B5 File Offset: 0x000232B5
		public int FromType { get; private set; }

		// Token: 0x060007A8 RID: 1960 RVA: 0x000250BE File Offset: 0x000232BE
		public PlayerMoveEventArgs(Player player, int fromType)
			: base(player)
		{
			this.FromType = fromType;
		}
	}
}
