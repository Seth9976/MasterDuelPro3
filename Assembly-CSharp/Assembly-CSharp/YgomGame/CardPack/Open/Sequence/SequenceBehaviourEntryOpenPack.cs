using System;
using YgomGame.CardPack.Open.Actor;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010CF RID: 4303
	public class SequenceBehaviourEntryOpenPack : SequenceBehaviour
	{
		// Token: 0x06007FCF RID: 32719 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourEntryOpenPack(SequenceBehaviourWork sequenceBehaviourWork, DrawPackData packData)
			: base(null)
		{
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnd()
		{
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B856 RID: 47190
		private readonly string k_LabelTrack;

		// Token: 0x0400B857 RID: 47191
		private readonly string k_CardPopLabelFormat;

		// Token: 0x0400B858 RID: 47192
		private readonly string k_CardPopFinishLabel;

		// Token: 0x0400B859 RID: 47193
		private readonly DrawPackData m_PackData;

		// Token: 0x0400B85A RID: 47194
		private CardPackPackActor m_BeforePackActor;

		// Token: 0x0400B85B RID: 47195
		private CardPackPackActor m_AfterPackActor;

		// Token: 0x0400B85C RID: 47196
		private double m_CardPopEndTime;

		// Token: 0x0400B85D RID: 47197
		private double m_CardPopFinishStartTime;

		// Token: 0x0400B85E RID: 47198
		private int m_TMStep;
	}
}
