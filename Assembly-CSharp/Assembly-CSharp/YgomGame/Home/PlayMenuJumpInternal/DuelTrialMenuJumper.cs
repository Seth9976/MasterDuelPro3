using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BE8 RID: 3048
	public class DuelTrialMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060056A7 RID: 22183 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x000F4CE2 File Offset: 0x000F2EE2
		public DuelTrialMenuJumper(int eventID)
		{
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}

		// Token: 0x0400938C RID: 37772
		private int m_eventID;
	}
}
