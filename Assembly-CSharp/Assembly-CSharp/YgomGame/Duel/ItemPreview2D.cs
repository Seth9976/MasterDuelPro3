using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EAB RID: 3755
	public class ItemPreview2D : MonoBehaviour
	{
		// Token: 0x06006D60 RID: 28000 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<ItemPreview2D> onFinish = null)
		{
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(GameObject gameObject, Transform parent, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, float imgW = -1f, float imgH = -1f, Action<ItemPreview2D> onFinish = null, bool enablePostEffect = false)
		{
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(GameObject gameObject, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, float imgW = -1f, float imgH = -1f, Action onFinish = null, bool enablePostEffect = false)
		{
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x0000216D File Offset: 0x0000036D
		public void LookAtItemObject()
		{
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDestroy()
		{
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06006D66 RID: 28006 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0400A850 RID: 43088
		[SerializeField]
		public RawImage rawImg;

		// Token: 0x0400A851 RID: 43089
		private int id;

		// Token: 0x0400A852 RID: 43090
		private GameObject itemObject;

		// Token: 0x0400A853 RID: 43091
		public Camera renderCam;
	}
}
