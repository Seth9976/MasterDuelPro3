using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B24 RID: 2852
	[DisallowMultipleComponent]
	public class BindingWallpaperThumb : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600531F RID: 21279 RVA: 0x0000216D File Offset: 0x0000036D
		public int wallpaperId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x0000216A File Offset: 0x0000036A
		public Image image
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005322 RID: 21282 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool validIconId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06005324 RID: 21284 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005325 RID: 21285 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005326 RID: 21286 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0000216D File Offset: 0x0000036D
		private void VisibleRefresh()
		{
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRebind()
		{
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOnBindingRoutine()
		{
			return null;
		}

		// Token: 0x0400910F RID: 37135
		[SerializeField]
		private int m_WallpaperId;

		// Token: 0x04009110 RID: 37136
		private Image m_ImageCache;

		// Token: 0x04009111 RID: 37137
		private bool m_Visible;

		// Token: 0x04009112 RID: 37138
		private IEnumerator m_OnBindingRoutine;
	}
}
