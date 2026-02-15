using System;

namespace YgomSystem.UI
{
	// Token: 0x020005A1 RID: 1441
	public class InputBlockerFlexible : AbstractInputBlocker
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int blockPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBlockPriority(int priority)
		{
		}

		// Token: 0x04002B74 RID: 11124
		public int _blockPriority;
	}
}
