using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000078 RID: 120
	[DocumentationSorting(DocumentationSortingAttribute.Level.API)]
	public abstract class CinemachineExtension : MonoBehaviour
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00012E79 File Offset: 0x00011079
		public CinemachineVirtualCameraBase VirtualCamera
		{
			get
			{
				if (this.m_vcamOwner == null)
				{
					this.m_vcamOwner = base.GetComponent<CinemachineVirtualCameraBase>();
				}
				return this.m_vcamOwner;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00012E9B File Offset: 0x0001109B
		protected virtual void Awake()
		{
			this.ConnectToVcam(true);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000429A File Offset: 0x0000249A
		protected virtual void OnEnable()
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012EA4 File Offset: 0x000110A4
		protected virtual void OnDestroy()
		{
			this.ConnectToVcam(false);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012E9B File Offset: 0x0001109B
		internal void EnsureStarted()
		{
			this.ConnectToVcam(true);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00012EB0 File Offset: 0x000110B0
		protected virtual void ConnectToVcam(bool connect)
		{
			if (connect && this.VirtualCamera == null)
			{
				Debug.LogError("CinemachineExtension requires a Cinemachine Virtual Camera component");
			}
			if (this.VirtualCamera != null)
			{
				if (connect)
				{
					this.VirtualCamera.AddExtension(this);
				}
				else
				{
					this.VirtualCamera.RemoveExtension(this);
				}
			}
			this.mExtraState = null;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void PrePipelineMutateCameraStateCallback(CinemachineVirtualCameraBase vcam, ref CameraState curState, float deltaTime)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00012F0A File Offset: 0x0001110A
		public void InvokePostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			this.PostPipelineStageCallback(vcam, stage, ref state, deltaTime);
		}

		// Token: 0x060002F2 RID: 754
		protected abstract void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime);

		// Token: 0x060002F3 RID: 755 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			return false;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public virtual float GetMaxDampTime()
		{
			return 0f;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool RequiresUserInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00012F18 File Offset: 0x00011118
		protected T GetExtraState<T>(ICinemachineCamera vcam) where T : class, new()
		{
			if (this.mExtraState == null)
			{
				this.mExtraState = new Dictionary<ICinemachineCamera, object>();
			}
			object extra = null;
			if (!this.mExtraState.TryGetValue(vcam, out extra))
			{
				extra = (this.mExtraState[vcam] = new T());
			}
			return extra as T;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00012F70 File Offset: 0x00011170
		protected List<T> GetAllExtraStates<T>() where T : class, new()
		{
			List<T> list = new List<T>();
			if (this.mExtraState != null)
			{
				foreach (KeyValuePair<ICinemachineCamera, object> v in this.mExtraState)
				{
					list.Add(v.Value as T);
				}
			}
			return list;
		}

		// Token: 0x040002BE RID: 702
		protected const float Epsilon = 0.0001f;

		// Token: 0x040002BF RID: 703
		private CinemachineVirtualCameraBase m_vcamOwner;

		// Token: 0x040002C0 RID: 704
		private Dictionary<ICinemachineCamera, object> mExtraState;
	}
}
