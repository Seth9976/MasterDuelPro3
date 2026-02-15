using System;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010C6 RID: 4294
	public interface ISequenceBehaviour
	{
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06007F88 RID: 32648
		SequenceBehaviour.State state { get; }

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06007F89 RID: 32649
		string name { get; }

		// Token: 0x06007F8A RID: 32650
		void Begin();

		// Token: 0x06007F8B RID: 32651
		bool Update();

		// Token: 0x06007F8C RID: 32652
		void End();

		// Token: 0x06007F8D RID: 32653
		bool OnBack();
	}
}
