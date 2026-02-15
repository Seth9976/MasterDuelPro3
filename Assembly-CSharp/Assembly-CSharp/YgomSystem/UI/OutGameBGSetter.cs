using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005A8 RID: 1448
	public class OutGameBGSetter : MonoBehaviour
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x0000216D File Offset: 0x0000036D
		public string prefBackPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBG()
		{
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetBG()
		{
		}

		// Token: 0x04002B95 RID: 11157
		public const string k_EmptyWallpaperPath = "EmptyWallpaper";

		// Token: 0x04002B96 RID: 11158
		[SerializeField]
		private string m_PrefBackPath;

		// Token: 0x04002B97 RID: 11159
		private bool isSet;

		// Token: 0x04002B98 RID: 11160
		public float duration;

		// Token: 0x04002B99 RID: 11161
		public bool async;
	}
}
