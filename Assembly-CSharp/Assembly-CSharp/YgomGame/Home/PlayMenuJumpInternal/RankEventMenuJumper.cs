using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BEC RID: 3052
	public class RankEventMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060056BE RID: 22206 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060056BF RID: 22207 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x000F4CE2 File Offset: 0x000F2EE2
		public RankEventMenuJumper(int eventID)
		{
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}

		// Token: 0x0400938F RID: 37775
		private int m_eventID;
	}
}
