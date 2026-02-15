using System;
using Cysharp.Threading.Tasks;
using MDPro3;
using UnityEngine;

namespace YgomSystem.Effect
{
	// Token: 0x02000783 RID: 1923
	public class ScreenEffect : MonoBehaviour
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000F3BEC File Offset: 0x000F1DEC
		public Camera targetCamera
		{
			get
			{
				if (this.cameraViewType == ScreenEffect.ViewType.View3D)
				{
					if (this.cameraAngle == Vector3.zero)
					{
						return Program.instance.camera_.cameraDuelOverlayEffect3D;
					}
					return Program.instance.camera_.cameraDuelOverlay3D;
				}
				else
				{
					if (this.overUI)
					{
						return Program.instance.camera_.cameraDuelOverlayEffect2D;
					}
					return Program.instance.camera_.cameraDuelOverlay2D;
				}
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000F3C5B File Offset: 0x000F1E5B
		private void OnEnable()
		{
			this.DelaySetupAsync();
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000F3C64 File Offset: 0x000F1E64
		private async UniTask DelaySetupAsync()
		{
			await UniTask.Yield(base.destroyCancellationToken, false);
			this.Setup();
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000F3CA8 File Offset: 0x000F1EA8
		public void Setup()
		{
			if (this.useMainCameraSetting)
			{
				this.cameraViewType = ScreenEffect.ViewType.View3D;
				this.cameraPosition = CameraManager.mainCameraDefaultPosition;
				this.cameraAngle = CameraManager.mainCameraDefaultAngle;
				this.overUI = false;
			}
			if (this.targetCamera == Program.instance.camera_.cameraDuelOverlayEffect2D)
			{
				CameraManager.DuelOverlayEffect2DPlus();
				this.SetupLayer("DuelOverlayEffect2D");
			}
			else if (this.targetCamera == Program.instance.camera_.cameraDuelOverlay2D)
			{
				CameraManager.DuelOverlay2DPlus();
				this.SetupLayer("DuelOverlay2D");
			}
			else if (this.targetCamera == Program.instance.camera_.cameraDuelOverlayEffect3D)
			{
				CameraManager.DuelOverlayEffect3DPlus();
				this.SetupLayer("DuelOverlayEffect3D");
			}
			else if (this.targetCamera == Program.instance.camera_.cameraDuelOverlay3D)
			{
				CameraManager.DuelOverlay3DPlus();
				this.SetupLayer("DuelOverlay3D");
			}
			this.SetupCamera(this.targetCamera);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
		public void SetupCamera(Camera target)
		{
			if (target == Program.instance.camera_.cameraDuelOverlay3D && CameraManager.overlaySticking)
			{
				return;
			}
			target.transform.localPosition = this.cameraPosition;
			target.transform.localEulerAngles = this.cameraAngle;
			if (target.name.Contains("2D"))
			{
				target.orthographicSize = this.camera2DSize;
			}
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x0000216D File Offset: 0x0000036D
		public void TraceCameraSetting(Camera target, string viewInfoLabel = "Top")
		{
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TraceMainCameraSetting(Camera target)
		{
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupLayer(int layer)
		{
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000F3E14 File Offset: 0x000F2014
		public void SetupLayer(string layerName)
		{
			Tools.ChangeLayer(base.gameObject, layerName, false);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupSpriteScaler()
		{
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupSpriteScaler(float screenWidth, float screenHeight, Camera setupCamera = null)
		{
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x000F3E24 File Offset: 0x000F2024
		private void OnDisable()
		{
			if (this.targetCamera == Program.instance.camera_.cameraDuelOverlayEffect2D)
			{
				CameraManager.DuelOverlayEffect2DMinus();
				return;
			}
			if (this.targetCamera == Program.instance.camera_.cameraDuelOverlay2D)
			{
				CameraManager.DuelOverlay2DMinus();
				return;
			}
			if (this.targetCamera == Program.instance.camera_.cameraDuelOverlayEffect3D)
			{
				CameraManager.DuelOverlayEffect3DMinus();
				return;
			}
			if (this.targetCamera == Program.instance.camera_.cameraDuelOverlay3D)
			{
				CameraManager.DuelOverlay3DMinus();
			}
		}

		// Token: 0x0400349E RID: 13470
		public bool useCameraSetting;

		// Token: 0x0400349F RID: 13471
		public bool useMainCameraSetting;

		// Token: 0x040034A0 RID: 13472
		public Vector3 cameraPosition;

		// Token: 0x040034A1 RID: 13473
		public Vector3 cameraAngle;

		// Token: 0x040034A2 RID: 13474
		public ScreenEffect.ViewType cameraViewType;

		// Token: 0x040034A3 RID: 13475
		public float camera2DSize;

		// Token: 0x040034A4 RID: 13476
		public bool overUI;

		// Token: 0x040034A5 RID: 13477
		public bool setupOnAwake;

		// Token: 0x02000784 RID: 1924
		public enum ViewType
		{
			// Token: 0x040034A7 RID: 13479
			View2D,
			// Token: 0x040034A8 RID: 13480
			View3D
		}
	}
}
