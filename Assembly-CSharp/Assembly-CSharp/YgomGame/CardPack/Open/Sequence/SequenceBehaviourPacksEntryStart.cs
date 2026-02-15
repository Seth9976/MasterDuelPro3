using System;
using System.Collections.Generic;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D4 RID: 4308
	public class SequenceBehaviourPacksEntryStart : SequenceBehaviour
	{
		// Token: 0x06007FE4 RID: 32740 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourPacksEntryStart(SequenceBehaviourWork sequenceBehaviourWork, bool isBegin, int packTotal, int packIdx, List<DrawPackData> drawPackDatas, int smokeType, bool isPickup, int labelType, string labelText)
			: base(null)
		{
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnd()
		{
		}

		// Token: 0x06007FE8 RID: 32744 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B86A RID: 47210
		private readonly bool m_isBegin;

		// Token: 0x0400B86B RID: 47211
		private readonly int m_PackTotal;

		// Token: 0x0400B86C RID: 47212
		private readonly int m_PackIdx;

		// Token: 0x0400B86D RID: 47213
		private readonly List<DrawPackData> m_DrawPackDatas;

		// Token: 0x0400B86E RID: 47214
		private readonly int m_SmokeType;

		// Token: 0x0400B86F RID: 47215
		private readonly bool m_IsPickup;

		// Token: 0x0400B870 RID: 47216
		private readonly int m_LabelType;

		// Token: 0x0400B871 RID: 47217
		private readonly string m_LabelText;
	}
}
