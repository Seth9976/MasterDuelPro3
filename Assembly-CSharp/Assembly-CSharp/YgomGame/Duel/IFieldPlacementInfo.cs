using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EA1 RID: 3745
	public interface IFieldPlacementInfo
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06006D35 RID: 27957
		int numMonsterPlaces { get; }

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06006D36 RID: 27958
		int numMagicPlaces { get; }

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06006D37 RID: 27959
		int monsterStartIdx { get; }

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06006D38 RID: 27960
		int monsterEndIdx { get; }

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06006D39 RID: 27961
		int magicStartIdx { get; }

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06006D3A RID: 27962
		int magicEndIdx { get; }
	}
}
