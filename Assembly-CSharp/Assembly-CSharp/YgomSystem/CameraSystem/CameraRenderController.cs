using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.CameraSystem
{
	// Token: 0x0200078C RID: 1932
	public class CameraRenderController : MonoBehaviour
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x0000216D File Offset: 0x0000036D
		public static RenderTexture mainCameraRenderTexture
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x0000216A File Offset: 0x0000036A
		private CameraRenderTargetUI renderTargetUI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float renderTextureScale
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateRenderTexture()
		{
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRenderTextureWidth(int width)
		{
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRenderCameraEnabled(bool render_camera_enabled)
		{
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispCameraRenderTexture(bool disp)
		{
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveSkyBox(bool active)
		{
		}

		// Token: 0x040034DC RID: 13532
		private CameraRenderTargetUI _renderTargetUI;

		// Token: 0x040034DD RID: 13533
		[SerializeField]
		private int renderTextureWidth;

		// Token: 0x040034DE RID: 13534
		public static CameraRenderController instance;
	}
}
