using System;

namespace YGOSharp
{
	// Token: 0x020001BA RID: 442
	public interface IGame
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000770 RID: 1904
		// (remove) Token: 0x06000771 RID: 1905
		event Action<object, EventArgs> OnNetworkReady;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000772 RID: 1906
		// (remove) Token: 0x06000773 RID: 1907
		event Action<object, EventArgs> OnNetworkEnd;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000774 RID: 1908
		// (remove) Token: 0x06000775 RID: 1909
		event Action<object, EventArgs> OnGameStart;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000776 RID: 1910
		// (remove) Token: 0x06000777 RID: 1911
		event Action<object, EventArgs> OnGameEnd;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000778 RID: 1912
		// (remove) Token: 0x06000779 RID: 1913
		event Action<object, EventArgs> OnDuelEnd;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600077A RID: 1914
		// (remove) Token: 0x0600077B RID: 1915
		event Action<object, PlayerEventArgs> OnPlayerJoin;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600077C RID: 1916
		// (remove) Token: 0x0600077D RID: 1917
		event Action<object, PlayerEventArgs> OnPlayerLeave;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600077E RID: 1918
		// (remove) Token: 0x0600077F RID: 1919
		event Action<object, PlayerMoveEventArgs> OnPlayerMove;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000780 RID: 1920
		// (remove) Token: 0x06000781 RID: 1921
		event Action<object, PlayerEventArgs> OnPlayerReady;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000782 RID: 1922
		// (remove) Token: 0x06000783 RID: 1923
		event Action<object, PlayerChatEventArgs> OnPlayerChat;
	}
}
