using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x020005B6 RID: 1462
	public class RenderTextureTarget : MonoBehaviour
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x0000216A File Offset: 0x0000036A
		public Camera Cam
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Create(GameObject obj, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, Action<int, RenderTexture, Texture2D> onFinish = null, bool enablePostEffect = false, int depth = 24)
		{
			return 0;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Destory(int id)
		{
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x0000216D File Offset: 0x0000036D
		public void DestroyRenderTexture()
		{
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetOnFinish()
		{
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(int renderTexW, int renderTexH, Action<int, RenderTexture, Texture2D> onFinish, int depth = 24, bool enablePostEffect = false)
		{
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishRender(RenderTextureBuilder renderTexBuilder)
		{
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitBuilder()
		{
		}

		// Token: 0x04002BCB RID: 11211
		[SerializeField]
		private RenderTextureBuilder renderTexBuilder;

		// Token: 0x04002BCC RID: 11212
		[SerializeField]
		private Transform rootTransform;

		// Token: 0x04002BCD RID: 11213
		[SerializeField]
		private Camera cam;

		// Token: 0x04002BCE RID: 11214
		private Action<int, RenderTexture, Texture2D> onFinish;

		// Token: 0x04002BCF RID: 11215
		private int oldCullingMask;

		// Token: 0x04002BD0 RID: 11216
		[SerializeField]
		private int renderDelay;

		// Token: 0x04002BD1 RID: 11217
		public int renderTargetId;

		// Token: 0x04002BD2 RID: 11218
		private GameObject targetObject;
	}
}
