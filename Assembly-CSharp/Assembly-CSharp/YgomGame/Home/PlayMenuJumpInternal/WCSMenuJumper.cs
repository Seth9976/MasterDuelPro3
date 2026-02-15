using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BEF RID: 3055
	public class WCSMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060056CE RID: 22222 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}

		// Token: 0x04009390 RID: 37776
		private int m_wcsID;
	}
}
