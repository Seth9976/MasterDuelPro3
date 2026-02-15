using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cinemachine
{
	// Token: 0x02000039 RID: 57
	[SaveDuringPlay]
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineStoryboard.html")]
	public class CinemachineStoryboard : CinemachineExtension
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000A03C File Offset: 0x0000823C
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (vcam != base.VirtualCamera || stage != CinemachineCore.Stage.Finalize)
			{
				return;
			}
			this.UpdateRenderCanvas();
			if (this.m_ShowImage)
			{
				state.AddCustomBlendable(new CameraState.CustomBlendable(this, 1f));
			}
			if (this.m_MuteCamera)
			{
				state.BlendHint |= (CameraState.BlendHintValue)67;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A090 File Offset: 0x00008290
		private void UpdateRenderCanvas()
		{
			for (int i = 0; i < this.mCanvasInfo.Count; i++)
			{
				if (this.mCanvasInfo[i] == null || this.mCanvasInfo[i].mCanvasComponent == null)
				{
					this.mCanvasInfo.RemoveAt(i--);
				}
				else
				{
					this.mCanvasInfo[i].mCanvasComponent.renderMode = (RenderMode)this.m_RenderMode;
					this.mCanvasInfo[i].mCanvasComponent.planeDistance = this.m_PlaneDistance;
					this.mCanvasInfo[i].mCanvasComponent.sortingOrder = this.m_SortingOrder;
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A146 File Offset: 0x00008346
		protected override void ConnectToVcam(bool connect)
		{
			base.ConnectToVcam(connect);
			CinemachineCore.CameraUpdatedEvent.RemoveListener(new UnityAction<CinemachineBrain>(this.CameraUpdatedCallback));
			if (connect)
			{
				CinemachineCore.CameraUpdatedEvent.AddListener(new UnityAction<CinemachineBrain>(this.CameraUpdatedCallback));
				return;
			}
			this.DestroyCanvas();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000A188 File Offset: 0x00008388
		private string CanvasName
		{
			get
			{
				return "_CM_canvas" + base.gameObject.GetInstanceID().ToString();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A1B4 File Offset: 0x000083B4
		private void CameraUpdatedCallback(CinemachineBrain brain)
		{
			bool showIt = base.enabled && this.m_ShowImage && CinemachineCore.Instance.IsLive(base.VirtualCamera);
			int layer = 1 << base.gameObject.layer;
			if (brain.OutputCamera == null || (brain.OutputCamera.cullingMask & layer) == 0)
			{
				showIt = false;
			}
			if (CinemachineStoryboard.s_StoryboardGlobalMute)
			{
				showIt = false;
			}
			CinemachineStoryboard.CanvasInfo ci = this.LocateMyCanvas(brain, showIt);
			if (ci != null && ci.mCanvas != null)
			{
				ci.mCanvas.SetActive(showIt);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A244 File Offset: 0x00008444
		private CinemachineStoryboard.CanvasInfo LocateMyCanvas(CinemachineBrain parent, bool createIfNotFound)
		{
			CinemachineStoryboard.CanvasInfo ci = null;
			int i = 0;
			while (ci == null && i < this.mCanvasInfo.Count)
			{
				if (this.mCanvasInfo[i] != null && this.mCanvasInfo[i].mCanvasParent == parent)
				{
					ci = this.mCanvasInfo[i];
				}
				i++;
			}
			if (createIfNotFound)
			{
				if (ci == null)
				{
					ci = new CinemachineStoryboard.CanvasInfo
					{
						mCanvasParent = parent
					};
					int numChildren = parent.transform.childCount;
					int j = 0;
					while (ci.mCanvas == null && j < numChildren)
					{
						RectTransform child = parent.transform.GetChild(j) as RectTransform;
						if (child != null && child.name == this.CanvasName)
						{
							ci.mCanvas = child.gameObject;
							RectTransform[] kids = ci.mCanvas.GetComponentsInChildren<RectTransform>();
							ci.mViewport = ((kids.Length > 1) ? kids[1] : null);
							ci.mRawImage = ci.mCanvas.GetComponentInChildren<RawImage>();
							ci.mCanvasComponent = ci.mCanvas.GetComponent<Canvas>();
						}
						j++;
					}
					this.mCanvasInfo.Add(ci);
				}
				if (ci.mCanvas == null || ci.mViewport == null || ci.mRawImage == null || ci.mCanvasComponent == null)
				{
					this.CreateCanvas(ci);
				}
			}
			return ci;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A3B4 File Offset: 0x000085B4
		private void CreateCanvas(CinemachineStoryboard.CanvasInfo ci)
		{
			ci.mCanvas = new GameObject(this.CanvasName, new Type[] { typeof(RectTransform) });
			ci.mCanvas.layer = base.gameObject.layer;
			ci.mCanvas.hideFlags = HideFlags.HideAndDontSave;
			ci.mCanvas.transform.SetParent(ci.mCanvasParent.transform);
			Canvas canvas = (ci.mCanvasComponent = ci.mCanvas.AddComponent<Canvas>());
			canvas.renderMode = (RenderMode)this.m_RenderMode;
			canvas.sortingOrder = this.m_SortingOrder;
			canvas.planeDistance = this.m_PlaneDistance;
			canvas.worldCamera = ci.mCanvasParent.OutputCamera;
			GameObject go = new GameObject("Viewport", new Type[] { typeof(RectTransform) });
			go.transform.SetParent(ci.mCanvas.transform);
			ci.mViewport = (RectTransform)go.transform;
			go.AddComponent<RectMask2D>();
			go = new GameObject("RawImage", new Type[] { typeof(RectTransform) });
			go.transform.SetParent(ci.mViewport.transform);
			ci.mRawImage = go.AddComponent<RawImage>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A4F8 File Offset: 0x000086F8
		private void DestroyCanvas()
		{
			int numBrains = CinemachineCore.Instance.BrainCount;
			for (int i = 0; i < numBrains; i++)
			{
				CinemachineBrain parent = CinemachineCore.Instance.GetActiveBrain(i);
				for (int j = parent.transform.childCount - 1; j >= 0; j--)
				{
					RectTransform child = parent.transform.GetChild(j) as RectTransform;
					if (child != null && child.name == this.CanvasName)
					{
						RuntimeUtility.DestroyObject(child.gameObject);
					}
				}
			}
			this.mCanvasInfo.Clear();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A588 File Offset: 0x00008788
		private void PlaceImage(CinemachineStoryboard.CanvasInfo ci, float alpha)
		{
			if (ci.mRawImage != null && ci.mViewport != null)
			{
				Rect screen = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
				if (ci.mCanvasParent.OutputCamera != null)
				{
					screen = ci.mCanvasParent.OutputCamera.pixelRect;
				}
				screen.x -= (float)Screen.width / 2f;
				screen.y -= (float)Screen.height / 2f;
				float wipeAmount = -Mathf.Clamp(this.m_SplitView, -1f, 1f) * screen.width;
				Vector3 pos = screen.center;
				pos.x -= wipeAmount / 2f;
				ci.mViewport.localPosition = pos;
				ci.mViewport.localRotation = Quaternion.identity;
				ci.mViewport.localScale = Vector3.one;
				ci.mViewport.ForceUpdateRectTransforms();
				ci.mViewport.sizeDelta = new Vector2(screen.width + 1f - Mathf.Abs(wipeAmount), screen.height + 1f);
				Vector2 scale = Vector2.one;
				if (this.m_Image != null && this.m_Image.width > 0 && this.m_Image.width > 0 && screen.width > 0f && screen.height > 0f)
				{
					float f = screen.height * (float)this.m_Image.width / (screen.width * (float)this.m_Image.height);
					switch (this.m_Aspect)
					{
					case CinemachineStoryboard.FillStrategy.BestFit:
						if (f >= 1f)
						{
							scale.y /= f;
						}
						else
						{
							scale.x *= f;
						}
						break;
					case CinemachineStoryboard.FillStrategy.CropImageToFit:
						if (f >= 1f)
						{
							scale.x *= f;
						}
						else
						{
							scale.y /= f;
						}
						break;
					}
				}
				scale.x *= this.m_Scale.x;
				scale.y *= (this.m_SyncScale ? this.m_Scale.x : this.m_Scale.y);
				ci.mRawImage.texture = this.m_Image;
				Color tintColor = Color.white;
				tintColor.a = this.m_Alpha * alpha;
				ci.mRawImage.color = tintColor;
				pos = new Vector2(screen.width * this.m_Center.x, screen.height * this.m_Center.y);
				pos.x += wipeAmount / 2f;
				ci.mRawImage.rectTransform.localPosition = pos;
				ci.mRawImage.rectTransform.localRotation = Quaternion.Euler(this.m_Rotation);
				ci.mRawImage.rectTransform.localScale = scale;
				ci.mRawImage.rectTransform.ForceUpdateRectTransforms();
				ci.mRawImage.rectTransform.sizeDelta = screen.size;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		private static void StaticBlendingHandler(CinemachineBrain brain)
		{
			CameraState state = brain.CurrentCameraState;
			int numBlendables = state.NumCustomBlendables;
			for (int i = 0; i < numBlendables; i++)
			{
				CameraState.CustomBlendable b = state.GetCustomBlendable(i);
				CinemachineStoryboard src = b.m_Custom as CinemachineStoryboard;
				if (!(src == null))
				{
					bool showIt = true;
					int layer = 1 << src.gameObject.layer;
					if (brain.OutputCamera == null || (brain.OutputCamera.cullingMask & layer) == 0)
					{
						showIt = false;
					}
					if (CinemachineStoryboard.s_StoryboardGlobalMute)
					{
						showIt = false;
					}
					CinemachineStoryboard.CanvasInfo ci = src.LocateMyCanvas(brain, showIt);
					if (ci != null)
					{
						src.PlaceImage(ci, b.m_Weight);
					}
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A992 File Offset: 0x00008B92
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			CinemachineCore.CameraUpdatedEvent.RemoveListener(new UnityAction<CinemachineBrain>(CinemachineStoryboard.StaticBlendingHandler));
			CinemachineCore.CameraUpdatedEvent.AddListener(new UnityAction<CinemachineBrain>(CinemachineStoryboard.StaticBlendingHandler));
		}

		// Token: 0x0400011F RID: 287
		[Tooltip("If checked, all storyboards are globally muted")]
		public static bool s_StoryboardGlobalMute;

		// Token: 0x04000120 RID: 288
		[Tooltip("If checked, the specified image will be displayed as an overlay over the virtual camera's output")]
		public bool m_ShowImage = true;

		// Token: 0x04000121 RID: 289
		[Tooltip("The image to display")]
		public Texture m_Image;

		// Token: 0x04000122 RID: 290
		[Tooltip("How to handle differences between image aspect and screen aspect")]
		public CinemachineStoryboard.FillStrategy m_Aspect;

		// Token: 0x04000123 RID: 291
		[Tooltip("The opacity of the image.  0 is transparent, 1 is opaque")]
		[Range(0f, 1f)]
		public float m_Alpha = 1f;

		// Token: 0x04000124 RID: 292
		[Tooltip("The screen-space position at which to display the image.  Zero is center")]
		public Vector2 m_Center = Vector2.zero;

		// Token: 0x04000125 RID: 293
		[Tooltip("The screen-space rotation to apply to the image")]
		public Vector3 m_Rotation = Vector3.zero;

		// Token: 0x04000126 RID: 294
		[Tooltip("The screen-space scaling to apply to the image")]
		public Vector2 m_Scale = Vector3.one;

		// Token: 0x04000127 RID: 295
		[Tooltip("If checked, X and Y scale are synchronized")]
		public bool m_SyncScale = true;

		// Token: 0x04000128 RID: 296
		[Tooltip("If checked, Camera transform will not be controlled by this virtual camera")]
		public bool m_MuteCamera;

		// Token: 0x04000129 RID: 297
		[Range(-1f, 1f)]
		[Tooltip("Wipe the image on and off horizontally")]
		public float m_SplitView;

		// Token: 0x0400012A RID: 298
		[Tooltip("The render mode of the canvas on which the storyboard is drawn.")]
		public CinemachineStoryboard.StoryboardRenderMode m_RenderMode;

		// Token: 0x0400012B RID: 299
		[Tooltip("Allows ordering canvases to render on top or below other canvases.")]
		public int m_SortingOrder;

		// Token: 0x0400012C RID: 300
		[Tooltip("How far away from the camera is the Canvas generated.")]
		public float m_PlaneDistance = 100f;

		// Token: 0x0400012D RID: 301
		private List<CinemachineStoryboard.CanvasInfo> mCanvasInfo = new List<CinemachineStoryboard.CanvasInfo>();

		// Token: 0x0200003A RID: 58
		public enum FillStrategy
		{
			// Token: 0x0400012F RID: 303
			BestFit,
			// Token: 0x04000130 RID: 304
			CropImageToFit,
			// Token: 0x04000131 RID: 305
			StretchToFit
		}

		// Token: 0x0200003B RID: 59
		private class CanvasInfo
		{
			// Token: 0x04000132 RID: 306
			public GameObject mCanvas;

			// Token: 0x04000133 RID: 307
			public Canvas mCanvasComponent;

			// Token: 0x04000134 RID: 308
			public CinemachineBrain mCanvasParent;

			// Token: 0x04000135 RID: 309
			public RectTransform mViewport;

			// Token: 0x04000136 RID: 310
			public RawImage mRawImage;
		}

		// Token: 0x0200003C RID: 60
		public enum StoryboardRenderMode
		{
			// Token: 0x04000138 RID: 312
			ScreenSpaceOverlay,
			// Token: 0x04000139 RID: 313
			ScreenSpaceCamera
		}
	}
}
