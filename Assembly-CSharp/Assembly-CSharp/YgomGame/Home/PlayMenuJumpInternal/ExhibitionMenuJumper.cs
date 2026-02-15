using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BEA RID: 3050
	public class ExhibitionMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060056B2 RID: 22194 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x000F4CE2 File Offset: 0x000F2EE2
		public ExhibitionMenuJumper(int eventID)
		{
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}

		// Token: 0x0400938E RID: 37774
		private int m_eventID;
	}
}
