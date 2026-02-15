using System;
using YgomGame.Card;
using YgomGame.CardPack.Open.Actor;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D2 RID: 4306
	public class SequenceBehaviourPackOpenShakeUpgrade : SequenceBehaviour
	{
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06007FDB RID: 32731 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourPackOpenShakeUpgrade(SequenceBehaviourWork sequenceBehaviourWork, string packImagePath, CardCollectionInfo.Rarity fromPackType, CardCollectionInfo.Rarity dstPackType)
			: base(null)
		{
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnd()
		{
		}

		// Token: 0x0400B862 RID: 47202
		private readonly CardCollectionInfo.Rarity m_FromPackType;

		// Token: 0x0400B863 RID: 47203
		private readonly CardCollectionInfo.Rarity m_DstPackType;

		// Token: 0x0400B864 RID: 47204
		private string m_PackImagePath;

		// Token: 0x0400B865 RID: 47205
		private CardPackPackActor m_BeforePackActor;

		// Token: 0x0400B866 RID: 47206
		private CardPackPackActor m_AfterActor;
	}
}
