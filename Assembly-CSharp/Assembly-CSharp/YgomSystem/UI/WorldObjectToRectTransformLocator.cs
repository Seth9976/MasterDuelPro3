using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YgomSystem.Effect;

namespace YgomSystem.UI
{
	// Token: 0x02000659 RID: 1625
	[ExecuteInEditMode]
	public class WorldObjectToRectTransformLocator : UIBehaviour
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera targetRenderCamera
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032FC RID: 13052 RVA: 0x0000216D File Offset: 0x0000036D
		public Transform targetWorldObject
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04002F49 RID: 12105
		[SerializeField]
		private Camera m_TargetRenderCamera;

		// Token: 0x04002F4A RID: 12106
		[SerializeField]
		private ScreenEffect.ViewType m_CameraViewType;

		// Token: 0x04002F4B RID: 12107
		[SerializeField]
		private bool m_CameraOverUI;

		// Token: 0x04002F4C RID: 12108
		[SerializeField]
		private Transform m_TargetWorldObject;

		// Token: 0x04002F4D RID: 12109
		private Canvas m_OwnerCanvasCache;

		// Token: 0x04002F4E RID: 12110
		private RectTransform m_RectTransformCache;
	}
}
