using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EC7 RID: 3783
	public class MainCameraOrganizer : MonoBehaviour
	{
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06006E44 RID: 28228 RVA: 0x0000216A File Offset: 0x0000036A
		public CameraViewSetting.ViewInfo view
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06006E45 RID: 28229 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006E46 RID: 28230 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera camera3D
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

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06006E47 RID: 28231 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006E48 RID: 28232 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera uiCameraContent
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

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06006E49 RID: 28233 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006E4A RID: 28234 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera uiCameraOverlay
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

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06006E4B RID: 28235 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006E4C RID: 28236 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cameraWorkFinish
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06006E4D RID: 28237 RVA: 0x0000216A File Offset: 0x0000036A
		public static MainCameraOrganizer instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06006E4E RID: 28238 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006E4F RID: 28239 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
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

		// Token: 0x06006E50 RID: 28240 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x0000216A File Offset: 0x0000036A
		public static MainCameraOrganizer Create(GameObject root, string name)
		{
			return null;
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupViews()
		{
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeToDuel(bool isAudience)
		{
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishToDuel()
		{
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reboot()
		{
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel(bool bgCameraEnabled)
		{
		}

		// Token: 0x06006E58 RID: 28248 RVA: 0x0000216D File Offset: 0x0000036D
		public void CameraWorkBegin(Camera camera)
		{
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x0000216D File Offset: 0x0000036D
		public void CameraWorkEnd()
		{
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetViewMode(MainCameraOrganizer.ViewMode viewMode, bool immediate = true, bool isDuelAudience = false)
		{
		}

		// Token: 0x06006E5B RID: 28251 RVA: 0x0000216D File Offset: 0x0000036D
		public void Shake(string type, bool shakeSubCamera = false)
		{
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActivePostProcessing(bool active)
		{
		}

		// Token: 0x06006E5D RID: 28253 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetExternalOperator(IMainCameraOperation externalOperator)
		{
		}

		// Token: 0x06006E5E RID: 28254 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006E5F RID: 28255 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingStep()
		{
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelStartShow()
		{
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecBgCameraStep()
		{
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndBgCameraStep()
		{
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateGameCam(float lerpT = 0.5f)
		{
		}

		// Token: 0x0400A920 RID: 43296
		[SerializeField]
		private Color _backgroundColorDebug;

		// Token: 0x0400A921 RID: 43297
		private MainCameraOrganizer.Step step;

		// Token: 0x0400A922 RID: 43298
		private CameraShaker camShaker;

		// Token: 0x0400A923 RID: 43299
		private bool shakeSubCamera;

		// Token: 0x0400A924 RID: 43300
		private IMainCameraOperation externalOperator;

		// Token: 0x0400A925 RID: 43301
		private float time;

		// Token: 0x0400A926 RID: 43302
		private Dictionary<MainCameraOrganizer.ViewMode, CameraViewSetting.ViewInfo> views;

		// Token: 0x0400A927 RID: 43303
		private MainCameraOrganizer.ViewMode currentViewMode;

		// Token: 0x0400A928 RID: 43304
		private Vector3 gameCamShakedPos;

		// Token: 0x0400A929 RID: 43305
		private Vector3 gameCamPos;

		// Token: 0x0400A92A RID: 43306
		private Quaternion gameCamRot;

		// Token: 0x0400A92B RID: 43307
		private float gameCamFov;

		// Token: 0x0400A92C RID: 43308
		private float gameCamNearClip;

		// Token: 0x0400A92D RID: 43309
		private float gameCamFarClip;

		// Token: 0x0400A92E RID: 43310
		private Vector3 bgCamEndPos;

		// Token: 0x0400A92F RID: 43311
		private Quaternion bgCamEndRot;

		// Token: 0x0400A930 RID: 43312
		private float bgCamFov;

		// Token: 0x0400A931 RID: 43313
		private float bgCamNearClip;

		// Token: 0x0400A932 RID: 43314
		private float bgCamFarClip;

		// Token: 0x0400A933 RID: 43315
		private Camera subCamera3D;

		// Token: 0x0400A934 RID: 43316
		private Camera subCamera2D;

		// Token: 0x0400A935 RID: 43317
		private Camera screenCamera3D;

		// Token: 0x0400A936 RID: 43318
		private Camera screenCamera2D;

		// Token: 0x0400A937 RID: 43319
		private Camera performCamera3D;

		// Token: 0x0400A938 RID: 43320
		private AudioListener listener;

		// Token: 0x0400A939 RID: 43321
		private static MainCameraOrganizer _instance;

		// Token: 0x0400A93A RID: 43322
		private const float bgToGameCamDuration = 2f;

		// Token: 0x02000EC8 RID: 3784
		private enum Step
		{
			// Token: 0x0400A93C RID: 43324
			Initializing,
			// Token: 0x0400A93D RID: 43325
			DuelCameraWork,
			// Token: 0x0400A93E RID: 43326
			ExecBgCamera,
			// Token: 0x0400A93F RID: 43327
			EndBgCamera,
			// Token: 0x0400A940 RID: 43328
			Idle
		}

		// Token: 0x02000EC9 RID: 3785
		public enum ViewMode
		{
			// Token: 0x0400A942 RID: 43330
			Default,
			// Token: 0x0400A943 RID: 43331
			DuelTop,
			// Token: 0x0400A944 RID: 43332
			DuelTopInput,
			// Token: 0x0400A945 RID: 43333
			Manual,
			// Token: 0x0400A946 RID: 43334
			DuelTopFar,
			// Token: 0x0400A947 RID: 43335
			DuelTopInputFar
		}
	}
}
