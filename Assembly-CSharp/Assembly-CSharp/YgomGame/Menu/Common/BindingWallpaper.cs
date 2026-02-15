using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B22 RID: 2850
	public class BindingWallpaper : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingWallpaperContext context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x0600530F RID: 21263 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005310 RID: 21264 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005311 RID: 21265 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingWallpaper Binding(RawImage target, BindingWallpaperContext mateContext)
		{
			return null;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateData(BindingWallpaperContext mateContext)
		{
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0000216D File Offset: 0x0000036D
		private void Load()
		{
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnReadyWallpaper()
		{
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x04009105 RID: 37125
		[SerializeField]
		private BindingWallpaperContext m_Context;

		// Token: 0x04009106 RID: 37126
		[SerializeField]
		private bool m_PlayLoopTween;

		// Token: 0x04009107 RID: 37127
		private IAsyncProgressContent m_AsyncProgress;

		// Token: 0x04009108 RID: 37128
		private bool m_LoadOnStart;

		// Token: 0x04009109 RID: 37129
		private GameObject m_Wallpaper;
	}
}
