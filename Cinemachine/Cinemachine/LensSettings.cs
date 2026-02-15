using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000097 RID: 151
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[Serializable]
	public struct LensSettings
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000154A3 File Offset: 0x000136A3
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000154C0 File Offset: 0x000136C0
		public bool Orthographic
		{
			get
			{
				return this.ModeOverride == LensSettings.OverrideModes.Orthographic || (this.ModeOverride == LensSettings.OverrideModes.None && this.m_OrthoFromCamera);
			}
			set
			{
				this.m_OrthoFromCamera = value;
				this.ModeOverride = (value ? LensSettings.OverrideModes.Orthographic : LensSettings.OverrideModes.Perspective);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000154D6 File Offset: 0x000136D6
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x000154DE File Offset: 0x000136DE
		public Vector2 SensorSize
		{
			get
			{
				return this.m_SensorSize;
			}
			set
			{
				this.m_SensorSize = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000154E7 File Offset: 0x000136E7
		public float Aspect
		{
			get
			{
				if (this.SensorSize.y != 0f)
				{
					return this.SensorSize.x / this.SensorSize.y;
				}
				return 1f;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00015518 File Offset: 0x00013718
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00015535 File Offset: 0x00013735
		public bool IsPhysicalCamera
		{
			get
			{
				return this.ModeOverride == LensSettings.OverrideModes.Physical || (this.ModeOverride == LensSettings.OverrideModes.None && this.m_PhysicalFromCamera);
			}
			set
			{
				this.m_PhysicalFromCamera = value;
				this.ModeOverride = (value ? LensSettings.OverrideModes.Physical : LensSettings.OverrideModes.Perspective);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001554C File Offset: 0x0001374C
		public static LensSettings FromCamera(Camera fromCamera)
		{
			LensSettings lens = LensSettings.Default;
			if (fromCamera != null)
			{
				lens.FieldOfView = fromCamera.fieldOfView;
				lens.OrthographicSize = fromCamera.orthographicSize;
				lens.NearClipPlane = fromCamera.nearClipPlane;
				lens.FarClipPlane = fromCamera.farClipPlane;
				lens.LensShift = fromCamera.lensShift;
				lens.GateFit = fromCamera.gateFit;
				lens.FocusDistance = fromCamera.focusDistance;
				lens.SnapshotCameraReadOnlyProperties(fromCamera);
			}
			return lens;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000155CC File Offset: 0x000137CC
		public void SnapshotCameraReadOnlyProperties(Camera camera)
		{
			this.m_OrthoFromCamera = false;
			this.m_PhysicalFromCamera = false;
			if (camera != null && this.ModeOverride == LensSettings.OverrideModes.None)
			{
				this.m_OrthoFromCamera = camera.orthographic;
				this.m_PhysicalFromCamera = camera.usePhysicalProperties;
				this.m_SensorSize = camera.sensorSize;
				this.GateFit = camera.gateFit;
			}
			if (this.IsPhysicalCamera)
			{
				if (camera != null && this.m_SensorSize == Vector2.zero)
				{
					this.m_SensorSize = camera.sensorSize;
					this.GateFit = camera.gateFit;
					return;
				}
			}
			else
			{
				if (camera != null)
				{
					this.m_SensorSize = new Vector2(camera.aspect, 1f);
				}
				this.LensShift = Vector2.zero;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015690 File Offset: 0x00013890
		public void SnapshotCameraReadOnlyProperties(ref LensSettings lens)
		{
			if (this.ModeOverride == LensSettings.OverrideModes.None)
			{
				this.m_OrthoFromCamera = lens.Orthographic;
				this.m_SensorSize = lens.m_SensorSize;
				this.m_PhysicalFromCamera = lens.IsPhysicalCamera;
			}
			if (!this.IsPhysicalCamera)
			{
				this.LensShift = Vector2.zero;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000156DC File Offset: 0x000138DC
		public LensSettings(float verticalFOV, float orthographicSize, float nearClip, float farClip, float dutch)
		{
			this = default(LensSettings);
			this.FieldOfView = verticalFOV;
			this.OrthographicSize = orthographicSize;
			this.NearClipPlane = nearClip;
			this.FarClipPlane = farClip;
			this.Dutch = dutch;
			this.m_SensorSize = new Vector2(1f, 1f);
			this.GateFit = Camera.GateFitMode.Horizontal;
			this.FocusDistance = 10f;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001573C File Offset: 0x0001393C
		public static LensSettings Lerp(LensSettings lensA, LensSettings lensB, float t)
		{
			t = Mathf.Clamp01(t);
			LensSettings blendedLens = ((t < 0.5f) ? lensA : lensB);
			blendedLens.FarClipPlane = Mathf.Lerp(lensA.FarClipPlane, lensB.FarClipPlane, t);
			blendedLens.NearClipPlane = Mathf.Lerp(lensA.NearClipPlane, lensB.NearClipPlane, t);
			blendedLens.FieldOfView = Mathf.Lerp(lensA.FieldOfView, lensB.FieldOfView, t);
			blendedLens.OrthographicSize = Mathf.Lerp(lensA.OrthographicSize, lensB.OrthographicSize, t);
			blendedLens.Dutch = Mathf.Lerp(lensA.Dutch, lensB.Dutch, t);
			blendedLens.m_SensorSize = Vector2.Lerp(lensA.m_SensorSize, lensB.m_SensorSize, t);
			blendedLens.LensShift = Vector2.Lerp(lensA.LensShift, lensB.LensShift, t);
			blendedLens.FocusDistance = Mathf.Lerp(lensA.FocusDistance, lensB.FocusDistance, t);
			return blendedLens;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00015828 File Offset: 0x00013A28
		public void Validate()
		{
			this.FarClipPlane = Mathf.Max(this.FarClipPlane, this.NearClipPlane + 0.001f);
			this.FieldOfView = Mathf.Clamp(this.FieldOfView, 0.01f, 179f);
			this.m_SensorSize.x = Mathf.Max(this.m_SensorSize.x, 0.1f);
			this.m_SensorSize.y = Mathf.Max(this.m_SensorSize.y, 0.1f);
			this.FocusDistance = Mathf.Max(this.FocusDistance, 0.01f);
		}

		// Token: 0x04000332 RID: 818
		public static LensSettings Default = new LensSettings(40f, 10f, 0.1f, 5000f, 0f);

		// Token: 0x04000333 RID: 819
		[Range(1f, 179f)]
		[Tooltip("This is the camera view in degrees. Display will be in vertical degress, unless the associated camera has its FOV axis setting set to Horizontal, in which case display will be in horizontal degress.  Internally, it is always vertical degrees.  For cinematic people, a 50mm lens on a super-35mm sensor would equal a 19.6 degree FOV")]
		public float FieldOfView;

		// Token: 0x04000334 RID: 820
		[Tooltip("When using an orthographic camera, this defines the half-height, in world coordinates, of the camera view.")]
		public float OrthographicSize;

		// Token: 0x04000335 RID: 821
		[Tooltip("This defines the near region in the renderable range of the camera frustum. Raising this value will stop the game from drawing things near the camera, which can sometimes come in handy.  Larger values will also increase your shadow resolution.")]
		public float NearClipPlane;

		// Token: 0x04000336 RID: 822
		[Tooltip("This defines the far region of the renderable range of the camera frustum. Typically you want to set this value as low as possible without cutting off desired distant objects")]
		public float FarClipPlane;

		// Token: 0x04000337 RID: 823
		[Range(-180f, 180f)]
		[Tooltip("Camera Z roll, or tilt, in degrees.")]
		public float Dutch;

		// Token: 0x04000338 RID: 824
		[Tooltip("Allows you to select a different camera mode to apply to the Camera component when Cinemachine activates this Virtual Camera.  The changes applied to the Camera component through this setting will remain after the Virtual Camera deactivation.")]
		public LensSettings.OverrideModes ModeOverride;

		// Token: 0x04000339 RID: 825
		public Vector2 LensShift;

		// Token: 0x0400033A RID: 826
		public Camera.GateFitMode GateFit;

		// Token: 0x0400033B RID: 827
		public float FocusDistance;

		// Token: 0x0400033C RID: 828
		[SerializeField]
		private Vector2 m_SensorSize;

		// Token: 0x0400033D RID: 829
		private bool m_OrthoFromCamera;

		// Token: 0x0400033E RID: 830
		private bool m_PhysicalFromCamera;

		// Token: 0x02000098 RID: 152
		public enum OverrideModes
		{
			// Token: 0x04000340 RID: 832
			None,
			// Token: 0x04000341 RID: 833
			Orthographic,
			// Token: 0x04000342 RID: 834
			Perspective,
			// Token: 0x04000343 RID: 835
			Physical
		}
	}
}
