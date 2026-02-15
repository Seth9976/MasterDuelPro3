using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BE9 RID: 3049
	public class DuelistCupMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060056AC RID: 22188 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060056AD RID: 22189 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}

		// Token: 0x0400938D RID: 37773
		private int m_cupID;
	}
}
