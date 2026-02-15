using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu.Common;
using YgomSystem.UI;

namespace YgomSystem.Home
{
	// Token: 0x02000764 RID: 1892
	public class BindingTopicsCardUV : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06003B07 RID: 15111 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003B08 RID: 15112 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExecuteBinding()
		{
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x0000216D File Offset: 0x0000036D
		private void FitScalePendulum(RawImage rawImage, int mrk)
		{
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yDelayFrame(int waitFrame = 1, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x04003472 RID: 13426
		[SerializeField]
		private string settingPath;

		// Token: 0x04003473 RID: 13427
		[SerializeField]
		public int mrk;

		// Token: 0x04003474 RID: 13428
		[SerializeField]
		private bool fitScalePendulum;

		// Token: 0x04003475 RID: 13429
		private bool loadStart;
	}
}
