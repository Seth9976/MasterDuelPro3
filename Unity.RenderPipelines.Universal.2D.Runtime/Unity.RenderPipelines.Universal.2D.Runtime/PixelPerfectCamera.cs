using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000046 RID: 70
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Rendering/2D/Pixel Perfect Camera")]
	[RequireComponent(typeof(Camera))]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2d-pixelperfect.html%23properties")]
	public class PixelPerfectCamera : MonoBehaviour, IPixelPerfectCamera, ISerializationCallbackReceiver
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00010277 File Offset: 0x0000E477
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0001027F File Offset: 0x0000E47F
		public PixelPerfectCamera.CropFrame cropFrame
		{
			get
			{
				return this.m_CropFrame;
			}
			set
			{
				this.m_CropFrame = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00010288 File Offset: 0x0000E488
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00010290 File Offset: 0x0000E490
		public PixelPerfectCamera.GridSnapping gridSnapping
		{
			get
			{
				return this.m_GridSnapping;
			}
			set
			{
				this.m_GridSnapping = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00010299 File Offset: 0x0000E499
		public float orthographicSize
		{
			get
			{
				return this.m_Internal.orthoSize;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000102A6 File Offset: 0x0000E4A6
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000102AE File Offset: 0x0000E4AE
		public int assetsPPU
		{
			get
			{
				return this.m_AssetsPPU;
			}
			set
			{
				this.m_AssetsPPU = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000102BE File Offset: 0x0000E4BE
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000102C6 File Offset: 0x0000E4C6
		public int refResolutionX
		{
			get
			{
				return this.m_RefResolutionX;
			}
			set
			{
				this.m_RefResolutionX = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000102D6 File Offset: 0x0000E4D6
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000102DE File Offset: 0x0000E4DE
		public int refResolutionY
		{
			get
			{
				return this.m_RefResolutionY;
			}
			set
			{
				this.m_RefResolutionY = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000102EE File Offset: 0x0000E4EE
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000102F9 File Offset: 0x0000E4F9
		[Obsolete("Use gridSnapping instead", false)]
		public bool upscaleRT
		{
			get
			{
				return this.m_GridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
			}
			set
			{
				this.m_GridSnapping = (value ? PixelPerfectCamera.GridSnapping.UpscaleRenderTexture : PixelPerfectCamera.GridSnapping.None);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00010308 File Offset: 0x0000E508
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00010313 File Offset: 0x0000E513
		[Obsolete("Use gridSnapping instead", false)]
		public bool pixelSnapping
		{
			get
			{
				return this.m_GridSnapping == PixelPerfectCamera.GridSnapping.PixelSnapping;
			}
			set
			{
				this.m_GridSnapping = (value ? PixelPerfectCamera.GridSnapping.PixelSnapping : PixelPerfectCamera.GridSnapping.None);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00010322 File Offset: 0x0000E522
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00010344 File Offset: 0x0000E544
		[Obsolete("Use cropFrame instead", false)]
		public bool cropFrameX
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill || this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox;
			}
			set
			{
				if (value)
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.None)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Pillarbox;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
						return;
					}
				}
				else
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.None;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Letterbox;
					}
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0001039F File Offset: 0x0000E59F
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000103C0 File Offset: 0x0000E5C0
		[Obsolete("Use cropFrame instead", false)]
		public bool cropFrameY
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill || this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox;
			}
			set
			{
				if (value)
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.None)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Letterbox;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
						return;
					}
				}
				else
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.None;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Pillarbox;
					}
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0001041B File Offset: 0x0000E61B
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00010426 File Offset: 0x0000E626
		[Obsolete("Use cropFrame instead", false)]
		public bool stretchFill
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill;
			}
			set
			{
				if (value)
				{
					this.m_CropFrame = PixelPerfectCamera.CropFrame.StretchFill;
					return;
				}
				this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0001043C File Offset: 0x0000E63C
		public int pixelRatio
		{
			get
			{
				if (!this.m_CinemachineCompatibilityMode)
				{
					return this.m_Internal.zoom;
				}
				if (this.m_GridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture)
				{
					return this.m_Internal.zoom * this.m_Internal.cinemachineVCamZoom;
				}
				return this.m_Internal.cinemachineVCamZoom;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00010489 File Offset: 0x0000E689
		public bool requiresUpscalePass
		{
			get
			{
				return this.m_Internal.requiresUpscaling;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00010498 File Offset: 0x0000E698
		public Vector3 RoundToPixel(Vector3 position)
		{
			float unitsPerPixel = this.m_Internal.unitsPerPixel;
			if (unitsPerPixel == 0f)
			{
				return position;
			}
			Vector3 result;
			result.x = Mathf.Round(position.x / unitsPerPixel) * unitsPerPixel;
			result.y = Mathf.Round(position.y / unitsPerPixel) * unitsPerPixel;
			result.z = Mathf.Round(position.z / unitsPerPixel) * unitsPerPixel;
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000104FE File Offset: 0x0000E6FE
		public float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			this.m_CinemachineCompatibilityMode = true;
			if (this.m_Internal == null)
			{
				return targetOrthoSize;
			}
			return this.m_Internal.CorrectCinemachineOrthoSize(targetOrthoSize);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0001051D File Offset: 0x0000E71D
		internal FilterMode finalBlitFilterMode
		{
			get
			{
				if (this.m_FilterMode != PixelPerfectCamera.PixelPerfectFilterMode.RetroAA)
				{
					return FilterMode.Point;
				}
				return FilterMode.Bilinear;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0001052A File Offset: 0x0000E72A
		internal Vector2Int offscreenRTSize
		{
			get
			{
				return new Vector2Int(this.m_Internal.offscreenRTWidth, this.m_Internal.offscreenRTHeight);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00010548 File Offset: 0x0000E748
		private Vector2Int cameraRTSize
		{
			get
			{
				RenderTexture targetTexture = this.m_Camera.targetTexture;
				if (!(targetTexture == null))
				{
					return new Vector2Int(targetTexture.width, targetTexture.height);
				}
				return new Vector2Int(Screen.width, Screen.height);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001058C File Offset: 0x0000E78C
		private void PixelSnap()
		{
			Vector3 cameraPosition = this.m_Camera.transform.position;
			Vector3 offset = this.RoundToPixel(cameraPosition) - cameraPosition;
			offset.z = -offset.z;
			Matrix4x4 invPos = Matrix4x4.TRS(cameraPosition + offset, Quaternion.identity, Vector3.one).inverse;
			Matrix4x4 invRot = Matrix4x4.Rotate(this.m_Camera.transform.rotation).inverse;
			Matrix4x4 scaleMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
			this.m_Camera.worldToCameraMatrix = scaleMatrix * invRot * invPos;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010639 File Offset: 0x0000E839
		private void Awake()
		{
			this.m_Camera = base.GetComponent<Camera>();
			this.m_Internal = new PixelPerfectCameraInternal(this);
			this.UpdateCameraProperties();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001065C File Offset: 0x0000E85C
		private void UpdateCameraProperties()
		{
			Vector2Int rtSize = this.cameraRTSize;
			this.m_Internal.CalculateCameraProperties(rtSize.x, rtSize.y);
			if (this.m_Internal.useOffscreenRT)
			{
				this.m_Camera.pixelRect = this.m_Internal.CalculateFinalBlitPixelRect(rtSize.x, rtSize.y);
				return;
			}
			this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000106E0 File Offset: 0x0000E8E0
		private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (camera == this.m_Camera)
			{
				this.UpdateCameraProperties();
				this.PixelSnap();
				if (!this.m_CinemachineCompatibilityMode)
				{
					this.m_Camera.orthographicSize = this.m_Internal.orthoSize;
				}
				PixelPerfectRendering.pixelSnapSpacing = this.m_Internal.unitsPerPixel;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010735 File Offset: 0x0000E935
		private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (camera == this.m_Camera)
			{
				PixelPerfectRendering.pixelSnapSpacing = 0f;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001074F File Offset: 0x0000E94F
		private void OnEnable()
		{
			this.m_CinemachineCompatibilityMode = false;
			RenderPipelineManager.beginCameraRendering += this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering += this.OnEndCameraRendering;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001077C File Offset: 0x0000E97C
		internal void OnDisable()
		{
			RenderPipelineManager.beginCameraRendering -= this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering -= this.OnEndCameraRendering;
			this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
			this.m_Camera.ResetWorldToCameraMatrix();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x04000188 RID: 392
		[SerializeField]
		private int m_AssetsPPU = 100;

		// Token: 0x04000189 RID: 393
		[SerializeField]
		private int m_RefResolutionX = 320;

		// Token: 0x0400018A RID: 394
		[SerializeField]
		private int m_RefResolutionY = 180;

		// Token: 0x0400018B RID: 395
		[SerializeField]
		private PixelPerfectCamera.CropFrame m_CropFrame;

		// Token: 0x0400018C RID: 396
		[SerializeField]
		private PixelPerfectCamera.GridSnapping m_GridSnapping;

		// Token: 0x0400018D RID: 397
		[SerializeField]
		private PixelPerfectCamera.PixelPerfectFilterMode m_FilterMode;

		// Token: 0x0400018E RID: 398
		private Camera m_Camera;

		// Token: 0x0400018F RID: 399
		private PixelPerfectCameraInternal m_Internal;

		// Token: 0x04000190 RID: 400
		private bool m_CinemachineCompatibilityMode;

		// Token: 0x02000047 RID: 71
		public enum CropFrame
		{
			// Token: 0x04000192 RID: 402
			None,
			// Token: 0x04000193 RID: 403
			Pillarbox,
			// Token: 0x04000194 RID: 404
			Letterbox,
			// Token: 0x04000195 RID: 405
			Windowbox,
			// Token: 0x04000196 RID: 406
			StretchFill
		}

		// Token: 0x02000048 RID: 72
		public enum GridSnapping
		{
			// Token: 0x04000198 RID: 408
			None,
			// Token: 0x04000199 RID: 409
			PixelSnapping,
			// Token: 0x0400019A RID: 410
			UpscaleRenderTexture
		}

		// Token: 0x02000049 RID: 73
		public enum PixelPerfectFilterMode
		{
			// Token: 0x0400019C RID: 412
			RetroAA,
			// Token: 0x0400019D RID: 413
			Point
		}

		// Token: 0x0200004A RID: 74
		private enum ComponentVersions
		{
			// Token: 0x0400019F RID: 415
			Version_Unserialized,
			// Token: 0x040001A0 RID: 416
			Version_1
		}
	}
}
