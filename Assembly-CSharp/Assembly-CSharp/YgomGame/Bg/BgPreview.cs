using System;
using System.Collections;
using UnityEngine;

namespace YgomGame.Bg
{
	// Token: 0x02001146 RID: 4422
	public class BgPreview : MonoBehaviour
	{
		// Token: 0x0600838F RID: 33679 RVA: 0x0000216A File Offset: 0x0000036A
		public static BgPreview Create(Transform root, Action onFinish = null, params int[] ids)
		{
			return null;
		}

		// Token: 0x06008390 RID: 33680 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializedCheck()
		{
			return null;
		}

		// Token: 0x06008391 RID: 33681 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400BEFF RID: 48895
		private BgManager mng;

		// Token: 0x0400BF00 RID: 48896
		private Action onInitilizeFinish;

		// Token: 0x0400BF01 RID: 48897
		public bool IsInitialized;
	}
}
