using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000025 RID: 37
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[RequireComponent(typeof(Camera))]
	[DisallowMultipleComponent]
	[AddComponentMenu("Cinemachine/CinemachineExternalCamera")]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineExternalCamera.html")]
	public class CinemachineExternalCamera : CinemachineVirtualCameraBase
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006D2B File Offset: 0x00004F2B
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00006D33 File Offset: 0x00004F33
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006D3B File Offset: 0x00004F3B
		public override Transform LookAt
		{
			get
			{
				return this.m_LookAt;
			}
			set
			{
				this.m_LookAt = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00006D44 File Offset: 0x00004F44
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00006D4C File Offset: 0x00004F4C
		public override Transform Follow { get; set; }

		// Token: 0x060000CE RID: 206 RVA: 0x00006D58 File Offset: 0x00004F58
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			if (this.m_Camera == null)
			{
				base.TryGetComponent<Camera>(out this.m_Camera);
			}
			this.m_State = CameraState.Default;
			this.m_State.RawPosition = base.transform.position;
			this.m_State.RawOrientation = base.transform.rotation;
			this.m_State.ReferenceUp = this.m_State.RawOrientation * Vector3.up;
			if (this.m_Camera != null)
			{
				this.m_State.Lens = LensSettings.FromCamera(this.m_Camera);
			}
			if (this.m_LookAt != null)
			{
				this.m_State.ReferenceLookAt = this.m_LookAt.transform.position;
				Vector3 dir = this.m_State.ReferenceLookAt - this.State.RawPosition;
				if (!dir.AlmostZero())
				{
					this.m_State.ReferenceLookAt = this.m_State.RawPosition + Vector3.Project(dir, this.State.RawOrientation * Vector3.forward);
				}
			}
			base.ApplyPositionBlendMethod(ref this.m_State, this.m_BlendHint);
			base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Finalize, ref this.m_State, deltaTime);
		}

		// Token: 0x040000B7 RID: 183
		[Tooltip("The object that the camera is looking at.  Setting this will improve the quality of the blends to and from this camera")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_LookAt;

		// Token: 0x040000B8 RID: 184
		private Camera m_Camera;

		// Token: 0x040000B9 RID: 185
		private CameraState m_State = CameraState.Default;

		// Token: 0x040000BB RID: 187
		[Tooltip("Hint for blending positions to and from this virtual camera")]
		[FormerlySerializedAs("m_PositionBlending")]
		public CinemachineVirtualCameraBase.BlendHint m_BlendHint;
	}
}
