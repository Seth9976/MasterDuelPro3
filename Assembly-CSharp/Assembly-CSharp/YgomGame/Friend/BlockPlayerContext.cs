using System;

namespace YgomGame.Friend
{
	// Token: 0x02000C08 RID: 3080
	public class BlockPlayerContext : PlayerContextBase
	{
		// Token: 0x06005774 RID: 22388 RVA: 0x0000216D File Offset: 0x0000036D
		public new void Import(object blockPlayerData)
		{
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int CompareTo(IPlayerContext other)
		{
			return 0;
		}

		// Token: 0x04009440 RID: 37952
		private long m_BlockDateTs;
	}
}
