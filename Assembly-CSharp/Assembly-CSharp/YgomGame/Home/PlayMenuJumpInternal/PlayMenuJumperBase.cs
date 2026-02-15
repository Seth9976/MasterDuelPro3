using System;
using System.Collections.Generic;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BEB RID: 3051
	public abstract class PlayMenuJumperBase
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060056B8 RID: 22200
		protected abstract ColosseumUtil.PlayMode playMode { get; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060056B9 RID: 22201
		protected abstract string prefabPath { get; }

		// Token: 0x060056BA RID: 22202 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ExtendArgs(ref Dictionary<string, object> args)
		{
		}

		// Token: 0x060056BB RID: 22203
		public abstract void Check(Action<bool> resultCallback);

		// Token: 0x060056BC RID: 22204 RVA: 0x0000216D File Offset: 0x0000036D
		public void Jump()
		{
		}
	}
}
