using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200051E RID: 1310
	public class FontRasterizer : MonoBehaviour
	{
		// Token: 0x06002A0C RID: 10764 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(int renderTexW, int renderTexH, string text, Action<MDText> onSetup, Action<RenderTexture, Texture2D> onFinish)
		{
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishRender(RenderTextureBuilder renderTexBuilder)
		{
		}

		// Token: 0x04002974 RID: 10612
		private Action<RenderTexture, Texture2D> onFinish;

		// Token: 0x04002975 RID: 10613
		private RenderTextureBuilder renderTexBuilder;

		// Token: 0x04002976 RID: 10614
		private Camera cam;

		// Token: 0x04002977 RID: 10615
		private RectTransform rootTransform;

		// Token: 0x04002978 RID: 10616
		private MDText textComponent;

		// Token: 0x04002979 RID: 10617
		private int oldCullingMask;
	}
}
