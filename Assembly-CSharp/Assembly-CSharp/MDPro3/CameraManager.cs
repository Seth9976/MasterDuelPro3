using System;
using DG.Tweening;
using MDPro3.Servant;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MDPro3
{
	// Token: 0x02001267 RID: 4711
	public class CameraManager : Manager
	{
		// Token: 0x06008A95 RID: 35477 RVA: 0x001157CC File Offset: 0x001139CC
		public override void Initialize()
		{
			base.Initialize();
			this.urpAsset = Resources.Load<UniversalRenderPipelineAsset>("Settings/URPAsset");
			this.forwardRendererData = Resources.Load<UniversalRendererData>("Settings/URPAsset_Renderer");
			this.urpAssetForUI = Resources.Load<UniversalRenderPipelineAsset>("Settings/URPAssetForUI");
			this.forwardRendererDataForUI = Resources.Load<UniversalRendererData>("Settings/URPAssetForUI_Renderer");
			CameraManager.ShiftTo2D();
			CameraManager.ChangeCameraFOV();
			CameraManager.DuelOverlay2DMinus();
			CameraManager.DuelOverlay3DMinus();
			CameraManager.DuelOverlayEffect2DMinus();
			CameraManager.DuelOverlayEffect3DMinus();
			CameraManager.UIBlurMinus();
			SystemEvent.OnResolutionChange += CameraManager.ChangeCameraFOV;
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x00115854 File Offset: 0x00113A54
		public static void ChangeCameraFOV()
		{
			float aspect = (float)Screen.width * 9f / (float)Screen.height;
			if (aspect > 16f)
			{
				Program.instance.camera_.cameraMain.fieldOfView = 46f - aspect;
				Program.instance.camera_.cameraDuelOverlay3D.fieldOfView = Program.instance.camera_.cameraMain.fieldOfView;
				return;
			}
			Program.instance.camera_.cameraMain.fieldOfView = 30f;
			Program.instance.camera_.cameraDuelOverlay3D.fieldOfView = 30f;
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x001158F4 File Offset: 0x00113AF4
		public static void ShiftTo2D()
		{
			Program.instance.camera_.cameraMain.gameObject.SetActive(false);
			Program.instance.camera_.light.SetActive(false);
			Program.instance.camera_.camera2D.gameObject.SetActive(true);
			QualitySettings.SetQualityLevel(6);
			SettingServant.SetFpsToConfig();
		}

		// Token: 0x06008A98 RID: 35480 RVA: 0x00115958 File Offset: 0x00113B58
		public static void ShiftTo3D()
		{
			Program.instance.camera_.cameraMain.gameObject.SetActive(true);
			Program.instance.camera_.light.SetActive(true);
			Program.instance.camera_.camera2D.gameObject.SetActive(false);
			QualitySettings.SetQualityLevel((int)Config.GetFloat("Quality", 2f));
			SettingServant.SetFpsToConfig();
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x001159C8 File Offset: 0x00113BC8
		public static void Overlay3DReset()
		{
			Program.instance.camera_.cameraDuelOverlay3D.transform.localPosition = new Vector3(0f, 95f, -37f);
			Program.instance.camera_.cameraDuelOverlay3D.transform.localEulerAngles = new Vector3(70f, 0f, 0f);
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x00115A2F File Offset: 0x00113C2F
		public static void DuelOverlay2DPlus()
		{
			CameraManager.DuelOverlay2DCount++;
			Program.instance.camera_.cameraDuelOverlay2D.gameObject.SetActive(true);
		}

		// Token: 0x06008A9B RID: 35483 RVA: 0x00115A57 File Offset: 0x00113C57
		public static void DuelOverlay2DMinus()
		{
			CameraManager.DuelOverlay2DCount--;
			if (CameraManager.DuelOverlay2DCount < 0)
			{
				CameraManager.DuelOverlay2DCount = 0;
			}
			if (CameraManager.DuelOverlay2DCount == 0)
			{
				Program.instance.camera_.cameraDuelOverlay2D.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008A9C RID: 35484 RVA: 0x00115A94 File Offset: 0x00113C94
		public static void DuelOverlayEffect2DPlus()
		{
			CameraManager.DuelOverlayEffect2DCount++;
			Program.instance.camera_.cameraDuelOverlayEffect2D.gameObject.SetActive(true);
		}

		// Token: 0x06008A9D RID: 35485 RVA: 0x00115ABC File Offset: 0x00113CBC
		public static void DuelOverlayEffect2DMinus()
		{
			CameraManager.DuelOverlayEffect2DCount--;
			if (CameraManager.DuelOverlayEffect2DCount < 0)
			{
				CameraManager.DuelOverlayEffect2DCount = 0;
			}
			if (CameraManager.DuelOverlayEffect2DCount == 0)
			{
				Program.instance.camera_.cameraDuelOverlayEffect2D.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x00115AF9 File Offset: 0x00113CF9
		public static void DuelOverlay3DPlus()
		{
			CameraManager.DuelOverlay3DCount++;
			Program.instance.camera_.cameraDuelOverlay3D.gameObject.SetActive(true);
		}

		// Token: 0x06008A9F RID: 35487 RVA: 0x00115B21 File Offset: 0x00113D21
		public static void DuelOverlay3DMinus()
		{
			CameraManager.DuelOverlay3DCount--;
			if (CameraManager.DuelOverlay3DCount < 0)
			{
				CameraManager.DuelOverlay3DCount = 0;
			}
			if (CameraManager.DuelOverlay3DCount == 0)
			{
				Program.instance.camera_.cameraDuelOverlay3D.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008AA0 RID: 35488 RVA: 0x00115B5E File Offset: 0x00113D5E
		public static void DuelOverlayEffect3DPlus()
		{
			CameraManager.DuelOverlayEffect3DCount++;
			Program.instance.camera_.cameraDuelOverlayEffect3D.gameObject.SetActive(true);
		}

		// Token: 0x06008AA1 RID: 35489 RVA: 0x00115B86 File Offset: 0x00113D86
		public static void DuelOverlayEffect3DMinus()
		{
			CameraManager.DuelOverlayEffect3DCount--;
			if (CameraManager.DuelOverlayEffect3DCount < 0)
			{
				CameraManager.DuelOverlayEffect3DCount = 0;
			}
			if (CameraManager.DuelOverlayEffect3DCount == 0)
			{
				Program.instance.camera_.cameraDuelOverlayEffect3D.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x00115BC3 File Offset: 0x00113DC3
		public static void UIBlurPlus()
		{
			CameraManager.uiBlurCount++;
			Program.instance.camera_.cameraUIBlur.gameObject.SetActive(true);
		}

		// Token: 0x06008AA3 RID: 35491 RVA: 0x00115BEB File Offset: 0x00113DEB
		public static void UIBlurMinus()
		{
			CameraManager.uiBlurCount--;
			if (CameraManager.uiBlurCount < 0)
			{
				CameraManager.uiBlurCount = 0;
			}
			if (CameraManager.uiBlurCount == 0)
			{
				Program.instance.camera_.cameraUIBlur.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008AA4 RID: 35492 RVA: 0x00115C28 File Offset: 0x00113E28
		public static void BlackInOut(float delay, float inTime, float time, float outTime)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(delay);
			sequence.Append(Program.instance.camera_.black.DOFade(0.75f, inTime));
			sequence.AppendInterval(time);
			sequence.Append(Program.instance.camera_.black.DOFade(0f, outTime));
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x00115C8B File Offset: 0x00113E8B
		public static void BlackIn(float delay, float inTime)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(delay);
			sequence.Append(Program.instance.camera_.black.DOFade(0.75f, inTime));
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x00115CBA File Offset: 0x00113EBA
		public static void BlackOut(float delay, float outTime)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(delay);
			sequence.Append(Program.instance.camera_.black.DOFade(0f, outTime));
		}

		// Token: 0x06008AA7 RID: 35495 RVA: 0x00115CEC File Offset: 0x00113EEC
		public static void ShakeCamera(bool heavy = false)
		{
			if (heavy)
			{
				Program.instance.camera_.cameraMain.DOShakePosition(0.4f, 5f, 100, 90f, true, ShakeRandomnessMode.Full);
				return;
			}
			Program.instance.camera_.cameraMain.DOShakePosition(0.2f, 0.5f, 50, 90f, true, ShakeRandomnessMode.Full);
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x00115D50 File Offset: 0x00113F50
		public static void Duel3DOverlayStickWithMain(bool stick)
		{
			if (stick)
			{
				CameraManager.overlaySticking = true;
				Program.instance.camera_.cameraDuelOverlay3D.transform.SetParent(Program.instance.camera_.cameraMain.transform, false);
				Program.instance.camera_.cameraDuelOverlay3D.transform.localPosition = Vector3.zero;
				Program.instance.camera_.cameraDuelOverlay3D.transform.localEulerAngles = Vector3.zero;
				return;
			}
			CameraManager.overlaySticking = false;
			Program.instance.camera_.cameraDuelOverlay3D.transform.SetParent(Program.instance.camera_.transform, false);
			Program.instance.camera_.cameraDuelOverlay3D.transform.localPosition = CameraManager.mainCameraDefaultPosition;
			Program.instance.camera_.cameraDuelOverlay3D.transform.localEulerAngles = CameraManager.mainCameraDefaultAngle;
		}

		// Token: 0x0400C62E RID: 50734
		public Camera cameraMain;

		// Token: 0x0400C62F RID: 50735
		public Camera camera2D;

		// Token: 0x0400C630 RID: 50736
		public Camera cameraDuelOverlay3D;

		// Token: 0x0400C631 RID: 50737
		public Camera cameraDuelOverlayEffect3D;

		// Token: 0x0400C632 RID: 50738
		public Camera cameraDuelOverlay2D;

		// Token: 0x0400C633 RID: 50739
		public Camera cameraDuelOverlayEffect2D;

		// Token: 0x0400C634 RID: 50740
		public Camera cameraUI;

		// Token: 0x0400C635 RID: 50741
		public Camera cameraUIBlur;

		// Token: 0x0400C636 RID: 50742
		public GameObject light;

		// Token: 0x0400C637 RID: 50743
		public SpriteRenderer black;

		// Token: 0x0400C638 RID: 50744
		public UniversalRenderPipelineAsset urpAsset;

		// Token: 0x0400C639 RID: 50745
		public UniversalRendererData forwardRendererData;

		// Token: 0x0400C63A RID: 50746
		public UniversalRenderPipelineAsset urpAssetForUI;

		// Token: 0x0400C63B RID: 50747
		public UniversalRendererData forwardRendererDataForUI;

		// Token: 0x0400C63C RID: 50748
		public static Vector3 mainCameraDefaultPosition = new Vector3(0f, 95f, -37f);

		// Token: 0x0400C63D RID: 50749
		public static Vector3 mainCameraDefaultAngle = new Vector3(70f, 0f, 0f);

		// Token: 0x0400C63E RID: 50750
		public static int DuelOverlay2DCount = 0;

		// Token: 0x0400C63F RID: 50751
		public static int DuelOverlayEffect2DCount = 0;

		// Token: 0x0400C640 RID: 50752
		public static int DuelOverlay3DCount = 0;

		// Token: 0x0400C641 RID: 50753
		public static int DuelOverlayEffect3DCount = 0;

		// Token: 0x0400C642 RID: 50754
		public static int uiBlurCount = 0;

		// Token: 0x0400C643 RID: 50755
		public static bool overlaySticking;
	}
}
