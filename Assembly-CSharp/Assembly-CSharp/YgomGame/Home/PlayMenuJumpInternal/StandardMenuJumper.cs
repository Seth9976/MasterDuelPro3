using System;
using System.Collections;
using YgomGame.Colosseum;

namespace YgomGame.Home.PlayMenuJumpInternal
{
	// Token: 0x02000BED RID: 3053
	public class StandardMenuJumper : PlayMenuJumperBase
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ColosseumUtil.PlayMode playMode
		{
			get
			{
				return ColosseumUtil.PlayMode.NONE;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string prefabPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkCoroutine(Action<bool> callback)
		{
			return null;
		}
	}
}
