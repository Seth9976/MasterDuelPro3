using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B18 RID: 2840
	public class BindingImageEx : BindingImage, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005271 RID: 21105 RVA: 0x0000216D File Offset: 0x0000036D
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return AspectRatioFitter.AspectMode.None;
			}
			set
			{
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06005273 RID: 21107 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005274 RID: 21108 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005275 RID: 21109 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingImageEx Binding(GameObject target, string spritePath, string materialPath, bool async = true, AspectRatioFitter.AspectMode aspectMode = AspectRatioFitter.AspectMode.None)
		{
			return null;
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingImageEx Binding(Image target, string spritePath, bool async = true, AspectRatioFitter.AspectMode aspectMode = AspectRatioFitter.AspectMode.None)
		{
			return null;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingImageEx Binding(RawImage target, string spritePath, bool async = true, AspectRatioFitter.AspectMode aspectMode = AspectRatioFitter.AspectMode.None)
		{
			return null;
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0000216D File Offset: 0x0000036D
		public static void BindingImageOrPrefab(Image target, string path, bool async = true, AspectRatioFitter.AspectMode imageAspectMode = AspectRatioFitter.AspectMode.None, BindingGameObjectEx.FitMode prefabFitMode = BindingGameObjectEx.FitMode.SCALE_FIT_HIGHEST, bool tryUseAssetSize = true, float overrideWidth = 0f, float overrideHeight = 0f, Action onComplete = null, Action handleOnFailedCallback = null)
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingImageEx InnerBinding(GameObject target, string spritePath, string materialPath, bool async = true, AspectRatioFitter.AspectMode aspectMode = AspectRatioFitter.AspectMode.None)
		{
			return null;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool YgomSystem_002EUI_002EILoadingIconHandler_002EIsDone()
		{
			return false;
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool YgomGame_002EMenu_002ECommon_002EIAsyncProgressContent_002EIsDone()
		{
			return false;
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBinding()
		{
			return false;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRebind()
		{
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyAspectRatio()
		{
		}

		// Token: 0x040090B5 RID: 37045
		private AspectRatioFitter.AspectMode m_AspectMode;
	}
}
