using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;

namespace YgomSystem.Viewer
{
	// Token: 0x020004F2 RID: 1266
	public class ViewerBgCameraOrganizerInner : MonoBehaviour
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x0000216A File Offset: 0x0000036A
		public CameraViewSetting.ViewInfo view
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cameraWorkStart
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600280C RID: 10252 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cameraWorkFinish
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x0000216A File Offset: 0x0000036A
		public static ViewerBgCameraOrganizerInner instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupViews()
		{
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeToDuel()
		{
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetViewMode(ViewerBgCameraOrganizerInner.ViewMode viewMode, bool immediate = true)
		{
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisplayType(ViewerBgCameraOrganizerInner.DisplayType displayType, bool immediate = true)
		{
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActivePostProcessing(bool active)
		{
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetExternalOperator(Action externalOperator)
		{
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateGameCam(float lerpT = 0.5f)
		{
		}

		// Token: 0x040028C4 RID: 10436
		private Action externalOperator;

		// Token: 0x040028C5 RID: 10437
		private float time;

		// Token: 0x040028C6 RID: 10438
		private Dictionary<ViewerBgCameraOrganizerInner.DisplayType, Dictionary<ViewerBgCameraOrganizerInner.ViewMode, CameraViewSetting.ViewInfo>> views;

		// Token: 0x040028C7 RID: 10439
		private ViewerBgCameraOrganizerInner.ViewMode currentViewMode;

		// Token: 0x040028C8 RID: 10440
		private ViewerBgCameraOrganizerInner.DisplayType currentDisplayType;

		// Token: 0x040028C9 RID: 10441
		private Vector3 gameCamShakedPos;

		// Token: 0x040028CA RID: 10442
		private Vector3 gameCamPos;

		// Token: 0x040028CB RID: 10443
		private Quaternion gameCamRot;

		// Token: 0x040028CC RID: 10444
		private float gameCamFov;

		// Token: 0x040028CD RID: 10445
		private float gameCamNearClip;

		// Token: 0x040028CE RID: 10446
		private float gameCamFarClip;

		// Token: 0x040028CF RID: 10447
		public Camera camera3D;

		// Token: 0x040028D0 RID: 10448
		public Camera subCamera3D;

		// Token: 0x040028D1 RID: 10449
		public Camera subCamera2D;

		// Token: 0x040028D2 RID: 10450
		public Camera screenCamera3D;

		// Token: 0x040028D3 RID: 10451
		public Camera screenCamera2D;

		// Token: 0x040028D4 RID: 10452
		public Camera performCamera3D;

		// Token: 0x040028D5 RID: 10453
		public Camera uiCameraContent;

		// Token: 0x040028D6 RID: 10454
		public Camera uiCameraOverlay;

		// Token: 0x040028D7 RID: 10455
		private static ViewerBgCameraOrganizerInner _instance;

		// Token: 0x020004F3 RID: 1267
		public enum ViewMode
		{
			// Token: 0x040028D9 RID: 10457
			DuelTop,
			// Token: 0x040028DA RID: 10458
			DuelTopFar
		}

		// Token: 0x020004F4 RID: 1268
		public enum DisplayType
		{
			// Token: 0x040028DC RID: 10460
			Vista,
			// Token: 0x040028DD RID: 10461
			Standard,
			// Token: 0x040028DE RID: 10462
			MobileDevice
		}
	}
}
