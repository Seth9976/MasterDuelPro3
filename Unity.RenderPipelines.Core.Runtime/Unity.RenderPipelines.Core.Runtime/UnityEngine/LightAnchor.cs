using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[AddComponentMenu("Rendering/Light Anchor")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class LightAnchor : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002352 File Offset: 0x00000552
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000235A File Offset: 0x0000055A
		public float yaw
		{
			get
			{
				return this.m_Yaw;
			}
			set
			{
				this.m_Yaw = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002368 File Offset: 0x00000568
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002370 File Offset: 0x00000570
		public float pitch
		{
			get
			{
				return this.m_Pitch;
			}
			set
			{
				this.m_Pitch = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000237E File Offset: 0x0000057E
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002386 File Offset: 0x00000586
		public float roll
		{
			get
			{
				return this.m_Roll;
			}
			set
			{
				this.m_Roll = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000239C File Offset: 0x0000059C
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = Mathf.Clamp(value, 0f, 10000f);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023B4 File Offset: 0x000005B4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023BC File Offset: 0x000005BC
		public LightAnchor.UpDirection frameSpace
		{
			get
			{
				return this.m_FrameSpace;
			}
			set
			{
				this.m_FrameSpace = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023C8 File Offset: 0x000005C8
		public Vector3 anchorPosition
		{
			get
			{
				if (this.anchorPositionOverride != null)
				{
					return this.anchorPositionOverride.position + this.anchorPositionOverride.TransformDirection(this.anchorPositionOffset);
				}
				return base.transform.position + base.transform.forward * this.distance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000242B File Offset: 0x0000062B
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002433 File Offset: 0x00000633
		public Transform anchorPositionOverride
		{
			get
			{
				return this.m_AnchorPositionOverride;
			}
			set
			{
				this.m_AnchorPositionOverride = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002444 File Offset: 0x00000644
		public Vector3 anchorPositionOffset
		{
			get
			{
				return this.m_AnchorPositionOffset;
			}
			set
			{
				this.m_AnchorPositionOffset = value;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000244D File Offset: 0x0000064D
		public static float NormalizeAngleDegree(float angle)
		{
			float num = angle - -180f;
			return num - Mathf.Floor(num / 360f) * 360f + -180f;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002470 File Offset: 0x00000670
		public void SynchronizeOnTransform(Camera camera)
		{
			LightAnchor.Axes axes = this.GetWorldSpaceAxes(camera, this.anchorPosition);
			Vector3 worldAnchorToLight = base.transform.position - this.anchorPosition;
			if (worldAnchorToLight.magnitude == 0f)
			{
				worldAnchorToLight = -base.transform.forward;
			}
			Vector3 projectOnGround = Vector3.ProjectOnPlane(worldAnchorToLight, axes.up);
			if (projectOnGround.magnitude < 0.0001f)
			{
				projectOnGround = Vector3.ProjectOnPlane(worldAnchorToLight, axes.up + axes.right * 0.0001f);
			}
			projectOnGround.Normalize();
			float extractedYaw = Vector3.SignedAngle(axes.forward, projectOnGround, axes.up);
			Vector3 yawedRight = Quaternion.AngleAxis(extractedYaw, axes.up) * axes.right;
			float extractedPitch = Vector3.SignedAngle(projectOnGround, worldAnchorToLight, yawedRight);
			this.yaw = extractedYaw;
			this.pitch = extractedPitch;
			this.roll = base.transform.rotation.eulerAngles.z;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000256C File Offset: 0x0000076C
		public void UpdateTransform(Camera camera, Vector3 anchor)
		{
			LightAnchor.Axes axes = this.GetWorldSpaceAxes(camera, anchor);
			this.UpdateTransform(axes.up, axes.right, axes.forward, anchor);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000259C File Offset: 0x0000079C
		private LightAnchor.Axes GetWorldSpaceAxes(Camera camera, Vector3 anchor)
		{
			if (base.transform.IsChildOf(camera.transform))
			{
				return new LightAnchor.Axes
				{
					up = Vector3.up,
					right = Vector3.right,
					forward = Vector3.forward
				};
			}
			Matrix4x4 viewToWorld = camera.cameraToWorldMatrix;
			if (this.m_FrameSpace == LightAnchor.UpDirection.Local)
			{
				Vector3 localUp = Camera.main.transform.up;
				viewToWorld = (Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * Matrix4x4.LookAt(camera.transform.position, anchor, localUp).inverse).inverse;
			}
			else if (!camera.orthographic && camera.transform.position != anchor)
			{
				Quaternion f = Quaternion.LookRotation((anchor - camera.transform.position).normalized);
				viewToWorld = (Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * Matrix4x4.TRS(camera.transform.position, f, Vector3.one).inverse).inverse;
			}
			Vector3 up = (viewToWorld * Vector3.up).normalized;
			Vector3 right = (viewToWorld * Vector3.right).normalized;
			Vector3 forward = (viewToWorld * Vector3.forward).normalized;
			return new LightAnchor.Axes
			{
				up = up,
				right = right,
				forward = forward
			};
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002758 File Offset: 0x00000958
		private void Update()
		{
			if (this.anchorPositionOverride == null || Camera.main == null)
			{
				return;
			}
			if (this.anchorPositionOverride.hasChanged || Camera.main.transform.hasChanged)
			{
				this.UpdateTransform(Camera.main, this.anchorPosition);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027B0 File Offset: 0x000009B0
		private void OnDrawGizmosSelected()
		{
			Camera camera = Camera.main;
			if (camera == null)
			{
				return;
			}
			Vector3 anchor = this.anchorPosition;
			LightAnchor.Axes axes = this.GetWorldSpaceAxes(camera, anchor);
			Vector3.ProjectOnPlane(base.transform.position - anchor, axes.up);
			Mathf.Min(this.distance * 0.25f, 5f);
			Mathf.Min(this.distance * 0.5f, 10f);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002828 File Offset: 0x00000A28
		private void UpdateTransform(Vector3 up, Vector3 right, Vector3 forward, Vector3 anchor)
		{
			Quaternion worldYawRot = Quaternion.AngleAxis(this.m_Yaw, up);
			Quaternion worldPitchRot = Quaternion.AngleAxis(this.m_Pitch, right);
			Vector3 worldPosition = anchor + worldYawRot * worldPitchRot * forward * this.distance;
			base.transform.position = worldPosition;
			Vector3 angles = Quaternion.LookRotation(-(worldYawRot * worldPitchRot * forward).normalized, up).eulerAngles;
			angles.z = this.m_Roll;
			base.transform.eulerAngles = angles;
		}

		// Token: 0x04000013 RID: 19
		private const float k_ArcRadius = 5f;

		// Token: 0x04000014 RID: 20
		private const float k_AxisLength = 10f;

		// Token: 0x04000015 RID: 21
		internal const float k_MaxDistance = 10000f;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		[Min(0f)]
		private float m_Distance;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private LightAnchor.UpDirection m_FrameSpace;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private Transform m_AnchorPositionOverride;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private Vector3 m_AnchorPositionOffset;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private float m_Yaw;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private float m_Pitch;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private float m_Roll;

		// Token: 0x0200000A RID: 10
		public enum UpDirection
		{
			// Token: 0x0400001E RID: 30
			World,
			// Token: 0x0400001F RID: 31
			Local
		}

		// Token: 0x0200000B RID: 11
		private struct Axes
		{
			// Token: 0x04000020 RID: 32
			public Vector3 up;

			// Token: 0x04000021 RID: 33
			public Vector3 right;

			// Token: 0x04000022 RID: 34
			public Vector3 forward;
		}
	}
}
