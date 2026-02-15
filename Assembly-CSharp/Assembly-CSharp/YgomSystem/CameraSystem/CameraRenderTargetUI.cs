using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.CameraSystem
{
	// Token: 0x0200078D RID: 1933
	public class CameraRenderTargetUI : MonoBehaviour
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x0000216D File Offset: 0x0000036D
		public RawImage mainCameraRenderTarget
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x0000216D File Offset: 0x0000036D
		public static CameraRenderTargetUI instance
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(RenderTexture render_texture)
		{
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool active)
		{
		}
	}
}
